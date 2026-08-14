#--------------------------------------------------------------------------------------------------
# CLI Parameters
#--------------------------------------------------------------------------------------------------
param(
    # Filename
    [Parameter(Mandatory=$true)]
    [string]$FileName,
    # ProgramSlot
    [int32]$programSlot = '10',
    # device
    [string]$device = '10.10.80.10',
    # username
    [string]$User = 'StudyGroup',
    # password
    [string]$pw = 'd08s#dfGy',
    # LPZ folder
    [string]$LPZFolder = '\SIMPL',
    # NewestInFolder
    [switch]$NewestInFolder,
    #FTPMethod
    [switch]$FTPMethod
)

#--------------------------------------------------------------------------------------------------
# Should not need to change anything from here down
#--------------------------------------------------------------------------------------------------
#$DevFolder = (Get-Item $PSScriptRoot).Parent.FullName

#$sourcePath = Join-Path $DevFolder $LPZFolder
$sourcePath = $LPZFolder



#Send to processor
if ($NewestInFolder)
{
    $newest = Get-ChildItem -path "$sourcePath\$FileName*" -Include '*.lpz' | Sort-Object  -Descending -Property LastWriteTime | select -First 1
    $FileName = $newest.Name
}

$source = Join-Path $sourcePath $FileName

Write-Host 'Sending program'

if($FTPMethod)
{
    $FTPDest = "\program"+ "{0:D2}"-f $programSlot + "\" + $FileName

    Send-FTPFile -Device $device -LocalFile $source -RemoteFile $FTPDest -Secure -Username $User -Password $pw 

    $loadcmd = "Progload -p:"+$programSlot

    Write-Host 'Loading program'

    Invoke-CrestronCommand -Device $device -Command $loadcmd -Secure -Username $User -Password $pw -Timeout 120 -Prompt 'Started...'
}
else
{
    Send-CrestronProgram -Device $device -LocalFile $source -ProgramSlot $programSlot -Secure -Username $User -Password $pw -Timeout 120 
}

