using UnityEngine;
using UnityEditor;

public class FBXLoopPostprocessor : AssetPostprocessor
{
    const string LOOP_NAME = "Loop";
    void OnPostprocessModel(GameObject g)
    {
        // Check if the imported asset is an FBX file
        if (assetPath.EndsWith(".fbx"))
        {
            // Access the ModelImporter associated with the asset
            ModelImporter modelImporter = assetImporter as ModelImporter;

            if (modelImporter != null)
            {
                // Retrieve the clip animations
                ModelImporterClipAnimation[] clips = modelImporter.defaultClipAnimations;

                for (int i = 0; i < clips.Length; i++)
                {
                    // Set the loopTime property to true
                    if (clips[i].name.Contains(LOOP_NAME))
                    {
                        clips[i].loopTime = true;
                        Debug.Log($"Loop Time enabled for animation clip: {clips[i].name}");
                    }
                }

                // Apply the modified clip animations back to the importer
                modelImporter.clipAnimations = clips;
            }
        }
    }
}