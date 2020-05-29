using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FlightMeterScript : MonoBehaviour
{
    private Slider sliderInstance;
    private PlayerMovement movementScript;
    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponent<PlayerMovement>();
        sliderInstance.maxValue = movementScript.FlyDistanceMax;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateValue(movementScript.FlyDistanceCurrent);
    }

    public void UpdateValue (float value)
    {
        if (value == -1) sliderInstance.value = 0;

        sliderInstance.value = sliderInstance.maxValue - value;
    }
}
