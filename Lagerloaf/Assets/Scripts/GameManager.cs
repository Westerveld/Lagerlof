using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public GameObject[] platformPrefabs;
    public GameObject collapsingWall;
    public GameObject knifes;
    public static float gameSpeed = 1; //Starting speed
    public static float gameSpeedIncreament = 0.3f; //The rate in which the gamespeed increases, called when a new "level" is added to the game. 
    int platformHeight = 10;
    int level = 0;
    float numOfPlayers;

    public GameObject[] Players;

    // Use this for initialization
    void Start () {
        List<GameObject> spawnedPlatforms = new List<GameObject>();
      
        numOfPlayers = GameObject.Find("InputManager").GetComponent<InputManager>().GetNumberOfPlayers();

        for (int i = 0; i < numOfPlayers; i++)
        {
            Players[i].SetActive(true);
        }

        //Create initial platforms
        Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Length - 1)], new Vector3(0, platformHeight * level, 0), Quaternion.identity);

    }

    // Update is called once per frame
    void Update () {
        if (Camera.main.transform.position.y >= level*platformHeight)
        {
            level++;
            gameSpeed += gameSpeedIncreament;
            Debug.Log(Time.time + " | GameSpeed = " + gameSpeed + " | GameManger.Update()");
            SpawnNewLevel();
            CleanPlatforms();
        }
    }


   void SpawnNewLevel()
    {
        Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Length-1)], new Vector3(0, platformHeight * level + 5, 0), Quaternion.identity);
     GameObject go = (GameObject)Instantiate(knifes, new Vector3(0, platformHeight * level  +5, 0), Quaternion.identity);
        WallScript ws = go.GetComponent<WallScript>();
        if (4.0f - gameSpeed > 0.03)
        { 
        ws.collapseTime =  4.0f - gameSpeed;
        }
        else
        {
        ws.GetComponent<WallScript>().collapseTime = 0.03f;
        }
    }


    void SpawnWalls(Vector2 pos)
    {
     GameObject  go = (GameObject)Instantiate(collapsingWall, new Vector3(pos.x,pos.y,0), Quaternion.identity);     
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
