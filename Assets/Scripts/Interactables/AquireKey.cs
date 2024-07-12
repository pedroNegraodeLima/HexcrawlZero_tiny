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

    public Dialogue dialogue;

    public Color baseColor;
    public Color32 emissiveColor;

    bool ableToInteract = true;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        sphereMaterials.SetColor("_BaseColor", baseColor);
        sphereMaterials.SetColor("_EmissionColor", emissiveColor);
    }

    public void Finish()
    {
        CameraEffects.ToggleZoom(false, 1);
        playerManager.SetMovementEnabled(true);
    }

    public bool Interact(PlayerPickUp interactor)
    {
        Debug.Log("This is hwo to aquire the key!");

        if (ableToInteract)
        {
            playerManager.SetMovementEnabled(false);

            playerAnimator.SetBool("isWalking", false);
            
            CameraEffects.ToggleZoom(true, 1);
            
            TriggerDialogue();

            playerManager.HasKey(true);
           
            animator.Play("Base Layer.SphereAction");

            sphereMaterials.DOColor(Color.cyan, "_EmissionColor", 3);
            sphereMaterials.DOColor(Color.cyan, "_BaseColor", 3);

            ableToInteract = false;

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

    public IEnumerator WaitForAnimationFinished()
    {
        yield return new WaitUntil(delegate { return playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.SphereAction"); });

        CameraEffects.ToggleZoom(false, 1);

        playerManager.SetMovementEnabled(true);
    }

    private void OnDestroy()
    {
        sphereMaterials.SetColor("_BaseColor", baseColor);
        sphereMaterials.SetColor("_EmissionColor", emissiveColor);
    }

}
