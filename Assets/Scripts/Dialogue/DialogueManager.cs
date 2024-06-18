using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    public static event System.Action OnDialogueFinish;

    public Text nameText;
    public TMP_Text dialogueText;

    public Animator animator;


    private Dialogue currentDialogue;
    [HideInInspector]
    public bool canDialogue;

    private void Awake()
    {
        canDialogue = true;


        PlayerPickUp.OnPickUpCollectible += OnPickUpCollectible;
    }

    private void OnDisable()
    {
        PlayerPickUp.OnPickUpCollectible -= OnPickUpCollectible;
    }
    public void StartDialogue (Dialogue dialogue)
    {
        canDialogue = false;
        currentDialogue = dialogue;
        animator.SetBool("IsOpen", true);

        Debug.Log("Starting coversation with " + dialogue.descriptor);

        StartCoroutine( DisplayNextSentence());
    }

    public IEnumerator DisplayNextSentence()
    {
        var monologue = currentDialogue.GetMonologue();
        for (int i = 0; i < monologue.Count; i++)
        {
            var sentence = monologue[i];
            Debug.Log(sentence);
            yield return StartCoroutine(TypeSentence(sentence.text));
            yield return new WaitForSeconds(sentence.duration);
        }

        EndDialogue();
    }

    IEnumerator TypeSentence (string sentence) //Isso aqui faz escrever letra por letra
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        Debug.Log("End of conversation.");
        animator.SetBool("IsOpen", false);
        currentDialogue = null;
        canDialogue = true;

        OnDialogueFinish?.Invoke();
    }


    
    private void OnPickUpCollectible(Collectible obj)
    {
        StartDialogue(obj.dialogue);
    }


}
