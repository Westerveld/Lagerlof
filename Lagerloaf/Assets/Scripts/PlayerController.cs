using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed, jumpForce;
    public string controller;
    public int playerNumber = 0;

    private Rigidbody2D rigidBody;
    private bool jumping;

    public float jumpReduction = 2.0f;

    public Animator anim;

    private bool slowed;
    private bool crushed;
    private bool dead;

    public GameObject ParticleEffect;

    public GameObject trapPrefab;
    public Items item = Items.None;

   

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        jumping = true;
        anim.SetBool("InAir", true);
        slowed = false;
        ParticleEffect.SetActive(false);
    }

    void Update()
    {
        Move();
        Jump();
        UseItem();
        CheckIfFallenOffMap();
        WrapeMovement();

        GameObject.Find("GameManager").GetComponent<GameManager>().CheckNumberOfPlayers(playerNumber);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.contacts.Length > 0 && other.contacts[0].normal == Vector2.up && other.gameObject.tag == "Platform")
        {
            jumping = false;
            anim.SetBool("InAir", false);
        }
    }

    void ChangeScale(bool value)
	{
		if (value) {
			gameObject.GetComponent<SpriteRenderer> ().flipX = true;

		} else {
			gameObject.GetComponent<SpriteRenderer> ().flipX = false;

		}
	}

    void Move()
    {
        Vector2 position = transform.position;
        if (Input.GetAxis(controller + "Horizontal") < -0.2f)
        {
            if (slowed)
            {
                transform.position = (position + Vector2.right * Input.GetAxis(controller + "Horizontal") * moveSpeed / 2 * Time.deltaTime);
                anim.SetBool("Walking", true);
                ChangeScale(false);
            }
            else
            {
                transform.position = position + Vector2.right * Input.GetAxis(controller + "Horizontal") * moveSpeed * Time.deltaTime;
                anim.SetBool("Walking", true);
                ChangeScale(false);
            }

        }
        else if (Input.GetAxis(controller + "Horizontal") > 0.2f)
        {
            if (slowed)
            {
                transform.position = (position + Vector2.right * Input.GetAxis(controller + "Horizontal") * moveSpeed / 2 * Time.deltaTime);
                anim.SetBool("Walking", true);
                ChangeScale(true);
            }
            else
            {
                transform.position = position + Vector2.right * Input.GetAxis(controller + "Horizontal") * moveSpeed * Time.deltaTime;
                anim.SetBool("Walking", true);
                ChangeScale(true);
            }
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown(controller + "Jump") && !jumping)
        {
            if (!crushed)
            {
                rigidBody.AddForce(Vector2.up * jumpForce);
            }
            else
            {
                rigidBody.AddForce ( new Vector2(0, 1 * jumpForce / jumpReduction));
            }
            jumping = true;
            anim.SetTrigger("Jumping");
            anim.SetBool("InAir", true);
        }
    }

    void UseItem()
    {
        if(Input.GetButtonDown(controller + "Use") && item != Items.None)
        {
            if (item == Items.Can)
            {
                transform.FindChild("CanBurst").gameObject.SetActive(true);
            }
            else
            {
                anim.SetTrigger("UsedItem");

                GameObject trap = (GameObject)Instantiate(trapPrefab);
                trap.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Pickups/" + item.ToString());
                trap.GetComponent<TrapController>().SetType(item);
                trap.transform.position = transform.position;
            }

            item = Items.None;
        }
    }

    public void SetSlow()
    {
        slowed = true;
        StartCoroutine(SlowHim());
    }

    IEnumerator SlowHim()
    {
        yield return new WaitForSeconds(2f);
        slowed = false;
    }

    public void CanCrushed()
    {
        crushed = true;
        StartCoroutine(JumpReduction());
    }

    IEnumerator JumpReduction()
    {
        yield return new WaitForSeconds(2f);
        crushed = false;
    }

    public void Death()
    {
        if(!dead)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            ParticleEffect.SetActive(true);
            dead = true;
            GameObject.Find("InputManager").GetComponent<InputManager>().AddNumberOfPlayer(-1);
            Destroy(gameObject, 1f);
            Debug.Log(Time.time + " | Player - " + this.gameObject.name + "has DIED | PlayerController.Death()");
        }
    }

    void CheckIfFallenOffMap()
    {
        if (gameObject.transform.position.y < Camera.main.transform.position.y - 30)
        {
            Death();
        }
    }


    void WrapeMovement()
    {
        if (transform.position.x > 17 || transform.position.x < -17)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);
        }
    }

}
