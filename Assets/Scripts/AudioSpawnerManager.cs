using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSpawnerManager : MonoBehaviour
{
    public AudioSource background;
    public AudioSource points;
    public AudioSource fishPoints;
    public AudioSource underwater;
    public AudioSource splash;
    public AudioSource flag;

    // Start is called before the first frame update
    void Start()
    {
        if (GodScript.Music) background.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartUnderwater()
    {
        underwater.Play();
        background.volume *= 0.15f;
    }

    public void PauseUnderwater()
    {
        underwater.Pause();
        background.volume /= 0.15f;
    }

    public void PlayPoints()
    {
        if (GodScript.SFX) points.Play();
    }

    public void PlayFishPoints()
    {
        if (GodScript.SFX) fishPoints.Play();
    }

    public void PlaySplash()
    {
        if (GodScript.SFX) splash.Play();
    }

    public void PlayFlag()
    {
        if (GodScript.SFX) flag.Play();
    }
}
