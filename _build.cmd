CLS

REM Set correct path to MSBuild (12 - is version VS2013, 14 - is version VS2015)
REM If MSBuild installed as standalone tool, the MSBuildbin variable should be updated appropriately. 
IF EXIST "%ProgramFiles(x86)%\MSBuild\12.0\Bin\" Set MSBuildbin=%ProgramFiles(x86)%\MSBuild\12.0\Bin\
IF EXIST "%ProgramFiles(x86)%\MSBuild\14.0\Bin\" Set MSBuildbin=%ProgramFiles(x86)%\MSBuild\14.0\Bin\

"%MSBuildbin%\msbuild.exe" .\CopyProductCode.sln /t:Rebuild /p:Configuration=Release