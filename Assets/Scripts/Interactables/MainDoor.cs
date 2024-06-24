using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDoor : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;

    [SerializeField] private string prompt;
    public string InteractorPrompt => prompt;

    public Dialogue dialogue;

    bool ableToInteract = true;

    public bool Interact(PlayerPickUp interactor)
    {
        Debug.Log("Main door is happening!");

        if (ableToInteract)
        {
            playerManager.SetMovementEnabled(false);

            CameraEffects.Shake(3, 1);
            TriggerDialogue();


            ableToInteract = false;
        }
        return true;
    }

    private void TriggerDialogue()
    {
        var dialogueManager = DialogueManager.Get();

        if (!dialogueManager.canDialogue) return;

        dialogueManager.StartDialogue(dialogue);

    }
}
