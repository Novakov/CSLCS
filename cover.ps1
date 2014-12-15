$base = [System.IO.Path]::GetDirectoryName($MyInvocation.MyCommand.Definition)

$nunit = [System.IO.Path]::Combine($base, 'packages', 'NUnit.Runners.2.6.3', 'tools', 'nunit-console.exe')

$tests = [System.IO.Path]::Combine($base, 'Tests', 'bin', 'Debug', 'Tests.dll')

$opencover = [System.IO.Path]::Combine($base, 'packages', 'OpenCover.4.5.3427', 'OpenCover.Console.exe')

$reportGenerator = [System.IO.Path]::Combine($base, 'packages', 'ReportGenerator.2.0.2.0', 'ReportGenerator.exe')

& $opencover -register:user -output:coverage.xml "-filter:-[Tests]* +[CSL]*" "-target:$nunit" "-targetargs:$tests /noshadow"

& $reportGenerator -reports:coverage.xml -targetdir:report