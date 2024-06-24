using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollows : MonoBehaviour
{
    public Transform target;

    public Vector3 offset = new Vector3(0, 15, -10);

    private void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
