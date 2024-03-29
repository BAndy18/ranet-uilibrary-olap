/*   
    Copyright (C) 2009 Galaktika Corporation ZAO

    This file is a part of Ranet.UILibrary.Olap
 
    Ranet.UILibrary.Olap is a free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
      
    You should have received a copy of the GNU General Public License
    along with Ranet.UILibrary.Olap.  If not, see <http://www.gnu.org/licenses/>.
  
    If GPL v.3 is not suitable for your products or company,
    Galaktika Corp provides Ranet.UILibrary.Olap under a flexible commercial license
    designed to meet your specific usage and distribution requirements.
    If you have already obtained a commercial license from Galaktika Corp,
    you can use this file under those license terms.
 
		Moreover you can use this file in source form under original
    "BSD licence" thereunder.
*/
/*
[The "BSD licence"]
Copyright (c) 2005-2007 Kunle Odutola
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions
are met:
1. Redistributions of source code MUST RETAIN the above copyright
 notice, this list of conditions and the following disclaimer.
2. Redistributions in binary form MUST REPRODUCE the above copyright
 notice, this list of conditions and the following disclaimer in 
 the documentation and/or other materials provided with the 
 distribution.
3. The name of the author may not be used to endorse or promote products
 derived from this software without specific prior WRITTEN permission.
4. Unless explicitly state otherwise, any contribution intentionally 
 submitted for inclusion in this work to the copyright owner or licensor
 shall be under the terms and conditions of this license, without any 
 additional terms or conditions.

THIS SOFTWARE IS PROVIDED BY THE AUTHOR ``AS IS'' AND ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT,
INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/


namespace Antlr.Runtime
{
	using System;

	/// <summary>
	///  A DFA implemented as a set of transition tables.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Any state that has a semantic predicate edge is special; those states are 
	/// generated with if-then-else structures in a SpecialStateTransition() 
	/// which is generated by cyclicDFA template.
	/// </para>
	/// <para>
	/// There are at most 32767 states (16-bit signed short). Could get away with byte 
	/// sometimes but would have to generate different types and the simulation code too.
	/// </para>
	/// <para>
	/// As a point of reference, the Tokens rule DFA for the lexer in the Java grammar 
	/// sample has approximately 326 states.
	/// </para>
	/// </remarks>
	abstract public class DFA
	{
		public delegate int SpecialStateTransitionHandler(DFA dfa, int s, IIntStream input);

		protected short[] eot;
		protected short[] eof;
		protected char[] min;
		protected char[] max;
		protected short[] accept;
		protected short[] special;
		protected short[][] transition;

		protected int decisionNumber;

		// HACK: Has to be public to allow generated recognizer to set.
		public SpecialStateTransitionHandler specialStateTransitionHandler;

		/// <summary>
		/// Which recognizer encloses this DFA?  Needed to check backtracking
		/// </summary>
		protected BaseRecognizer recognizer;

		/// <summary>
		/// From the input stream, predict what alternative will succeed using this 
		/// DFA (representing the covering regular approximation to the underlying CFL).  
		/// </summary>
		/// <param name="input">Input stream</param>
		/// <returns>Return an alternative number 1..n.  Throw an exception upon error.</returns>
		public int Predict(IIntStream input)
		{
			int mark = input.Mark();	// remember where decision started in input
			int s = 0; // we always start at s0
			try
			{
				while (true)
				{
					int specialState = special[s];
					if (specialState >= 0)
					{
						s = specialStateTransitionHandler(this, specialState, input);
						if (s == -1)
						{
							NoViableAlt(s, input);
							return 0;
						}
						input.Consume();
						continue;
					}
					if (accept[s] >= 1)
					{
						return accept[s];
					}
					// look for a normal char transition
					char c = (char)input.LA(1);		// -1 == \uFFFF, all tokens fit in 65000 space
					if ((c >= min[s]) && (c <= max[s]))
					{
						int snext = transition[s][c - min[s]]; // move to next state
						if (snext < 0)
						{
							// was in range but not a normal transition
							// must check EOT, which is like the else clause.
							// eot[s]>=0 indicates that an EOT edge goes to another
							// state.
							if (eot[s] >= 0)  // EOT Transition to accept state?
							{
								s = eot[s];
								input.Consume();
								// TODO: I had this as return accept[eot[s]]
								// which assumed here that the EOT edge always
								// went to an accept...faster to do this, but
								// what about predicated edges coming from EOT
								// target?
								continue;
							}
							NoViableAlt(s, input);
							return 0;
						}
						s = snext;
						input.Consume();
						continue;
					}
					if (eot[s] >= 0)
					{  // EOT Transition?
						s = eot[s];
						input.Consume();
						continue;
					}
					if ((c == (char)Token.EOF) && (eof[s] >= 0))
					{  // EOF Transition to accept state?
						return accept[eof[s]];
					}
					// not in range and not EOF/EOT, must be invalid symbol
					NoViableAlt(s, input);
					return 0;
				}
			}
			finally
			{
				input.Rewind(mark);
			}
		}

		protected void NoViableAlt(int s, IIntStream input)
		{
			if (recognizer.state.backtracking > 0)
			{
				recognizer.state.failed = true;
				return;
			}
			NoViableAltException nvae =
				new NoViableAltException(Description,
										 decisionNumber,
										 s,
										 input);
			Error(nvae);
			throw nvae;
		}

		/// <summary>
		/// A hook for debugging interface 
		/// </summary>
		/// <param name="nvae"></param>
		public virtual void Error(NoViableAltException nvae) { ; }

		public virtual int SpecialStateTransition(int s, IIntStream input)
		{
			return -1;
		}

		public virtual string Description
		{
			get { return "n/a"; }
		}

		public static short[] UnpackEncodedString(string encodedString)
		{
			int size = 0;
			for (int i = 0; i < encodedString.Length; i += 2)
				size += (int)encodedString[i];

			short[] data = new short[size];
			int di = 0;
			for (int i = 0; i < encodedString.Length; i += 2)
			{
				char n = encodedString[i];
				char v = encodedString[i + 1];
				// add v n times to data
				for (int j = 1; j <= n; j++)
					data[di++] = (short)v;
			}

			return data;
		}
		public static short[][] UnpackEncodedStringArray(string[] encodedStrings)
		{
			short[][] result = new short[encodedStrings.Length][];
			for (int i = 0; i < encodedStrings.Length; i++)
				result[i] = UnpackEncodedString(encodedStrings[i]);
			return result;
		}
		public static char[] UnpackEncodedStringToUnsignedChars(string encodedString)
		{
			int size = 0;
			for (int i = 0; i < encodedString.Length; i += 2)
				size += (int)encodedString[i];

			char[] data = new char[size];
			int di = 0;
			for (int i = 0; i < encodedString.Length; i += 2)
			{
				char n = encodedString[i];
				char v = encodedString[i + 1];
				// add v n times to data
				for (int j = 1; j <= n; j++)
					data[di++] = v;
			}

			return data;
		}

		public int SpecialTransition(int state, int symbol)
		{
			return 0;
		}
	}
}