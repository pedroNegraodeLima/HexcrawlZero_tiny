using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCutOut : MonoBehaviour
{
    [SerializeField] Material wallMaterial;
    [SerializeField] LayerMask mask;
    Camera cam;

    public static int PosID = Shader.PropertyToID("_pPosition");
    public static int SizeID = Shader.PropertyToID("_Size");

    private void Awake()
    {
        cam = Camera.main;
    }
    void Update()
    {
        var direction = cam.transform.position - transform.position;
        var ray = new Ray(transform.position, direction.normalized);

        if(Physics.Raycast(ray, 3000, mask))
        {
            wallMaterial.SetFloat(SizeID, 1);
        }
        else
        {
            wallMaterial.SetFloat(SizeID, 0);
        }

        var view = cam.WorldToViewportPoint(transform.position);
        wallMaterial.SetVector(PosID, view);
    }
}
