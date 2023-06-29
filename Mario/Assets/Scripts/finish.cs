using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finish : MonoBehaviour
{
    [SerializeField] Transform _mario, _flag;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _mario.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        _flag.SetParent(_mario);

        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(BringFlagDown());
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }


    IEnumerator BringFlagDown()
    {
        Vector3 collisionPos = _mario.position;

        while(_mario.transform.position.y >= -2.75f)
        {
            _mario.transform.Translate(Vector3.down * 1.5f * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }

        _flag.SetParent(null);
        _mario.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

    }
}
