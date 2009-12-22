﻿/*   
    Copyright (C) 2009 Galaktika Corporation ZAO

    This file is a part of Ranet.UILibrary.Olap
 
    Ranet.UILibrary.Olap is a free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
      
    You should have received a copy of the GNU General Public License
    along with Ranet.UILibrary.Olap.  If not, see
  	<http://www.gnu.org/licenses/> 
  
    If GPL v.3 is not suitable for your products or company,
    Galaktika Corp provides Ranet.UILibrary.Olap under a flexible commercial license
    designed to meet your specific usage and distribution requirements.
    If you have already obtained a commercial license from Galaktika Corp,
    you can use this file under those license terms.
*/

using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Ranet.AgOlap.Controls.General.ItemControls;
using Ranet.Olap.Core.Metadata;

namespace Ranet.AgOlap.Controls.MdxDesigner.Filters
{
    public class LevelPropertyCombo : UserControl
    {
        ComboBox comboBox;

        public LevelPropertyCombo()
        {
            Grid LayoutRoot = new Grid();

            comboBox = new ComboBox();
            LayoutRoot.Children.Add(comboBox);

            comboBox.Items.Add(new LevelPropertyItemControl(new LevelPropertyInfo("Caption", "Caption")));
            comboBox.Items.Add(new LevelPropertyItemControl(new LevelPropertyInfo("Name", "Name")));
            comboBox.Items.Add(new LevelPropertyItemControl(new LevelPropertyInfo("UniqueName", "UNIQUE_NAME")));

            comboBox.SelectedIndex = 0;
            this.Content = LayoutRoot;
        }

        public String CurrentProperty
        {
            get {
                LevelPropertyItemControl ctrl = comboBox.SelectedItem as LevelPropertyItemControl;
                if (ctrl != null)
                {
                    return ctrl.Info.Name;
                }
                return String.Empty;
            }
        }

        public void SelectItem(String name)
        {
            if (comboBox.Items.Count > 0)
                comboBox.SelectedIndex = 0;
            else
                comboBox.SelectedIndex = -1;

            int i = 0;
            foreach (LevelPropertyItemControl item in comboBox.Items)
            {
                if (item.Info.Name == name)
                {
                    comboBox.SelectedIndex = i;
                    break;
                }
                i++;
            }
        }
    }
}
