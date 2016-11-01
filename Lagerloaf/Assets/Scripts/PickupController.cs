using UnityEngine;
using System.Collections;

public class PickupController : MonoBehaviour
{
    private enum Pickups
    {
        Butter,
        Lager,
        Beer,
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
}
