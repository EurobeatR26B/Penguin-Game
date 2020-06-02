using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaScript : MonoBehaviour
{
    public GameObject Fish;
	public int FishCount { get; set; }
	float SeaPos;

	void Start()
	{
		FishCount = 0;
		SpawnFish();
		SeaPos = GameObject.FindGameObjectWithTag("SeaSpawnerObject").GetComponent<SeaSpawner>().GetLastSeaZ();
	}
	
	void SpawnFish()
	{	
        int count = UnityEngine.Random.Range(4, 12);

        for (int i = 0; i < count; i++)
        {
            GameObject fish = Instantiate(Fish) as GameObject;
			fish.transform.position = new Vector3 (0, -20, SeaPos);
			//fish.transform.SetParent(transform);

			fish.GetComponent<AudiScript>().FishCount = FishCount;

			FishCount++;
        }
	}
}

