using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    double countCollectables;
    bool hasCollectAll;


    public static Action AllItemsCollected;
    // Start is called before the first frame update
    void Start()
    {
        hasCollectAll = false;

        countCollectables = FindObjectsOfType<InspectStuff>().Length;
        Debug.Log($"encontrei: {countCollectables} coletaveis.");

        PlayerPickUp.OnPickUpCollectible += CountingTheCollectables;
        DialogueManager.OnDialogueFinish += AllCollectedSpeech;
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log($"ainda tem por aí: {countCollectables} coletaveis.");
        } //só serve pra dar debug na quantidade dew coletáveis 

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log($"Force Collect All");
            countCollectables = 0;
            CountingTheCollectables(null);
        } //só serve pra dar debug na quantidade dew coletáveis 
    }
    void OnDisable()
    {
        PlayerPickUp.OnPickUpCollectible -= CountingTheCollectables;
        DialogueManager.OnDialogueFinish -= AllCollectedSpeech;
    }

    private void CountingTheCollectables(InspectStuff item)
    {
        countCollectables--;
        if (countCollectables <= 0)
        {
            Debug.Log("coletei tudo!");
            hasCollectAll = true;
        }
    }
    public void AllCollectedSpeech()
    {
        if (hasCollectAll)
        {
            Debug.Log("Eu coletei tudo e falei tudo também, agora eu vou enviar minha ultima mensagem de amor");
            FindObjectOfType<FinalSpeech>()?.PlayFinalSpeech();
            AllItemsCollected?.Invoke();
            hasCollectAll = false;
        }
    }
}

