using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    [SerializeField]
    private Portal[] portals = new Portal[2];
    [SerializeField]
    private Transform[] marks = new Transform[2];
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
            portalCamera.transform.SetPositionAndRotation(pos[i], rot[i]);
            
            // Vector3 norm = portalCamera.transform.TransformDirection(Vector3.Normalize(outPortal.transform.forward));
            // Vector3 point = portalCamera.transform.TransformPoint(pos[i] - outPortal.transform.position);
            // Vector3 norm = new Vector3(0, 0, -1);
            // Vector3 point = new Vector3(0, 0, -dist);
            // norm = Vector3.Scale(norm, -Vector3.one);
            // point = Vector3.Scale(point, -Vector3.one);
            // Plane plane = new Plane(norm, point);
            // Vector4 vplane = new Vector4(norm.x, norm.y, norm.z, plane.distance);

            // outPortal.transform.position += outPortal.transform.forward * 0.01f;
            Vector4 clipPlane = CalculateClipPlane(portalCamera, outPortal.transform);
            // outPortal.transform.position -= outPortal.transform.forward * 0.01f;

            if (i == iterations - 1) {
                if (inPortal.name == "Portal 1") {
                    marks[0].position = pos[i];
                }
                else {
                    marks[1].position = pos[i];
                }
            }
            portalCamera.projectionMatrix = portalCamera.CalculateObliqueMatrix(clipPlane);
            portalCamera.Render();
        }
    }
    
    // https://qiita.com/tsgcpp/items/3816e4cf188db257df19
    private static Vector4 CalculateClipPlane(Camera camera, Transform clip_plane_transform) 
    {
        Matrix4x4 world_to_camera_matrix = camera.worldToCameraMatrix;
        Vector3 clip_plane_normal = world_to_camera_matrix.MultiplyVector(clip_plane_transform.forward);
        Vector3 clip_plane_arbitary_position = world_to_camera_matrix.MultiplyPoint(clip_plane_transform.position);

        return CalculateClipPlane(clip_plane_normal, clip_plane_arbitary_position);
    }
    

    private static Vector4 CalculateClipPlane(Vector3 clip_plane_normal, Vector3 clip_plane_arbitary_position) 
    {
        float distance = -Vector3.Dot(clip_plane_normal, clip_plane_arbitary_position);
        Vector4 clip_plane = new Vector4(clip_plane_normal.x, clip_plane_normal.y, clip_plane_normal.z, distance);

        return clip_plane;
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
