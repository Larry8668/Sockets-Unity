using UnityEngine;
using SocketIOClient;
using Newtonsoft.Json;
using System.Collections.Generic;
using static DataFormat;

public class SocketManager : MonoBehaviour
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

        /*
        socket.On("translate", (data) =>
        {
            List<(Vector3 position, Quaternion rotation)> moveCoordsList = getMoveCoords(data.ToString());

            foreach (var moveCoords in moveCoordsList)
            {
                Vector3 tempVec = moveCoords.position;
                Quaternion tempQuat = moveCoords.rotation;
                movementScript.SetMoveCoords(tempVec, tempQuat);
            }
        });*/

        socket.On("mirror", (data) =>
        {

            List<(Vector3 position, Quaternion rotation)> moveCoordsList = mirrorPlayerMovement(data.ToString());

            foreach (var moveCoords in moveCoordsList)
            {
                Vector3 tempVec = moveCoords.position;
                Quaternion tempQuat = moveCoords.rotation;
                movementScript.SetMoveCoords(tempVec, tempQuat);
            }
        });


        socket.Connect();
    }

    private Vector3 getCoord(string jsonData)
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
            Vector3 positionVector = new Vector3(data.position.x - 15, data.position.y, data.position.z);
            Quaternion rotationQuaternion = Quaternion.Euler(data.rotation.x, data.rotation.y, data.rotation.z);

            moveCoordsList.Add((positionVector, rotationQuaternion));
        }

        return moveCoordsList;
    }

    public void SendPlayerMovement(Vector3 position, Quaternion rotation)
    {
        DataFormat data = new DataFormat
        {
            position = new PositionData { x = position.x - 15, y = position.y, z = position.z },
            rotation = new RotationData { x = rotation.x, y = rotation.y, z = rotation.z, w = rotation.w }
        };

        string jsonData = JsonConvert.SerializeObject(data);

        socket.Emit("player-move", jsonData);
    }

    public List<(Vector3 position, Quaternion rotation)> mirrorPlayerMovement(string jsonData)
    {
        List<string> outerArray = JsonConvert.DeserializeObject<List<string>>(jsonData);
        string innerJson = outerArray[0];
        DataFormat receivedData = JsonConvert.DeserializeObject<DataFormat>(innerJson);



        List<(Vector3 position, Quaternion rotation)> moveCoordsList = new List<(Vector3 position, Quaternion rotation)>();

        Vector3 positionVector = new Vector3(receivedData.position.x-15, receivedData.position.y, receivedData.position.z);
        Quaternion rotationQuaternion = new Quaternion(receivedData.rotation.x, receivedData.rotation.y, receivedData.rotation.z, receivedData.rotation.w);

        moveCoordsList.Add((positionVector, rotationQuaternion));

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