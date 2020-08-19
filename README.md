[![version](https://img.shields.io/powershellgallery/v/DsReg.svg?include_prereleases)](https://www.powershellgallery.com/packages/DsReg) [![downloads](https://img.shields.io/powershellgallery/dt/DsReg.svg?label=downloads)](https://www.powershellgallery.com/stats/packages/DsReg?groupby=Version)

A PowerShell module that wraps "dsregcmd.exe" executable''s output.  It also supports pulling the output from a remote computer through WinRM (using PSSessions).

```powershell
Get-DsRegStatus -ComputerName "Win10-Remote.contoso.com"

# AzureADJoined                  : True
# AzureTenantId                  : 321d2a96-c69d-4f5b-b19c-7c8789e32e9f
# AzureTenantName                : Contoso
# DeviceId                       : 37c28cc5-ae59-4dfa-b11a-320fe0f44309
# AdfsEnterpriseJoined           : False
# HybridAzureADJoined            : True
# DomainJoined                   : True
# DomainName                     : CONTOSO
# DeviceCertificateThumbprint    : DD023AD3D457E624615219843015C800064565C5
# DeviceCertificateValidityStart : 11/18/2019 4:42:25 PM -05:00
# DeviceCertificateValidityEnd   : 11/18/2029 5:12:25 PM -05:00
# DeviceTpmProtected             : True
# DiagnosticDetails              : {KeySignTest = PASSED; AadRecoveryEnabled = False}
# KeyContainerId                 : 12b9ef99-cf03-4ab1-bec5-b78dbd41f0fe
# NgcPrerequisiteCheck           : {CertEnrollment = none; PreReqResult = WillNotProvision; ...}
# SsoState                       : {AzureAdPrimaryRefreshToken = True; ...}
# WorkAccounts                   : {Contoso, VolcanoCoffee, NorthwindTraders, BlueYonder, WorldwideImports}
```
