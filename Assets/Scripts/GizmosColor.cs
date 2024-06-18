using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosColor : MonoBehaviour
{
    public Color _mycolor;

    private void OnDrawGizmos()
    {
        Gizmos.color = _mycolor;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
    }
}
