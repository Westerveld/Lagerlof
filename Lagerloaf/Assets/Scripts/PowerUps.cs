using UnityEngine;
using System.Collections;

enum PowerUpTypes { butter, lager };
public class PowerUps : MonoBehaviour
{

    PowerUpTypes powerup;

	// Use this for initialization
	void Start ()
    {
        int type = Random.Range(0, 1);
        powerup = (PowerUpTypes)type;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public string getType()
    {
        return powerup.ToString();
    }
}
