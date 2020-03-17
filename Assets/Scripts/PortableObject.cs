using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortableObject : MonoBehaviour
{
    private GameObject cloneObject;
    private Rigidbody rigidbody;
    private Collider collider;

    // Start is called before the first frame update
    private void Awake()
    {
        cloneObject = new GameObject();
        cloneObject.SetActive(false);
        var meshFilter = cloneObject.AddComponent<MeshFilter>();
        var meshRenderer = cloneObject.AddComponent<MeshRenderer>();

        meshFilter.mesh = GetComponent<MeshFilter>().mesh;
        meshRenderer.material = GetComponent<MeshRenderer>().material;

        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();

    }


    private void LateUpdate()
    {
                
    }
}
