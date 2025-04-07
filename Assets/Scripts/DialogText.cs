using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogText : MonoBehaviour
{
    public GameObject SoundEffect;
    public TextMeshProUGUI textMesh;
    public string TEXTBOX = "Your text here";
    public int displayedCharacters = 0;

    public Image OuterBox;
    public Image InnerBox;
    private void Start()
    {
        if (textMesh == null)
        {
            Debug.LogError("TextMeshProUGUI component not found.");
            return;
        }
    }
    void Update()
    {
        if (displayedCharacters <= TEXTBOX.Length)
        {
            textMesh.text = TEXTBOX.Substring(0, displayedCharacters);
            //speed up how much is displayed depending on how much we have to go
            if (displayedCharacters + 300 <= TEXTBOX.Length)
                displayedCharacters += 1;
            if (displayedCharacters + 200 <= TEXTBOX.Length)
                displayedCharacters += 1;
            if (displayedCharacters + 100 <= TEXTBOX.Length)
                displayedCharacters += 1;
            displayedCharacters += 1;
            //make sure we have no sound effect then talk
            if (transform.childCount == 0)
                Instantiate(SoundEffect, transform);

        }
    }
    public void NewText()
    {
        OuterBox.enabled = true;
        InnerBox.enabled = true;
        textMesh.enabled = true;
        displayedCharacters = 0;
    }
    public void EndText()
    {
        OuterBox.enabled = false;
        InnerBox.enabled = false;
        textMesh.enabled = false;
    }
}
