using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GodScript God;

    private Transform follow;
    private Vector3 offset;

    public GameObject life;
    public int lifeCount;
    private List<GameObject> activeLives;

    // Start is called before the first frame update
    void Start()
    {
        activeLives = new List<GameObject>();
        God = GameObject.FindGameObjectWithTag("God").GetComponent<GodScript>();
        follow = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - follow.position;


        lifeCount = 3;
        //Debug.Log("GODLIVES: " + God.Lives.ToString());
        float addXpos = 0;
        for (int i = 0; i < lifeCount; i++)
        {
            GameObject ob = Instantiate(life) as GameObject;

            ob.transform.position = transform.position;

            Vector3 lifePos = new Vector3(-42f + addXpos, 75f, -15f);
            ob.transform.position = lifePos;

            addXpos += 8;

            ob.transform.SetParent(transform);
            activeLives.Add(ob);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = follow.position + offset;        
    }

    public void ReduceLife()
    {
        lifeCount--;
        God.Lives--;
        Destroy(activeLives[activeLives.Count - 1]);
        activeLives.RemoveAt(0);
    }
}
