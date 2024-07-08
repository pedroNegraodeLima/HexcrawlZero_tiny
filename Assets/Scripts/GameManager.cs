using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static int inspectedRelicCount;
    public static int totalRelics = 8;

    public Image backdrop;

    // Start is called before the first frame update
    void Start()
    {
        inspectedRelicCount = 0;

        DialogueManager.OnDialogueFinish += AllCollectedSpeech;

    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log($"ainda tem por a�: {inspectedRelicCount-totalRelics} coletaveis.");
        } //s� serve pra dar debug na quantidade dew colet�veis 

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log($"Force Collect All");
            inspectedRelicCount = totalRelics;
        } //s� serve pra dar debug na quantidade dew colet�veis 
    }

    void OnDisable()
    {
        DialogueManager.OnDialogueFinish -= AllCollectedSpeech;
    }

    public void AllCollectedSpeech(Dialogue d)
    {
        if (inspectedRelicCount >= totalRelics)
        {
            DOVirtual.Float(0, 1, 4f, null).OnComplete(() =>
            {
                Debug.Log("Eu coletei tudo e falei tudo tamb�m, agora eu vou enviar minha ultima mensagem de amor");
                FindObjectOfType<FinalSpeech>()?.PlayFinalSpeech();
            });
        }
    }

    public void ExitDungeon()
    {
        backdrop.DOFade(1,3f).OnComplete(()=>{
            SceneManager.LoadScene(2);
        });
    }
}

