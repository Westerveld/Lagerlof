using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public GameObject[] platformPrefabs;
    public GameObject collapsingWall;
    public GameObject knife;
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
        
        if (Camera.main.transform.position.y >= (level*platformHeight) - 2)//offset because of camera change 
        {
            level++;
            gameSpeed += 0.01f;
            print(gameSpeed);
            SpawnNewLevel();
            CleanPlatforms();
        }
    }


   void SpawnNewLevel()
    {
        Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Length-1)], new Vector3(0, platformHeight * level, 0), Quaternion.identity);
        Instantiate(knife, new Vector3(0, platformHeight * level, 0), Quaternion.identity);
    }

    void closeWall()
    {

    }

    void SpawnWalls(Vector2 pos)
    {
     GameObject  go = (GameObject)Instantiate(collapsingWall, new Vector3(pos.x,pos.y,0), Quaternion.identity);
       // go.GetComponent<WallScript>().startTime = 2;
        
    }

        void CleanPlatforms()
    {

        foreach (var go in GameObject.FindGameObjectsWithTag("Platform"))
        {
            if (go.transform.position.y < Camera.main.transform.position.y - platformHeight)
            {
                Destroy(go);
            }
        }
   
    }
}
