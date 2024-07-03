using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeOnContact : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    public string InteractorPrompt => prompt;

    public bool Interact(PlayerPickUp interactor)
    {
        Debug.Log("You found a secrete room!");
        return true;
    }

}
