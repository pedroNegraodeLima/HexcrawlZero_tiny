using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AquireKey : MonoBehaviour, IInteractable
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] Animator playerAnimator;
    Animator animator;

    Renderer _selection;
    Material[] sphereMaterials;

    [SerializeField] private string prompt;
    public string InteractorPrompt => prompt;

    public Dialogue dialogue;

    bool ableToInteract = true;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponentInChildren<Renderer>();
            sphereMaterials = selectionRenderer.materials;
           
        }
    }

    public bool Interact(PlayerPickUp interactor)
    {
        Debug.Log("This is a Relic!");

        if (ableToInteract)
        {
            playerManager.SetMovementEnabled(false);

            playerAnimator.SetBool("isWalking", false);
            
            CameraEffects.ToggleZoom(true, 1);
            
            TriggerDialogue();

            playerManager.HasKey(true);
           
            animator.Play("Base Layer.SphereAction");

            sphereMaterials[1].DOColor(Color.cyan, "Emission Map", 1);
            sphereMaterials[1].DOColor(Color.cyan, "Base Map", 1);

            ableToInteract = false;
        }
        else
        {
            Debug.Log("I have arrived!");
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
        yield return new WaitUntil(delegate { return playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.SphereAction"); });

        CameraEffects.ToggleZoom(false, 1);

        playerManager.SetMovementEnabled(true);
    }

}
