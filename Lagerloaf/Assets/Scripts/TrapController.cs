using UnityEngine;
using System.Collections;

public class TrapController : MonoBehaviour
{
    public Items item;
    public float speed = 4f;
    
    void Update()
    {
        if(item.ToString() == "Lager")
        {
            transform.Translate(Vector2.down * speed);
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && item.ToString() == "Butter")
        {
            collider.gameObject.GetComponent<PlayerController>().SetSlow();
            Destroy(gameObject);
        }
        if(collider.gameObject.tag == "Player" && item.ToString() == "Lager")
        {
            collider.gameObject.GetComponent<PlayerController>().CanCrushed();
            Destroy(gameObject);
        }
    }

    public void SetType(Items m_item)
    {
        item |= m_item;
    }
    
}
