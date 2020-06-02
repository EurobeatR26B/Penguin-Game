using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BreathMeterScript : MonoBehaviour
{
    private Slider sliderInstance;
    private GodScript God;
    private PlayerMovement Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<PlayerMovement>();
        God = GetComponent<GodScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateValue(Player.BreathCurrent);
    }

    public void UpdateValue (float value)
    {
        //if (value == -1) sliderInstance.value = 0;

        sliderInstance.value = sliderInstance.maxValue - value;
    }
}
