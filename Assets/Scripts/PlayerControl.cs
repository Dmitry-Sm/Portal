using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Movement settings")]

    [SerializeField]
    private float speed = 6.0f;
    [SerializeField]
    private float jumpSpeed = 8.0f;
    [SerializeField]
    private float rotateSpeed = 0.8f;
    [SerializeField]
    private float gravity = 20.0f;

    [HideInInspector]
    public CharacterController controller;

    private PlayerAnimation _anim;

    private bool isRunning = false;
    private Transform playerCamera;
    private Vector3 moveDirection = Vector3.zero;


    void Start()
    {
        print("Start");

        playerCamera = GetComponentInChildren<Camera>().transform;
        controller = GetComponent<CharacterController>();
        _anim = GetComponent<PlayerAnimation>();
        Cursor.lockState = CursorLockMode.Locked;
    }

void Update()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed, 0);

        playerCamera.Rotate(-Input.GetAxis("Mouse Y") * rotateSpeed, 0, 0);
        if (playerCamera.localRotation.eulerAngles.y != 0)
        {
            playerCamera.Rotate(Input.GetAxis("Mouse Y") * rotateSpeed, 0, 0);
        }

        moveDirection.x = Input.GetAxis("Horizontal") * speed;
        moveDirection.z = Input.GetAxis("Vertical") * speed;
        _anim.Move(Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        // moveDirection = new Vector3(Input.GetAxis("Horizontal") * speed, moveDirection.y, Input.GetAxis("Vertical") * speed);
        moveDirection = transform.TransformDirection(moveDirection);

        if (controller.isGrounded)
        {
            if (Input.GetButton("Jump")) moveDirection.y = jumpSpeed;
            else moveDirection.y = 0;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
