using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    public string InteractorPrompt => prompt;

    public Dialogue dialogue;
    public bool Interact(PlayerPickUp interactor)
    {
        Debug.Log("This is a Relic!");

        TriggerDialogue();

        return true;
    }

    private void TriggerDialogue()
    {
        var dialogueManager = DialogueManager.Get();

        if (!dialogueManager.canDialogue) return;

        dialogueManager.StartDialogue(dialogue);
        
    }
}
