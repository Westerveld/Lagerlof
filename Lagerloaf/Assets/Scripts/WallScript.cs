using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour
{

    public bool moveLeft, moving = true;
    private Transform trans;
    public float startTime, collapseTime;
    public GameObject pEffect;
    // Use this for initialization
    void Start()
    {
        trans = gameObject.GetComponent<Transform>();
        var pos = trans.position;
        if (moveLeft)
        {
            pos.x = 18f;
            trans.position = pos;
        }
        else
        {
            pos.x = -18f;
            trans.position = pos;
        }
        // startTime += Time.time ;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = trans.position;
        //  if (startTime + collapseTime < Time.time)
        if (gameObject.transform.position.y + 1 < Camera.main.transform.position.y)
        {
            if (moveLeft && pos.x > 11f)
            {
                pos.x -= 0.1f;
                trans.position = pos;
            }
            else if (!moveLeft && pos.x < -11f)
            {
                pos.x += 0.1f;
                trans.position = pos;
            }
            else
            {
                if (moveLeft && pos.x > 8f)
                {
                    pos.x -= 0.5f;
                    trans.position = pos;
                }
                else if (!moveLeft && pos.x < -8f)
                {
                    pos.x += 0.5f;
                    trans.position = pos;
                }
                else
                {
                    moving = false;
                }
            }
        }
        if (!moving)
        {
            pEffect.SetActive(true);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().Death();
        }
    }
}
    
