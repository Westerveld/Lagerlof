using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public GameObject[] platformPrefabs;
    public static float gameSpeed = 1;
    int platformHeight = 10;
    int level = 0;
    
	// Use this for initialization
	void Start () {
        List<GameObject> spawnedPlatforms = new List<GameObject>();
 
    }

    // Update is called once per frame
    void Update () {
     //   UpdateCameraPosition();
        
        if (Camera.main.transform.position.y >= level*platformHeight)
        {
            level++;
            SpawnNewLevel();
               
        }
        

	}


   void SpawnNewLevel()
    {
       
        Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Length-1)], new Vector3(0, platformHeight * level, 0), Quaternion.identity);

    }

    void closeWall()
    {

    }

    void SpawnWalls(Vector2 pos)
    {
    }

}
