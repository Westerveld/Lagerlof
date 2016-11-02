using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed, jumpForce, slowTime;
    public string controller;

    private Rigidbody2D rigidBody;
    private bool jumping;

    public Animator anim;

    public bool slowed;

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
        if(value)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

        }
        //Original code
        /*
        var scale = transform.localScale;
        scale.x = value;
        transform.localScale = scale;
        */
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
            rigidBody.AddForce(Vector2.up * jumpForce);
            jumping = true;
            anim.SetTrigger("Jumping");
            anim.SetBool("InAir", true);
        }
    }

    void UseItem()
    {
        if(Input.GetButtonDown(controller + "Use") && item != Items.None)
        {
            anim.SetTrigger("UsedItem");

            GameObject trap = (GameObject)Instantiate(trapPrefab);
            trap.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Pickups/" + item.ToString());
            trap.transform.position = transform.position + transform.right * -2;

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
        yield return new WaitForSeconds(slowTime);
        slowed = false;
    }

    public void Death()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        ParticleEffect.SetActive(true);
        Destroy(gameObject, 1f);
        Debug.Log(Time.time + " | Player - " + this.gameObject.name + "has DIED | PlayerController.Death()");
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
        if (transform.position.x > 20 || transform.position.x < -20)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);
        }
    }



   

}
