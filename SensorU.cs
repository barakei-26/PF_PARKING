using UnityEngine;

public class SensorUltrasonido : MonoBehaviour
{
    public float distanciaMaxima = 5f;
    public float anguloAperturaVertical = 45f;
    private int valorDeDeteccion; // Valor de detección (1 si detecta, 0 si no)
    public int sensorID; // Identificación única del sensor

    private void Start()
    {
        valorDeDeteccion = 0; // Inicialmente, no se ha detectado nada
    }

    private void Update()
    {
        // Calcular la dirección de detección y lanzar un rayo para detectar objetos
        Vector3 direccionDeDeteccion = transform.forward;
        Ray rayo = new Ray(transform.position, direccionDeDeteccion);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayo, out hitInfo, distanciaMaxima))
        {
            // Si la distancia al objeto es menor que la distancia máxima, se ha detectado algo
            valorDeDeteccion = 1;
        }
        else
        {
            // Si no detectamos un objeto, no se ha detectado nada
            valorDeDeteccion = 0;
        }

        // Enviar el estado al controlador
        ControladorSensores.Instance.EnviarLectura(sensorID, valorDeDeteccion);
    }
}
