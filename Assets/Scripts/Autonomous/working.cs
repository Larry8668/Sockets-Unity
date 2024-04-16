using System.Collections;
using UnityEngine;

public class working : MonoBehaviour
{
    public Vector3[] positions; // Array of positions
    public Quaternion[] rotations; // Array of rotations
    public float speed = 1.0f; // Speed of movement

    private int currentIndex = 0; // Index to track the current position/rotation

    void Start()
    {
        Debug.Log(positions);
        Debug.Log(rotations);
        StartCoroutine(MoveAndRotate());
    }

    IEnumerator MoveAndRotate()
    {
        while (true)
        {
            Debug.Log("Position: " + positions[currentIndex] + ", Rotation: " + rotations[currentIndex]);

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

/*using UnityEngine;
using SocketIOClient;

public class SocketConnection : MonoBehaviour
{
    private SocketIOUnity socket;

    void Start()
    {
        socket = new SocketIOUnity("http://localhost:3000");

        socket.OnConnected += (sender, e) =>
        {
            Debug.Log("Connected");
        };

        socket.On("test-back", (data) =>
        {
            string res = data.ToString();
            *//*Debug.Log(res);*//*
            res = res.Substring(1, res.Length - 2);
            Test t = JsonUtility.FromJson<Test>(res);

            Debug.Log(t.text);
        });


        socket.Connect();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            socket.Emit("test", "bruh");
            Debug.Log("Msg sent");
        }
    }

    private void OnApplicationQuit()
    {
        socket.Disconnect();
    }

}

[System.Serializable]
public class Test
{
    public string text;
}*/