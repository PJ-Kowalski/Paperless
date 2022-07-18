$kill = read-host "Should I kill? (yes or [no])"
if($kill -notmatch "^(yes|no|^$)$")
{
	exit
}
if($kill -eq "")
{
	$kill = "no"
}
if($kill -eq "yes")
{
	$credential = get-credential
	if($credential -eq $null)
	{
		write-host "Cancelled by user" -backgroundcolor yellow -foregroundcolor black
		exit
	}
}
$backup = read-host "Should I do a directory backup (will do one backup per day, if todays backup already exist will not override)? ([yes] or no)"
if($backup -notmatch "^(yes|no|^$)$")
{
	exit
}
if($backup -eq "")
{
	$backup = "yes"
}

Import-Module '.\Publish-Project.psm1' -Force
Import-Module '.\New-Shortcut.psm1' -Force

Publish-Project -kill $kill -backup $backup -build 'TEST' -prog_name "TabletTransfer" -admin $credential -shortcuts @{Name = "- SIM"; Pars = "--sim"}
Publish-Project -kill $kill -backup $backup -build 'TEST' -prog_name "TabletManager" -admin $credential