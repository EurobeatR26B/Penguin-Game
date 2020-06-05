using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GodScript
{
    public static string _hat;
    public static string _glasses;

    public static string Hat
    {
        get { return _hat; }
        set { _hat = value; Debug.Log("HAT SET"); }
    }
    public static string Glasses
    {
        get { return _glasses; }
        set { _glasses = value; }
    }
    //public static DeathMenu deathMenu;
    private static List<GameObject> activeLives;

    //FOR ENABLING/DISABLING BUTTONS IN THE UPGRADES SCENE
    public static bool[,] purchasedUpgrades = new bool[3, 3];

    #region Player

    private static int _speed = 60;
    private static int _waterSpeed = 15;
    private static float _breathMax = 100.0f;
    private static float _waterSurvivalSeconds = 3.0f;

    private static float _maxJumpVelocity = 3.5f;
    private static float _fallVelocity = 1.8f;

    private static float _flySeconds = 0.2f;
    private static float _flyDistanceMax = 20.0f;

    private static int _scoreMultiplier = 1;
    private static bool _waterScoreUpgrade = false;
    private static bool _flySpeedUpgrade = false;
    private static bool _flyGroundUpgrade = false;

    public static bool FlyGroundUpgrade
    {
        get { return _flyGroundUpgrade; }
        set { _flyGroundUpgrade = value; }
    }

    public static bool FlySpeedUpgrade
    {
        get { return _flySpeedUpgrade; }
        set { _flySpeedUpgrade = value; }
    }

    public static bool WaterScoreUpgrade
    {
        get { return _waterScoreUpgrade; }
        set { _waterScoreUpgrade = value; }
    }

    public static int ScoreMultiplier
    {
        get { return _scoreMultiplier; }
        set { if (value > 0) _scoreMultiplier = (int)value; }
    }

    public static float FlySeconds
    {
        get { return _flyDistanceMax / _flySeconds; }
        set { _flySeconds = value; }
    }

    public static float WaterSeconds
    {
        get { return _breathMax / _waterSurvivalSeconds; }
        set { _waterSurvivalSeconds = value; }
    }

    public static int WaterSpeed
    {
        get { return _waterSpeed; }
        set { if (value > 0) _waterSpeed = value; }
    }

    public static int Speed
    {
        get { return _speed; }
        set { if (value > 0) _speed = value; }
    }
    public static float MaxJumpVelocity
    {
        get { return _maxJumpVelocity; }
        set { if (value > 0) _maxJumpVelocity = value; }
    }
    public static float FallVelocity
    {
        get { return _fallVelocity; }
        set { if (value > 0) _fallVelocity = value; }
    }
    public static float FlyDistanceMax
    {
        get { return _flyDistanceMax; }
        set { if (value > 0) _flyDistanceMax = value; }
    }
    public static float BreathMax
    {
        get { return _breathMax; }
        set { if (value > 0) _breathMax = value; }
    }
    #endregion


    #region Platforming

    private static int _lives = 3;
    private static int _currentScore = 0;
    private static int _totalScore = 0;
    private static int _totalSpent = 0;
    private static int _level = 0;
    private static int _floesJumped = 0;
    private static int _flagSaveInterval = 15;
    private static float _flagLatestSpawnedZ = 0f;
    private static float _flagLatestSpawnedY = 40f;

    public static int FlagSaveInterval
    {
        get { return _flagSaveInterval; }
        set { _flagSaveInterval = value; }
    }

    public static float FlagLatestTouchedZ
    {
        get { return _flagLatestSpawnedZ; }
        set { _flagLatestSpawnedZ = value; }
    }
    public static float FlagLatestTouchedY
    {
        get { return _flagLatestSpawnedY; }
        set { _flagLatestSpawnedY = value; }
    }

    public static int FloesJumped
    {
        get { return _floesJumped; }
        set { if (value >= 0) _floesJumped = value; }
    }

    public static int Lives
    {
        get { return _lives; }
        set { _lives = value; }
    }

    public static int CurrentScore
    {
        get { return _currentScore; }
        set
        {
            if (value > 0)
            {
                _currentScore = (int)value;
                //scoreText.text = _score.ToString();
            }
        }
    }

    public static int TotalScore
    {
        get { return _totalScore; }
        set { _totalScore = value; }
    }

    public static int TotalSpent
    {
        get { return _totalSpent; }
        set { _totalSpent = value; }
    }

    public static int Level
    {
        get { return _level; }
        set { _level = value; }
    }


    //public static Text scoreText;
    #endregion

    #region System
    private static bool _music = true;
    private static bool _sfx = true;
    private static bool _audioPause = false;

    public static bool Music
    {
        get { return _music; }
        set { _music = value; }
    }
    public static bool SFX
    {
        get { return _sfx; }
        set { _sfx = value; }
    }
    public static bool AudioPause { get => _audioPause; set => _audioPause = value; }
    #endregion



    public static void ResetUpgradeBoolArray()
    {
        for (int col = 0; col < purchasedUpgrades.GetLength(0); col++)
        {
            for (int row = 0; row < purchasedUpgrades.GetLength(1); row++)
            {
                purchasedUpgrades[row, col] = false;
            }
        }
    }

    public static string[] GetMovementData()
    {
        string[] data = new string[4];
        data[0] = _speed.ToString();
        data[1] = _maxJumpVelocity.ToString();
        data[2] = _flyDistanceMax.ToString();
        data[3] = _fallVelocity.ToString();

        return data;
    }

    public static void SpawnLives(GameObject life)
    {
        activeLives = new List<GameObject>();
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");

        float addXpos = 0;
        for (int i = 0; i < _lives; i++)
        {
            GameObject ob = MonoBehaviour.Instantiate(life) as GameObject;

            ob.transform.position = camera.transform.position;

            Vector3 lifePos = new Vector3(-42f + addXpos, 75f, -15f);
            ob.transform.position = lifePos;

            addXpos += 8;

            ob.transform.SetParent(camera.transform);
            activeLives.Add(ob);
        }
    }

    public static void OnPlayerDeath(DeathMenu deathMenu)
    {
        if (_lives - 1 == 0)
        {
            DestroyLife();
            deathMenu.ToggleEndMenu(_currentScore);


            MonoBehaviour.Destroy(GameObject.FindGameObjectWithTag("Player"));
            GameObject.FindGameObjectWithTag("FlightMeter").gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("BreathMeter").gameObject.SetActive(false);
        }
        else
        {
            _lives--;
            DestroyLife();
        }
    }

    private static void DestroyLife()
    {

        MonoBehaviour.Destroy(activeLives[activeLives.Count - 1]);
        activeLives.RemoveAt(activeLives.Count - 1);
    }

    public static void SaveData()
    {
        PlayerPrefs.SetInt("Lives", _lives);
        PlayerPrefs.SetInt("Total Score", _totalScore);
        PlayerPrefs.SetInt("Total Spent", _totalSpent);
        PlayerPrefs.SetInt("Level", _level);
        PlayerPrefs.SetInt("Flag Interval", _flagSaveInterval);

        PlayerPrefs.SetInt("Speed", _speed);
        PlayerPrefs.SetInt("Water Speed", _waterSpeed);
        PlayerPrefs.SetFloat("Max Breath", _breathMax);
        PlayerPrefs.SetFloat("Water Seconds", _waterSurvivalSeconds);
        PlayerPrefs.SetFloat("Jump Velocity", _maxJumpVelocity);
        PlayerPrefs.SetFloat("Fly Seconds", _flySeconds);
        PlayerPrefs.SetString("Fly Ground", _flyGroundUpgrade.ToString());
        PlayerPrefs.SetString("Hat", _hat);
        PlayerPrefs.SetString("Glasses", _glasses);


        PlayerPrefs.SetString("Fly Speed", FlySpeedUpgrade.ToString());
        PlayerPrefs.SetString("Water Score", WaterScoreUpgrade.ToString());
        PlayerPrefs.SetString("Music", Music.ToString());
        PlayerPrefs.SetString("SFX", SFX.ToString());
        PlayerPrefs.SetString("Audio Pause", AudioPause.ToString());
    }

    public static void LoadData()
    {
        _lives = PlayerPrefs.GetInt("Lives", 3);
        _totalScore = PlayerPrefs.GetInt("Total Score", 0);
        _totalSpent = PlayerPrefs.GetInt("Total Spent", 0);
        _level = PlayerPrefs.GetInt("Level", 0);
        _flagSaveInterval = PlayerPrefs.GetInt("Flag Interval", 15);
        _speed = PlayerPrefs.GetInt("Speed", 60);
        _waterSpeed = PlayerPrefs.GetInt("Water Speed", 15);

        _breathMax = PlayerPrefs.GetFloat("Max Breath", 100.0f);
        _waterSurvivalSeconds = PlayerPrefs.GetFloat("Water Seconds", 3.0f);
        _maxJumpVelocity = PlayerPrefs.GetFloat("Jump Velocity", 3.5f);
        _flySeconds = PlayerPrefs.GetFloat("Fly Seconds", 0.2f);

        _hat = PlayerPrefs.GetString("Hat", null);
        _glasses = PlayerPrefs.GetString("Glasses", null);

        FlyGroundUpgrade = bool.Parse(PlayerPrefs.GetString("Fly Ground", "false"));
        FlySpeedUpgrade = bool.Parse(PlayerPrefs.GetString("Fly Speed", "false"));
        WaterScoreUpgrade = bool.Parse(PlayerPrefs.GetString("Water Score", "false"));
        Music = bool.Parse(PlayerPrefs.GetString("Music", "true"));
        SFX = bool.Parse(PlayerPrefs.GetString("SFX", "true"));
        AudioPause = bool.Parse(PlayerPrefs.GetString("Audio Pause", "true"));
    }

}
