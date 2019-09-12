﻿#
# Module manifest for module 'DsReg'
#
# Generated by: Mike Garvey
#
# Generated on: 9/12/2019
#

@{

# Script module or binary module file associated with this manifest.
# RootModule = ''

# Version number of this module.
ModuleVersion = '1.1.1'

# Supported PSEditions
CompatiblePSEditions = @('Desktop')

# ID used to uniquely identify this module
GUID = '5d30aac2-ac6d-498f-85d7-33b7c5836cb6'

# Author of this module
Author = 'Mike Garvey'

# Company or vendor of this module
CompanyName = 'Yevrag35, LLC.'

# Copyright statement for this module
Copyright = '(c) 2019 Mike Garvey. All rights reserved.'

# Description of the functionality provided by this module
Description = 'A PowerShell wrapper for the dsregcmd executable''s output.'

# Minimum version of the Windows PowerShell engine required by this module
PowerShellVersion = '5.1'

# Name of the Windows PowerShell host required by this module
# PowerShellHostName = ''

# Minimum version of the Windows PowerShell host required by this module
# PowerShellHostVersion = ''

# Minimum version of Microsoft .NET Framework required by this module. This prerequisite is valid for the PowerShell Desktop edition only.
DotNetFrameworkVersion = '4.7.2'

# Minimum version of the common language runtime (CLR) required by this module. This prerequisite is valid for the PowerShell Desktop edition only.
# CLRVersion = ''

# Processor architecture (None, X86, Amd64) required by this module
# ProcessorArchitecture = ''

# Modules that must be imported into the global environment prior to importing this module
# RequiredModules = @()

# Assemblies that must be loaded prior to importing this module
RequiredAssemblies = @('MG.DsReg.dll', 'Newtonsoft.Json.dll')

# Script files (.ps1) that are run in the caller's environment prior to importing this module.
# ScriptsToProcess = @()

# Type files (.ps1xml) to be loaded when importing this module
# TypesToProcess = @()

# Format files (.ps1xml) to be loaded when importing this module
FormatsToProcess = @('DsRegResult.ps1xml')

# Modules to import as nested modules of the module specified in RootModule/ModuleToProcess
NestedModules = @('DsReg.psm1')

# Functions to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no functions to export.
FunctionsToExport = 'Get-DsRegStatus'

# Cmdlets to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no cmdlets to export.
CmdletsToExport = @()

# Variables to export from this module
VariablesToExport = @()

# Aliases to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no aliases to export.
AliasesToExport = @()

# DSC resources to export from this module
# DscResourcesToExport = @()

# List of all modules packaged with this module
# ModuleList = @()

# List of all files packaged with this module
FileList = @(
	'DsReg.psd1',
    'DsReg.psm1',
    'DsRegResult.ps1xml',
	'MG.DsReg.dll',
	'Newtonsoft.Json.dll'
)

# Private data to pass to the module specified in RootModule/ModuleToProcess. This may also contain a PSData hashtable with additional module metadata used by PowerShell.
PrivateData = @{

    PSData = @{

        # Tags applied to this module. These help with module discovery in online galleries.
        Tags = @('dsreg', 'dsregcmd', 'exe', 'Azure', 'AD', 'Tenant', 'Details', 'SSO', 'Certificate', 'ID', 'State', 'Device', 'Intune', 'Authority'
			'remote', 'pssession', 'computername')

        # A URL to the license for this module.
        # LicenseUri = ''

        # A URL to the main website for this project.
        ProjectUri = 'https://github.com/Yevrag35/DsRegModule'

        # A URL to an icon representing this module.
        # IconUri = ''

        # ReleaseNotes of this module
        ReleaseNotes = 'Adds a format file to help see data right away.'

    } # End of PSData hashtable

} # End of PrivateData hashtable

# HelpInfo URI of this module
# HelpInfoURI = ''

# Default prefix for commands exported from this module. Override the default prefix using Import-Module -Prefix.
# DefaultCommandPrefix = ''

}

