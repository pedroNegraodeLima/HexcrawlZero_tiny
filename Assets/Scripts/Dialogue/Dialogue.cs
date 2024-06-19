using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    public List<SentenceData> sentences = new List<SentenceData>();

    [System.Serializable]
    public class SentenceData
    {
        [TextArea(3, 10)]
        public string texts = "insert text";
        public bool isPressButtonToClose = true;
        [Tooltip("Ignore if isPressButtonToClose")]
        public float duration = 3f;
    }

}
