using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public bool moveLeft = false;

    private bool movingLeft = false;
    [SerializeField] Transform _mario;

    private void Start()
    {
        movingLeft = moveLeft;
    }

    private void Update()
    {
        if (transform.position.x - _mario.position.x > 20) return;

        transform.eulerAngles = Vector3.zero;

        // Calculate the movement direction
        float direction = movingLeft ? -1f : 1f;

        // Translate the enemy horizontally
        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with a wall
        if (collision.gameObject.CompareTag("GameController") || collision.gameObject.CompareTag("Finish"))
        {
            movingLeft = !movingLeft;
        }

        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.GetChild(2).GetComponent<BoxCollider2D>().enabled = false;
            Debug.Log("Game Over");
        }
    }


}
