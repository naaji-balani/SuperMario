using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour
{
    [SerializeField] GameObject _brokenBrick;
    public List<GameObject> clones;
    bool hasCollided;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject clone = Instantiate(_brokenBrick, transform.position,transform.rotation);
                clones[i] = clone;
            }
            hasCollided = true;

            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.P)) UnityEngine.SceneManagement.SceneManager.LoadScene(0);

        if (hasCollided)
        {
           if(clones[0] != null) clones[0].transform.Translate(new Vector3(-.15f, .6f) * 5 * Time.deltaTime);
           if(clones[1] != null) clones[1].transform.Translate(new Vector3(-.05f, .3f) * 5 * Time.deltaTime);
           if(clones[2] != null) clones[2].transform.Translate(new Vector3(.05f, .6f) * 5 * Time.deltaTime);
           if(clones[3] != null) clones[3].transform.Translate(new Vector3(.15f, .3f) * 5 * Time.deltaTime);

            Invoke("DestroyBox", .5f);
        }
    }

    void DestroyBox()
    {
        for (int i = 0; i < clones.Count; i++)
        {
            if (clones[i] == null) break;
            Destroy(clones[i]);
        }
    }
}
