using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSpeech : MonoBehaviour
{
    public Dialogue dialogue;

    internal void PlayFinalSpeech()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        Destroy(gameObject);
    }
}
