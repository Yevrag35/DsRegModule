Function Get-DsRegStatus()
{
    [CmdletBinding(PositionalBinding=$false, DefaultParameterSetName='None')]
    [OutputType([MG.DsReg.IDsRegResult])]
    param
    (
        [parameter(Mandatory=$true, Position = 0, ParameterSetName='RemoteQuery')]
        [parameter(Mandatory=$true, Position = 0, ParameterSetName='RemoteQueryAsJson')]
        [parameter(Mandatory=$true, Position = 0, ParameterSetName='RemoteQueryExpanded')]
        [string] $ComputerName,

        [parameter(Mandatory=$true, Position = 0, ParameterSetName='RemoteQueryPSSession')]
        [parameter(Mandatory=$true, Position = 0, ParameterSetName='RemoteQueryPSSessionAsJson')]
        [parameter(Mandatory=$true, Position = 0, ParameterSetName='RemoteQueryPSSessionExpanded')]
        [System.Management.Automation.Runspaces.PSSession] $Session,

        [parameter(Mandatory=$false)]
        [ValidateSet("DeviceDetails", "DeviceState", "DiagnosticData", "NgcPrerequisiteCheck", "SsoState", "TenantDetails", "UserState", "WorkAccounts")]
        [string] $Display,

        [parameter(Mandatory=$true, ParameterSetName='LocalQueryAsJson')]
        [parameter(Mandatory=$true, ParameterSetName='RemoteQueryAsJson')]
        [parameter(Mandatory=$true, ParameterSetName='RemoteQueryPSSessionAsJson')]
        [switch] $AsJson,

        [parameter(Mandatory=$true, ParameterSetName='LocalQueryExpanded')]
        [parameter(Mandatory=$true, ParameterSetName='RemoteQueryExpanded')]
        [parameter(Mandatory=$true, ParameterSetName='RemoteQueryPSSessionExpanded')]
        [switch] $Expand
    )
    $dsArgs = @{
        AsJson = $AsJson.ToBool()
        Expanded = $Expand.ToBool()
    };
    if ($PSBoundParameters.ContainsKey("Display"))
    {
        $remArgs.Display = $Display;
    }
    if ($PSCmdlet.ParameterSetName.Contains("RemoteQuery"))
    {
        if ($PSBoundParameters.ContainsKey("ComputerName"))
        {
            $Session = New-PSSession -ComputerName $ComputerName -ErrorAction Stop;
            $removeAfter = $true;
        }
        $status = Get-RemoteDsRegStatus -Session $Session @dsArgs;
        if ($removeAfter)
        {
            $Session | Remove-PSSession;
        }
    }
    else
    {
        $status = Get-LocalDsRegStatus @dsArgs;
    }
    Write-Output -InputObject $status -NoEnumerate;
}

Function Get-LocalDsRegStatus()
{
    [CmdletBinding()]
    param
    (
        [string] $Display,
        [bool] $AsJson,
        [bool] $Expanded
    )
    $executor = [MG.DsReg.DsRegExecutor]::NewExecutor();
    $cmdResult = $executor.GetStatus();
    if (-not [string]::IsNullOrEmpty($Display))
    {
        $object = $cmdResult.$Display;
        if ($AsJson)
        {
            $object = $object.ToJson("Indented", $false);
        }
    }
    else
    {
        if ($AsJson)
        {
            $object = $cmdResult;
            $object = $object | ConvertTo-Json -Depth 100;
        }
        elseif ($Expanded)
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

Function Get-RemoteDsRegStatus()
{
    [CmdletBinding()]
    param
    (
        [System.Management.Automation.Runspaces.PSSession] $Session,
        [string] $Display = $null,
        [bool] $AsJson,
        [bool] $Expanded
    )
    $psObj = Invoke-Command -Session $Session -ScriptBlock {
        [string[]]$status = & dsregcmd.exe /status;
        return [pscustomobject]@{
            Text = $status
        };
    }
    if ($null -ne $psObj.Text)
    {
        [string[]]$text = $psObj.Text;
        $details = [MG.DsReg.DsRegParser]::ParseFromText($text);
        if ($null -ne $details)
        {
            if (-not [string]::IsNullOrEmpty($Display))
            {
                $object = $details.$Display;
                if ($AsJson)
                {
                    $object = $object.ToJson("Indented", $false);
                }
            }
            else
            {
                if ($AsJson)
                {
                    $object = $details;
                    $object = $object | ConvertTo-Json -Depth 100;
                }
                elseif ($Expanded)
                {
                    $object = New-Object 'System.Collections.Generic.List[MG.DsReg.BaseDetail]'
                    $object.Add($details.DeviceDetails);
                    $object.Add($details.DeviceState);
                    $object.Add($details.DiagnosticData);
                    $object.Add($details.NgcPrerequisiteCheck);
                    $object.Add($details.SsoState);
                    $object.Add($details.TenantDetails);
                    $object.Add($details.UserState);
                    $object.AddRange($details.WorkAccounts);
                }
                else
                {
                    $object = $details;
                }
            }
        }
        Write-Output -InputObject $object;
    }
}