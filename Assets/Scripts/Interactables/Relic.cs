using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Relic : MonoBehaviour, IInteractable
{
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

            CameraEffects.ToggleZoom(true, 1);
            
            TriggerDialogue();

            ableToInteract = false;

        }
        else
        {
            playerAnimator.SetBool("isKneeling", false);
            playerAnimator.SetBool("isGettingUp", true);

            StartCoroutine(WaitForAnimationFinished());

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

    public IEnumerator WaitForAnimationFinished()
    {
        yield return new WaitUntil(delegate { return playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Explorer_RIG_idle"); });

        CameraEffects.ToggleZoom(false, 1);

        playerManager.SetMovementEnabled(true);
    }

}
