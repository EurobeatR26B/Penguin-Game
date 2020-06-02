using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloeSpawnerManager : MonoBehaviour
{
    public GameObject[] spawnList;
    public GameObject objectFlag;
    public Material flagFloe;

    GameObject Sea;
    GodScript God;

    private Transform playerTransform;
    private int maxFloes = 16;

    public float spawnZ = 60f;
    public float floeLength = 50f;
	private float safeZone = 100f;
	private List<GameObject> activeFloes;

    private float seaSizeY;
    private float seaTop;


    private List<GameObject> activeFlags;
    private bool isSpawnFlag = false;
    private int FloesJumped;
    private int FlagInterval;
    private float LastFlagZ;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Sea = GameObject.FindGameObjectWithTag("Sea");
        God = GameObject.FindGameObjectWithTag("God").GetComponent<GodScript>();

        FlagInterval = God.FlagSaveInterval;

        seaSizeY = Sea.GetComponent<Renderer>().bounds.size.y / 2;
        seaTop = Sea.transform.position.y + seaSizeY;
		
		activeFloes = new List<GameObject>();
        activeFlags = new List<GameObject>();

        SpawnFloeNormal();
        SpawnFloeNormal();
        SpawnFloeNormal();
        SpawnFloeNormal();

        LastFlagZ = -100;

    }

    // Update is called once per frame
    void Update()
    {
        //UnityEngine.Random.seed = Time.frameCount;
        //if (playerTransform.position.z > (spawnZ - (maxFloes * floeLength * 4f))) SpawnFloeBurst();
        float spawnerPos = (spawnZ - (maxFloes * floeLength));

        if (playerTransform.position.z > spawnerPos)
        {
            int SpawnChoice = UnityEngine.Random.Range(1, 15);

            if (SpawnChoice < 2) SpawnFloeStairs();
            else if (SpawnChoice < 5) SpawnFloeBurst();
            else SpawnFloeNormal();

        }

        //Every FLAGINTERVAL floes spawn a save point
        FloesJumped = God.FloesJumped;                
        if(FloesJumped != 0 && FloesJumped == FlagInterval) SpawnFlag();
        

        DeleteFloe();
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

		activeFloes.Add(ob);

        spawnZ += floeLength * UnityEngine.Random.Range(2f, 2.5f);
    }

    /*private void SpawnAnotherFloe(Vector3 newPos, Vector3 newScale)
    {
        int[] zValues = { -15, -10, 10, 15 };
        int newZ = UnityEngine.Random.Range(0, 3);
        GameObject ob = Instantiate(spawnList[UnityEngine.Random.Range(0, 3)]) as GameObject;



        ob.transform.localScale = newScale;
        ob.transform.position = new Vector3(newPos.x * -1f, newPos.y, newPos.z + zValues[newZ]);
    }*/

    private void SpawnFloeAtLocation(float locationX, float locationY, float locationZ, GameObject ob)
    {
        Instantiate(ob);
        ob.transform.position = new Vector3(locationX, locationY, locationZ);
		
		activeFloes.Add(ob);
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

            ob.transform.position = new Vector3(0, seaTop - 3.5f, spawnZ + ((i * 30) + (i * 2f)));
            spawnZ += floeLength * 2f;
			
			activeFloes.Add(ob);
        }

        spawnZ += 30 * (count - 1);

    }

    private void SpawnFloeStairs ()
    {        
        int count = UnityEngine.Random.Range(3, 7);

        //Keep all this in the loop, as otherwise they will not be DeleteFloe()ed
        for (int i = 0; i < count; i++)
        {
            GameObject ob = Instantiate(spawnList[UnityEngine.Random.Range(1, 3)]) as GameObject;

            Vector3 oldScale = ob.transform.localScale;
            ob.transform.localScale = new Vector3(oldScale.x, oldScale.y * 15, oldScale.z);

            SpawnFloeAtLocation(0, seaTop - 20 + (i * 30), spawnZ + (i * 35), ob);
        }

        spawnZ += count * 50f;
    }

    private void SpawnFlag()
    {
        Vector3 pos = activeFloes[activeFloes.Count - 1].transform.position + Vector3.up * 4f;
        activeFloes[activeFloes.Count - 1].transform.GetComponent<MeshRenderer>().material = flagFloe;

        GameObject flag = Instantiate(objectFlag) as GameObject;
        flag.transform.position = pos;

        flag.transform.SetParent(activeFloes[activeFloes.Count - 1].transform);

        if (activeFlags.Count < 2) activeFlags.Add(flag);
        else
        {
            activeFlags[0] = activeFlags[1];
            activeFlags[1] = flag;
        }

        God.FloesJumped = 0;
    }
	
	private void DeleteFloe()
	{
        /*float oldFlagZ = activeFlags[0].transform.position.z;
        float newFlagZ = activeFlags[1].transform.position.z;*/
        if (LastFlagZ > activeFloes[0].transform.position.z + safeZone || playerTransform.position.z - floeLength * 30 > activeFloes[0].transform.position.z)
        {
            Destroy(activeFloes[0]);
            activeFloes.RemoveAt(0);
        }
	}
}
