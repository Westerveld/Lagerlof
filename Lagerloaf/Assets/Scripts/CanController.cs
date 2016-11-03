using UnityEngine;
using System.Collections;

public class CanController : MonoBehaviour
{
    public float blastForce = 500f;
    bool blasting = false;

    void Update()
    {
        Vector3 scale = transform.localScale;
        scale.x = transform.parent.GetComponent<SpriteRenderer>().flipX ? 1 : -1;
        transform.localScale = scale;
    }

    void Burst()
    {
        blasting = true;
        transform.parent.Translate(Vector2.zero);
        StartCoroutine(End());
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (blasting && collider.transform != transform.parent)
        {
            collider.GetComponent<Rigidbody2D>().AddForce(Vector2.right * transform.localScale.x * blastForce);
        }
    }
}
