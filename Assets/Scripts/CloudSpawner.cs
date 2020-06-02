using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject[] cloudList;

    private List<GameObject> activeClouds;
    private GameObject Player;
    private float playerZ;

    float spawnZ;

    // Start is called before the first frame update
    void Start()
    {
        activeClouds = new List<GameObject>();

        Player = GameObject.FindGameObjectWithTag("Player");
        spawnZ = 0;
    }

    // Update is called once per frame
    void Update()
    {
        playerZ = Player.transform.position.z;

        if (activeClouds.Count <= 10) Spawn();
        Delete();
    }

    void Spawn()
    {
        GameObject cloud = Instantiate(cloudList[UnityEngine.Random.Range(0, cloudList.Length - 1)]) as GameObject;

        float posZ = UnityEngine.Random.Range(spawnZ, spawnZ + 30);
        float posY = UnityEngine.Random.Range(100f, 140f);

        cloud.transform.position = new Vector3(-155, posY, posZ);


        activeClouds.Add(cloud);
        spawnZ += 70;
    }

    void Delete()
    {
        if(activeClouds[0].transform.position.z + 200 < playerZ)
        {
            Destroy(activeClouds[0]);
            activeClouds.RemoveAt(0);
        }
    }
}
