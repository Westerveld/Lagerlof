using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour
{

    public bool moveLeft, moving = true;
    private Transform trans;
    public float startTime, collapseTime, startX, slowMoveX, fastMoveX;
    public GameObject pEffect;
    // Use this for initialization
    void Start()
    {
        trans = gameObject.GetComponent<Transform>();
        var pos = trans.position;
        if (moveLeft)
        {
            pos.x = startX;
            trans.position = pos;
        }
        else
        {
            pos.x = -startX;
            trans.position = pos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = trans.position;
        //Checks to see if the walls are at the cameras y location, if so it will start to move towards the center.
        if (gameObject.transform.position.y + 3 < Camera.main.transform.position.y)
        {
            if (moveLeft && pos.x > 11f)
            {
                pos.x -= slowMoveX;
                trans.position = pos;
            }
            else if (!moveLeft && pos.x < -11f)
            {
                pos.x += slowMoveX;
                trans.position = pos;
            }
            else
            {
                if (moveLeft && pos.x > 8f)
                {
                    pos.x -= fastMoveX;
                    trans.position = pos;
                }
                else if (!moveLeft && pos.x < -8f)
                {
                    pos.x += fastMoveX;
                    trans.position = pos;
                }
                else
                {
                    moving = false;
                }
            }
        }
        if (!moving) //Sets off the particle effect once the walls has completed its movement
        {
            pEffect.SetActive(true);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        //Walls can kill the player
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().Death();
        }
    }

    //This function is used to keep up with the game time from the GameManager script. It allows the walls to progressively move in quicker
    public void ChangeValues(float newXSlow, float newXFast)
    {
        slowMoveX = newXSlow;
        fastMoveX = newXFast;
    }
}
    
