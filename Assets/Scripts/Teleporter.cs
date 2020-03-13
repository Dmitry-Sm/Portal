using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private Transform otherPortal;
    
    [SerializeField]
    private PlayerControl player;

    [SerializeField]
    private Transform[] marks = new Transform[2];

    private static Quaternion halfTurn = Quaternion.Euler(0.0f, 180.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        // playerCamera = player.GetComponentInChildren<Camera>().transform;
    }


    private void OnTriggerStay(Collider other)
    {
        float zPos = transform.worldToLocalMatrix.MultiplyPoint3x4(other.transform.position).z;

        if (zPos < 0) 
        {
            print("Teleport " + other.name + other.transform.position);
            Teleport(other.transform);
        }
    }

    private void Teleport(Transform obj)
    {
        // Position
        Vector3 relativePosition = transform.InverseTransformPoint(obj.position);
        relativePosition = halfTurn * relativePosition;

        // Rotation
        Quaternion difference = otherPortal.rotation * 
            Quaternion.Inverse(transform.rotation) * halfTurn;
        
        if (obj.name == "Player") 
        {
            player.controller.enabled = false;
            obj.position = otherPortal.TransformPoint(relativePosition);
            obj.rotation = difference * obj.rotation;
            player.controller.enabled = true;
        }
        else
        {
            obj.position = otherPortal.TransformPoint(relativePosition);
            obj.rotation = difference * obj.rotation;
        }

    }
    
    // private void OnTriggerEnter(Collider other)
    // {
    //     other.gameObject.layer = 11;
    // }

    // private void OnTriggerExit(Collider other)
    // {
    //     other.gameObject.layer = 9;
    // }
}
