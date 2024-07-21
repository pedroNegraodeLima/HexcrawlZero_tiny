using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BotArmRelic : MonoBehaviour, IInteractable
{
    [SerializeField] PlayerManager playerManager;
    Animator playerAnimator;

    [SerializeField] GameObject baterry;
    [SerializeField] GameObject torsoBone;

    [SerializeField] private string prompt;
    public string InteractorPrompt => prompt;

    public bool CanInteract => ableToInteract;

    public Dialogue dialogue;

    bool ableToInteract = true;

    private void Awake()
    {
        GameObject playerChar = GameObject.Find("char_newAttempt");

        playerAnimator = playerChar.GetComponent<Animator>();
        DialogueManager.OnDialogueFinish += (d) => EndInteraction(d);

    }
    public bool Interact(PlayerPickUp interactor)
    {
        Debug.Log("This is a Relic!");

        if (ableToInteract)
        {
            playerManager.SetMovementEnabled(false);
            playerAnimator.SetTrigger("Kneel");

            CameraEffects.ToggleZoom(true, 1);

            TriggerDialogue();

            ableToInteract = false;
            GameManager.inspectedRelicCount++;
        }
        else
        {
            //playerAnimator.SetTrigger("StandUp");

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
        baterry.transform.parent = torsoBone.transform;
        baterry.transform.SetLocalPositionAndRotation(new Vector3(0, 0.00183f, -0.0031f), Quaternion.Euler(7, 0, 0));

        baterry.layer = LayerMask.NameToLayer("Player");
        DOVirtual.DelayedCall(1,() => 
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

    //    baterry.transform.parent = torsoBone.transform;
    //    baterry.transform.SetLocalPositionAndRotation(new Vector3 (0, 0.00183f, -0.0031f), Quaternion.Euler( 7, 0, 0));

    //    playerAnimator.SetTrigger("Kneel");

    //    gameObject.layer = LayerNonInteractable;

    //    baterry.layer = LayerMask.NameToLayer("Player");
    //}
}
