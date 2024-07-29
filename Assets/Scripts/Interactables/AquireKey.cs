using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class AquireKey : MonoBehaviour, IInteractable
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] Animator playerAnimator;
    Animator animator;

    Renderer _selection;
    [SerializeField] Material sphereMaterials;

    [SerializeField] private string prompt;
    public string InteractorPrompt => prompt;

    public bool CanInteract => ableToInteract;

    public Dialogue dialogue;

    public Color baseColor;
    public Color32 emissiveColor;

    bool ableToInteract = true;
    bool animationEnded = false;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        sphereMaterials.SetColor("_BaseColor", baseColor);
        sphereMaterials.SetColor("_EmissionColor", emissiveColor);

        DialogueManager.OnDialogueFinish += (d) => EndInteraction(d);
    }

    public void Finish()
    {
        animationEnded = true;
    }

    public bool Interact(PlayerPickUp interactor)
    {
        Debug.Log("This is hwo to aquire the key!");

        if (ableToInteract)
        {
            ableToInteract = false;
            playerManager.SetMovementEnabled(false);
            playerAnimator.SetTrigger("Kneel");

            CameraEffects.ToggleZoom(true, 1);
            
            TriggerDialogue();

            playerManager.HasKey(true);
           
            animator.Play("Base Layer.SphereAction");

            sphereMaterials.DOColor(Color.cyan, "_EmissionColor", 3);
            sphereMaterials.DOColor(Color.cyan, "_BaseColor", 3);


            GameManager.inspectedRelicCount++;

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

        StartCoroutine(EndInteractionRoutine());
        
    }
    IEnumerator EndInteractionRoutine()
    {
        yield return new WaitUntil(()=>animationEnded);
        Debug.Log("EndInteraction");
        DOVirtual.DelayedCall(1, () =>
        {
            playerAnimator.SetTrigger("StandUp");
            CameraEffects.ToggleZoom(false, 1);
            DOVirtual.DelayedCall(1.5f, () =>
            {
                playerManager.SetMovementEnabled(true);
            });
        });
    }

    //public IEnumerator WaitForAnimationFinished()
    //{
    //    int LayerNonInteractable = LayerMask.NameToLayer("GROUND");

    //    yield return new WaitUntil(delegate { return playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.SphereAction"); });

    //    CameraEffects.ToggleZoom(false, 1);

    //    playerManager.SetMovementEnabled(true);

    //    gameObject.layer = LayerNonInteractable;
    //}

    private void OnDestroy()
    {
        sphereMaterials.SetColor("_BaseColor", baseColor);
        sphereMaterials.SetColor("_EmissionColor", emissiveColor);
    }

}
