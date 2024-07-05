using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class TriggerHelper : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        DOVirtual.Float(0, 0, 3f, null).OnComplete(()=> { 
            FindObjectOfType<GameManager>().ExitDungeon();
        });
    }
}
