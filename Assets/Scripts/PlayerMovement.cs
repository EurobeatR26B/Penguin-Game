using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int Speed = 2;
    public float MaxJumpVelocity = 3;
    public float FallVelocity = 1.5f;

    public float JumpVelocity;
    public float JumpDamp = 0.7f;

    public float distanceToGround = 0.7f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();

        if (Input.GetKey(KeyCode.Space) && isGrounded()) Jump();
        if (Input.GetKey(KeyCode.R)) transform.position = new Vector3(0, 20, 0);


        Vector3 pos = transform.position;

        if (JumpVelocity > 0)
        {
            pos.y += JumpVelocity;

            JumpVelocity -= JumpDamp;

            if(JumpVelocity <= 0)
            {
                rb.useGravity = true;
                JumpVelocity = 0;
            }
        }
        if(!isGrounded())
        {
            rb.velocity += new Vector3(0, -FallVelocity, 0);
        }

        transform.position = pos;

    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround);
    }

    void Jump()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        JumpVelocity = MaxJumpVelocity;
        rb.useGravity = false;
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
