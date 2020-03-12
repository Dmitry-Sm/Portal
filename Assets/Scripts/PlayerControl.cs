using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private CharacterController controller;

    [Header("Movement settings")]
    [SerializeField]
    private float walkingSpeed = 5f;
    [SerializeField]
    private float runningSpeed = 9f;
    [Header("Rotation settings")]
    [SerializeField]
    private float mouseSensitivity = 150f;

    private Vector3 rotation = new Vector3(0f, 0f, 0f);
    private Vector3 speed = new Vector3(0f, 0f, 0f);
    private bool isRunning = false;

    void Start()
    {
        print("Start");
        Cursor.lockState = CursorLockMode.Locked;
    }

void Update()
    {
 
        Rotate();
        Move();
    }

    void Rotate() 
    {
        float raw = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float pich = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        rotation.x -= pich;
        rotation.x = Mathf.Clamp(rotation.x, -85, 85);
        cam.transform.localEulerAngles = Vector3.right * rotation.x;
        transform.eulerAngles += Vector3.up * raw;
    }

    void Move() 
    {        
        float forward =  Input.GetAxis("Vertical");
        float right =  Input.GetAxis("Horizontal");

        Vector3 move = new Vector3(0f, 0f, 0f);
        float moveSpeed = Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift) && controller.isGrounded)
        {
            moveSpeed *= runningSpeed;
            isRunning = true;
        }
        else
        {
            moveSpeed *= walkingSpeed;
            isRunning = false;
        }
        
        if (controller.isGrounded)
        {
            speed.y = -0.01f;
        }
        else 
        {
            speed.y -= 0.005f;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            speed.y += 0.2f;
        }
        
        move += transform.forward * forward * moveSpeed;
        move += transform.right * right * moveSpeed;

        move += speed;
        controller.Move(move);
    }
}
