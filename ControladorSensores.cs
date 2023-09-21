using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ControladorSensores : MonoBehaviour
{
    public static ControladorSensores Instance;

    private string servidorURL = "http://localhost:3000/actualizar_estado"; // URL del servidor Node.js
    public int idControlador; // ID del controlador

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método para enviar lectura con datos de sensor
    public void EnviarLectura(int idSensor, int valor)
    {
        // Enviar el ID del controlador, el ID del sensor y el valor al servidor
        StartCoroutine(EnviarDatosAlServidor(idControlador, idSensor, valor));
    }

    private IEnumerator EnviarDatosAlServidor(int idControlador, int idSensor, int valor)
    {
        // Crear un objeto para almacenar los datos
        LecturaSensorData data = new LecturaSensorData(idControlador, idSensor, valor);
        string json = JsonUtility.ToJson(data);

        // Crear y enviar la solicitud al servidor
        UnityWebRequest request = UnityWebRequest.PostWwwForm(servidorURL, "POST");
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(jsonBytes);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al enviar datos al servidor: " + request.error);
        }
        else
        {
            Debug.Log("Datos enviados al servidor correctamente.");
        }
    }
}

[System.Serializable]
public class LecturaSensorData
{
    public int id_controlador;
    public int id_sensor;
    public int valor;

    public LecturaSensorData(int idControlador, int idSensor, int valor)
    {
        this.id_controlador = idControlador;
        this.id_sensor = idSensor;
        this.valor = valor;
    }
}
