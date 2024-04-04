using UnityEngine;
using SocketIOClient;
using Newtonsoft.Json;
using System.Collections.Generic;

public class SocketManager: MonoBehaviour
{
    private SocketIOUnity socket;
    public MovementHandler movementScript;

    private void Start()
    {
        socket = new SocketIOUnity("http://localhost:3000");
        socket.OnConnected += (sender, e) =>
        {
            Debug.Log("Connected to Server");
        };

        socket.On("move", (data) =>
        {
            Debug.Log("coords --> " + getCoord(data.ToString()));
            movementScript.Target = getCoord(data.ToString());
        });

        socket.Connect();
    }

    Vector3 getCoord( string jsonData )
    {
        List<Dictionary<string, float>> dataList = JsonConvert.DeserializeObject<List<Dictionary<string, float>>>(jsonData);

        float x = dataList[0]["x"];
        float y = dataList[0]["y"];
        float z = dataList[0]["z"];

        Vector3 vector3Data = new Vector3(x, y, z);

        return vector3Data;
    }

    private void OnApplicationQuit()
    {
        socket.Disconnect();
    }
}