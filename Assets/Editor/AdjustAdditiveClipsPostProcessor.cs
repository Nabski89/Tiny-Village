using UnityEditor;
using UnityEngine;

public class AdjustAdditiveClipsPostProcessor : AssetPostprocessor
{
    static string _lastTPoseAssetPath;
    static AnimationClip _lastTPoseClip;

    const string T_POSE_NAME = "T-Pose";
    const string ADDITIVE_NAME = "Add";
    const string LOOP_NAME = "Loops";
//make sure you have your TPOSE FIRST, so actually name it something like 1.T-Pose
    void OnPostprocessAnimation(GameObject go, AnimationClip clip)
    {
         Debug.Log($"Importing {clip.name}");
        if (clip.name.Contains(T_POSE_NAME))
        {
            _lastTPoseAssetPath = assetPath;
            _lastTPoseClip = clip;
        }
        else if (clip.name.Contains(ADDITIVE_NAME))
        {
            // Check that we have a valid TPose cached from this asset
            if (_lastTPoseAssetPath != assetPath || _lastTPoseClip == null)
            {
                Debug.LogError($"No T-Pose available from asset. Was the t-pose not defined first? {assetPath}");
            }
            context.DependsOnCustomDependency(nameof(AdjustAdditiveClipsPostProcessor));
            AnimationUtility.SetAdditiveReferencePose(clip, _lastTPoseClip, 0);
        }
    }
}