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
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        jumping = true;
        anim.SetBool("InAir", true);
        slowed = false;
    }

    void Update()
    {
        Move();
        Jump();
        UseItem();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.contacts.Length > 0 && other.contacts[0].normal == Vector2.up && other.gameObject.tag == "Platform")
        {
            jumping = false;
            anim.SetBool("InAir", false);
        }
    }

    void ChangeScale(float value)
    {
        var scale = transform.localScale;
        scale.x = value;
        transform.localScale = scale;
    }

    void Move()
    {
        Vector2 position = transform.position;
        if (Input.GetAxis(controller + "Horizontal") < -0.2f)
        {
            if (slowed)
            {
                transform.position = (position + Vector2.right * Input.GetAxis(controller + "Horizontal") * moveSpeed * Time.deltaTime)/2;
                anim.SetBool("Walking", true);
                ChangeScale(1);
            }
            else
            {
                transform.position = position + Vector2.right * Input.GetAxis(controller + "Horizontal") * moveSpeed * Time.deltaTime;
                anim.SetBool("Walking", true);
                ChangeScale(1);
            }

        }
        else if (Input.GetAxis(controller + "Horizontal") > 0.2f)
        {
            if (slowed)
            {
                transform.position = (position + Vector2.right * Input.GetAxis(controller + "Horizontal") * moveSpeed * Time.deltaTime)/2;
                anim.SetBool("Walking", true);
                ChangeScale(-1);
            }
            else
            {
                transform.position = position + Vector2.right * Input.GetAxis(controller + "Horizontal") * moveSpeed * Time.deltaTime;
                anim.SetBool("Walking", true);
                ChangeScale(-1);
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
        if(Input.GetButtonDown(controller + "Use"))
        {
            anim.SetTrigger("UsedItem");
        }
    }

    public void IsSlowed()
    {
        slowed = true;
        StartCoroutine(SlowHim());
    }

    IEnumerator SlowHim()
    {
        yield return new WaitForSeconds(slowTime);
        slowed = false;
    }
}
