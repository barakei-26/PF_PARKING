using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Controlador : MonoBehaviour
{
    private List<LecturaSensorData> lecturas = new List<LecturaSensorData>();
    private string servidorURL = "http://52.201.66.102:5001/actualizar_estado"; // URL del servidor Node.js
    [SerializeField] private int idControlador; // ID del controlador
    [SerializeField] private int numeroDeSensores;

    // Método para enviar lectura con datos de sensor
    public void EnviarLectura(int idSensor, int valor)
    {
        if (valor == 1)
        {
            Debug.Log(idSensor + idControlador + " Ocupado");
        }

        if (lecturas.Count < numeroDeSensores)
        {
            LecturaSensorData lectura = new LecturaSensorData(idControlador, idSensor, valor);
            lecturas.Add(lectura);
        }
        else
        {
            int controladorBuscado = idControlador;
            int sensorBuscado = idSensor;

            LecturaSensorData lecturaBuscada = lecturas.FirstOrDefault(
                lectura => lectura.id_controlador == controladorBuscado && lectura.id_sensor == sensorBuscado);

            if (lecturaBuscada != null)
            {
                Console.WriteLine($"Se encontró la lectura: Controlador={lecturaBuscada.id_controlador}, Sensor={lecturaBuscada.id_sensor}, Valor={lecturaBuscada.valor}");
                lecturaBuscada.valor = valor;
                Debug.Log(valor);
            }
            else
            {
                Console.WriteLine("No se encontró ninguna lectura con la combinación específica.");
            }
        }

    }

    public void Start()
    {
        InvokeRepeating("Actualizar", 1, 1);
    }

    private void Actualizar()
    {
        StartCoroutine(EnviarDatosAlServidor());
    }

    private IEnumerator EnviarDatosAlServidor()
    {
        Debug.Log("lecturas" + lecturas.Count);
        if (lecturas.Count == numeroDeSensores)
        {
            // Crear un objeto para almacenar los datos
            string json = JsonUtility.ToJson(lecturas);
            Debug.Log("lecturas: " + json);

            // Crear un objeto WWWForm y agregar el JSON como un campo
            WWWForm form = new WWWForm();

            form.AddField("idControlador", idControlador.ToString());

            foreach (LecturaSensorData lectura in lecturas)
            {
                string idSensor = lectura.id_sensor.ToString();
                form.AddField("Sensor" + idSensor, lectura.valor);
            }


            // Crear y enviar la solicitud al servidor
            UnityWebRequest request = UnityWebRequest.Post(servidorURL, form);

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al enviar datos al servidor: " + request.error);
            }
            else
            {
                Debug.Log("Datos enviados al servidor correctamente.");
            }
            request.Dispose();
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
