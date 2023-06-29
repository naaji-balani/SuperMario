using UnityEngine;

public class EnemyLegs : MonoBehaviour
{
    public float minScale = 1f;
    public float maxScale = 3f;
    public float scaleSpeed = 2f;
    public bool isLeftLeg = false;

    private bool scalingUp = false;

    private void Start()
    {
        scalingUp = !isLeftLeg;
    }

    private void Update()
    {
        // Calculate the current scale based on the time and direction
        float currentScale = Mathf.Lerp(minScale, maxScale, Mathf.PingPong(Time.time * scaleSpeed, 1f));

        // Apply the scale to the leg
        if (isLeftLeg)
        {
            transform.localScale = new Vector3(currentScale, currentScale, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(maxScale - currentScale + minScale, maxScale - currentScale + minScale, transform.localScale.z);
        }

        // Check if we should change the direction
        if (isLeftLeg && currentScale >= maxScale)
        {
            scalingUp = false;
        }
        else if (!isLeftLeg && currentScale <= minScale)
        {
            scalingUp = true;
        }
    }
}
