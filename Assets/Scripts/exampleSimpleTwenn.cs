using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class exampleSimpleTwenn : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        DOTween.SetTweensCapacity(3000, 200);
    }

    // Update is called once per frame
    void Update()
    {
        transform.DOMove(new Vector3(2, 2, -10), 2).SetEase(Ease.OutQuint).SetLoops(4, LoopType.Yoyo);
    }
}
