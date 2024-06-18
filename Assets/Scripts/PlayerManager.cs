using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    ExplorerCtrl explorerCtrl;

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
        explorerCtrl.HandleAllMovement();
    }
}
