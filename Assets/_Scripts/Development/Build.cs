using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build.Reporting;

public static class Build {
    static BuildReport BuildBinary(BuildPlayerOptions options, string title) {
        Debug.Log($"{DateTime.Now} Building {title}...");
        var result = BuildPipeline.BuildPlayer(options);
        string report = result.summary.result.ToString().ToUpper();
        Debug.Log($"{DateTime.Now} - {title} build {report}");
        return result;
    }

    static BuildReport BuildGame(string folderName, string fileType, BuildTarget buildTarget) {
        string productName = Application.productName;
        string version = Application.version;
        string destination = $".\\Builds\\{folderName}\\V{version}\\{productName}{fileType}";
        BuildPlayerOptions build = new BuildPlayerOptions {
            scenes = Array.ConvertAll(EditorBuildSettings.scenes, x => x.path),
            locationPathName = destination,
            target = buildTarget
        };
        return BuildBinary(build, productName);
    }

    [MenuItem("Data Structures and Algorithms/Build/Build PC")]
    public static void BuildPC() {
        BuildGame("PC", ".exe", BuildTarget.StandaloneWindows64 );
    }
    
    [MenuItem("Data Structures and Algorithms/Build/Build WebGL")]
    public static void BuildWebGL() {
        BuildGame("WebGL", "",BuildTarget.WebGL);
    }
}
#endif