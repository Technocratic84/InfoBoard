
param([string]$ProjectDir, [string]$ProjectPath);

$daysSince = [math]::Ceiling(((Get-Date)-([DateTime]'02-22-2024')).TotalDays)
$rando = Get-Random -Minimum 1009 -Maximum 9898

$find = "<Version>(.|\n)*?</Version>";
$replace = "<Version>" + "0.9." + $daysSince + "." + $rando + "</Version>";

$csproj = Get-Content $ProjectPath
$csprojUpdated = $csproj -replace $find, $replace

Set-Content -Path $ProjectPath -Value $csprojUpdated

