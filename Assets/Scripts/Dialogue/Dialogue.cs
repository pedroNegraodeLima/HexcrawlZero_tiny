using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    public string descriptor;

    [TextArea(3, 10)]
    public List<string> texts;
    public List<float> durations;

    public Dialogue(Dialogue source)
    {
        this.descriptor = source.descriptor;
        this.texts = new List<string>( source.texts);
        this.durations = new List<float>( source.durations);
    }

    public List<Sentences> GetMonologue()
    {
        var monologue = new List<Sentences>();

        if (durations == null) durations = new List<float>();

        if (durations.Count < texts.Count)
        {
            for (int i = 0; i < texts.Count; i++)
            {
                durations.Add(3);
            }
        }
        for (int i = 0; i < texts.Count; i++)
        {
            monologue.Add(new Sentences() { text = texts[i],duration= durations[i]});
        }

        return monologue;
    }
}
