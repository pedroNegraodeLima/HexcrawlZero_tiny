using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeOnContact : MonoBehaviour, IInteractable
{
    [SerializeField] Material mat;

    [SerializeField] AudioSource aSource;
    public float volume = 0.5f;
    

    [SerializeField] private string prompt;
    public string InteractorPrompt => prompt;

    public bool Interact(PlayerPickUp interactor)
    {
        Debug.Log("You found a secrete room!");
        mat.DOFade(0, 1);
        aSource.PlayOneShot(aSource.clip, volume);
        return true;
    }

}
