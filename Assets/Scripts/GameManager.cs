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

    public GameObject quitMenu;

    
    void Start()
    {
        quitMenu.SetActive(false);

        inspectedRelicCount = 0;

        DialogueManager.OnDialogueFinish += AllCollectedSpeech;

    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log($"ainda tem por aí: {inspectedRelicCount-totalRelics} coletaveis.");
        } //só serve pra dar debug na quantidade dew coletáveis 

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log($"Force Collect All");
            inspectedRelicCount = totalRelics;
        } //só serve pra dar debug na quantidade dew coletáveis 

        ExitMenu();
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
                Debug.Log("Eu coletei tudo e falei tudo também, agora eu vou enviar minha ultima mensagem de amor");
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


    public void ExitMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitMenu.SetActive(true);
        }
        
    }

    public void ExitAnytime()
    {
        Application.Quit();
        
    }
}

