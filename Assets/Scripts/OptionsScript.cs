using UnityEngine;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{
    public Text textSFX;
    public Text textMusic;
    public Text textMute;
    // Start is called before the first frame update
    void Start()
    {
        if (GodScript.SFX) textSFX.text = "ON";
        if (GodScript.Music) textMusic.text = "ON";

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleSFX()
    {
        GodScript.SFX = !GodScript.SFX;

        string a;
        a = GodScript.SFX == true ? "ON" : "OFF";

        textSFX.text = a;

    }

    public void ToggleMusic()
    {
        GodScript.Music = !GodScript.Music;

        string a;
        a = GodScript.Music == true ? "ON" : "OFF";

        textMusic.text = a;
    }

    public void ToggleMute()
    {
        GodScript.AudioPause = AudioListener.pause = !AudioListener.pause;

        string a;
        a = AudioListener.pause == true ? "UNMUTE" : "MUTE";

        textMute.text = a;
    }

    public void ToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
