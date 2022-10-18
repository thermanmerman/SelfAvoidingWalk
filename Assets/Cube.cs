using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube 
{
    public Vector3 position;
    public GameObject cubeObj;
    public Cube(Vector3 pos, GameObject obj)
    {
        position = pos;
        cubeObj = obj;
    }
}
