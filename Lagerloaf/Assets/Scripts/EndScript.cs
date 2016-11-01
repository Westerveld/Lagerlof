using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
}
