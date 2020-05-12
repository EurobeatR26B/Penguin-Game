using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{

    public GameObject[] ScoreObjects;
    public bool isTilted;
    // Start is called before the first frame update
    void Start()
    {
        if (UnityEngine.Random.Range(1, 10) >= 1) SpawnSnowflake();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnSnowflake()
    {
        GameObject flake = Instantiate(ScoreObjects[0]) as GameObject;
        //flake.transform.SetParent(transform);

        Vector3 transformPos = transform.position;

        //Calculating how far up the snowflake should be lifted
        float floeY = transform.GetComponent<Renderer>().bounds.max.y;
        float yCoef = UnityEngine.Random.Range(2.2f, 3f);

        //If the platform is tilted, the Y placement gets all messed up and the only way of bypassing this I could think of
        //was to just calculate it seperately
        if(isTilted)
        {
            floeY = transform.GetComponent<Renderer>().bounds.center.y;
            yCoef = UnityEngine.Random.Range(-3f, -3.8f);
        }


        flake.transform.position = transformPos + (Vector3.up * floeY * yCoef);
        flake.transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
