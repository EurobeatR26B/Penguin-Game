using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public int Speed = 2;
    public int Height = 2;

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
    }

    bool isGrounded ()
    {
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround);
    }

    void Jump()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(hAxis, 0f, vAxis);

        rb.velocity += -movement * 2 * Speed;
        rb.velocity += Vector3.up * Height;
    }

    void Move()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(hAxis, 0f, vAxis);

        if(movement != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.3f);

        if (isGrounded())
        transform.Translate(-movement * Speed * Time.deltaTime, Space.World);
    }
}
