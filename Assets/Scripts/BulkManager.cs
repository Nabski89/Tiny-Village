using UnityEngine;

public class BulkManager : MonoBehaviour
{
    public DialogText MainTextReference;
    public bool MoveLockdown;
    public bool InDialogue;
    void Start()
    {
        Application.targetFrameRate = 30;
    }
}
