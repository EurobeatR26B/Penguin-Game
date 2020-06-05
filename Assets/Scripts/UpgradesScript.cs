using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpgradesScript : MonoBehaviour
{
    public AudioSource fastMusic;
    public AudioSource buttonClick;

    public Text upgradeTextPrefab;

    private GameObject[,] buttonArray;
    private int[,] upgradeCosts;
    private string[,] upgradeDescriptions;
    private Vector3[,] upgradePrefabPositions;
    private GameObject[,] upgradeTextObjects;

    private Text levelText;
    private Text totalSpentText;
    private Text currentMoneyText;
    private Text flagText;
    private Text heartText;
    private int swimUpgrade1;
    private int heartCost = 20;
    private int flagCost = 30;

    //SWIM = Array[0][level]
    //JUMP  = Array[1][level]
    //FLY = Array[2][level]

    // Start is called before the first frame update
    void Start()
    {
        swimUpgrade1 = 20;

        buttonArray = new GameObject[3, 3];
        upgradeCosts = new int[3, 3];
        upgradeDescriptions = new string[3, 3];
        upgradePrefabPositions = new Vector3[3, 3];
        upgradeTextObjects = new GameObject[3, 3];

        LoadButtonArray();
        LoadCostsArray();
        LoadStrings();
        LoadUpgradePrefabPositions();
        LoadText();


        if (GodScript.Music) fastMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadButtonArray()
    {
        //SWIM = [0, level]
        buttonArray[0, 0] = GameObject.FindGameObjectWithTag("Swim1");
        buttonArray[0, 1] = GameObject.FindGameObjectWithTag("Swim2");
        buttonArray[0, 2] = GameObject.FindGameObjectWithTag("Swim3");

        //JUMP = [1, level]
        buttonArray[1, 0] = GameObject.FindGameObjectWithTag("Jump1");
        buttonArray[1, 1] = GameObject.FindGameObjectWithTag("Jump2");
        buttonArray[1, 2] = GameObject.FindGameObjectWithTag("Jump3");

        //FLY = [2, level]
        buttonArray[2, 0] = GameObject.FindGameObjectWithTag("Fly1");
        buttonArray[2, 1] = GameObject.FindGameObjectWithTag("Fly2");
        buttonArray[2, 2] = GameObject.FindGameObjectWithTag("Fly3");
    }

    void LoadCostsArray()
    {
        //SWIM = [0, level]
        upgradeCosts[0, 0] = 5;
        upgradeCosts[0, 1] = 10;
        upgradeCosts[0, 2] = 15;

        //JUMP = [1, level] 
        upgradeCosts[1, 0] = 15;
        upgradeCosts[1, 1] = 40;
        upgradeCosts[1, 2] = 100;

        //FLY = [2, level]  
        upgradeCosts[2, 0] = 20;
        upgradeCosts[2, 1] = 40;
        upgradeCosts[2, 2] = 60;
    }

    void LoadStrings()
    {
        //SWIM = [0, level]
        upgradeDescriptions[0, 0] = GetSwimStrings()[0];
        upgradeDescriptions[0, 1] = GetSwimStrings()[1];
        upgradeDescriptions[0, 2] = GetSwimStrings()[2];

        //JUMP = [1, level]    
        upgradeDescriptions[1, 0] = GetJumpStrings()[0];
        upgradeDescriptions[1, 1] = GetJumpStrings()[1];
        upgradeDescriptions[1, 2] = GetJumpStrings()[2];

        //FLY = [2, level]     
        upgradeDescriptions[2, 0] = GetFlyStrings()[0];
        upgradeDescriptions[2, 1] = GetFlyStrings()[1];
        upgradeDescriptions[2, 2] = GetFlyStrings()[2];
    }

    void LoadUpgradePrefabPositions()
    {
        /*
        int colCount = upgradePrefabPositions.GetLength(0);

        //SWIM DESCRIPTIONS
        float posX = GameObject.FindGameObjectWithTag("Swim1").transform.position.x + 90;
        float posY = GameObject.FindGameObjectWithTag("Swim2").transform.position.y;

        float posDifference = Math.Abs(GameObject.FindGameObjectWithTag("Swim1").transform.position.y + GameObject.FindGameObjectWithTag("Swim2").transform.position.y);
        float a = posDifference;
        for (int col = 0; col < colCount; col++)
        {
            upgradePrefabPositions[0, col] = new Vector3(posX, posY - (col * posDifference), 0);
        }


        //JUMP DESCRIPTIONS
        posX = GameObject.FindGameObjectWithTag("Jump1").transform.position.x + 90;
        posY = GameObject.FindGameObjectWithTag("Jump1").transform.position.y;

        posDifference = Math.Abs(GameObject.FindGameObjectWithTag("Jump2").transform.position.x + GameObject.FindGameObjectWithTag("Jump1").transform.position.x);
        for (int col = 0; col < colCount; col++)
        {
            upgradePrefabPositions[1, col] = new Vector3(posX + (col * posDifference), posY, 0);
        }

        //FLY DESCRIPTIONS
        posX = GameObject.FindGameObjectWithTag("Fly1").transform.position.x - 90;        

        posDifference = GameObject.FindGameObjectWithTag("Fly2").transform.position.y + GameObject.FindGameObjectWithTag("Jump1").transform.localPosition.y;
        for (int col = 0; col < colCount; col++)
        {
            posY = upgradePrefabPositions[0, col].y;
            upgradePrefabPositions[2, col] = new Vector3(posX, posY + (col * a), 0);
        }*/


        //SWIM
        upgradePrefabPositions[0, 0] = new Vector3(-410, 240, 0);
        upgradePrefabPositions[0, 1] = new Vector3(-410, 0, 0);
        upgradePrefabPositions[0, 2] = new Vector3(-410, -240, 0);

        //JUMP
        upgradePrefabPositions[1, 0] = new Vector3(-193, 360, 0);
        upgradePrefabPositions[1, 1] = new Vector3(147, 360, 0);
        upgradePrefabPositions[1, 2] = new Vector3(487, 360, 0);

        //FLY
        upgradePrefabPositions[2, 0] = new Vector3(410, 240, 0);
        upgradePrefabPositions[2, 1] = new Vector3(410, 0, 0);
        upgradePrefabPositions[2, 2] = new Vector3(410, -240, 0);
    }

    void LoadText()
    {
        currentMoneyText = GameObject.FindGameObjectWithTag("CurrentMoney").GetComponent<Text>();
        totalSpentText = GameObject.FindGameObjectWithTag("TotalSpent").GetComponent<Text>();
        levelText = GameObject.FindGameObjectWithTag("Level").GetComponent<Text>();
        flagText = GameObject.FindGameObjectWithTag("FlagText").GetComponent<Text>();
        heartText = GameObject.FindGameObjectWithTag("HeartText").GetComponent<Text>();

        currentMoneyText.text = GodScript.TotalScore.ToString();
        totalSpentText.text = GodScript.TotalSpent.ToString();
        levelText.text = GodScript.Level.ToString();
        flagText.text = GodScript.FlagSaveInterval.ToString();
        heartText.text = GodScript.Lives.ToString();


        for (int row = 0; row < upgradePrefabPositions.GetLength(0); row++)
        {
            for (int col = 0; col < upgradePrefabPositions.GetLength(1); col++)
            {
                Text ob = Instantiate(upgradeTextPrefab) as Text;
                ob.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
                ob.transform.localPosition = upgradePrefabPositions[row, col];
                ob.text = upgradeDescriptions[row, col];
                upgradeTextObjects[row, col] = ob.gameObject;
            }
        }
    }

    public void UpgradeSwim1()
    {
        if (GodScript.TotalScore < upgradeCosts[0, 0]) return;

        GodScript.WaterSpeed += swimUpgrade1;

        swimUpgrade1 = (int)(swimUpgrade1 * 0.9);

        UpdateStats(0, 0);
        upgradeCosts[0, 0] += 10;
        upgradeTextObjects[0, 0].GetComponent<Text>().text = GetSwimStrings()[0];

    }

    public void UpgradeSwim2()
    {
        if (GodScript.TotalScore < upgradeCosts[0, 1]) return;

        GodScript.WaterSeconds += 1;


        UpdateStats(0, 1);
        upgradeCosts[0, 1] += 10;
        upgradeTextObjects[0, 1].GetComponent<Text>().text = GetSwimStrings()[1];

    }

    public void UpgradeSwim3()
    {
        if (GodScript.TotalScore < upgradeCosts[0, 2]) return;

        GodScript.WaterScoreUpgrade = true;

        UpdateStats(0, 2);
        buttonArray[0, 2].GetComponent<Button>().interactable = false;

    }

    public void UpgradeJump1()
    {
        if (GodScript.TotalScore < upgradeCosts[1, 0]) return;

        GodScript.MaxJumpVelocity += 1;

        UpdateStats(1, 0);
        buttonArray[1, 0].GetComponent<Button>().interactable = false;

        UpdateJumpDescriptions();
    }

    public void UpgradeJump2()
    {
        if (GodScript.TotalScore < upgradeCosts[1, 1]) return;

        GodScript.MaxJumpVelocity += 1;

        UpdateStats(1, 1);
        buttonArray[1, 1].GetComponent<Button>().interactable = false;

        UpdateJumpDescriptions();
    }

    public void UpgradeJump3()
    {
        if (GodScript.TotalScore < upgradeCosts[1, 2]) return;

        GodScript.MaxJumpVelocity += 1.5f;

        UpdateStats(1, 2);
        buttonArray[1, 2].GetComponent<Button>().interactable = false;

        UpdateJumpDescriptions();

    }

    public void UpgradeFly1()
    {
        if (GodScript.TotalScore < upgradeCosts[2, 0]) return;

        GodScript.FlySeconds += 0.2f;

        UpdateStats(2, 0);
        upgradeTextObjects[2, 0].GetComponent<Text>().text = GetFlyStrings()[0];
    }

    public void UpgradeFly2()
    {
        if (GodScript.TotalScore < upgradeCosts[2, 1]) return;

        GodScript.FlySpeedUpgrade = true;

        UpdateStats(2, 1);        
        buttonArray[2, 1].GetComponent<Button>().interactable = false;
    }

    public void UpgradeFly3()
    {
        if (GodScript.TotalScore < upgradeCosts[2, 2]) return;

        GodScript.FlyGroundUpgrade = true;

        UpdateStats(2, 2);        
        buttonArray[2, 2].GetComponent<Button>().interactable = false;
    }

    public void UpgradeHeart()
    {
        if (GodScript.TotalScore < heartCost) return;
        if(GodScript.Lives == 10)
        {
            heartText.text = "MAX!";
            return;
        }

        GodScript.Lives++;
        heartText.text = GodScript.Lives.ToString();

        UpdateStats(heartCost);
    }

    public void UpgradeFlag()
    {
        if (GodScript.TotalScore < flagCost) return;
        if (GodScript.FlagSaveInterval == 5)
        {
            flagText.text = "MIN!";
            return;
        }

        GodScript.FlagSaveInterval--;
        flagText.text = GodScript.FlagSaveInterval.ToString();

        UpdateStats(flagCost);
    }


    void UpdateStats(int row, int col)
    {
        if (GodScript.SFX) buttonClick.Play();

        GodScript.TotalSpent += upgradeCosts[row, col];
        GodScript.TotalScore -= upgradeCosts[row, col];
        GodScript.Level++;

        totalSpentText.text = GodScript.TotalSpent.ToString();
        currentMoneyText.text = GodScript.TotalScore.ToString();
        levelText.text = GodScript.Level.ToString();
    }

    void UpdateStats(int num)
    {
        if (GodScript.SFX) buttonClick.Play();

        GodScript.TotalSpent += num;
        GodScript.TotalScore -= num;
        GodScript.Level++;

        totalSpentText.text = GodScript.TotalSpent.ToString();
        currentMoneyText.text = GodScript.TotalScore.ToString();
        levelText.text = GodScript.Level.ToString();
    }

    public void GoToGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    string[] GetSwimStrings()
    {
        return new string[]{String.Format("Increase under-water speed by {0} (current: {1}).\r\nCost: {2}", swimUpgrade1, GodScript.WaterSpeed, upgradeCosts[0, 0]),
                            String.Format("Add +1 second of air underwater (current: {0}).\r\nCost: {1}", (Math.Round(GodScript.BreathMax / GodScript.WaterSeconds, 2)), upgradeCosts[0, 1]),
                            String.Format("Double points underwater.\r\nCost: " + upgradeCosts[0, 2])};

    }

    string[] GetJumpStrings()
    {
        return new string[]{"Jump height + 1 (current: " + GodScript.MaxJumpVelocity.ToString() + ").\r\nCost: " + upgradeCosts[1, 0],
                            "Jump height + 1 (current: " + GodScript.MaxJumpVelocity.ToString() + ").\r\nCost: " + upgradeCosts[1, 1],
                            "Jump height + 1.5 (current: " + GodScript.MaxJumpVelocity.ToString() + ").\r\nCost: " + upgradeCosts[1, 2] };
    }

    void UpdateJumpDescriptions()
    {
        string[] temp = GetJumpStrings();

        upgradeTextObjects[1, 0].GetComponent<Text>().text = temp[0];
        upgradeTextObjects[1, 1].GetComponent<Text>().text = temp[1];
        upgradeTextObjects[1, 2].GetComponent<Text>().text = temp[2];
    }

    string[] GetFlyStrings()
    {
        return new string[]{
            String.Format("Add 0.2 seconds to fly bar (current: {0}s).\r\nCost: {1}", (GodScript.FlyDistanceMax / GodScript.FlySeconds).ToString(), upgradeCosts[2, 0]),
            String.Format("While flying, gain double movement speed.\r\nCost: " + upgradeCosts[2, 1]),
            String.Format("Fly even off the ground.\r\nCost: " + upgradeCosts[2, 2])};
    }
	
	public void Demo()
	{
		GodScript.TotalScore += 1000;
		
		totalSpentText.text = GodScript.TotalSpent.ToString();
        currentMoneyText.text = GodScript.TotalScore.ToString();
        levelText.text = GodScript.Level.ToString();
	}
   

}
