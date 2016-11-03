using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject[] platformPrefabs;
    public GameObject knifes;
    public static float gameSpeed = 1; //Starting speed
    public static float gameSpeedIncreament = 0.05f; //The rate in which the gamespeed increases, called when a new "level" is added to the game. 
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
        Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Length - 1)], new Vector3(0, 0, 0), Quaternion.identity);

    }

    // Update is called once per frame
    void Update () {
        if (Camera.main.transform.position.y >= level * platformHeight)
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
        WallScript wall1 = go.transform.GetChild(0).gameObject.GetComponent<WallScript>();
        WallScript wall2 = go.transform.GetChild(1).gameObject.GetComponent<WallScript>();
        wall1.ChangeValues((gameSpeed *0.1f), (gameSpeed * 0.5f));
        wall2.ChangeValues((gameSpeed * 0.1f), (gameSpeed * 0.5f));
        
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
        foreach(var go in GameObject.FindGameObjectsWithTag("Edge"))
        {
            if (go.transform.position.y < Camera.main.transform.position.y - platformHeight)
            {
                Destroy(go);
            }
        }
    }

    public void CheckNumberOfPlayers()
    {
        if (GameObject.Find("InputManager").GetComponent<InputManager>().GetNumberOfPlayers() == 1)
        {
            foreach (var play in GameObject.FindGameObjectsWithTag("Player"))
            {
                if(!play.GetComponent<PlayerController>().dead)
                {
                    StartCoroutine(loadGame(play.GetComponent<PlayerController>().playerNumber));
                }
            }
            
        }
    }

    private IEnumerator loadGame(int Winner)
    {
        float fadeTime = GameObject.Find("GameManager").GetComponent<MainFader>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        GameObject.Find("InputManager").GetComponent<InputManager>().SetWinner(Winner);
        SceneManager.LoadScene("End");
       
    }

}
