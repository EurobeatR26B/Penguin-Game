using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiScript : MonoBehaviour
{
    private float moveX;
    private float moveY;
    private float moveZ;

    private float SeaZ;
    public int FishCount { get; set; }
    public float lerpSpeed;
    public bool VectorSame;
    Vector3 nextPos;
    public bool started;
    // Start is called before the first frame update
    void Start()
    {
        lerpSpeed = 1f;

        Random.seed = FishCount;
        UpdateSeaZ();
        GenerateNextPosition();
    }

    // Update is called once per frame
    void Update()
    {
        VectorSame = CompareVectors(transform.position, nextPos);
        if (VectorSame)
        {
            GenerateNextPosition();
        }
        else
        {
            Move(nextPos);
        }
    }

    void Move(Vector3 newPos)
    {
        transform.position = Vector3.Lerp(transform.position, newPos, lerpSpeed * Time.deltaTime);
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newPos), 0.5f);
    }

    public void GenerateNextPosition()
    {
        //moveX = UnityEngine.Random.Range(-0.4f, 0.45f);

        //UnityEngine.Random.seed = (int)System.DateTime.Now.Ticks;
        //moveY = UnityEngine.Random.Range(-0.4f, 0.43f);

        //UnityEngine.Random.seed = (int)System.DateTime.Now.Ticks;
        //moveZ = UnityEngine.Random.Range(-0.5f, 0.5f);

        moveX = UnityEngine.Random.Range(-60f, 60);
        moveY = UnityEngine.Random.Range(-78f, -30f);
        moveZ = UnityEngine.Random.Range(SeaZ + 18f, SeaZ + 850f);

        nextPos = new Vector3(moveX, moveY, moveZ);
    }

    //True if same, false if not
    bool CompareVectors(Vector3 A, Vector3 B)
    {
        return Mathf.Abs(A.x - B.x) <= 7 && Mathf.Abs(A.y - B.y) <= 7 && Mathf.Abs(A.z - B.z) <= 7;
    }

    void UpdateSeaZ()
    {
        SeaZ = GameObject.FindGameObjectWithTag("SeaSpawnerObject").GetComponent<SeaSpawner>().GetLastSeaZ();
    }
}
