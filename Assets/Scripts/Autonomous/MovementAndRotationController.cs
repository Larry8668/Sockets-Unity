using UnityEngine;
using System.Collections;
using SocketIOClient;

public class MovementAndRotationController : MonoBehaviour
{
    private SocketIOUnity socket;
    private bool socketConnected = false;
    private Vector3 targetPosition;
    private Quaternion targetRotation;

    void Start()
    {
        Debug.Log(gameObject.name);
        socket = new SocketIOUnity("http://localhost:3000");

        socket.OnConnected += (sender, e) =>
            {
                Debug.Log("Connected");
                socketConnected = true;
            };

        socket.On("movementData", (data) =>
        {
            if (socketConnected)
            {
                Debug.Log("movementData received: " + data);
                string res = data.ToString();
                res = res.Substring(1, res.Length - 2);
                MovementData movementData = JsonUtility.FromJson<MovementData>(res);

                Debug.Log("movementData received: " + movementData);

                Debug.Log("Received position: " + movementData.position);
                Debug.Log("Received rotation: " + movementData.rotation);

                MoveAndRotate(movementData.position, movementData.rotation);
            }
            else
            {
                Debug.LogWarning("Socket is not connected. Skipping movement and rotation.");
            }
        });

        StartCoroutine(MoveCoroutine(new Vector3(0, 0, 0))); // Start the coroutine
        socket.Connect(); // Connect the socket
    }

    IEnumerator MoveCoroutine(Vector3 targetPosition)
    {
        while (true)
        {
            // Perform movement logic here
            yield return new WaitForSeconds(1); // Example: Wait for 1 second before checking again
        }
    }

    void MoveAndRotate(Vector3 newPosition, Quaternion newRotation)
    {
        Debug.Log("Starting move coroutine");
        StartCoroutine(MoveCoroutine(newPosition));
        Debug.Log("Here");
        Debug.Log("Distance between transform.position and targetPosition: " + Vector3.Distance(transform.position, newPosition));
    }

    private void OnApplicationQuit()
    {
        socket.Disconnect();
    }
}

[System.Serializable]
public class MovementData
{
    public Vector3 position;
    public Quaternion rotation;
}


/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocketIOClient;

public class MovementAndRotationController : MonoBehaviour
{
    private SocketIOUnity socket;
    private List<Vector3> positions = new List<Vector3>();
    private List<Quaternion> rotations = new List<Quaternion>();

    void Start()
    {
        Debug.Log(gameObject.name);
        socket = new SocketIOUnity("http://localhost:3000");

        socket.OnConnected += (sender, e) =>
        {
            Debug.Log("Connected");
            socket.Emit("getposition");
            socket.Emit("getrotation");
        };

        socket.On("positionsData", (data) =>
        {
            Debug.Log("Position data ->" + data);
        });

        socket.On("rotationsData", (data) =>
        {
            Debug.Log("rotation data ->" + data);
        });

        StartCoroutine(MoveAndRotate()); // Start the coroutine
        socket.Connect(); // Connect the socket
    }

    IEnumerator MoveAndRotate()
    {
        Debug.Log("In courrotinen");
        while (true)
        {
            if (positions.Count > 0 && rotations.Count > 0)
            {
                // Perform movement and rotation logic using positions and rotations lists
                yield return new WaitForSeconds(1); // Example: Wait for 1 second before checking again
            }
            else
            {
                yield return null;
            }
        }
    }

    private void OnApplicationQuit()
    {
        socket.Disconnect();
    }
}
*/