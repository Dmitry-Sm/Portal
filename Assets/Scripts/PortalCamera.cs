using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    [SerializeField]
    private Portal[] portals = new Portal[2];
    [SerializeField]
    private Camera portalCamera;

    private Transform player;
    private RenderTexture tempTexture1;
    private RenderTexture tempTexture2;
    private static int iterations = 4;
    // private Transform[] cameraTransforms = new Transform[iterations];


    private void Awake() 
    {
        player = GetComponent<Camera>().transform;
        tempTexture1 = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
        tempTexture2 = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);   
    }


    private void Start()
    {
        portals[0].SetTexture(tempTexture1);
        portals[1].SetTexture(tempTexture2);
    }


    private void OnPreRender() {
        portalCamera.targetTexture = tempTexture1;
        RecursiveRenderPortal(portals[0], portals[1]);
        portalCamera.targetTexture = tempTexture2;
        RecursiveRenderPortal(portals[1], portals[0]);
    }


    void RecursiveRenderPortal(Portal inPortal, Portal outPortal)
    {
        // Transform[] cams = new Transform[iterations];
        Transform prev = portalCamera.transform;
        prev.SetPositionAndRotation(player.position, player.rotation);

        Vector3[] pos = new Vector3[iterations];
        Quaternion[] rot = new Quaternion[iterations];


        for (int i = iterations - 1; i >= 0 ; i--)
        {
            prev = RenderPortal(inPortal, outPortal, prev);
            pos[i] = prev.position;
            rot[i] = prev.rotation;
            // cams[i].SetPositionAndRotation(prev.position, prev.rotation);
        }
        for (int i = 0; i < iterations; i++)
        {
            float dist = Vector3.Magnitude(pos[i] - outPortal.transform.position);
            portalCamera.nearClipPlane = 1f + dist;
            portalCamera.transform.SetPositionAndRotation(pos[i], rot[i]);
            portalCamera.Render();
        }
    }
    
    Transform RenderPortal(Portal inPortal, Portal outPortal, Transform looker)
    {
        Transform inTransform = inPortal.transform;
        Transform outTransform = outPortal.transform;
        Transform cameraTransform = looker;
        
        Vector3 relativePosition = inTransform.InverseTransformPoint(looker.position);
        relativePosition = Vector3.Scale(relativePosition, new Vector3(-1, 1, -1));
        cameraTransform.position = outTransform.TransformPoint(relativePosition);

        Vector3 relativeRotation = inTransform.InverseTransformDirection(looker.forward);
        relativeRotation = Vector3.Scale(relativeRotation, new Vector3(-1, 1, -1));
        cameraTransform.forward = outTransform.TransformDirection(relativeRotation);

        return cameraTransform;
    }
}
