using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    CharacterController characterController;
    [SerializeField] Animator anim;
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes
            float move = Input.GetAxis("Vertical");
            anim.SetFloat("Speed", move);

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }

        //    if (Input.GetButton("Horizontal"))
        //    {
                
        //    } else if (Input.GetButton("Vertical"))
        //    {
                
        //    }
        //    else
        //    {
        //        anim.SetBool("walk", false);
        //    }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 0)
        {
            Debug.Log("player collition");
            Debug.Log(other.gameObject.layer);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("WhatIsEnemy"))
        {
            Character.Instance.MinionLockTarget(other.gameObject.transform);
        }
    }
}
