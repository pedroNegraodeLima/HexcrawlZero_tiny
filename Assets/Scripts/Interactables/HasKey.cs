using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasKey : MonoBehaviour
{
    InputManager inputManager;

    public bool hasKey = false;

    private void Awake()
    {
       // inputManager = GetComponent<InputManager>();
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.M) != true) hasKey = !hasKey; //isso � um cheat pra pegar a chave apertando o 'P'. A forma correta de peg�-la tem de ser implementada.

    }
}
