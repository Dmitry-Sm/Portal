using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using RenderPipeline = UnityEngine.Rendering.RenderPipeline;
using RenderPipeline = UnityEngine.Rendering.RenderPipeline;

public class Portal : MonoBehaviour
{
    public Portal other;
    public Camera portalCamera;
    // public Camera playerCamera;
    public GameObject view;

    // public Texture initTexture;
    public Material crtMat;


    // [SerializeField]
    // private CustomRenderTexture crt;
    private CustomRenderTexture rt;

    int counter = 0;


    void Start()
    {
        portalCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        crtMat.SetTexture("_Tex", portalCamera.targetTexture);

        // rt = new CustomRenderTexture(Screen.width, Screen.height);
        // rt.doubleBuffered = true;
        // // // rt.enableRandomWrite = true;
        // rt.format = RenderTextureFormat.ARGB32;
        // rt.material = initMaterial;
        // rt.initializationMode = CustomRenderTextureUpdateMode.OnDemand;
        // rt.updateMode = CustomRenderTextureUpdateMode.OnDemand;
        // rt.initializationSource = CustomRenderTextureInitializationSource.TextureAndColor;
        // rt.initializationColor = Color.clear;
        // rt.initializationTexture = Texture2D.normalTexture;
        // rt.anisoLevel = 0;
        // // rt.ClearUpdateZones();
        
        // portalCamera.targetTexture = rt;
        // // print(crt);
        

        // rt.Create();
        // rt.Initialize();

        // view.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = portalCamera.targetTexture;
    }

    private void OnPostRender() {
        // rt.Update(1);

    }

    private void LateUpdate() 
    {
        // print("Late update");
    }

    // Update is called once per frame
    void Update()
    {
        // rt.Update();

        // print(rt.updateCount);
        Transform player = Camera.main.transform;
        float dist = Vector3.Magnitude(transform.position - player.position);
        portalCamera.GetComponent<Camera>().nearClipPlane = 1 + dist;

        //Quaternion angleBetweenPortals = other.transform.rotation * Quaternion.Inverse(transform.rotation);
        Vector3 relativePosition = transform.InverseTransformPoint(player.position);
        relativePosition = Vector3.Scale(relativePosition, new Vector3(-1, 1, -1));
        portalCamera.transform.position = other.transform.TransformPoint(relativePosition);

        Vector3 relativeRotation = transform.InverseTransformDirection(player.forward);
        relativeRotation = Vector3.Scale(relativeRotation, new Vector3(-1, 1, -1));
        portalCamera.transform.forward = other.transform.TransformDirection(relativeRotation);

        // Quaternion p1back = Quaternion.AngleAxis(180, transform.up);
        // Quaternion p2back = Quaternion.AngleAxis(180, other.transform.up);
        // transform.position = other.transform.position - angleBetweenPortals * p1back * (transform.position - player.transform.position);
        // transform.rotation = p2back * angleBetweenPortals * player.rotation;
    }
}
