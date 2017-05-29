[![Build status](https://ci.appveyor.com/api/projects/status/v94urgemv76oid9q/branch/master?svg=true)](https://ci.appveyor.com/project/Shenderchuk/copyproductcode/branch/master)
# CopyProductCode


## Synopsis

The tool for copying MSI ProductCode into clipboard.

## Motivation

When working a lot with MSI installers, is not very convinient to open a MSI files, go to Property table, look for ProductCode and select it and copy each time. This tool should simplify the life of peope who works with Windows Installer packages, because you need only do 2 clicks to receive ProductCode.

## Installation

You can [download](https://github.com/shenderchuk/CopyProductCode/releases/tag/v2.0.17.0) latest installable version of CopyProductCode for Windows. It is built on AppVeyor and ready for simple Next-Next installation.
It will install the tool themselves and add dynamic right-click menu to Windows Explorer.

## How to use

Right-click on any .msi file and choose "Copy ProductCode to Clipboard"

Also, the tool can be used from command-line:
```PowerShell
copyproductcode.exe "c:\path_to_msi\testappliaction.msi"
```

Tests

Describe and show how to run the tests with code examples.

#### License

Apache License (Version 2.0)

