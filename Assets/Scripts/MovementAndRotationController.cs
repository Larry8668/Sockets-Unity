using System.Collections;
using UnityEngine;

public class MovementAndRotationController : MonoBehaviour
{
    public Vector3[] positions; // Array of positions
    public Quaternion[] rotations; // Array of rotations
    public float speed = 1.0f; // Speed of movement

    private int currentIndex = 0; // Index to track the current position/rotation

    void Start()
    {
        StartCoroutine(MoveAndRotate());
    }

    IEnumerator MoveAndRotate()
    {
        while (true)
        {
            // Move to the next position
            transform.position = Vector3.MoveTowards(transform.position, positions[currentIndex], speed * Time.deltaTime);

            // Rotate to the next rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotations[currentIndex], speed * Time.deltaTime);

            // Check if we've reached the target position/rotation
            if (Vector3.Distance(transform.position, positions[currentIndex]) < 0.01f && Quaternion.Angle(transform.rotation, rotations[currentIndex]) < 0.01f)
            {
                // Move to the next index
                currentIndex = (currentIndex + 1) % positions.Length;
            }

            yield return null; // Wait for the next frame
        }
    }
}
