using UnityEngine;

public class BodyColorCustom : MonoBehaviour
{
    public SkinnedMeshRenderer BodyColors;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BodyColors = GetComponent<SkinnedMeshRenderer>();
        if (BodyColors == null)
        {
            Debug.LogError("SkinnedMeshRenderer reference is missing!");
            return;
        }
    }
    public void ChangeMaterial(int ToChange, Material NewColor)
    {
        Material[] ReplacementColor = BodyColors.materials;

        if (ReplacementColor.Length > 0)
        {
            // Replace the first material
            ReplacementColor[ToChange] = NewColor;

            // Apply the updated materials array back to the renderer
            BodyColors.materials = ReplacementColor;

            Debug.Log("First material has been successfully updated!");
        }
        else
        {
            Debug.LogError("SkinnedMeshRenderer does not have any materials!");
        }
    }
}
