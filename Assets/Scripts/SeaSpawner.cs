using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaSpawner : MonoBehaviour
{
    private GameObject Sea;
    private Transform playerTransform;

    public int SpawnZ = 0;
    public int SeaSize = 150;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Sea = GameObject.FindGameObjectWithTag("Sea");
    }

    // Update is called once per frame
    void Update()
    {
        //Sea size is 150
        if (playerTransform.position.z > SpawnZ - SeaSize * 4) Spawn();
    }

    void Spawn()
    {
        GameObject ob = Instantiate(GameObject.FindGameObjectWithTag("Sea"));
        ob.transform.SetParent(transform);

        ob.transform.position = (Vector3.forward * SpawnZ) + (-Vector3.up * 1f);
        SpawnZ += 150;
        Debug.Log("Spawned");
    }
}
