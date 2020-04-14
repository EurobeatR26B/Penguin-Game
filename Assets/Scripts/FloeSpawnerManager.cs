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

    private void SpawnFloe ()
    {
        GameObject ob = Instantiate(spawnList[Random.Range(0, 2)]) as GameObject;
        ob.transform.SetParent(transform);

        float SeaY = Sea.transform.position.y;
        float obY = ob.GetComponent<Renderer>().bounds.size.y;

        
        //ROTATION
        //X - sideways, Y - axis (the one you need), Z - same as Y, ignore
        // 5/10 chance to rotate the floe
        if (Random.Range(1, 10) >= 5)
        ob.transform.rotation = Quaternion.Euler(Random.Range(75, 105), Random.Range(0, 360), 0);

        //Y AND X POSITIONS
        float height = Random.Range(10, 22);
        float side = Random.Range(-40, 40);

        float obZscale = Random.Range(0.7f, 1.4f);

        float posY = 5;
        if (obZscale > 1.2) posY = -0.5f;

        Vector3 newScale = new Vector3(Random.Range(0.8f, 1.2f), Random.Range(0.7f, 1.4f), obZscale);
        Vector3 newPos = new Vector3(side, posY, spawnZ);

        ob.transform.localScale = newScale;
        ob.transform.position = newPos;
        



        spawnZ += floeLength * 2f;
    }
}
