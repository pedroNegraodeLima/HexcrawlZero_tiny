using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip intro;
    public AudioClip loop;
    public AudioSource source;

    private IEnumerator Start()
    {
        source.clip = intro;
        source.Play();

        yield return new WaitUntil(() => !source.isPlaying);

        source.clip = loop;
        source.loop = true;
        source.Play();
    }
}
