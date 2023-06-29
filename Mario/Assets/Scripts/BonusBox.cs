using System.Collections;
using UnityEngine;
using TMPro;

public class BonusBox : MonoBehaviour
{
    bool _hasCollected;
    SpriteRenderer _box;
    [SerializeField] Sprite _openBox;

    private void Start()
    {
        _box = GetComponent<SpriteRenderer>();
        StartCoroutine(BonusBoxes());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player") return;

        StartCoroutine(MoveUpAndDown());
        _hasCollected = true;
        _box.sprite = _openBox;
        GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>().text = "000" + ((int.Parse)(GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>().text) + 100).ToString();
    }

    IEnumerator MoveUpAndDown()
    {
        Vector3 currentPos = transform.position;

        while(transform.position.y - currentPos.y <= .3f)
        {
            transform.Translate(Vector3.up * 2 * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }

        while(transform.position != currentPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentPos, 2 * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    private void Update()
    {
        if (_hasCollected && transform.GetChild(0) != null)
        {
            transform.GetChild(0).transform.Translate(new Vector3(0.5f, 1) * 2 * Time.deltaTime);
            Invoke("DestroyText", 1.5f);
        }
    }

    void DestroyText()
    {
        if(transform.GetChild(0) != null) transform.GetChild(0).gameObject.SetActive(false);
    }

    IEnumerator BonusBoxes()
    {
        while (!_hasCollected)
        {
            _box.color = Color.red;


            yield return new WaitForSeconds(.5f);

            _box.color = Color.white;

            yield return new WaitForSeconds(.5f);

        }
    }
}
