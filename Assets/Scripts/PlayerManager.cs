using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    ExplorerCtrl explorerCtrl;
    public Animator animator;

    private bool canMove = true;
   

    public bool keyPossession = false;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        explorerCtrl = GetComponent<ExplorerCtrl>();
        animator = GetComponentInChildren<Animator>();
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
            if (inputManager.directionalInput != new Vector2(0, 0))
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        

    }

    public void SetMovementEnabled(bool enabled)
    {
        Debug.Log("SetMovementEnabled " + enabled);
        canMove = enabled;
        if(enabled==false) animator.SetBool("isWalking", false);
    }

    public void HasKey(bool enabled)
    {
        keyPossession = true;
    }

   
}
