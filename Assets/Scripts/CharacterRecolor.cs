using UnityEngine;

public class CharacterRecolor : MonoBehaviour
{
    public GameObject UIElements;
    public int CharacterBodyFlag;

    public Material[] BodyColor;
    public Material[] BodyOutline;
    public int CharacterHornFlag;
    public Material[] HornColor;
    public Material[] HornOutline;
    public CharacterColors ToRecolor;
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null && other.attachedRigidbody.GetComponent<CharacterColors>() != null)
        {
            // Hide the cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            ToRecolor = other.attachedRigidbody.GetComponent<CharacterColors>();

            UIElements.SetActive(true);
            ToRecolor.GetComponentInChildren<CameraMover>().MoveToOtherView();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody != null && other.attachedRigidbody.GetComponent<CharacterColors>() != null)
        {
            // Hide the cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
            UIElements.SetActive(false);
        }
        ToRecolor.GetComponentInChildren<CameraMover>().MoveToDefaultLocation();
    }
    public void ChangeBodyColor(int Up)
    {
        CharacterBodyFlag += Up;
        if (CharacterBodyFlag > BodyColor.Length)
            CharacterBodyFlag = 0;
        if (CharacterBodyFlag < 0)
            CharacterBodyFlag = BodyColor.Length;
        ToRecolor.RecolorBody(BodyColor[CharacterBodyFlag]);
    }
    public void RandomBodyColor()
    {
        ToRecolor.RecolorBody(BodyColor[Random.Range(0, BodyColor.Length)]);
    }

    public void ChangeHornColor(int Up)
    {
        CharacterHornFlag += Up;
        if (CharacterHornFlag > HornColor.Length)
            CharacterHornFlag = 0;
        if (CharacterHornFlag < 0)
            CharacterHornFlag = HornColor.Length;
        ToRecolor.RecolorBody(HornColor[CharacterHornFlag]);
    }
    public void RandomHornColor()
    {
        ToRecolor.RecolorHorn(HornColor[Random.Range(0, HornColor.Length)]);
    }


}