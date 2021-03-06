#load nuget:?package=Cake.Recipe&version=2.2.0

Environment.SetVariableNames();

BuildParameters.SetParameters(
    context: Context,
    buildSystem: BuildSystem,
    sourceDirectoryPath: "./Source",
    testDirectoryPath: "./Tests",
    title: "Anori.Common",
    repositoryOwner: "anorisoft",
    repositoryName: "Anori.Common",
    appVeyorAccountName: "anorisoft",
    shouldGenerateDocumentation: true,
    shouldRunDupFinder: false,
	shouldRunCodecov: true,
    shouldRunCoveralls: true,
    shouldRunDotNetCorePack: true);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(
    context: Context,
    dupFinderExcludePattern: new string[]
    {
        BuildParameters.RootDirectoryPath + "/Source/Anori.Common*/**/*.AssemblyInfo.cs",
        BuildParameters.RootDirectoryPath + "/Tests/**/*.cs"
    },
    testCoverageFilter: "+[*]* -[NUnit*]* -[*.Tests]* -[EmptyFiles]*",
    testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
    testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

Build.RunDotNetCore();
