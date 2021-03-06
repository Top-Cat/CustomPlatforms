using System.IO;
using UnityEditor;
using UnityEngine;

namespace CustomFloorPlugin
{
    [CustomEditor(typeof(CustomPlatform))]
    public class CustomPlatformEditor : Editor
    {
        CustomPlatform customPlat;

        private void OnEnable()
        {
            customPlat = (CustomPlatform)target;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Export"))
            {
                string path = EditorUtility.SaveFilePanel("Save Platform file", "", customPlat.platName + ".plat", "plat");

                if (path != "")
                {
                    string fileName = Path.GetFileName(path);
                    string folderPath = Path.GetDirectoryName(path);

                    PrefabUtility.CreatePrefab("Assets/_CustomPlatform.prefab", customPlat.gameObject);
                    AssetBundleBuild assetBundleBuild = default(AssetBundleBuild);
                    assetBundleBuild.assetNames = new string[] {
                    "Assets/_CustomPlatform.prefab"
                        };

                    assetBundleBuild.assetBundleName = fileName;

                    BuildTargetGroup selectedBuildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
                    BuildTarget activeBuildTarget = EditorUserBuildSettings.activeBuildTarget;

                    BuildPipeline.BuildAssetBundles(Application.temporaryCachePath, new AssetBundleBuild[] { assetBundleBuild }, 0, EditorUserBuildSettings.activeBuildTarget);

                    EditorPrefs.SetString("currentBuildingAssetBundlePath", folderPath);

                    EditorUserBuildSettings.SwitchActiveBuildTarget(selectedBuildTargetGroup, activeBuildTarget);

                    AssetDatabase.DeleteAsset("Assets/_CustomPlatform.prefab");

                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

                    File.Move(Application.temporaryCachePath + "/" + fileName, path);

                    AssetDatabase.Refresh();

                    EditorUtility.DisplayDialog("Exportation Successful!", "Exportation Successful!", "OK");
                }
                else
                {
                    EditorUtility.DisplayDialog("Exportation Failed!", "Path is invalid.", "OK");
                }

            }
        }
    }
}