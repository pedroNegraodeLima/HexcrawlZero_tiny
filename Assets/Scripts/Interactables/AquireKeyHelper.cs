using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquireKeyHelper : MonoBehaviour
{
    public AquireKey root;

    public void OnAnimFinished()
    {
        root.Finish();
    }
}
