using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;
    public static System.Action<Dialogue> OnDialogueStart;
    public static System.Action<Dialogue> OnDialogueFinish;

    public Text nameText;
    public TMP_Text dialogueText;

    private Dialogue currentDialogue;
    private DialogueBoxUI dialogueBoxUI;
    [HideInInspector]
    public bool canDialogue;

    private InputManager inputManager;

    public static DialogueManager Get()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
        canDialogue = true;
        inputManager = FindObjectOfType<InputManager>();
        dialogueBoxUI = FindObjectOfType<DialogueBoxUI>();
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
        dialogueBoxUI.ToggleDialogueBox(true);

        OnDialogueStart?.Invoke(dialogue);
        StartCoroutine( DisplayNextSentence());
    }

    public IEnumerator DisplayNextSentence()
    {
        if (currentDialogue == null) yield break;

        for (int i = 0; i < currentDialogue.sentences.Count; i++)
        {
            var sentence = currentDialogue.sentences[i];
            Debug.Log(sentence.texts);
            //dialogueBoxUI.SetText(sentence.texts);
            dialogueBoxUI.ToggleText(true);
            yield return StartCoroutine(TypeSentence(sentence.isPressButtonToClose ? sentence.texts + "\n(click to continue)" : sentence.texts));

            if (sentence.isPressButtonToClose)
            {
                Debug.Log("waitig");
                yield return new WaitUntil(()=> inputManager.confirmButton);
            }
            else
            {
                yield return new WaitForSeconds(sentence.duration);
            }
        }

        EndDialogue();
    }

    IEnumerator TypeSentence (string sentence) //Isso aqui faz escrever letra por letra
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void EndDialogue()
    {
        Debug.Log("End of conversation.");
        dialogueBoxUI.ToggleDialogueBox(false);
        OnDialogueFinish?.Invoke(currentDialogue);
        currentDialogue = null;
        canDialogue = true;

    }


    
    private void OnPickUpCollectible(InspectStuff obj)
    {
        StartDialogue(obj.dialogue);
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
