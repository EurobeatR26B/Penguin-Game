using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = UnityEngine.Random.Range(0.1f, 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * moveSpeed;

        if (transform.position.x >= 200 || transform.position.x <= -200) moveSpeed *= -1;
    }
}
