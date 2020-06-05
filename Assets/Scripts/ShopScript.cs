using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public AudioSource slowMenu;
    public AudioSource buttonClick;

    float speed;

    int[] itemLevels;
    List<string> clothes;

    GameObject[] buttons;
    Dictionary<string, Transform> PenguinChildren;

    //0 - hat
    //1 - glasses
    Transform[] clothingToBePassed;

    public GameObject Penguin;



    Color activeGreen = new Color(155, 255, 155, 155);
    static bool isRotateAuto;   

    // Start is called before the first frame update
    void Start()
    {
        PenguinChildren = new Dictionary<string, Transform>();
        clothingToBePassed = new Transform[2];
        itemLevels = new int[]{ 10, 15, 0};
        buttons = new GameObject[3];
        
        LoadText();
        LoadClothingNames();
        LoadButtons();
        GetClothing();

        isRotateAuto = true;
        speed = 800;

        if (GodScript.Music) slowMenu.Play();

        string hat = GodScript.Hat;
        string glasses = GodScript.Glasses;

        if (hat != "none") PenguinChildren[hat].gameObject.SetActive(true);
        if (glasses != "none") PenguinChildren[glasses].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(isRotateAuto) Penguin.transform.Rotate(0, speed * 0.03f * Time.deltaTime, 0);

        if (Input.GetMouseButton(0))
        {
            Penguin.transform.Rotate(0, speed * Time.deltaTime * -Input.GetAxis("Mouse X"), 0);
            isRotateAuto = false;
        }

    }

    void LoadText()
    {
        GameObject.FindGameObjectWithTag("itemLabel1").GetComponent<UnityEngine.UI.Text>().text = "Level " + itemLevels[0];
        GameObject.FindGameObjectWithTag("itemLabel2").GetComponent<UnityEngine.UI.Text>().text = "Level " + itemLevels[1];
        GameObject.FindGameObjectWithTag("labelMoney").GetComponent<UnityEngine.UI.Text>().text = GodScript.TotalScore.ToString();
    }

    void LoadButtons()
    {
        buttons[0] = GameObject.FindGameObjectWithTag("buttonItem1");
        buttons[1] = GameObject.FindGameObjectWithTag("buttonItem2");
        buttons[2] = GameObject.FindGameObjectWithTag("buttonItem3");
    }

    void LoadClothingNames()
    {
        clothes = new List<string>();

        clothes.Add("glasses");
        clothes.Add("fullcap");
        clothes.Add("ziemine");
    }

    public void RotateAuto()
    {
        if (GodScript.SFX) buttonClick.Play();
        isRotateAuto = !isRotateAuto;
    }

    public void GoToUpgrades()
    {
        if (GodScript.SFX) buttonClick.Play();
        PassClothingToGod();
        UnityEngine.SceneManagement.SceneManager.LoadScene("UpgradeMenu");
    }

    public void GoToMain()
    {
        if (GodScript.SFX) buttonClick.Play();
        PassClothingToGod();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void Button1()
    {
        if (GodScript.Level < itemLevels[0]) return;
        if (GodScript.SFX) buttonClick.Play();

        PenguinChildren["ziemine"].gameObject.SetActive(false);
        PenguinChildren["fullcap"].gameObject.SetActive(true);

        clothingToBePassed[0] = PenguinChildren["fullcap"];

        isRotateAuto = true;
    }

    public void Button2()
    {
        if (GodScript.Level < itemLevels[1]) return;
        if (GodScript.SFX) buttonClick.Play();

        clothingToBePassed[1] = PenguinChildren["glasses"];
        clothingToBePassed[1].gameObject.SetActive(true);

        isRotateAuto = true;
    }

    public void Button3()
    {
        if (GodScript.SFX) buttonClick.Play();

        PenguinChildren["fullcap"].gameObject.SetActive(false);
        PenguinChildren["ziemine"].gameObject.SetActive(true);


        clothingToBePassed[0] = PenguinChildren["ziemine"];

        isRotateAuto = true;
    }

    public void ButtonClear()
    {
        if (GodScript.SFX) buttonClick.Play();
        foreach (KeyValuePair<string, Transform> kvp in PenguinChildren)
        {
            kvp.Value.gameObject.SetActive(false);
        }

        isRotateAuto = true;
    }

    void GetClothing()
    {
        foreach(Transform child in Penguin.transform)
        {
            if(clothes.Contains(child.name))
            {
                PenguinChildren.Add(child.name, child);
            }
        }
    }

    void PassClothingToGod()
    {
        if(clothingToBePassed[0] != null) GodScript.Hat = clothingToBePassed[0].name;
        if (clothingToBePassed[1] != null) GodScript.Glasses = clothingToBePassed[1].name;
    }

}
