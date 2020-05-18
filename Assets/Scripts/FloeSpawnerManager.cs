using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloeSpawnerManager : MonoBehaviour
{
    public GameObject[] spawnList;

    GameObject Sea;

    private Transform playerTransform;
    private int maxFloes = 4;

    public float spawnZ = 60f;
    public float floeLength = 50f;

    private float seaSizeY;
    private float seaTop;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Sea = GameObject.FindGameObjectWithTag("Sea");

        seaSizeY = Sea.GetComponent<Renderer>().bounds.size.y / 2;
        seaTop = Sea.transform.position.y + seaSizeY;
    }

    // Update is called once per frame
    void Update()
    {
        //UnityEngine.Random.seed = Time.frameCount;
        //if (playerTransform.position.z > (spawnZ - (maxFloes * floeLength * 4f))) SpawnFloeBurst();

        if (playerTransform.position.z > (spawnZ - (maxFloes * floeLength * 4f)))
        {
            int SpawnChoice = UnityEngine.Random.Range(1, 15);

            if (SpawnChoice < 2) SpawnFloeStairs();
            else if (SpawnChoice < 5) SpawnFloeBurst();
            else SpawnFloeNormal();
        }

    }

    private void SpawnFloeNormal()
    {
        GameObject ob = Instantiate(spawnList[UnityEngine.Random.Range(0, 3)]) as GameObject;

        float side = UnityEngine.Random.Range(-40, 40);
        float xzScale = UnityEngine.Random.Range(1f, 1.3f);
        Vector3 defaultScale = ob.transform.localScale;

        //ROTATION
        //X - sideways, Y - axis (the one you need), Z - same as Y, ignore
        // 5/10 chance to rotate the floe
        if (UnityEngine.Random.Range(1, 10) >= 6)
        {
            ob.transform.rotation = Quaternion.Euler(UnityEngine.Random.Range(-15, 15), UnityEngine.Random.Range(0, 360), 0);
            ob.GetComponent<PlatformScript>().isTilted = true;
        }

        Vector3 newScale = new Vector3(xzScale * defaultScale.x, defaultScale.y * UnityEngine.Random.Range(3f, 4.5f), xzScale * defaultScale.x);
        Vector3 newPos = new Vector3(side, seaTop - 1.5f, spawnZ);

        ob.transform.localScale = newScale;
        ob.transform.position = newPos;

        //if (side < -30 || side > 30) spawnAnotherFloe(newPos, newScale);

        spawnZ += floeLength * UnityEngine.Random.Range(2f, 2.5f);
    }

    private void SpawnAnotherFloe(Vector3 newPos, Vector3 newScale)
    {
        int[] zValues = { -15, -10, 10, 15 };
        int newZ = UnityEngine.Random.Range(0, 3);
        GameObject ob = Instantiate(spawnList[UnityEngine.Random.Range(0, 3)]) as GameObject;



        ob.transform.localScale = newScale;
        ob.transform.position = new Vector3(newPos.x * -1f, newPos.y, newPos.z + zValues[newZ]);
    }

    private void SpawnFloeAtLocation(float locationX, float locationY, float locationZ, GameObject ob)
    {
        Instantiate(ob);
        ob.transform.position = new Vector3(locationX, locationY, locationZ);
        /*GameObject ob = Instantiate(spawnList[UnityEngine.Random.Range(0, 3)]) as GameObject;

        Vector3 oldScale = ob.transform.localScale;

        ob.transform.localScale = oldScale + (Vector3.up * oldScale.y * 1.5f);

        ob.transform.position = new Vector3(0, seaTop - 3.5f, locationZ);
        spawnZ += floeLength * 2f;*/
    }

    private void SpawnFloeBurst()
    {
        int count = UnityEngine.Random.Range(3, 7);

        for (int i = 0; i < count; i++)
        {
            //pawnFloeAtLocation(spawnZ + ((i * 27) + (i * 2f)));
            GameObject ob = Instantiate(spawnList[UnityEngine.Random.Range(0, 3)]) as GameObject;

            Vector3 oldScale = ob.transform.localScale;

            ob.transform.localScale = oldScale + (Vector3.up * oldScale.y * 1.5f);

            ob.transform.position = new Vector3(0, seaTop - 3.5f, spawnZ + ((i * 27) + (i * 2f)));
            spawnZ += floeLength * 2f;
        }

        spawnZ += 30 * (count - 1);

    }

    private void SpawnFloeStairs ()
    {
        GameObject ob = Instantiate(spawnList[UnityEngine.Random.Range(1, 3)]) as GameObject;
        
        Vector3 oldScale = ob.transform.localScale;
        ob.transform.localScale = new Vector3(oldScale.x, oldScale.y * 15, oldScale.z);

        float startingY = seaTop;


        int count = UnityEngine.Random.Range(3, 6);
        for (int i = 0; i < count; i++)
        {
            SpawnFloeAtLocation(0, seaTop - 20 + (i * 30), spawnZ + (i * 35), ob);
        }

        spawnZ += count * 50f;


    }
}
