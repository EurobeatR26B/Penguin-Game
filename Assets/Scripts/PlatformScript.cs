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
        

        Vector3 transformPos = transform.position;

        //Calculating how far up the snowflake should be lifted
        float floeY = transform.GetComponent<Renderer>().bounds.size.y;
        float yCoef = UnityEngine.Random.Range(1f, 1.4f);

        if (isTilted) yCoef = 0.8f;

        


        flake.transform.position = transformPos + (Vector3.up * floeY * yCoef);//(Vector3.up * floeY * yCoef);

        flake.transform.SetParent(transform);
        //flake.transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
