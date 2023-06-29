using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GetComponent<BoxCollider2D>().enabled = false;
            transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            transform.parent.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 350);

        }
    }
}
