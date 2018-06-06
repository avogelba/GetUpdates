# GetUpdates
Windows Update API tool to fetch updates and export to CSV

![Screenshot](https://github.com/avogelba/GetUpdates/blob/master/Screenshot.png)

- Tool is in german
- a lot of plaing around went into this (SVG Buttons, AboutBox, etc.)
- uses WUApi https://msdn.microsoft.com/en-us/library/windows/desktop/aa387287(v=vs.85).aspx
- Actually done to show output of diferent Windows Update display (Clasic Programs & Features, windows 10 Update History, Powershell Scripts) and be able to search better for KB Names
- does not show updates not distributed by Windows Update

![Different Outputs](https://github.com/avogelba/GetUpdates/blob/master/diffs.png)

# WUApi using
Method 1:
- use: tlbimp.exe wuapi.dll /out=WUApiInterop.dll
- add WUApiInterop.dll to references in the project
- add using WUApiInterop; to the code

Method 2:
- Add ![References](https://github.com/avogelba/GetUpdates/blob/master/REF1.png)
- add using WUApiLib;


# Code
A mess!

# Bugs
probably plenty

# ThirdParty Code Used
For AboutBox (I am lazy)
	https://blogs.msdn.microsoft.com/pedrosilva/2009/05/08/wpfaboutbox-dialog-layout-and-styles/
	https://marketplace.visualstudio.com/items?itemName=PedroSilvaVisualStudio.WPFAboutBoxCS

For Searching in DataDrid:
	DataGrid Searcher von sa_ddam213(retired)
	https://stackoverflow.com/questions/15467553/proper-datagrid-search-from-textbox-in-wpf-using-mvvm

# License
MIT