Function Get-DsRegStatus()
{
    [CmdletBinding(PositionalBinding=$false, DefaultParameterSetName='None')]
    [OutputType([MG.DsReg.DsRegPoshResult])]
    param
    (
        [parameter(Mandatory=$true, Position = 0, ParameterSetName='RemoteQuery')]
        [parameter(Mandatory=$true, Position = 0, ParameterSetName='RemoteQueryAsJson')]
        [string] $ComputerName,

        [parameter(Mandatory=$true, Position = 0, ParameterSetName='RemoteQueryPSSession')]
        [parameter(Mandatory=$true, Position = 0, ParameterSetName='RemoteQueryPSSessionAsJson')]
        [System.Management.Automation.Runspaces.PSSession] $Session,

        [parameter(Mandatory=$true, ParameterSetName='LocalQueryAsJson')]
        [parameter(Mandatory=$true, ParameterSetName='RemoteQueryAsJson')]
        [parameter(Mandatory=$true, ParameterSetName='RemoteQueryPSSessionAsJson')]
        [switch] $AsJson
    )
    $dsArgs = @{
        AsJson = $AsJson.ToBool()
    };
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
    $status
}

Function Get-LocalDsRegStatus()
{
    [CmdletBinding()]
    param
    (
        [string] $Display,
        [bool] $AsJson
    )
    $executor = New-Object -TypeName "MG.DsReg.DsRegExecutor"
    $cmdResult = $executor.GetStatus();
    if ($AsJson)
    {
        $object = New-Object MG.DsReg.DsRegPoshResult($cmdResult);
        $object = $object.ToJson()
    }
    else
    {
        $object = New-Object MG.DsReg.DsRegPoshResult($cmdResult);
    }

    , $object
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
            if ($AsJson)
            {
                $object = $details;
                $object = $object.ToJson()
            }
            else
            {
                $object = $details;
            }
        }
        $object
    }
}