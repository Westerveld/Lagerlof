using UnityEngine;
using System.Collections;

public class TrapController : MonoBehaviour
{
    public Items item;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<PlayerController>().SetSlow();
            Destroy(gameObject);
        }
    }
}
