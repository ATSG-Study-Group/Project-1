#--------------------------------------------------------------------------------------------------
# CLI Parameters
#--------------------------------------------------------------------------------------------------
param(
    # FilenameRoot
    [Parameter(Mandatory=$true)]
    [string]$ProjectName,
    # ProgramSlot
    [string]$programSlot = '10',
    # device
    [string]$device = '10.10.80.10',
    # username
    [string]$User = 'StudyGroup',
    # password
    [string]$pw = 'd08s#dfGy',
    # DevelopmentFolder
    [string]$DevFolder = '',
    # SolutionFolder
    [string]$SolutionFolder = '',
    # CLZ destination
    [string]$CLZFolder = '\SIMPL'
)

#--------------------------------------------------------------------------------------------------
# Should not need to change anything from here down
#--------------------------------------------------------------------------------------------------
if ($DevFolder.Length.Equals(0))
{
    $DevFolder = (Get-Item $PSScriptRoot).Parent.FullName
}

$programSlot = $programSlot.PadLeft(2,'0');

$str = '\Sharp'+ $SolutionFolder+ '\' + $ProjectName + '\bin\Debug'
$sourcePath = Join-Path $DevFolder $str

$CLZDest = Join-Path $DevFolder $CLZFolder

$FileDLL = $ProjectName + '.dll'
$FileCLZ = $ProjectName + '.clz'

#Send to processor
$source = Join-Path $sourcePath $FileDLL

$str = '\program' + $programSlot
$dest = Join-Path $str $FileDLL

Write-Host 'Stopping Program'

$cmd = 'Stopprog -p:' + $programSlot
Invoke-CrestronCommand -Command $cmd  -Device $device -Secure -Username $User -Password $pw -Timeout 30 -Prompt 'Stopped:'

Write-Host 'Sending file'
$str = '\program' + $programSlot

$source = Join-Path $sourcePath $FileDLL
$dest = Join-Path $str $FileDLL
Send-FTPFile -Device $device -LocalFile $source -RemoteFile $dest -Password $pw -Secure -Username $User

Write-Host 'Restarting Program'

$cmd = 'Progreset -p:' + $programSlot
Invoke-CrestronCommand -Command $cmd -Device $device -Secure -Username $User -Password $pw -Timeout 60 -Prompt 'Started...'

#copy to SIMPL folder to keep things in sync

$source = Join-Path $sourcePath $FileCLZ

Copy-Item $source -Destination $CLZDest
