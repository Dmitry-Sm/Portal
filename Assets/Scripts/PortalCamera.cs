using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using RenderPipeline = UnityEngine.Rendering.RenderPipelineManager;

// namespace UnityEngine.Rendering.LWRP

public class PortalCamera : MonoBehaviour
{
    [SerializeField]
    private Portal[] portals = new Portal[2];
    [SerializeField]
    private Camera portalCamera;

    private Camera mainCamera;
    private Transform player;
    private RenderTexture tempTexture1;
    private RenderTexture tempTexture2;
    private static int iterations = 2;

    // private CameraData
    private static readonly Quaternion halfTurn = Quaternion.Euler(0.0f, 180.0f, 0.0f);

    private void OnEnable() {
        RenderPipeline.beginCameraRendering += CaptureCamera;        
    }

    private void OnDisable() {
        RenderPipeline.beginCameraRendering -= CaptureCamera;
    }

    private void Awake() 
    {
        tempTexture1 = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
        tempTexture2 = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);   

        mainCamera = GetComponent<Camera>();
        player = mainCamera.transform;
    }


    private void Start()
    {
   

        portals[0].SetTexture(tempTexture1);
        portals[1].SetTexture(tempTexture2);
    }


    public void CaptureCamera(ScriptableRenderContext context, Camera camportalCameraera) {
        portalCamera.targetTexture = tempTexture1;
        RecursiveRenderPortal(portals[0], portals[1], context);
        portalCamera.targetTexture = tempTexture2;
        RecursiveRenderPortal(portals[1], portals[0], context);
    }


    void RecursiveRenderPortal(Portal inPortal, Portal outPortal, ScriptableRenderContext context)
    {
        Transform prev = portalCamera.transform;
        prev.SetPositionAndRotation(player.position, player.rotation);

        Vector3[] pos = new Vector3[iterations];
        Quaternion[] rot = new Quaternion[iterations];

        for (int i = iterations - 1; i >= 0 ; i--)
        {
            prev = RenderPortal(inPortal, outPortal, prev);
            pos[i] = prev.position;
            rot[i] = prev.rotation;
        }
        for (int i = 0; i < iterations; i++)
        {
            float dist = Vector3.Magnitude(pos[i] - outPortal.transform.position);
            portalCamera.transform.SetPositionAndRotation(pos[i], rot[i]);
            
            Vector4 clipPlane = CalculateClipPlane(portalCamera, outPortal.transform);
            portalCamera.projectionMatrix = mainCamera.CalculateObliqueMatrix(clipPlane);
            // portalCamera.Render();
            UniversalRenderPipeline.RenderSingleCamera(context, portalCamera);
        }
    }
    

    // https://qiita.com/tsgcpp/items/3816e4cf188db257df19
    private static Vector4 CalculateClipPlane(Camera camera, Transform clip_plane_transform) 
    {
        Matrix4x4 world_to_camera_matrix = camera.worldToCameraMatrix;
        
        Vector3 clip_plane_normal = world_to_camera_matrix.MultiplyVector(clip_plane_transform.forward);
        Vector3 clip_plane_arbitary_position = world_to_camera_matrix.MultiplyPoint(clip_plane_transform.position);

        Vector3 norm = camera.transform.TransformVector(clip_plane_transform.forward);
        Vector3 pos = camera.transform.TransformPoint(clip_plane_transform.position);

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
        relativePosition = halfTurn * relativePosition;
        cameraTransform.position = outTransform.TransformPoint(relativePosition);

        Quaternion relativeRotation = Quaternion.Inverse(inTransform.rotation) * looker.rotation;
        relativeRotation = outTransform.rotation * halfTurn * relativeRotation;
        cameraTransform.rotation = relativeRotation;
        
        return cameraTransform;
    }
}
