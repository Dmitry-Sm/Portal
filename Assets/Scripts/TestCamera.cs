using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{
    private new Camera camera;
    private Vector3 pos;

    private void Awake() {
        camera = GetComponent<Camera>();
        pos = new Vector3(0, 2, 0);
    }


    void Update()
    {
        float s = Mathf.Sin(Time.time);
        float s2 = Mathf.Sin(Time.time * 1.4f);

        Vector3 norm = new Vector3(s2 * 0.2f, 0, -1);
        Vector3 point = new Vector3(0, 0, -8 +  2);
        Plane plane = new Plane(norm, point);
        Vector4 vplane = new Vector4(plane.normal.x, plane.normal.y, plane.normal.z, plane.distance);

        camera.projectionMatrix = camera.CalculateObliqueMatrix(vplane);
        pos.z = s2 * 22;
        transform.position = pos;
    }
}
