using UnityEngine;
using System.Collections;

public class TrapController : MonoBehaviour
{
    public Items item;
    public float speed = 4f;
	public float activationDelay = 0.1f;
	bool isEnabled = false;
	bool falling = true;


    void Update()
    {
        if(falling)
        {
            transform.Translate(Vector2.down * (speed * Time.deltaTime));
        }

		if (!isEnabled) {
			if (Time.time > activationDelay) {
				isEnabled = true;
			}
		}
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
		if(isEnabled)
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

		if(collider.gameObject.tag == "Platform" && item.ToString() != "Lager" )
		{
			falling = false;
		}

    }

    public void SetType(Items m_item)
    {
        item |= m_item;
    }
    
}
