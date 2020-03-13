using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    public new Camera camera;
    public GameObject p1;
    public GameObject p2;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            GameObject p;
            if (Input.GetMouseButtonDown(0)) 
            {
                p = p1;
            }
            else 
            {
                p = p2;
            }

            if (Physics.Raycast(transform.position, camera.transform.forward, out hit) 
                && hit.collider.gameObject.layer == 10)
            {
                p.transform.rotation = Quaternion.LookRotation(hit.normal);
                p.transform.position = hit.point + 0.4f * hit.normal;
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            }
        }
    }
}
