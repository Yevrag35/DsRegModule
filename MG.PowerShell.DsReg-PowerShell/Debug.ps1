[CmdletBinding()]
param ()

$curDir = Split-Path -Parent $MyInvocation.MyCommand.Definition;
Import-Module "$curDir\MG.PowerShell.DsReg.dll" -ea Stop;