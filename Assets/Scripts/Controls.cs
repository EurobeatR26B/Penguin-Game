using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public AudioSource music;
    public AudioSource buttonClick;

    // Start is called before the first frame update
    void Start()
    {
        AudioListener.pause = GodScript.AudioPause;
        if (GodScript.Music) SetupMusic();

        //if (PlayerPrefs.GetInt("Level") > GodScript.Level) GodScript.LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToGame()
    {
        if (GodScript.SFX) buttonClick.Play();
        SceneManager.LoadScene("Scene");
    }

    public void ToShop()
    {
        if (GodScript.SFX) buttonClick.Play();
        SceneManager.LoadScene("Store");
    }

    public void ToOptions()
    {
        if (GodScript.SFX) buttonClick.Play();
        SceneManager.LoadScene("Options");
    }

    public void Exit()
    {
        if (GodScript.SFX) buttonClick.Play();

        //GodScript.SaveData();
        Application.Quit(0);
    }


    public void Load()
    {
        if (GodScript.SFX) buttonClick.Play();
        GodScript.LoadData();
    }

    public void Save()
    {
        if (GodScript.SFX) buttonClick.Play();
        GodScript.SaveData();
    }

    void SetupMusic()
    {
        music.Play();
        music.loop = true;
        music.volume = 0.2f;
    }

    
}
