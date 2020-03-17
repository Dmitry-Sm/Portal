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

    private static readonly Quaternion halfTurn = Quaternion.Euler(0.0f, 180.0f, 0.0f);
    private Transform playerCamera;

    void Start()
    {
        playerCamera = player.GetComponentInChildren<Camera>().transform;
    }


    private void OnTriggerStay(Collider other)
    {
        float zPos = transform.worldToLocalMatrix.MultiplyPoint3x4(other.transform.position).z;

        if (zPos < 0) 
        {
            Teleport(other.transform);
        }
    }

    private void Teleport(Transform obj)
    {
        Transform inTransform = transform;
        Transform outTransform = otherPortal;
        // // Position
        Vector3 relativePosition = inTransform.InverseTransformPoint(obj.position);
        relativePosition = halfTurn * relativePosition;
        relativePosition = outTransform.TransformPoint(relativePosition);

        if (obj.name == "Player") 
        {
            player.controller.enabled = false;
            Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * playerCamera.rotation;
            relativeRot = outTransform.rotation * halfTurn * relativeRot;

            player.transform.position = relativePosition;
            Vector3 a = relativeRot.eulerAngles;
            
            player.transform.rotation = Quaternion.Euler(new Vector3(0f, a.y, 0f));
            playerCamera.localEulerAngles = new Vector3(a.x, 0f, 0f);
            player.controller.enabled = true;
        }
        else
        {
            Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * obj.rotation;
            relativeRot = outTransform.rotation * halfTurn * relativeRot;
            obj.position = relativePosition;
            obj.rotation = relativeRot;
        }
    }
}
