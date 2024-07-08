using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMainScene : MonoBehaviour
{
    [SerializeField] Button btn01;
    [SerializeField] Button btn02;

    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider slider;



    private void Start()
    {
        btn01.onClick.AddListener(StartGame);
        btn02.onClick.AddListener(QuitGame);
    }
    void StartGame()
    {
        StartCoroutine(LoadYourAsyncScene());
    }
    
    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainScene");

        loadingScreen.SetActive(true);

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            slider.value = progress;
            
            yield return null;
        }
    }

    void QuitGame()
    {
        Application.Quit();
    }

}
