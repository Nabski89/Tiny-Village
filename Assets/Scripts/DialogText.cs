using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DialogueGraph.Runtime;

public class DialogText : MonoBehaviour
{
    public RuntimeDialogueGraph DialogueSystem;
    public GameObject SoundEffect;
    public TextMeshProUGUI textMesh;
    public string TEXTBOX = "Your text here";
    public int displayedCharacters = 0;

    public GameObject NPCui;
    public GameObject CharacterUI;
    public BulkManager BulkManager;
    public int PossibleResponses;
    public TextMeshProUGUI[] textMeshResponses;
    public int CurrentResponseSelection;
    private void Start()
    {
        BulkManager = GetComponentInParent<BulkManager>();
        if (textMesh == null)
        {
            Debug.LogError("TextMeshProUGUI component not found.");
            return;
        }
        UpdateSelection();
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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            CurrentResponseSelection = (CurrentResponseSelection - 1 + PossibleResponses) % PossibleResponses;
            UpdateSelection();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            CurrentResponseSelection = (CurrentResponseSelection + 1) % PossibleResponses;
            UpdateSelection();
        }
        //you can continue dialogue with e if you are already in it
        if (BulkManager.InDialogue == true)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                AdvanceDialogue();
        }
        //you can continue dialogue by hitting enter or clicking
        if (Input.GetKeyDown(KeyCode.Return))
        {
            AdvanceDialogue();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndDialogue();
        }
    }

    public void StartDialogue()
    {
        DialogueSystem.ResetConversation();
        BulkManager.InDialogue = true;
        AdvanceDialogue();
    }
    void AdvanceDialogue()
    {
        if (DialogueSystem.IsConversationDone())
        {
            EndDialogue();
        }
        var isNpc = DialogueSystem.IsCurrentNpc();
        if (isNpc)
            NonPlayer();
        else
            Player();
    }
    void NonPlayer()
    {
        NPCui.SetActive(true);
        CharacterUI.SetActive(false);
        displayedCharacters = 0;
        textMesh.text = "";
        TEXTBOX = DialogueSystem.ProgressNpc();
        CurrentResponseSelection = -1;
    }
    private List<LineEntry> entries;
    void Player()
    {
        if (CurrentResponseSelection != -1)
        {
            textMesh.text = "";
            TEXTBOX = DialogueSystem.ProgressSelf(CurrentResponseSelection);
            AdvanceDialogue();
        }
        else
        {
            NPCui.SetActive(false);
            CharacterUI.SetActive(true);

            List<ConversationLine> currentLines = DialogueSystem.GetCurrentLines();
            for (int i = 0; i < textMeshResponses.Length; i++)
            {
                if (i < currentLines.Count)
                {
                    textMeshResponses[i].text = currentLines[i].Message;
                    PossibleResponses = i + 1;
                }
                else
                    textMeshResponses[i].text = ""; // Clear text if no corresponding string in the list
            }
            CurrentResponseSelection = 0;
            UpdateSelection();
        }
    }
    public void NewText()
    {
        NPCui.SetActive(false);
        CharacterUI.SetActive(false);
        displayedCharacters = 0;
    }
    void UpdateSelection()
    {
        for (int i = 0; i < textMeshResponses.Length; i++)
        {
            if (i == CurrentResponseSelection)
            {
                textMeshResponses[i].color = Color.yellow; // Highlight selected
            }
            else
            {
                textMeshResponses[i].color = Color.black; // Default color
            }
        }
    }
    public void EndDialogue()
    {
        NPCui.SetActive(false);
        CharacterUI.SetActive(false);
        BulkManager.MoveLockdown = false;
        BulkManager.InDialogue = false;
    }












    //this is for basic conversations only, single line no responses, no interaction required you just walk over and it pops up
    public void NewChatter()
    {
        NPCui.SetActive(true);
        displayedCharacters = 0;
    }
    public void EndChatter()
    {
        NPCui.SetActive(false);
    }
}
