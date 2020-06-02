using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaSpawner : MonoBehaviour
{
    public GameObject Sea;
    private Transform playerTransform;

    public float SpawnZ;
    float SeaSize;
	
	float SeaPositionY;
    private List<GameObject> seaList;
    // Start is called before the first frame update
    void Start()
    {
        seaList = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //Sea = GameObject.FindGameObjectWithTag("Sea");
		
		SeaPositionY = Sea.transform.position.y;
		SeaSize = Sea.GetComponent<Renderer>().bounds.size.z;

        SpawnZ = SeaSize * 1.4f;
    }

    // Update is called once per frame
    void Update()
    {
        //Sea size is 150
        if (playerTransform.position.z > SpawnZ - SeaSize * 1.5f) Spawn();
        Delete();
    }

    void Spawn()
    {
        GameObject ob = Instantiate(Sea);
        //ob.transform.SetParent(transform);

        ob.transform.position = (Vector3.forward * SpawnZ) + (Vector3.up * SeaPositionY);
        SpawnZ += (int)SeaSize;
        seaList.Add(ob);

    }

    void Delete()
    {
        if(seaList.Count > 3)
        {
            Destroy(seaList[0]);
            seaList.RemoveAt(0);
        }
    }

    public float GetLastSeaZ()
    {
        /*if (seaList.Count == 1) return seaList[0].transform.position.z;
        else if (seaList.Count == 0) return 0;
        return seaList[seaList.Count - 1].transform.position.z;*/

        return SpawnZ / 1.4f - SeaSize;
    }

}
