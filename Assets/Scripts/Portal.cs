using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using RenderPipeline = UnityEngine.Rendering.RenderPipeline;
using RenderPipeline = UnityEngine.Rendering.RenderPipeline;

public class Portal : MonoBehaviour
{
    public new Camera camera;
    [SerializeField]
    private Portal otherPortal;
    [SerializeField]
    private Color portalColour;
    // public Camera portalCamera;
    [SerializeField]
    private GameObject mesh;

    private Material material;
    private new Renderer renderer;


    private void Awake() {
        renderer = mesh.GetComponent<Renderer>();
        material = renderer.material;
    }

    void Start()
    {
        // portalCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        // view.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = portalCamera.targetTexture;
    }

    void Update()
    {
        // RenderPortal(portalCamera, otherPortal, playerCamera);

    }

    public void SetTexture(Texture texture)
    {
        material.mainTexture = texture;
    }


}
