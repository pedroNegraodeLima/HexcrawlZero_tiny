using TMPro;
using UnityEngine;

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

        DialogueManager.OnDialogueStart += (dialogue) => CloseUI();
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
        isDisplayed = true;
    }

    public void CloseUI()
    {
        uIPanel.SetActive(false);
        isDisplayed = false;
    }
}
