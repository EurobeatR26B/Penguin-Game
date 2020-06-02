using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GodScript godScript;
    public DeathMenu death;

    public int Speed;  

    public float MaxJumpVelocity;
    public float JumpVelocity;
    private float JumpDamp;
    private float JumpDampStore;

    public float FlyDistanceMax;
    public float FlyDamp = 2;
    public float FlyDistanceCurrent;

    public float FallVelocity;
    private float FallVelocityStore;

    public float BreathMax;
    public float BreathDamp;
    public float BreathCurrent;

    public bool grounded;
    public bool isInWater = false;

    public float distanceToGround = 0.7f;
    Rigidbody rb;

    UnityEngine.UI.Slider flightMeter;
    UnityEngine.UI.Slider breathMeter;

    // Start is called before the first frame update
    void Start()
    {
        godScript = GameObject.FindGameObjectWithTag("God").GetComponent<GodScript>();
        flightMeter = GameObject.FindGameObjectWithTag("FlightMeter").GetComponent<UnityEngine.UI.Slider>();
        breathMeter = GameObject.FindGameObjectWithTag("BreathMeter").GetComponent<UnityEngine.UI.Slider>();

        rb = GetComponent<Rigidbody>();
        

        LoadStats();
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
            if (Input.GetKey(KeyCode.LeftShift) && FlyDistanceCurrent <= FlyDistanceMax && !grounded)
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
                if (FlyDistanceCurrent <= FlyDistanceMax && Input.GetKey(KeyCode.LeftShift) && !grounded)
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
        if (!grounded && rb.velocity.y >= -130)
        {
            rb.velocity += new Vector3(0, -FallVelocity, 0);
        }
        if(grounded)
        {
            FlyDistanceCurrent = 0;
            flightMeter.value = flightMeter.maxValue;
        }


        transform.position = pos;
        #endregion Jump

        if(!isInWater)
        {
            if (BreathCurrent >= 0)
            {
                BreathCurrent -= BreathDamp * 0.5f;
                breathMeter.value = BreathMax - BreathCurrent;
            }
        }

        if (BreathCurrent >= BreathMax) Die();

    }

    void LoadStats()
    {
        JumpDamp = 0.13f;
        FlyDamp = 2;
        distanceToGround = 2;
        BreathDamp = 2;
        FlyDistanceCurrent = 0;
        BreathCurrent = 0;

        Speed = godScript.Speed;
        MaxJumpVelocity = godScript.MaxJumpVelocity;
        FallVelocity = godScript.FallVelocity;
        FlyDistanceMax = godScript.FlyDistanceMax;
        BreathMax = godScript.BreathMax;

        FallVelocityStore = FallVelocity;
        JumpDampStore = JumpDamp;

        flightMeter.maxValue = FlyDistanceMax;
        breathMeter.maxValue = BreathMax;
        breathMeter.value = breathMeter.maxValue;
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

        rb.velocity += new Vector3(0, 0.8f, 0);

        JumpVelocity = 0.01f;
        flightMeter.value = FlyDistanceMax - FlyDistanceCurrent;
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

    void Die()
    {
        godScript.Lives--;
        transform.position = new Vector3(0, godScript.FlagLatestTouchedY, godScript.FlagLatestTouchedZ);

        BreathCurrent = 0;
        FlyDistanceCurrent = 0;

        if (godScript.Lives == 0)
        {
            death.ToggleEndMenu(godScript.Score);
            Speed = 0;
            MaxJumpVelocity = 0;

            flightMeter.gameObject.SetActive(false);
            breathMeter.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter (Collision hit)
    {
        if(hit.collider.gameObject.tag == "Snowflake")
        {
            Destroy(hit.gameObject);
            godScript.Score += 1;
        }

        if (hit.collider.gameObject.tag == "Floe")
        {
            godScript.FloesJumped++;
        }
        if (hit.collider.gameObject.tag == "Flag")
        {
            godScript.FlagLatestTouchedZ = hit.transform.position.z;
            godScript.FlagLatestTouchedY = hit.transform.position.y + 10;
        }

        /*if (hit.collider.gameObject.tag == "Sea")
        {
            BreathCurrent += BreathDamp * Time.deltaTime * 1f;
            breathMeter.value = BreathMax - BreathCurrent;
        }*/
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Sea")
        {
            BreathCurrent += BreathDamp;
            breathMeter.value = BreathMax - BreathCurrent;
            isInWater = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Sea") isInWater = false;
        
    }
}
