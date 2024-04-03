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
            string res = data.ToString();
            /*Debug.Log(res);*/
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
}