using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    public GameObject cubePrefab;
    private List<Cube> cubes = new List<Cube>();
    private List<Vector3> directions = new List<Vector3>();
    public int width, length, height;
    private List<List<Cube>> ghostPaths = new List<List<Cube>>();
    Cube currentCube = null;
    private BoxCollider col;
    public int failCount;
    void Start()
    {
        col = GetComponent<BoxCollider>();
        Vector3 sizevec = new Vector3(width, length, height);
        col.size = sizevec;

        Cube startCube = new Cube(Vector3.zero, this.gameObject);
        cubes.Add(startCube);
        directions.Add(new Vector3(1,0,0));
        directions.Add(new Vector3(-1,0,0));
        directions.Add(new Vector3(0,1,0));
        directions.Add(new Vector3(0,-1,0));
        directions.Add(new Vector3(0,0,1));
        directions.Add(new Vector3(0,0,-1));
    }

    void Update()
    {
        failCount = ghostPaths.Count;
        if (cubes.Count == width*length*height)
        {
            Debug.Log("Aw shit he done");
            return;
        }
        if (cubes.Count <= 0)
        {
            currentCube = NewCube(directions[Random.Range(0, directions.Count)]);
            return;
        }
        else
        {
            Vector3 newPos = RandomPosFromCube(cubes[cubes.Count-1]);
            if (CheckBorders(newPos) && CheckPosition(newPos) && CheckGhosts(newPos)) 
            {
                currentCube = NewCube(newPos);
                return;
            }
            else
            {
                foreach(Vector3 direction in directions)
                {
                    if (CheckPosition(direction+newPos) && CheckGhosts(direction+newPos) && CheckBorders(direction+newPos))
                    {
                        currentCube = NewCube(direction+newPos);
                        return;
                    }
                }
                RemoveCube(currentCube);
            }
        }

        currentCube = cubes[cubes.Count-1];

    }

    public Cube NewCube(Vector3 position)
    {
        
        GameObject instance = Instantiate(cubePrefab, position, Quaternion.identity);
        
        Cube cube = new Cube(position, instance);
        cubes.Add(cube);

        return cube;
        
    }
    
    public void RemoveCube(Cube cube)
    {
        cubes.Remove(cube);
        
        ghostPaths.Add(cubes);

        Destroy(cube.cubeObj);
    }

    public Vector3 RandomPosFromCube(Cube cube)
    {
        return cube.position+directions[Random.Range(0, directions.Count)];
    }

    public bool CheckBorders(Vector3 position)
    {
        //return Mathf.Abs(position.x) <= width/3 && Mathf.Abs(position.y) <= height/3 && Mathf.Abs(position.z) <= length/3;
        //return Mathf.Abs(position.x) <= col.size.x && Mathf.Abs(position.y) <= col.size.y && Mathf.Abs(position.z) <= col.size.z;
        return col.bounds.Contains(position);
    }

    public bool CheckPosition(Vector3 position)
    {
        foreach(Cube cube in cubes)
        {
            if (cube.position == position)
                return false;
        }
        return true;
    }

    public bool CheckGhosts(Vector3 cubePos)
    {
        List<Cube> tempList = new List<Cube>();
        foreach(Cube cube in cubes)
        {
            tempList.Add(cube);
        }
        GameObject test = new GameObject("test");
        tempList.Add(new Cube(cubePos, test));
        foreach(List<Cube> path in ghostPaths)
        {
            if (tempList == path)
            {
                Destroy(test);
                return false;
            }
        }
        Destroy(test);
        return true;
    }

    public void Restart()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject obj in objs)
        {
            Destroy(obj);
        }
        cubes.Clear();
        ghostPaths.Clear();

        col = GetComponent<BoxCollider>();
        Vector3 sizevec = new Vector3(width, length, height);
        col.size = sizevec;

        Cube startCube = new Cube(Vector3.zero, this.gameObject);
        cubes.Add(startCube);
    }
}
