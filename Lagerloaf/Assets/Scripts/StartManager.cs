using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    private int numOfPlayers = 2;

    public Text playerText;

    public GameObject playButton, infoButton, minusArrow, plusArrow, players, returnButton, controller, creditsButton, creditText, quitButton;
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

    public void InfoButton()
    {
        playButton.SetActive(false);
        infoButton.SetActive(false);
        playerText.gameObject.SetActive(false);
        minusArrow.SetActive(false);
        plusArrow.SetActive(false);
        players.SetActive(false);
        creditsButton.SetActive(false);
        quitButton.SetActive(false);
        returnButton.SetActive(true);
        controller.SetActive(true);
    }

    public void ReturnButton()
    {
        playButton.SetActive(true);
        infoButton.SetActive(true);
        playerText.gameObject.SetActive(true);
        minusArrow.SetActive(true);
        plusArrow.SetActive(true);
        players.SetActive(true);
        creditsButton.SetActive(true);
        quitButton.SetActive(true);
        returnButton.SetActive(false);
        if (controller.activeInHierarchy)
        {
            controller.SetActive(false);
        }
        if (creditText.activeInHierarchy)
        {
            creditText.SetActive(false);
        }
        
    }

    public void CreditsButton()
    {
        playButton.SetActive(false);
        infoButton.SetActive(false);
        playerText.gameObject.SetActive(false);
        minusArrow.SetActive(false);
        plusArrow.SetActive(false);
        players.SetActive(false);
        creditsButton.SetActive(false);
        quitButton.SetActive(false);
        returnButton.SetActive(true);
        creditText.SetActive(true);
    }

    private IEnumerator loadGame()
    {
        float fadeTime = GameObject.Find("GM").GetComponent<MainFader>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        GameObject.Find("InputManager").GetComponent<InputManager>().SetNumberOfPlayers(numOfPlayers);
        SceneManager.LoadScene("Level1");
    }

    public void QuitButton()
    {
        Application.Quit();
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
