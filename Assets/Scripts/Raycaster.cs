using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    public new Camera camera;
    public GameObject p1;
    public GameObject p2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

            if (Physics.Raycast(transform.position, camera.transform.forward, out hit))
            {
                p.transform.position = hit.point;
                //p1.transform.localEulerAngles = hit.normal * 90;
                p.transform.rotation = Quaternion.LookRotation(hit.normal);
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            }
        }
    }
}
