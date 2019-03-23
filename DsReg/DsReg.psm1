Function Get-DsRegStatus()
{
    [CmdletBinding(DefaultParameterSetName='AsJson')]
    [OutputType([MG.DsReg.IDsRegResult])]
    param
    (
        [parameter(Mandatory=$false, Position=0, ParameterSetName="AsJson")]
        [ValidateSet("DeviceDetails", "DeviceState", "DiagnosticData", "NgcPrerequisiteCheck", "SsoState", "TenantDetails", "UserState", "WorkAccounts")]
        [string] $Display,

        [parameter(Mandatory=$false, ParameterSetName="AsJson")]
        [switch] $AsJson,

        [parameter(Mandatory=$false, ParameterSetName="Expand")]
        [switch] $Expand
    )

    $executor = [MG.DsReg.DsRegExecutor]::NewExecutor();
    $cmdResult = $executor.GetStatus();
    if ($PSBoundParameters.ContainsKey("Display"))
    {
        $object = $cmdResult.$Display;
        if ($PSBoundParameters.ContainsKey("AsJson"))
        {
            $object = $object.ToJson("Indented", $false);
        }
    }
    else
    {
        if ($PSBoundParameters.ContainsKey("AsJson"))
        {
            $object = $cmdResult;
            $object = $object | ConvertTo-Json -Depth 100;
        }
        elseif ($PSBoundParameters.ContainsKey("Expand"))
        {
            $object = New-Object 'System.Collections.Generic.List[MG.DsReg.BaseDetail]'
            $object.Add($cmdResult.DeviceDetails);
            $object.Add($cmdResult.DeviceState);
            $object.Add($cmdResult.DiagnosticData);
            $object.Add($cmdResult.NgcPrerequisiteCheck);
            $object.Add($cmdResult.SsoState);
            $object.Add($cmdResult.TenantDetails);
            $object.Add($cmdResult.UserState);
            $object.AddRange($cmdResult.WorkAccounts);
        }
        else
        {
            $object = $cmdResult;
        }
    }

    Write-Output -InputObject $object -NoEnumerate;
}