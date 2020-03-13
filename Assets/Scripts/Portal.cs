using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using RenderPipeline = UnityEngine.Rendering.RenderPipeline;
using RenderPipeline = UnityEngine.Rendering.RenderPipeline;

public class Portal : MonoBehaviour
{
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
        renderer = GetComponentInChildren<Renderer>();
        material = renderer.material;
    }


    public void SetTexture(Texture texture)
    {
        material.mainTexture = texture;
    }
}
