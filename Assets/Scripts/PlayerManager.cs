using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    ExplorerCtrl explorerCtrl;

    private bool canMove = true;
   

    public bool keyPossession = false;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        explorerCtrl = GetComponent<ExplorerCtrl>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();

        
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            explorerCtrl.HandleAllMovement();
        }

        

    }

    public void SetMovementEnabled(bool enabled)
    {
        canMove = enabled;
    }

    public void HasKey(bool enabled)
    {
        keyPossession = true;
    }

   
}
