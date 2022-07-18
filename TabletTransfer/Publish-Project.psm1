function Publish-Project {
	param ([string]$kill, [string]$backup, [string]$build, [string]$prog_name, $admin, $shortcuts)

	if($build -eq "TEST")
	{
		$dest = "\\appsprod\prod\_TESTY\01_JJonda\\Paperless"
		$srv_regex = "^d:\\prod\\_TESTY\\01_JJonda\\Paperless\\$($prog_name)\\.*"
	}
	if($build -eq "PROD")
	{
		$dest = "\\appsprod\prod"
		$srv_regex = "^d:\\prod\\$($prog_name)\\.*"
	}

	$7z = "C:\Program Files\7-Zip\7z.exe"
	$now = `get-date -Format "yyyyMMdd"`

	if($backup -eq "yes")
	{
		$exists = test-Path -path "$($dest)\$($prog_name)\_bkp_$($prog_name)_$($now).7z" -pathtype  Leaf
		if(-not $exists)
		{
			& $7z a -t7z "$($dest)\$($prog_name)\_bkp_$($prog_name)_$($now).7z" "$($dest)\$($prog_name)\" "-xr!*.7z"
		}
		else
		{
			write-host "backup juz istnieje, nie wykonuje kolejnego!" -backgroundcolor yellow -foregroundcolor black
		}
	}

	if($kill -eq "yes")
	{
		invoke-command -computername appsprod -scriptblock { Get-SmbOpenFile | where Path -match $srv_regex|close-smbopenfile -Force } -Credential $admin
	}

	robocopy /S /E "$($prog_name)\bin\Debug\" "$($dest)\$($prog_name)\"
	
	foreach($s in $shortcuts)
	{
		New-Shortcut "$($dest)\$($prog_name)\$($prog_name).exe" "$($s.Pars)" "$($dest)\$($prog_name)\$($prog_name) $($build) $($s.Name).lnk"	
	}
	write-host "koniec $($prog_name), ok" -backgroundcolor green -foregroundcolor black
}

Export-ModuleMember -Function Publish-Project