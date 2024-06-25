using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] Animator playerAnimator;

    [SerializeField] private string prompt;
    public string InteractorPrompt => prompt;

    public Dialogue dialogue;

    bool ableToInteract = true;

    private void Start()
    {
        DialogueManager.OnDialogueFinish += Door_OnDialogueFinish;
    }

    private void Door_OnDialogueFinish(Dialogue d)
    {
        if (d != dialogue) return;

        CameraEffects.ToggleZoom(false, 1);
        CameraEffects.DoRotation(false, 1);
        DialogueManager.OnDialogueFinish -= Door_OnDialogueFinish;
        playerManager.SetMovementEnabled(true);

        DOVirtual.DelayedCall(1, () => playerManager.SetMovementEnabled(true));
    }

    public bool Interact(PlayerPickUp interactor)
    {
        var keyPossession = interactor.GetComponent<HasKey>();

        if (keyPossession == null) return false;

        if (keyPossession.hasKey)
        {
            Debug.Log("Opening Door");

            if (ableToInteract)
            {
                ableToInteract = false;
                playerManager.SetMovementEnabled(false);
                playerAnimator.SetBool("isWalking", false);

                CameraEffects.ToggleZoom(true, 1, () => {
                    transform.DOLocalMoveY(-5, 1).SetEase(Ease.InOutBounce);
                    TriggerDialogue();
                });
                CameraEffects.DoRotation(true, 1);

            }
            return true;
        }

        Debug.Log("No key Found");
        return false;
    }

    private void TriggerDialogue()
    {
        var dialogueManager = DialogueManager.Get();

        if (!dialogueManager.canDialogue) return;

        dialogueManager.StartDialogue(dialogue);

    }

}
