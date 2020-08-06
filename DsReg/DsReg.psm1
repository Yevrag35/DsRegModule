Function Get-DsRegStatus() {
    [CmdletBinding(PositionalBinding = $false, DefaultParameterSetName = 'None')]
	[Alias("Get-DsReg")]
    [OutputType([MG.DsReg.DsRegPoshResult])]
    param
    (
        [parameter(Mandatory = $true, Position = 0, ParameterSetName = 'RemoteQuery')]
        [parameter(Mandatory = $true, Position = 0, ParameterSetName = 'RemoteQueryAsJson')]
        [string] $ComputerName,

        [parameter(Mandatory = $true, Position = 0, ParameterSetName = 'RemoteQueryPSSession')]
        [parameter(Mandatory = $true, Position = 0, ParameterSetName = 'RemoteQueryPSSessionAsJson')]
        [System.Management.Automation.Runspaces.PSSession] $Session,

        [parameter(Mandatory = $true, ParameterSetName = 'LocalQueryAsJson')]
        [parameter(Mandatory = $true, ParameterSetName = 'RemoteQueryAsJson')]
        [parameter(Mandatory = $true, ParameterSetName = 'RemoteQueryPSSessionAsJson')]
        [switch] $AsJson,

        [Parameter(Mandatory = $false, ParameterSetName='None')]
        [Parameter(Mandatory=$false, ParameterSetName='RemoteQuery')]
        [Parameter(Mandatory=$false, ParameterSetName='RemoteQueryPSSession')]
        [ValidateSet("DiagnosticDetails", "NgcPrerequisiteCheck", "SsoState", "TenantDetails", "UserState", "WorkAccounts")]
        [string[]] $Display
    )
    $dsArgs = @{
        AsJson = $AsJson.ToBool()
    };
    if ($PSCmdlet.ParameterSetName.Contains("RemoteQuery")) {

        if ($PSBoundParameters.ContainsKey("ComputerName")) {
            $dsArgs.Add("ComputerName", $ComputerName)
        }
        elseif ($PSBoundParameters.ContainsKey("Session")) {
            $dsArgs.Add("Session", $Session)
        }

        $status = Get-RemoteDsRegStatus @dsArgs;
    }
    else {
        $status = Get-LocalDsRegStatus @dsArgs;
    }
    #$status
    if ($PSBoundParameters.ContainsKey("Display")) {

        foreach ($prop in $Display) {
            $status."$prop"
        }
    }
    else {
        $status
    }
}

Function Get-LocalDsRegStatus() {
    [CmdletBinding()]
    param
    (
        [bool] $AsJson
    )
    $executor = New-Object -TypeName "MG.DsReg.DsRegExecutor"
    $cmdResult = $executor.GetStatus()

    if ($AsJson) {

        $object = New-Object MG.DsReg.DsRegPoshResult($cmdResult)
        $object = $object.ToJson()
    }
    else {
        $object = New-Object MG.DsReg.DsRegPoshResult($cmdResult)
    }
    $object
}

Function Get-RemoteDsRegStatus() {
    [CmdletBinding()]
    param
    (
        [string[]] $ComputerName,
        [System.Management.Automation.Runspaces.PSSession] $Session,
        [bool] $AsJson
    )
    $invokeArgs = @{
        ScriptBlock = {
            [string[]]$status = & dsregcmd.exe /status

            return [pscustomobject]@{
                Text = $status
            }
        }
    }
    if ($PSBoundParameters.ContainsKey("ComputerName")) {
        $invokeArgs.Add("ComputerName", $ComputerName)
    }
    elseif ($PSBoundParameters.ContainsKey("Session")) {
        $invokeArgs.Add("Session", $Session)
    }

    $psObj = Invoke-Command @invokeArgs
    if ($null -ne $psObj.Text) {

        [string[]]$text = $psObj.Text
        $parsed = [MG.DsReg.DsRegParser]::ParseFromText($text)
        $details = New-Object MG.DsReg.DsRegPoshResult($parsed)

        if ($null -ne $details) {

            if ($AsJson) {
                $object = $details;
                $object = $object.ToJson()
            }
            else {
                $object = $details
            }
        }
        $object
    }
}