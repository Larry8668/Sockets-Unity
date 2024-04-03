using UnityEngine;
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
            Debug.Log(data);
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

}
