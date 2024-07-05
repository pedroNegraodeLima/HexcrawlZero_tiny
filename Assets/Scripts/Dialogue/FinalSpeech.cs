using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinalSpeech : MonoBehaviour
{
    public Dialogue dialogue;
    public UnityEvent onComplete;
    internal void PlayFinalSpeech()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        onComplete?.Invoke();
        Destroy(gameObject);
    }
}
