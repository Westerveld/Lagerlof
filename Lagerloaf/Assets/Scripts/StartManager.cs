using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    private float numOfPlayers = 2;

    public Text playerText;

	// Use this for initialization
	void Start ()
    {
        playerText.text = numOfPlayers.ToString();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void StartButton()
    {
        StartCoroutine(loadGame());
    }

    private IEnumerator loadGame()
    {
        float fadeTime = GameObject.Find("GM").GetComponent<MainFader>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        GameObject.Find("InputManager").GetComponent<InputManager>().SetNumberOfPlayers(numOfPlayers);
        SceneManager.LoadScene("Level1");
    }

    public void AddPlayer()
    {
        if(numOfPlayers < 4)
        {
            numOfPlayers++;
            playerText.text = numOfPlayers.ToString();
        }
    }

    public void RemovePlayer()
    {
        if(numOfPlayers> 2)
        {
            numOfPlayers--;
            playerText.text = numOfPlayers.ToString();
        }
    }
}
