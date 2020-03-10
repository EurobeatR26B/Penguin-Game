using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloeSpawnerManager : MonoBehaviour
{

    public GameObject[] spawnList;

    private Transform playerTransform;
    private int maxFloes = 3;
    public float spawnZ = 0f;
    public float floeLength = 60f;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        SpawnFloe();
        SpawnFloe();
        SpawnFloe();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z > (spawnZ - maxFloes * floeLength * 4)) SpawnFloe();

    }

    private void SpawnFloe (int index = -1)
    {
        GameObject ob = Instantiate(spawnList[0]) as GameObject;
        ob.transform.SetParent(transform);
        ob.transform.position = Vector3.forward * spawnZ + Vector3.up * 10f;
        spawnZ += floeLength * 5;
    }
}
