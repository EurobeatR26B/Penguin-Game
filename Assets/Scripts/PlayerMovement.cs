using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public DeathMenu death;
    private AudioSpawnerManager AudioSpawner;
    private UnityEngine.UI.Text ScoreText;

    public GameObject life;
    public DeathMenu deathMenu;

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
    public bool FlySpeedUpgrade = false;
    public bool FlyGroundUpgrade = false;

    public int ScoreMultiplier;

    public float distanceToGround = 0.7f;
    Rigidbody rb;

    public static UnityEngine.UI.Slider flightMeter;
    public static UnityEngine.UI.Slider breathMeter;

    // Start is called before the first frame update
    void Start()
    {
        GodScript.SpawnLives(life);
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
            if (Input.GetKey(KeyCode.LeftShift) && FlyDistanceCurrent <= FlyDistanceMax && (!grounded || FlyGroundUpgrade))
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
                Speed = GodScript.Speed;
            }


            if (JumpVelocity <= 0f || FlyDistanceCurrent >= FlyDistanceMax)
            {
                if (FlyDistanceCurrent <= FlyDistanceMax && Input.GetKey(KeyCode.LeftShift) && (!grounded || FlyGroundUpgrade))
                {
                    Fly(ref pos);
                }
                else
                {
                    rb.useGravity = true;

                    JumpVelocity = 0;
                    FallVelocity = FallVelocityStore;
                    Speed = GodScript.Speed;
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
        BreathDamp = 2;
        
        FlyDistanceCurrent = 0;
        BreathCurrent = 0;

        Speed = GodScript.Speed;
        MaxJumpVelocity = GodScript.MaxJumpVelocity;
        FlyDistanceMax = GodScript.FlyDistanceMax;
        BreathMax = GodScript.BreathMax;
        FallVelocity = GodScript.FallVelocity;        
        
        distanceToGround = 2;

        FallVelocityStore = FallVelocity;
        JumpDampStore = JumpDamp;

        ScoreMultiplier = GodScript.ScoreMultiplier;
        flightMeter.maxValue = FlyDistanceMax;
        breathMeter.maxValue = BreathMax;
        breathMeter.value = breathMeter.maxValue;

        FlySpeedUpgrade = GodScript.FlySpeedUpgrade;
        FlyGroundUpgrade = GodScript.FlyGroundUpgrade;

        if (GodScript.Hat != null) LoadClothing(GodScript.Hat);
        if (GodScript.Glasses != null) LoadClothing(GodScript.Glasses);

        ScoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<UnityEngine.UI.Text>();
        AudioSpawner = GameObject.FindGameObjectWithTag("AudioSpawner").GetComponent<AudioSpawnerManager>();
    }

    void LoadClothing(string objectName)
    {
        foreach(Transform t in transform)
        {
            if (t.name == objectName) t.gameObject.SetActive(true);
        }
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
        if (FlySpeedUpgrade) Speed = GodScript.Speed * 2;

        FallVelocity = 0;
        FlyDistanceCurrent += GodScript.FlySeconds * Time.deltaTime;

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
        GodScript.OnPlayerDeath(deathMenu);
        transform.position = new Vector3(0, GodScript.FlagLatestTouchedY, GodScript.FlagLatestTouchedZ);

        LoadStats();
    }

    private void OnCollisionEnter (Collision hit)
    {
        if(hit.collider.gameObject.tag == "Snowflake")
        {
            Destroy(hit.gameObject);

            if(hit.gameObject.name == "The Special Flake") GodScript.CurrentScore += 10 * ScoreMultiplier;
            else GodScript.CurrentScore += 1 * ScoreMultiplier;

            ScoreText.text = GodScript.CurrentScore.ToString();
            AudioSpawner.PlayPoints();
        }

        if (hit.collider.gameObject.tag == "Fish")
        {
            Destroy(hit.gameObject);
            GodScript.CurrentScore += 2 * ScoreMultiplier;

            ScoreText.text = GodScript.CurrentScore.ToString();

            AudioSpawner.PlayFishPoints();
        }



        if (hit.collider.gameObject.tag == "Floe")
        {
            GodScript.FloesJumped++;
        }
        if (hit.collider.gameObject.tag == "Flag")
        {
            GodScript.FlagLatestTouchedZ = hit.transform.position.z;
            GodScript.FlagLatestTouchedY = hit.transform.position.y + 10;

            AudioSpawner.PlayFlag();
        }
        if (hit.gameObject.tag == "Sea")
        {
            AudioSpawner.PlaySplash();
            AudioSpawner.StartUnderwater();
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
            BreathCurrent += Time.deltaTime * GodScript.WaterSeconds;
            breathMeter.value = BreathMax - BreathCurrent;

            Speed = GodScript.WaterSpeed;
            isInWater = true;

            if (GodScript.WaterScoreUpgrade) ScoreMultiplier *= 2;

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Sea")
        {
            AudioSpawner.PauseUnderwater();
            isInWater = false;
            Speed = GodScript.Speed;
            ScoreMultiplier = GodScript.ScoreMultiplier;
        }
        
    }
}
