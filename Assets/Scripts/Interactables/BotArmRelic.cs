using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotArmRelic : MonoBehaviour, IInteractable
{
    [SerializeField] PlayerManager playerManager;
    Animator playerAnimator;

    [SerializeField] GameObject baterry;
    [SerializeField] GameObject torsoBone;

    [SerializeField] private string prompt;
    public string InteractorPrompt => prompt;

    public Dialogue dialogue;

    bool ableToInteract = true;

    private void Awake()
    {
        GameObject playerChar = GameObject.Find("char_newAttempt");

        playerAnimator = playerChar.GetComponent<Animator>();

    }
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
            GameManager.inspectedRelicCount++;
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
        int LayerNonInteractable = LayerMask.NameToLayer("GROUND");

        yield return new WaitUntil(delegate { return playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Explorer_RIG_idle"); });

        CameraEffects.ToggleZoom(false, 1);

        playerManager.SetMovementEnabled(true);

        baterry.transform.parent = torsoBone.transform;
        baterry.transform.SetLocalPositionAndRotation(new Vector3 (0, 0.00183f, -0.0031f), Quaternion.Euler( 7, 0, 0));

        playerAnimator.SetBool("isGettingUp", false);

        gameObject.layer = LayerNonInteractable;
    }
}
