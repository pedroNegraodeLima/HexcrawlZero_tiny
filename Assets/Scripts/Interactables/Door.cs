using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    public string InteractorPrompt => prompt;
    public bool Interact(PlayerPickUp interactor)
    {
        var keyPossession = interactor.GetComponent<HasKey>();

        if (keyPossession == null) return false;

        if (keyPossession.hasKey)
        {
            Debug.Log("Opening Door");
            return true;
        }

        Debug.Log("No key Found");
        return false;
    }

}
