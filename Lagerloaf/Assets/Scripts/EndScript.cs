using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class EndScript : MonoBehaviour {
    int winningPlayer;
    public Text text;
    public GameObject[] SS;
	// Use this for initialization
	void Start () {
        print(GameObject.Find("InputManager").GetComponent<InputManager>().winner);
        switch (GameObject.Find("InputManager").GetComponent<InputManager>().winner)
        {
            case 1:
               SS[0].SetActive(true);
                text.text = "Player One WINS";
                break;
            case 2:
                SS[1].SetActive(true);
                text.text = "Player Two WINS";
                break;
            case 3:
                SS[2].SetActive(true);
                text.text = "Player Three WINS";
                break;
            case 4:
                SS[3].SetActive(true);
                text.text = "Player Four WINS";
                break;
            default:
                print("End");
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void HomeButton()
    {
        StartCoroutine(LoadStart());
    }

    IEnumerator LoadStart()
    {
        float fadetime = GameObject.Find("GM").GetComponent<MainFader>().BeginFade(1);
        yield return new WaitForSeconds(fadetime);
        SceneManager.LoadScene("start");
    }
    public void SetWinningPlayer(int player)
    {
        winningPlayer = player;
    }

}
