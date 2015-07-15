Junу 21, 2011

## We have released a new version Ranet OLAP v2.2 ##
It includes the following enhancements:
  * Transition to platform Silverlight 4.0 is carried out
  * Support Windows 7 (x86, x64)
  * Support Microsoft Internet Information Server 7
  * At installation Ranet.UILibrary.OLAP the installer automatically creates Web application in Default Web Site also adjusts it for correct work with an example entering into the delivery complete set
  * Support of work with local cubes
  * Local cube Finance.cub is included in installer
  * Examples are established in the catalog: Documents and Settings\All Users\Documents\Ranet.UILibrary.Olap-2.2 Samples
  * Example of use, adjustment of context menu
  * Fix MDX syntax http://code.google.com/p/ranet-uilibrary-olap/issues/detail?id=17
  * Some bugs and regressions fixed


## [You can download Ranet OLAP v2.2 from the official web-site!](http://silverlight.galantis.com/) ##


April 12, 2011


## [Visit  our new official web-site](http://silverlight.galantis.com/) ##

Visit  our new official web-site at http://silverlight.galantis.com/

January 14, 2011

## Ranet.UILibrary.Olap-1.3.3.0-6571.msi is published ##

  * MdxDesigner: Fix for the issue where when an element is clicked, the mouse wheel stops working until the cursor leaves and re-enters the scrolling zone. http://code.google.com/p/ranet-uilibrary-olap/issues/detail?id=8

February 12, 2010

## Ranet.UILibrary.Olap-1.3.2.0-6545.msi is published ##

Main improvements:

  * Expand/Collapse on any member works fine
  * Control is able to sort axes members (by column title click including)
  * Columns autowidth is implemented

February 11, 2010

  * Branched to Ranet.UILibrary.OLAP-1.3
  * Some bugs and regressions fixed

February 03, 2010

  * PivotGrid: Sorting rows by columns values is implemented. PivotGrid.ColumnTitleClickBehavior = Ranet.AgOlap.Controls.ColumnTitleClickBehavior.SortByValue
  * Some bugs and regressions fixed

January 29, 2010

  * PivotGrid: Hierarchy Expansions with Subfields http://code.google.com/p/ranet-uilibrary-olap/issues/detail?id=5
  * PivotGrid: Ability to Sort Data http://code.google.com/p/ranet-uilibrary-olap/issues/detail?id=7
  * PivotGrid: Autofit Columns http://code.google.com/p/ranet-uilibrary-olap/issues/detail?id=9
  * Stablilization is started
  * etc...

January 22, 2010

  * PivotGrid: Expand/Collapse

January 20, 2010
  * PivotGrid: Axes sorting implementation started

January 12, 2010

  * Branched to Ranet.UILibrary.OLAP-1.2 (stabilization is started)
  * PivotGrid: Implemented custom cell condition designer (see UpdateablePivotGridControl.UseCellConditionsDesigner property).

December 20, 2009

  * Core: JSon and ZIP using cuts down network traffic more than 10 times for large queries. http://code.google.com/p/ranet-uilibrary-olap/issues/detail?id=4
  * Core: https/http switching is supported now. http://code.google.com/p/ranet-uilibrary-olap/issues/detail?id=6
  * DataService: Fixed multithreading issues. http://code.google.com/p/ranet-uilibrary-olap/issues/detail?id=3
  * MdxDesigner: Enabling users to remove items from filters, rows, columns, or data by dragging them out of the box.
  * MdxDesigner: Ability to select cube
  * MdxDesigner: Import/Export layout allows to remove old settings
  * PivotGrid: Display data if only rows or only columns are selected. http://code.google.com/p/ranet-uilibrary-olap/issues/detail?id=2
  * PivotGrid: Changed (calculated, but not saved) cells are displayed as bold blue
  * PivotGrid: On calculation error the appropriate icon and error description are displayed
  * PivotGrid: Added "DrillThrough" context menu item for cell (is not available for calculated cells and can be turned off programmatically)
  * PivotGrid: Changed icon for non expandable rows and columns ( [+] -> [**] )
  * ... small bug fixes**

November 05, 2009

  * Core: Added library for zip/unzip ability.

October 20, 2009

  * Samples:  Added UI controls samples
  * PivotGrid: fixed bug in Rotate axes. http://ranetuilibraryolap.codeplex.com/WorkItem/View.aspx?WorkItemId=5239
  * MdxDesigner: Added designer for calculated members (WITH MEMBER clause) http://ranetuilibraryolap.codeplex.com/WorkItem/View.aspx?WorkItemId=5237
  * MdxDesigner: Added designer for named sets (WITH SET clause)
  * DataService: Fully reimplemented. Added exceptions handling.
  * MDX: fixed bug in quoted and double quoted strings
  * MDX: fixed bug for NON EMPTY keyword in axes clause
  * MDX: fixed bug for &[key1](key1.md)&[key2](key2.md)... unique name parsing
  * Installer: English localization


## [Visit  our new official web-site](http://silverlight.galantis.com/) ##