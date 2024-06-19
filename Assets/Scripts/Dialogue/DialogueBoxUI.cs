using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DialogueBoxUI : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public CanvasGroup canvasGroup;

    private void Start()
    {
        dialogueText.text = "";
        canvasGroup.DOFade(0, 0f);
    }

    public void SetText(string text, bool fade=false)
    {
        if (fade)
        {
            dialogueText.DOFade(0, 0.33f).OnComplete(() =>
            {
                dialogueText.text = text;
                dialogueText.DOFade(1, 0.33f);
            });
        }
        else
        {
            dialogueText.text = text;
        }
    }

    public void ToggleText(bool show)
    {
        dialogueText.alpha = show ? 1 : 0;
    }

    public void ToggleDialogueBox(bool show)
    {
        if (show)
        {
            dialogueText.text = "";
            canvasGroup.DOFade(1, 0.33f);
        }
        else { 
            canvasGroup.DOFade(0, 0.33f).OnComplete(() => dialogueText.text = "");
        }
    }
}
