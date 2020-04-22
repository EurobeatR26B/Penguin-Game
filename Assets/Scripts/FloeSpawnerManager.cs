using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloeSpawnerManager : MonoBehaviour
{
    public GameObject[] spawnList;

    GameObject Sea;

    private Transform playerTransform;
    private int maxFloes = 3;

    public float spawnZ = 60f;
    public float floeLength = 50f;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Sea = GameObject.FindGameObjectWithTag("Sea");


        SpawnFloe();
        SpawnFloe();
        SpawnFloe();
    }

    // Update is called once per frame
    void Update()
    {
        Random.seed = Time.frameCount;
        if (playerTransform.position.z > (spawnZ - (maxFloes * floeLength * 4f))) SpawnFloe();

    }

    private void SpawnFloe()
    {
        float seaSizeY = Sea.GetComponent<Renderer>().bounds.size.y / 2;

        GameObject ob = Instantiate(spawnList[Random.Range(0, 3)]) as GameObject;
        //ob.transform.SetParent(transform);


        //ROTATION
        //X - sideways, Y - axis (the one you need), Z - same as Y, ignore
        // 5/10 chance to rotate the floe
        if (Random.Range(1, 10) >= 6)
            ob.transform.rotation = Quaternion.Euler(Random.Range(-15, 15), Random.Range(0, 360), 0);

        //Y AND X POSITIONS
        float height = Random.Range(1, 35);
        float side = Random.Range(-40, 40);


        float obSizeY = ob.GetComponent<Renderer>().bounds.size.y / 2;
        float newScaleY = 1f;

        float seaTop = Sea.transform.position.y + seaSizeY;
        float heightDifference = (height - obSizeY) - seaTop;
        
        if (height - obSizeY > seaTop)
        {
            newScaleY = heightDifference / obSizeY;
            if (newScaleY < 1) newScaleY = 1;

            height = (seaTop) - (obSizeY * newScaleY);
        }

        Vector3 defaultScale = ob.transform.localScale;

        float xzScale = Random.Range(0.8f, 1.2f);

        Vector3 newScale = new Vector3(xzScale * defaultScale.x, defaultScale.y * newScaleY, xzScale * defaultScale.x);
        Vector3 newPos = new Vector3(side, height, spawnZ);

        ob.transform.localScale = newScale;

        obSizeY = ob.GetComponent<Renderer>().bounds.size.y / 2;
        //if (height - obSizeY > seaTop) ob.transform.position = new Vector3(side, height + obSizeY, spawnZ);
        ob.transform.position = newPos;

        spawnZ += floeLength * 2f;

        Debug.Log("Floe height:" + height + " --- " + obSizeY);
    }
}
