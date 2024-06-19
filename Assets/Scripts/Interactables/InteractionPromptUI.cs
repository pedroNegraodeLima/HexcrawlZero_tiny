using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionPromptUI : MonoBehaviour
{
    private Camera cam;

    [SerializeField] private GameObject uIPanel;
    [SerializeField] private TextMeshProUGUI proptText;

    public bool isDisplayed = false;

    void Start()
    {
        cam = Camera.main;
        uIPanel.SetActive(false);
    }

    void LateUpdate()
    {
        //bilboard da camera
        var rotation = cam.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }

    public void SetUp(string displayText)
    {
        proptText.text = displayText;
        uIPanel.SetActive(true);
    }

    public void CloseUI()
    {
        uIPanel.SetActive(false);
        isDisplayed = false;
    }
}
