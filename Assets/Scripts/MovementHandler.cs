using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    public float smoothing = 7f;
    private Vector3 target;

    public Vector3 Target
    {
        get { return target; }
        set
        {
            target = value;
            Debug.Log("Got value -->" + target);
        }
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target) > 0.5f)
        {
            Debug.Log("Moving towards target");
            transform.position = Vector3.Lerp(transform.position, target, smoothing * Time.deltaTime);
        }
    }
}
