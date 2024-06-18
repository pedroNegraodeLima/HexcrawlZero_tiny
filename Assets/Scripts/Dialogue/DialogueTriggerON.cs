using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerON : MonoBehaviour
{
    public Dialogue dialogue;

    public void OnTriggerEnter(Collider other)
    {
        var dialogueManager = FindObjectOfType<DialogueManager>();

        if (!dialogueManager.canDialogue) return;

        dialogueManager.StartDialogue(dialogue);
        Destroy(gameObject);
    }
}
