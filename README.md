[![Build status](https://ci.appveyor.com/api/projects/status/v94urgemv76oid9q/branch/master?svg=true)](https://ci.appveyor.com/project/Shenderchuk/copyproductcode/branch/master)
# CopyProductCode


## Synopsis

The tool for copying MSI ProductCode into clipboard.

## Motivation

When working a lot with MSI installers, is not very convinient to open a MSI files, go to Property table, look for ProductCode and select it and copy each time. This tool should simplify the life of peope who work with Windows Installer packages, because you need only 2 clicks to receive ProductCode.

## Installation

You can [download](https://github.com/Shenderchuk/CopyProductCode/releases/latest) latest installable version of CopyProductCode for Windows. It is built on AppVeyor and ready for simple Next-Next-Finish installation.
It will install the tool themselves and add dynamic right-click menu to Windows Explorer.

## How to use

Right-click on any .msi file and choose "Copy ProductCode to Clipboard"

Also, the tool can be used from command-line:
```PowerShell
CopyProductCode.exe "c:\path_to_msi\testappliaction.msi"
```

#### License

Apache License (Version 2.0)

