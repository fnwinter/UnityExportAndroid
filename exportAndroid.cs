#if (UNITY_EDITOR)
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

using UnityEngine;
using UnityEditor;

public class exportAndroid {

  public static string androidProjectPath = @"C:\your\android\project\path";

  [MenuItem("Export/Export Android %#e")]
  private static void ExportAndroid()
  {
    string export_path_name = "exported_temp";
    string expoted_android_path = GetProjectPath() + export_path_name;

    Debug.Log("BuildPlayer");
    BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
    buildPlayerOptions.scenes = GetScenePaths();
    buildPlayerOptions.locationPathName = export_path_name;
    buildPlayerOptions.target = BuildTarget.Android;
    buildPlayerOptions.options = BuildOptions.AcceptExternalModificationsToPlayer;
    BuildPipeline.BuildPlayer(buildPlayerOptions);

    string exported_android_assets_path =
      String.Format("{0}/{1}/src/main/assets", expoted_android_path, GetProjectName());

    string target_android_assets_path =
      String.Format("{0}/{1}/src/main/assets", androidProjectPath, GetProjectName());

    Debug.Log("copy from : " + exported_android_assets_path);
    Debug.Log("     to   : " + target_android_assets_path);
    // remove directory first.
    DirectoryRemove(target_android_assets_path);
    // then copy the exported one
    DirectoryCopy(exported_android_assets_path, target_android_assets_path, true);
  }

  static string GetProjectPath() {
    string projectPath = Application.dataPath;
    int index = projectPath.IndexOf("Assets");
    string returnStr = projectPath.Substring(0, index);
    return returnStr;
  }

  static string GetProjectName() {
    string projectPath = Application.dataPath;
    string[] paths = projectPath.Split('/');
    return paths[paths.Length -2];
  }

  // http://wiki.unity3d.com/index.php?title=AutoBuilder
  static string[] GetScenePaths()
  {
    string[] scenes = new string[EditorBuildSettings.scenes.Length];
    for(int i = 0; i < scenes.Length; i++) {
      scenes[i] = EditorBuildSettings.scenes[i].path;
    }
    return scenes;
  }

  private static void DirectoryRemove(string path)
  {
    System.IO.DirectoryInfo di = new DirectoryInfo(path);

    foreach (FileInfo file in di.GetFiles())
    {
      file.Delete(); 
    }
    foreach (DirectoryInfo dir in di.GetDirectories())
    {
      dir.Delete(true); 
    }
  }

  // https://docs.microsoft.com/ko-kr/dotnet/standard/io/how-to-copy-directories
  private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
  {
    // Get the subdirectories for the specified directory.
    DirectoryInfo dir = new DirectoryInfo(sourceDirName);

    if (!dir.Exists)
    {
      throw new DirectoryNotFoundException(
        "Source directory does not exist or could not be found: "
        + sourceDirName);
    }

    DirectoryInfo[] dirs = dir.GetDirectories();
    // If the destination directory doesn't exist, create it.
    if (!Directory.Exists(destDirName))
    {
      Directory.CreateDirectory(destDirName);
    }
    
    // Get the files in the directory and copy them to the new location.
    FileInfo[] files = dir.GetFiles();
    foreach (FileInfo file in files)
    {
      string temppath = Path.Combine(destDirName, file.Name);
      file.CopyTo(temppath, false);
    }

    // If copying subdirectories, copy them and their contents to new location.
    if (copySubDirs)
    {
      foreach (DirectoryInfo subdir in dirs)
      {
        string temppath = Path.Combine(destDirName, subdir.Name);
        DirectoryCopy(subdir.FullName, temppath, copySubDirs);
      }
    }
  }
}
#endif