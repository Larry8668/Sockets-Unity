using UnityEngine;
using SocketIOClient;
using Newtonsoft.Json;
using System.Collections.Generic;

public class SocketManager: MonoBehaviour
{
    private SocketIOUnity socket;
    public MovementHandler movementScript;
    public HelperFunctions helperFunctions;

    private void Start()
    {
        socket = new SocketIOUnity("http://localhost:3000");
        socket.OnConnected += (sender, e) =>
        {
            Debug.Log("Connected to Server");
        };

        socket.On("translate", (data) =>
        {
            List<(Vector3 position, Quaternion rotation)> moveCoordsList = getMoveCoords(data.ToString());

            foreach (var moveCoords in moveCoordsList)
            {
                Vector3 tempVec = moveCoords.position;
                Quaternion tempQuat = moveCoords.rotation;
                movementScript.SetMoveCoords(tempVec, tempQuat);
            }
        });

        socket.Connect();
    }

    private Vector3 getCoord( string jsonData )
    {
        List<Dictionary<string, float>> dataList = JsonConvert.DeserializeObject<List<Dictionary<string, float>>>(jsonData);

        float x = dataList[0]["x"];
        float y = dataList[0]["y"];
        float z = dataList[0]["z"];

        Vector3 vector3Data = new Vector3(x, y, z);

        return vector3Data;
    }

    private List<(Vector3 position, Quaternion rotation)> getMoveCoords(string jsonData)
    {
        List<DataFormat> dataList = JsonConvert.DeserializeObject<List<DataFormat>>(jsonData);

        List<(Vector3 position, Quaternion rotation)> moveCoordsList = new List<(Vector3 position, Quaternion rotation)>();

        foreach (var data in dataList)
        {
            Vector3 positionVector = new Vector3(data.position.x, data.position.y, data.position.z);
            Quaternion rotationQuaternion = Quaternion.Euler(data.rotation.x, data.rotation.y, data.rotation.z);

            moveCoordsList.Add((positionVector, rotationQuaternion));
        }

        return moveCoordsList;
    }


    private void OnApplicationQuit()
    {
        socket.Disconnect();
    }
}


public class DataFormat
{
    public class PositionData
    {
        public float x;
        public float y;
        public float z;
    }

    public class RotationData
    {
        public float x;
        public float y;
        public float z;
        public float w;
    }

    public PositionData position;
    public RotationData rotation;
}