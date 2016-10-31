using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed, jumpForce;
    public string controller;

    private Rigidbody2D rigidBody;
    private bool jumping;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        jumping = true;
    }

    void Update()
    {
        Vector2 position = transform.position;
        transform.position = position + Vector2.right * Input.GetAxis(controller + "Horizontal") * moveSpeed * Time.deltaTime;

        if (Input.GetButtonDown(controller + "Jump") && !jumping)
        {
            rigidBody.AddForce(Vector2.up * jumpForce);
            jumping = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.contacts.Length > 0 && other.contacts[0].normal == Vector2.up && other.gameObject.tag == "Platform")
        {
            jumping = false;
        }
    }
}
