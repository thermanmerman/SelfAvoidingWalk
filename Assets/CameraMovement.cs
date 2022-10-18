using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject startingCube;
    public float speed = 1.2f;
    Walker walker;
    void Start()
    {
        walker = startingCube.GetComponent<Walker>();
        
        
    }

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
        transform.LookAt(startingCube.transform, Vector3.up);

        Camera.main.farClipPlane = walker.width*8;
        Camera.main.nearClipPlane = -walker.width*8;
        Camera.main.orthographicSize = walker.width*1.2f;
        speed = walker.width/2;
        /*
        Vector3 pos = transform.position;
        pos.z = -walker.width;
        transform.position = pos;*/
    }
}
