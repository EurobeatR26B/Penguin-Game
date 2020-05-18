﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int Speed = 2;
    public float MaxJumpVelocity = 3;
    public float FallVelocity = 1.5f;
    private float FallVelocityStore;

    public float JumpVelocity;
    public float JumpDamp;
    private float JumpDampStore;

    public float FlyDistanceMax = 100;
    public float FlyDamp = 2;
    public float FlyDistanceCurrent;

    public bool grounded;

    public float distanceToGround = 0.7f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        FlyDistanceCurrent = 0;
        FallVelocityStore = FallVelocity;
        JumpDampStore = JumpDamp;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        grounded = isGrounded();

        if (Input.GetKey(KeyCode.Space) && grounded) Jump();
        if (Input.GetKey(KeyCode.R)) transform.position = new Vector3(0, 20, 0);


        Vector3 pos = transform.position;

        #region Jump
        if (JumpVelocity > 0 || FlyDistanceCurrent < FlyDistanceMax)
        {
            //If there is input and conditions to flap
            if (Input.GetKey(KeyCode.LeftShift) && FlyDistanceCurrent <= FlyDistanceMax)
            {
                Fly(ref pos);
                pos.y += 2.5f;
            }
            //If no flap, simply jump as usual
            else
            {
                pos.y += JumpVelocity;
                JumpVelocity -= JumpDamp;

                FallVelocity = 0;
            }


            if (JumpVelocity <= 0f || FlyDistanceCurrent >= FlyDistanceMax)
            {
                if (FlyDistanceCurrent <= FlyDistanceMax && Input.GetKey(KeyCode.LeftShift))
                {
                    Fly(ref pos);
                }
                else
                {
                    rb.useGravity = true;

                    JumpVelocity = 0;
                    FallVelocity = FallVelocityStore;
                }

            }

        }
        //Fall back down the ground
        if (!grounded)
        {
            rb.velocity += new Vector3(0, -FallVelocity, 0);
        }
        else FlyDistanceCurrent = 0;


        transform.position = pos;
        #endregion Jump

    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround);
    }

    void Jump()
    {
        JumpVelocity = MaxJumpVelocity;
        rb.useGravity = false;
    }

    void Fly(ref Vector3 position)
    {
        if (grounded) return;
        FallVelocity = 0;
        FlyDistanceCurrent += FlyDamp;

        position.y += 5f;

        JumpVelocity = 0.01f;

    }

    void Move()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(hAxis, 0f, vAxis);

        if (movement != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.3f);

        //if (isGrounded())
        transform.Translate(movement * Speed * Time.deltaTime, Space.World);
    }
}