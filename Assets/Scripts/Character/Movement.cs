using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Movement : MonoBehaviour
{
    CharacterController characterController;
    [SerializeField] Animator anim;

    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float moveSpeed = 5f;
    public float rotationSpeed = 2f;

    [Header("Camera configuration")]
    public Transform lookAtTarget;
    public CinemachineFreeLook freeLookCamera;
    public Transform MainCamera;
    public float scrollSpeed = 2.0f; // Kecepatan zoom menggunakan mouse scroll.
    public float minDistance = 3.0f; // Jarak minimum kamera.
    public float maxDistance = 10.0f; // Jarak maksimum kamera.

    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (KeyBinding.Instance.IsLock) return;


        // Trigger animations
        //anim.SetFloat("Speed", characterController.velocity.magnitude);
        //Debug.Log("walk");

        if (characterController.isGrounded)
        {
            // Character Controller Movement
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            //Vector3 inputDirection = Quaternion.Euler(0, MainCamera.eulerAngles.y, 0) * new Vector3(horizontalInput, 0, verticalInput);
            Vector3 inputDirection = new Vector3(horizontalInput, 0, verticalInput);
            if (inputDirection.magnitude > 1)
            {
                inputDirection.Normalize();
            }

            moveDirection = transform.TransformDirection(inputDirection) * moveSpeed;

            //if (Input.GetButton("Jump"))
            //{
            //    anim.SetBool("Walk", false);
            //    anim.SetBool("Jump", true);
            //    moveDirection.y = jumpSpeed;
            //}


            if (verticalInput > 0)
            {
                transform.rotation = Quaternion.Euler(0, MainCamera.eulerAngles.y, 0);
            }

            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("jump");
                anim.SetTrigger("Jump");
                moveDirection.y = jumpSpeed;
            }

            anim.SetFloat("Speed", verticalInput);

        }

        
        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        if (Input.GetButtonDown("Attack"))
        {
            anim.SetTrigger("Attack");
            transform.rotation = Quaternion.Euler(0, MainCamera.eulerAngles.y, 0);
        }
        

        // Mendapatkan input dari mouse scroll and Mengubah jarak kamera berdasarkan input scroll.
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        float newDistance = freeLookCamera.m_Orbits[1].m_Radius;
        newDistance -= scrollInput * scrollSpeed;
        newDistance = Mathf.Clamp(newDistance, minDistance, maxDistance);
        freeLookCamera.m_Orbits[1].m_Radius = newDistance;

        characterController.Move(moveDirection * Time.deltaTime);
        if (moveDirection == Vector3.zero)
            anim.SetFloat("Speed", 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 0)
        {
            Debug.Log("player collision");
            Debug.Log(other.gameObject.layer);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("WhatIsEnemy"))
        {
            Debug.Log("enemy collision");
            Character.Instance.MinionLockTarget(other.gameObject.transform);
        }
    }
}
