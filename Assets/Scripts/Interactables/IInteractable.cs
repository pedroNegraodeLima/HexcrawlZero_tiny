using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
  public string InteractorPrompt { get; }

    public bool Interact(PlayerPickUp interactor);

}
