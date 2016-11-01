using UnityEngine;
using System.Collections;

public class PickupController : MonoBehaviour
{
    public enum Pickups
    {
        Butter,
        Lager,
        Count
    }

    Pickups pickup;

    public string texturesPath;

    public string Type
    {
        set
        {
            pickup = (Pickups)Pickups.Parse(typeof(Pickups), value);
        }

        get
        {
            return pickup.ToString();
        }
    }
    
	void Start ()
    {
        pickup = (Pickups)Random.Range(0, (float)Pickups.Count);
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(texturesPath + "/" + Type);
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //collision.gameObject.GetComponent<PlayerController>().Item = pickup;
            Destroy(gameObject);
        }
    }
}
