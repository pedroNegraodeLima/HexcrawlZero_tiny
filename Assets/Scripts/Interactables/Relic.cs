using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Relic : MonoBehaviour, IInteractable
{
    [SerializeField] PlayerManager playerManager;
    Animator playerAnimator;


    [SerializeField] private string prompt;
    public string InteractorPrompt => prompt;

    public bool CanInteract => ableToInteract;

    public Dialogue dialogue;

    bool ableToInteract = true;

    private void Awake()
    {
        GameObject playerChar = GameObject.Find("char_newAttempt");

        playerAnimator = playerChar.GetComponent<Animator>();
        DialogueManager.OnDialogueFinish += (d)=>EndInteraction(d);
    }
    public bool Interact(PlayerPickUp interactor)
    {        
        Debug.Log("This is a Relic! ableToInteract = "+ ableToInteract);

        if (ableToInteract)
        {
            ableToInteract = false;
            playerManager.SetMovementEnabled(false);
            playerAnimator.SetTrigger("Kneel");

            CameraEffects.ToggleZoom(true, 1);
            
            TriggerDialogue();

            GameManager.inspectedRelicCount++;
        }
        else
        {
            //playerAnimator.SetBool("isKneeling", false);
            //playerAnimator.SetBool("isGettingUp", true);

            //StartCoroutine(WaitForAnimationFinished());

            //ableToInteract = true;

        }

        return true;
    }

    private void TriggerDialogue()
    {
        var dialogueManager = DialogueManager.Get();

        if (!dialogueManager.canDialogue) return;

        dialogueManager.StartDialogue(dialogue);
    }

    private void EndInteraction(Dialogue d)
    {
        if (d != dialogue) return;
        Debug.Log("EndInteraction");
        DOVirtual.DelayedCall(1, () =>
        {
            playerAnimator.SetTrigger("StandUp");
            CameraEffects.ToggleZoom(false, 1);
            DOVirtual.DelayedCall(1, () =>
            {
                playerManager.SetMovementEnabled(true);
            });
        });
    }

    //public IEnumerator WaitForAnimationFinished()
    //{
    //    int LayerNonInteractable = LayerMask.NameToLayer("GROUND");

    //    yield return new WaitUntil(delegate { return playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Explorer_RIG_idle"); });

    //    CameraEffects.ToggleZoom(false, 1);

    //    playerManager.SetMovementEnabled(true);

    //    playerAnimator.SetTrigger("Kneel");

    //    gameObject.layer = LayerNonInteractable;

    //}

  

}
