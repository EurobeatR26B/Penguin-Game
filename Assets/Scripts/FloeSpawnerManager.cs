using System;
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
        UnityEngine.Random.seed = Time.frameCount;
        if (playerTransform.position.z > (spawnZ - (maxFloes * floeLength * 4f))) SpawnFloe();

    }

    private void SpawnFloe()
    {
        GameObject ob = Instantiate(spawnList[UnityEngine.Random.Range(0, 3)]) as GameObject;

        float seaSizeY = Sea.GetComponent<Renderer>().bounds.size.y / 2;
        float seaTop = Sea.transform.position.y + seaSizeY;
        
        float side = UnityEngine.Random.Range(-40, 40);
        float xzScale = UnityEngine.Random.Range(1f, 1.3f);
        Vector3 defaultScale = ob.transform.localScale;

        //ROTATION
        //X - sideways, Y - axis (the one you need), Z - same as Y, ignore
        // 5/10 chance to rotate the floe
        if (UnityEngine.Random.Range(1, 10) >= 6)
            ob.transform.rotation = Quaternion.Euler(UnityEngine.Random.Range(-15, 15), UnityEngine.Random.Range(0, 360), 0);

        Vector3 newScale = new Vector3(xzScale * defaultScale.x, defaultScale.y * UnityEngine.Random.Range(3f, 4.5f), xzScale * defaultScale.x);
        Vector3 newPos = new Vector3(side, seaTop - 1.5f, spawnZ);

        ob.transform.localScale = newScale;
        ob.transform.position = newPos;

        //if (side < -30 || side > 30) spawnAnotherFloe(newPos, newScale);

        spawnZ += floeLength * UnityEngine.Random.Range(2f, 2.5f);
    }

    private void spawnAnotherFloe(Vector3 newPos, Vector3 newScale)
    {
        int[] zValues = {-15, -10, 10, 15};
        int newZ = UnityEngine.Random.Range(0, 3);
        GameObject ob = Instantiate(spawnList[UnityEngine.Random.Range(0, 3)]) as GameObject;



        ob.transform.localScale = newScale;
        ob.transform.position = new Vector3(newPos.x * -1f, newPos.y, newPos.z + zValues[newZ]);
    }
    /* OLD SPAWNFLOE()
*private void SpawnFloe()
{
   GameObject ob = Instantiate(spawnList[Random.Range(0, 3)]) as GameObject;

   float seaSizeY = Sea.GetComponent<Renderer>().bounds.size.y / 2;
   float seaTop = Sea.transform.position.y + seaSizeY;
   float height = Random.Range(1, 35);
   float side = Random.Range(-40, 40);
   float xzScale = Random.Range(1f, 1.4f);
   Vector3 defaultScale = ob.transform.localScale;


   //ob.transform.SetParent(transform);


   //ROTATION
   //X - sideways, Y - axis (the one you need), Z - same as Y, ignore
   // 5/10 chance to rotate the floe
   if (Random.Range(1, 10) >= 6)
       ob.transform.rotation = Quaternion.Euler(Random.Range(-15, 15), Random.Range(0, 360), 0);

   //Y AND X POSITIONS


   /*
   float obSizeY = ob.GetComponent<Renderer>().bounds.size.y / 2;
   float newScaleY = 1f;

   float heightDifference = (height - obSizeY) - seaTop;

   if (height - obSizeY > seaTop)
   {
       newScaleY = heightDifference / obSizeY;
       if (newScaleY < 1) newScaleY = 1;

       height = (seaTop) - (obSizeY * newScaleY);
   }


Vector3 newPos = new Vector3(side, seaTop - 0.5f, spawnZ);
Vector3 newScale = new Vector3(xzScale * defaultScale.x, defaultScale.y * Random.Range(3f, 4.5f), xzScale * defaultScale.x);

ob.transform.localScale = newScale;

   //obSizeY = ob.GetComponent<Renderer>().bounds.size.y / 2;
   //if (height - obSizeY > seaTop) ob.transform.position = new Vector3(side, height + obSizeY, spawnZ);
   ob.transform.position = newPos;

   spawnZ += floeLength* Random.Range(2f, 3f);
}*/
}
