using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainDoor : MonoBehaviour, IInteractable
{
    [SerializeField] PlayerManager playerManager;

    [SerializeField] private string prompt;
    public string InteractorPrompt => prompt;

    public bool CanInteract => ableToInteract;

    public Dialogue dialogue;

    bool ableToInteract = true;

    private void Start()
    {
        DialogueManager.OnDialogueFinish += MainDoor_OnDialogueFinish;
    }

    private void MainDoor_OnDialogueFinish(Dialogue d)
    {
        if (d != dialogue) return;

        CameraEffects.ToggleZoom(false, 1);
        DialogueManager.OnDialogueFinish -= MainDoor_OnDialogueFinish;
        playerManager.SetMovementEnabled(false);

        DOVirtual.DelayedCall(1, () => playerManager.SetMovementEnabled(true));
    }

    public bool Interact(PlayerPickUp interactor)
    {
        Debug.Log("Main door is happening!");

        if (ableToInteract)
        {
            ableToInteract = false;
            playerManager.SetMovementEnabled(false);

            playerManager.animator.SetBool("isWalking",false);

            CameraEffects.ToggleZoom(true, 1, ()=>{
                CameraEffects.Shake(3, 1);
                transform.DOLocalMoveY(-5, 1).SetEase(Ease.InOutBounce);
                TriggerDialogue();
            });
           

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
