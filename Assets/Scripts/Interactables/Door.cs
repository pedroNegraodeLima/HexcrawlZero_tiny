using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    public string InteractorPrompt => prompt;
    public bool Interact(PlayerPickUp interactor)
    {
        Debug.Log("Opening Door");
        return true;
    }

}
