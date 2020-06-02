using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GodScript : MonoBehaviour
{
    public DeathMenu deathMenu;

    #region Player
    private int _speed;
    private float _maxJumpVelocity;
    private float _fallVelocity;
    private float _flyDistanceMax;
    private float _breathMax;

    public int Speed
    {
        get { return _speed; }
        set { if (value > 0) _speed = value; }
    }
    public float MaxJumpVelocity
    {
        get { return _maxJumpVelocity; }
        set { if (value > 0) _maxJumpVelocity = value; }
    }
    public float FallVelocity
    {
        get { return _fallVelocity; }
        set { if (value > 0) _fallVelocity = value; }
    }
    public float FlyDistanceMax
    {
        get { return _flyDistanceMax; }
        set { if (value > 0) _flyDistanceMax = value; }
    }
    public float BreathMax
    {
        get { return _breathMax; }
        set { if (value > 0) _breathMax = value; }
    }
    #endregion 


    #region Platforming
    private int _lives;
    private int _score;
    private int _floesJumped;
    private int _flagSaveInterval;
    private float _flagLatestSpawnedZ;
    private float _flagLatestSpawnedY;

    public int FlagSaveInterval
    {
        get { return _flagSaveInterval; }
        set { _flagSaveInterval = value; }
    }

    public float FlagLatestTouchedZ
    {
        get { return _flagLatestSpawnedZ; }
        set { _flagLatestSpawnedZ = value; }
    }
    public float FlagLatestTouchedY
    {
        get { return _flagLatestSpawnedY; }
        set { _flagLatestSpawnedY = value; }
    }

    public int FloesJumped
    {
        get { return _floesJumped; }
        set { if (value >= 0) _floesJumped = value; }
    }

    public int Lives
    {
        get { return _lives; }
        set { _lives = value; }
    }

    public int Score
    {
        get { return _score; }
        set
        {
            if (value > 0)
            {
                _score = (int)value;
                scoreText.text = _score.ToString();
            }
        }
    }

    public Text scoreText; 
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _speed = 60;
        _maxJumpVelocity = 3.5f;
        _fallVelocity = 1.8f;
        _flyDistanceMax = 20;
        _breathMax = 150f;


        _score = 0;
        _floesJumped = 0;
        _lives = 1;

        _flagSaveInterval = 5;
        _flagLatestSpawnedZ = 0;
        _flagLatestSpawnedY = 40;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public string[] GetMovementData()
    {
        string[] data = new string[4];
        data[0] = _speed.ToString();
        data[1] = _maxJumpVelocity.ToString();
        data[2] = _flyDistanceMax.ToString();
        data[3] = _fallVelocity.ToString();

        return data;
    }

    public void ReduceLife()
    {
        //CameraFollow camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        //camera.ReduceLife();
        Lives--;

        if(_lives == 0)
        {
            //deathMenu.ToggleEndMenu(2);
        }
    }
}
