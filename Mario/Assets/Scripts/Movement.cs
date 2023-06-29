using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] GameObject _mario;
    [SerializeField] Sprite[] _walkingSprites;
    [SerializeField] Sprite _jumpSprite;
    [SerializeField] bool _isWalking,_isGrounded;
    [SerializeField] SpriteRenderer _marioImage;
    [SerializeField] Rigidbody2D _marioRigidBody;
    [SerializeField] Camera _mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        _marioRigidBody = _mario.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _mario.transform.eulerAngles = Vector3.zero;

        if (Input.GetKey(KeyCode.D) && !_isWalking)
        {
            if (_marioImage.flipX) _marioImage.flipX = false;

            _isWalking = true;
            StartCoroutine(WalkingAnimation());
            StartCoroutine(Walk(Vector2.right));
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            _isWalking = false;
            StopCoroutine(WalkingAnimation());
        }

        if (Input.GetKey(KeyCode.A) && !_isWalking)
        {

            if (!_marioImage.flipX) _marioImage.flipX = true;

            _isWalking = true;
            StartCoroutine(WalkingAnimation());
            StartCoroutine(Walk(Vector2.left));
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            _isWalking = false;
            StopCoroutine(WalkingAnimation());
        }

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded )
        {
            _marioImage.sprite = _jumpSprite;
            _marioRigidBody.AddForce(Vector2.up * 375);
        }

        if(_mario.transform.position.x > 2)
        {
            _mainCamera.transform.position = new Vector3(_mario.transform.position.x - 2, 1, -10);
        }

    }

    IEnumerator WalkingAnimation()
    {
        for (int i = 0; i < _walkingSprites.Length; i++)
        {
            if (!_isWalking || !_isGrounded) break;

            _marioImage.sprite = _walkingSprites[i];
            yield return new WaitForSeconds(.1f);
        }

        if(_isWalking && _isGrounded) StartCoroutine(WalkingAnimation());
    }

    IEnumerator Walk(Vector2 direction)
    {
        while (_isWalking)
        {
            _mario.transform.Translate(direction * 4 * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(WalkingAnimation());
        _isWalking = false;

        _marioImage.sprite = _walkingSprites[0];

        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "GameController")
        {
            _isGrounded = true;

            if (transform.position.x > 150)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                GameObject.Find("Flag").GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isGrounded = false;
        }

    }
}
