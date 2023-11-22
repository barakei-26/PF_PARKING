using UnityEngine;

public class SensorU : MonoBehaviour
{
    private const float distanciaMaxima = 5f;
    private const float anguloAperturaVertical = 45f;
    [SerializeField] private Controlador controlador;
    private int valorDeDeteccion; // Valor de detección (1 si detecta, 0 si no)
    [SerializeField] private int sensorID; // Identificación única del sensor

    private void Start()
    {
        valorDeDeteccion = 0; // Inicialmente, no se ha detectado nada
        InvokeRepeating("Medir",1f, 1f);
    }

    public void ConfigurarControlador(Controlador controlador) 
    {
        this.controlador = controlador;

    }

    private void Medir() 
    {
        // Calcular la dirección de detección y lanzar un rayo para detectar objetos
        Vector3 direccionDeDeteccion = Vector3.up;
        Ray rayo = new Ray(transform.position, direccionDeDeteccion);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayo, out hitInfo, distanciaMaxima))
        {
            Debug.Log("Detectado");
            // Si la distancia al objeto es menor que la distancia máxima, se ha detectado algo
            valorDeDeteccion = 1;
        }
        else
        {
            // Si no detectamos un objeto, no se ha detectado nada
            valorDeDeteccion = 0;
        }

        // Enviar el estado al controlador
        controlador.EnviarLectura(sensorID,valorDeDeteccion);

    }
}
