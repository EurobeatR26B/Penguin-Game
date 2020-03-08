using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform follow;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        follow = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - follow.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = follow.position + offset;
    }
}
