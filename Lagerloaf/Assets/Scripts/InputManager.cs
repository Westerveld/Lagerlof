using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
    public int winner = 0;
    private int numOfPlayers;
	// Use this for initialization
	void Start () {
        if(GameObject.FindGameObjectsWithTag("IM").Length > 1)
        {
            for (int i = 1; i < GameObject.FindGameObjectsWithTag("IM").Length; i++)
            {
                Destroy(GameObject.FindGameObjectsWithTag("IM")[i]);
            }
        }

    }

    public void SetWinner(int win)
    {
        winner = win;
    }
	
	// Update is called once per frame
	void Awake () {
        DontDestroyOnLoad(this);

    }

    public void SetNumberOfPlayers(int value)
    {
        numOfPlayers = value;
    }

    public int GetNumberOfPlayers()
    {
        print(numOfPlayers);
        return numOfPlayers;
    }

    public void AddNumberOfPlayer(int value)
    {
        numOfPlayers += value;
    }

}
