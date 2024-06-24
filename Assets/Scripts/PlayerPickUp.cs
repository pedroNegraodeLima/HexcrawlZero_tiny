using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickUp : MonoBehaviour
{
    InputManager inputManager;

    [SerializeField] private float pickupRange;
    [SerializeField] private Transform interactionPoint;
    [SerializeField] LayerMask pickupLayer;
    
    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int numFound;

    public static System.Action<InspectStuff> OnScanCollectible;
    public static System.Action<InspectStuff> OnPickUpCollectible;

    private IInteractable interactable;

    [SerializeField] private InteractionPromptUI interactionPromptUI;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
    }
    // Update is called once per frame
    private void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, pickupRange, colliders, pickupLayer);

        if (numFound > 0)
        {
            interactable = colliders[0].GetComponent<IInteractable>();

            if (interactable != null) 
            {
                if (!interactionPromptUI.isDisplayed) interactionPromptUI.SetUp(interactable.InteractorPrompt);

                if (inputManager.confirmButton) interactable.Interact(this);
            }
        }
        else
        {
            if (interactable != null) interactable = null;
            if (interactionPromptUI.isDisplayed) interactionPromptUI.CloseUI();
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, pickupRange);
    }

    

}
