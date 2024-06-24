using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CameraEffects : MonoBehaviour
{
    public static CameraEffects Instance;

    public Vector3 defaultOffset;
    public Vector3 zoomInOffset;

    public bool isZoomIn;

    private void Awake() => Instance = this;

    private void OnShake(float duration, float strenght, System.Action callback = null)
    {
        transform.DOShakePosition(duration, strenght);
        transform.DOShakeRotation(duration, strenght).OnComplete(() => callback?.Invoke());

    }

    private void OnZoom(bool zoomIn, float duration, System.Action callback)
    {
        Debug.Log("zoom");
        if (isZoomIn == zoomIn) return;

        isZoomIn = zoomIn;
        transform.DOLocalMove(isZoomIn ? zoomInOffset : defaultOffset, duration).SetEase(Ease.OutCubic).OnComplete(() => callback?.Invoke());

    }

    public static void Shake(float duration, float strenght, System.Action callback = null) => Instance.OnShake(duration, strenght, callback);

    public static void ToggleZoom(bool zoomIn, float duration, System.Action callback = null) => Instance.OnZoom(zoomIn, duration, callback);
}
