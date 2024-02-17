param (
    [string][ValidateSet("Debug", "Release")]$Configuration = "Debug"
)

$publishPath = "$PSScriptRoot/aspnet-core/src/RtlSdrServer.Web.Mvc/wwwroot/Plugins"
$logPath = "$PSScriptRoot/aspnet-core/src/RtlSdrServer.Web.Mvc/App_Data/logs/Logs.txt"

if (Test-Path $publishPath) {
    Remove-Item $publishPath -Recurse
}

if (Test-Path $logPath) {
    Remove-Item $logPath
}

New-Item -ItemType "Directory" -Path $publishPath | Out-Null
New-Item -ItemType "File" -Path $publishPath -Name ".gitkeep" | Out-Null

Copy-Item -Path "$PSScriptRoot/aspnet-core/src/RtlSdrServer.Lettura.Application/bin/$($Configuration)/net8.0/RtlSdrServer.Lettura.Application.dll" -Destination $publishPath
Copy-Item -Path "$PSScriptRoot/aspnet-core/src/RtlSdrServer.Lettura.Application/bin/$($Configuration)/net8.0/RtlSdrServer.Lettura.Application.pdb" -Destination $publishPath