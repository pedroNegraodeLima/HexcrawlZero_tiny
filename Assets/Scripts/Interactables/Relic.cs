using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    public string InteractorPrompt => prompt;
    public bool Interact(PlayerPickUp interactor)
    {
        Debug.Log("This is a Relic!");
        return true;
    }

}
