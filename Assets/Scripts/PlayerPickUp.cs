using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private float pickupRange;
    [SerializeField] private Transform interactionPoint;
    [SerializeField] LayerMask pickupLayer;
    
    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int numFound;

    public static System.Action<InspectStuff> OnScanCollectible;
    public static System.Action<InspectStuff> OnPickUpCollectible;

    bool tryPick;

    private void Start()
    {
     
    }
    // Update is called once per frame
    private void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, pickupRange, colliders, pickupLayer);

        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Apertou o Ezim");
            tryPick = true;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, pickupRange);
    }

    private void FixedUpdate()
    {
        if (numFound > 0)
        {
            var interactable = colliders[0].GetComponent<InspectStuff>();
            if (interactable != null)
                //Debug.Log("soltou rainho em: " + scanHit.transform.name);
                OnScanCollectible?.Invoke(interactable);
        }
        else
        {
            OnScanCollectible?.Invoke(null);
        }

        if (tryPick)
            TryPickUp();
    }


    private void TryPickUp()
    {
        if (numFound > 0)
        {
            var interactable = colliders[0].GetComponent<InspectStuff>();
            if (interactable != null)
                OnPickUpCollectible?.Invoke(interactable);
                Debug.Log("TryPickUp rolou");
        }
        tryPick = false;
    }

}
