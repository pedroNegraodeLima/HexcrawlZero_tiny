using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GizmosColor))]
public class DialogueTrigger : MonoBehaviour
{
    public TriggerType triggerType;
    public Dialogue dialogue;

    private bool isInsideRange;


    private InputManager inputManager;

    public enum TriggerType
    {
        TriggerOnEnter,
        TriggerOnInteraction
    }

    private void Awake()
    {
        if( gameObject.TryGetComponent<Collider>( out var collider)){
            if (!collider.isTrigger) collider.isTrigger = true;
        }
        else
        {
            Debug.LogError($"Missing Collider at {gameObject.name}", gameObject);
        }

        inputManager = FindObjectOfType<InputManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        isInsideRange = true;
        if (triggerType != TriggerType.TriggerOnEnter) return;
        TriggerDialogue();
    }

    private void OnTriggerExit(Collider other)
    {
        isInsideRange = false;
    }

    private void Update()
    {
        if (triggerType == TriggerType.TriggerOnInteraction)
        {
            if (inputManager.interactButton)
            {
                TriggerDialogue();
            }
        }
    }

    private void TriggerDialogue()
    {
        var dialogueManager = DialogueManager.Get();

        if (!dialogueManager.canDialogue) return;

        dialogueManager.StartDialogue(dialogue);
        Destroy(gameObject);
    }
}
