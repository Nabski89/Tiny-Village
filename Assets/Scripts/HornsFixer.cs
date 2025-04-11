using UnityEngine;

public class HornsFixer : MonoBehaviour
{
    public GameObject Horns;
    public GameObject HornsBone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Horns.transform.parent = HornsBone.transform;
        Horns.transform.localPosition = Vector3.zero;
        Horns.transform.localRotation = Quaternion.Euler(-90, 0, 0);
    }
}
