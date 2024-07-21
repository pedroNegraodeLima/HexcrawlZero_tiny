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

#if UNITY_EDITOR
        DebugCommands();
#endif
    }

    private void DebugCommands()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Time.timeScale = Time.timeScale == 1 ? 3 : 1;
        }
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
