using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloeSpawnerManager : MonoBehaviour
{
    public GameObject[] spawnList;

    GameObject Sea;

    private Transform playerTransform;
    private int maxFloes = 3;

    public float spawnZ = 0f;
    public float floeLength = 60f;

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
        if (playerTransform.position.z > (spawnZ - (maxFloes * floeLength * 4))) SpawnFloe();

    }

    private void SpawnFloe (int index = -1)
    {
        GameObject ob = Instantiate(spawnList[Random.Range(0, 2)]) as GameObject;
        ob.transform.SetParent(transform);

        //-------------------ROTATION
        //X - sideways, Y - axis (the one you need), Z - same as Y, ignore
        if (Random.Range(1, 10) >= 5)
        ob.transform.rotation = Quaternion.Euler(Random.Range(75, 105), Random.Range(0, 360), 0);

        //-------------------HEIGHT
        float height = Random.Range(15, 30);
        float side = Random.Range(-80, 80);

        if(ob.transform.position.y > Sea.transform.position.y)
        {
            ob.transform.localScale = new Vector3 (1f, 1f, 15f);
        }

        //-------------------SPAWN
        if(side > 30 || side < -30)
        {
            GameObject ob2 = Instantiate(spawnList[Random.Range(0, 2)]) as GameObject;
            ob2.transform.localScale = new Vector3(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), 1f);
            ob2.transform.position = (Vector3.forward * spawnZ) + (Vector3.up * Random.Range(15, 30) + (-Vector3.right * side));
        }

        ob.transform.localScale = new Vector3(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), 1f);
        ob.transform.position = (Vector3.forward * spawnZ) + (Vector3.up * height) + (Vector3.right * side);
        spawnZ += floeLength * 2f;
    }
}
