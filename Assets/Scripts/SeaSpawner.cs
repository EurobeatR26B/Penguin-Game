using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaSpawner : MonoBehaviour
{
    private GameObject Sea;
    private Transform playerTransform;

    public int SpawnZ = 150;
    public int SeaSize = 150;

    private List<GameObject> seaList;
    // Start is called before the first frame update
    void Start()
    {
        seaList = new List<GameObject>();

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

        ob.transform.position = (Vector3.forward * SpawnZ) + (Vector3.up * Sea.transform.position.y);
        SpawnZ += 150;
        Debug.Log("Spawned");
    }

    void Delete()
    {
        if(SpawnZ + 450 > seaList[0].transform.position.z)
        {
            Destroy(seaList[0]);
            seaList.RemoveAt(0);
        }
    }
}
