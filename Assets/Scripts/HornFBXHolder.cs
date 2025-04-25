using UnityEngine;

public class HornFBXHolder : MonoBehaviour
{
    public CharacterRecolor CharacterChanger;
    public GameObject fbxRootObject; // The root GameObject of the FBX file
    public Mesh[] meshes; // Array to store all meshes
    public int HornIntFlag;
    void Start()
    {
        CharacterChanger = GetComponent<CharacterRecolor>();
        if (fbxRootObject == null)
        {
            Debug.LogError("FBX root object is not assigned!");
            return;
        }

        // Get all MeshFilter components in the FBX hierarchy
        MeshFilter[] meshFilters = fbxRootObject.GetComponentsInChildren<MeshFilter>();

        // Initialize the mesh array
        meshes = new Mesh[meshFilters.Length];

        // Extract meshes and store them in the array
        for (int i = 0; i < meshFilters.Length; i++)
        {
            meshes[i] = meshFilters[i].sharedMesh;
           // Debug.Log($"Found mesh: {meshes[i].name}");
        }

        Debug.Log($"Total Horn meshes found: {meshes.Length}");
    }

    public void ChangeHornType(int Up)
    {
        MeshFilter HornsToChange = CharacterChanger.ToRecolor.GetComponent<HornsFixer>().Horns.GetComponent<MeshFilter>();
        HornIntFlag += Up;
        if (HornIntFlag > meshes.Length)
            HornIntFlag = 0;
        if (HornIntFlag < 0)
            HornIntFlag = meshes.Length;
        Debug.Log("Mesh Length:" + meshes.Length);
        Debug.Log("The name of the horns to change is:" + HornsToChange.name);
        HornsToChange.mesh = meshes[HornIntFlag];
    }
    public void RandomHorn()
    {
        MeshFilter HornsToChange = CharacterChanger.ToRecolor.GetComponent<HornsFixer>().Horns.GetComponent<MeshFilter>();
        HornsToChange.mesh = meshes[Random.Range(0, meshes.Length)];
    }
}
