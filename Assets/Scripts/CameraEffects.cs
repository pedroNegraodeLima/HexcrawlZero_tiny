using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CameraEffects : MonoBehaviour
{
    public static CameraEffects Instance;

    private void Awake() => Instance = this;

    private void OnShake(float duration, float strenght)
    {
        transform.DOShakePosition(duration, strenght);
        transform.DOShakeRotation(duration, strenght);

    }

    private void OnZoom(float duration)
    {
        transform.DOMove(new Vector3(0, 10, -20), duration).SetEase(Ease.OutCubic);

    }

    public static void Shake(float duration, float strenght) => Instance.OnShake(duration, strenght);

    public static void Zoom(float duration) => Instance.OnZoom(duration);
}
