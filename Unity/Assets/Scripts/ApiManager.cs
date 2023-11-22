using System.IO;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine;
using System;

public class ApiManager : MonoBehaviour
{   
    public static ApiManager instance;
    private AssignedPositionResponse assignedPosition;
    private int[] carAsignedPosition;
    private const string apiUrl = "http://52.201.66.102:5001/get_assigned_position";
    private const string csvFilePath = "tiempos_respuesta_api_aws.csv";

    private System.Action<int[]> onAssignedPositionReceived;

    // This method will initiate the coroutine and provide a callback

    private void Start()
    {
        instance = this;
    }
    public void RequestAssignedPosition(System.Action<int[]> callback)
    {
        onAssignedPositionReceived = callback;
        StartCoroutine(GetAssignedPosition());
    }

    // The actual coroutine
    IEnumerator GetAssignedPosition()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);

        float startTime = Time.time;

        yield return request.SendWebRequest();

        float elapsedTime = Time.time - startTime;

        try
        {
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Debug.Log("Time: " + elapsedTime);
                WriteToCSV(elapsedTime);

                string jsonResponse = request.downloadHandler.text;
                ApiResponse response = JsonUtility.FromJson<ApiResponse>(jsonResponse);

                if (response != null && response.assigned_position != null)
                {
                    assignedPosition = response.assigned_position;
                    carAsignedPosition = new int[] { int.Parse(assignedPosition.piso), int.Parse(assignedPosition.posicion) };

                    // Trigger the callback and pass the position
                    onAssignedPositionReceived?.Invoke(carAsignedPosition);
                }
                else
                {

                    Debug.Log("No hay lugares de estacionamiento disponibles");
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error en GetAssignedPosition: {ex}");
        }
        finally
        {
            request.Dispose();
        }
    }

    void WriteToCSV(float elapsedTime)
    {
        if (!System.IO.File.Exists(csvFilePath))
        {
            using (StreamWriter writer = new StreamWriter(csvFilePath))
            {
                writer.WriteLine("Tiempo de Respuesta");
            }
        }

        using (StreamWriter writer = new StreamWriter(csvFilePath, true))
        {
            writer.WriteLine(elapsedTime);
        }
    }

    [System.Serializable]
    public class AssignedPositionResponse
    {
        public string posicion;
        public string piso;
    }

    [System.Serializable]
    public class ApiResponse
    {
        public AssignedPositionResponse assigned_position;
        public string message;
    }
}

