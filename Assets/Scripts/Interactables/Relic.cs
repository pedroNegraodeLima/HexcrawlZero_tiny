using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Relic : MonoBehaviour, IInteractable
{
    [SerializeField] cameraFollows cam;
    [SerializeField] PlayerManager playerManager;
    [SerializeField] Animator playerAnimator;


    [SerializeField] private string prompt;
    public string InteractorPrompt => prompt;

    public Dialogue dialogue;

    bool ableToInteract = true;


    public bool Interact(PlayerPickUp interactor)
    {
        Debug.Log("This is a Relic!");

        if (ableToInteract) 
        {            
            playerManager.SetMovementEnabled(false);

            playerAnimator.SetBool("isWalking", false);
            playerAnimator.SetBool("isKneeling", true);

            cam.transform.DOMoveY(-5, 2);
            cam.transform.DOMoveZ(5, 2);

            TriggerDialogue();

            ableToInteract = false;

        }
        else
        {
            playerAnimator.SetBool("isKneeling", false);
            playerAnimator.SetBool("isGettingUp", true);
            playerManager.SetMovementEnabled(true);

            cam.transform.DORewind();

            ableToInteract = true;
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
