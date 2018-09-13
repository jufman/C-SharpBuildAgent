using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Framework;
using Microsoft.Build.Logging;
using ILogger = Microsoft.Build.Framework.ILogger;

namespace C_SharpBuildAgent.Lib.Build
{
    public static class BuildLogic
    {
        private static ILogger logger;

        public static bool BuildProject(Objects.BuildItem buildItem, Settings.Objects.Details settingsDetails)
        {
            var projectLocation = settingsDetails.SourcesLocation + buildItem.Name;
            var outputLocation = settingsDetails.BuildLocation + buildItem.Name;
            var solutionLocation = projectLocation + "\\" + buildItem.PathToSln;

            if (Directory.Exists(outputLocation) == false)
            {
                Directory.CreateDirectory(outputLocation);
            }

            //InstallNuGetPackages(solutionLocation);

            FileLogger fileLogger = new FileLogger()
            {
                Verbosity = LoggerVerbosity.Detailed,
                ShowSummary = true,
                SkipProjectStartedText = true
            };

            BuildParameters param = new BuildParameters()
            {
                MaxNodeCount = Environment.ProcessorCount,
                Loggers = new[] {
                    fileLogger
                },
                
            };

            Dictionary<string, string> globalProperties = new Dictionary<string, string>
            {
                { "Configuration", "Release" }, // always "Debug"
                { "Platform", buildItem.Platform }, // always "Any CPU"
                { "OutputPath", outputLocation },
            };

            BuildManager.DefaultBuildManager.BeginBuild(param);

            var buildRequest = new BuildRequestData(solutionLocation, globalProperties, null, new String[] { "Build" }, null);
            var buildSubmission = BuildManager.DefaultBuildManager.PendBuildRequest(buildRequest);

            buildSubmission.Execute();
            BuildManager.DefaultBuildManager.EndBuild();
            if (buildSubmission.BuildResult.OverallResult == BuildResultCode.Failure)
            {
                return false;
            }

            return true;
        }

        private static bool InstallNuGetPackages(string solutionLocation)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "nuget.exe";
            startInfo.Arguments = "restore \"" + solutionLocation + "\"";
            startInfo.WorkingDirectory = @"C:\temp\";
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            using (Process exeProcess = Process.Start(startInfo))
            {
                exeProcess.WaitForExit();

                var output = exeProcess.StandardOutput.ReadToEnd();
                output.ToString();
            }

            return true;
        }

    }
}
