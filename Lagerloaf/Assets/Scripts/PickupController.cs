using UnityEngine;
using System.Collections;

public enum Items
{
    Butter,
    Lager,
    Can,
    Count,
    None
}

public class PickupController : MonoBehaviour
{
    Items pickup;


    public string Type
    {
        set
        {
            pickup = (Items)Items.Parse(typeof(Items), value);
        }
        get
        {
            return pickup.ToString();
        }
    }
    
	void Start ()
    {
        pickup = (Items)Random.Range(0, (float)Items.Count);
	}

  
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().item = pickup;
            Destroy(gameObject);
        }
    }
}
