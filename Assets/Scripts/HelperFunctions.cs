using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperFunctions : MonoBehaviour
{
    public Vector3 getCoord(string jsonData)
    {
        List<Dictionary<string, float>> dataList = JsonConvert.DeserializeObject<List<Dictionary<string, float>>>(jsonData);

        float x = dataList[0]["x"];
        float y = dataList[0]["y"];
        float z = dataList[0]["z"];

        Vector3 vector3Data = new Vector3(x, y, z);

        return vector3Data;
    }

    public List<(Vector3 position, Quaternion rotation)> getMoveCoords(string jsonData)
    {
        List<DataFormat> dataList = JsonConvert.DeserializeObject<List<DataFormat>>(jsonData);

        List<(Vector3 position, Quaternion rotation)> moveCoordsList = new List<(Vector3 position, Quaternion rotation)>();

        foreach (var data in dataList)
        {
            Vector3 positionVector = new Vector3(data.position.x, data.position.y, data.position.z);
            Quaternion rotationQuaternion = new Quaternion(data.rotation.x, data.rotation.y, data.rotation.z, data.rotation.w);

            moveCoordsList.Add((positionVector, rotationQuaternion));
        }

        return moveCoordsList;
    }
}
