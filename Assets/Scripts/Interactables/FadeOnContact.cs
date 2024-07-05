using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeOnContact : MonoBehaviour
{
    [SerializeField] Material mat;

    [SerializeField] AudioSource aSource;
    public float volume = 0.5f;
    

    [SerializeField] private string prompt;
    public string InteractorPrompt => prompt;

    bool hasInteracted;

    private void Start()
    {
        mat.SetColor("_BaseColor", new Color(0, 0, 0, 1));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerPickUp>(out var player))
        {
            Interact(player);
        }
    }

    public bool Interact(PlayerPickUp interactor)
    {
        if (hasInteracted) return false;
        Fade();
        return true;
    }

    public void Fade()
    {
        hasInteracted = true;
        Debug.Log("You found a secrete room!");
        mat.DOFade(0, 1);
        aSource.PlayOneShot(aSource.clip, volume);
    }

    private void OnDestroy()
    {
        mat.SetColor("_BaseColor", new Color(0,0,0,1));
    }
}
