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
            Debug.Log($"ainda tem por a�: {countCollectables} coletaveis.");
        } //s� serve pra dar debug na quantidade dew colet�veis 

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log($"Force Collect All");
            countCollectables = 0;
            CountingTheCollectables(null);
        } //s� serve pra dar debug na quantidade dew colet�veis 
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
            Debug.Log("Eu coletei tudo e falei tudo tamb�m, agora eu vou enviar minha ultima mensagem de amor");
            FindObjectOfType<FinalSpeech>()?.PlayFinalSpeech();
            AllItemsCollected?.Invoke();
            hasCollectAll = false;
        }
    }
}

