using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    private float numOfPlayers;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Awake () {
        DontDestroyOnLoad(this);
	}

    public void SetNumberOfPlayers(float value)
    {
        numOfPlayers = value;
    }
}
