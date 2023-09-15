using UnityEngine;

public class SensorUltrasonido : MonoBehaviour
{
    public float distanciaMaxima = 5f;
    public float anguloAperturaVertical = 45f; // Ángulo de apertura vertical en grados
    public GameObject esfera; // Referencia al GameObject de la esfera que deseas cambiar de color
    private Material esferaMaterial; // Material de la esfera

    private void Start()
    {
        // Obtén el material de la esfera
        esferaMaterial = esfera.GetComponent<Renderer>().material;
    }

    private void Update()
    {
        // Calcular la dirección de detección hacia adelante (eje Z positivo)
        Vector3 direccionDeDeteccion = transform.forward;

        // Crear un cono de detección utilizando la matriz de rotación
        Quaternion rotacion = Quaternion.Euler(0f, 0f, 0f); // No es necesario cambiar la rotación en este caso
        Vector3 direccionInicial = rotacion * direccionDeDeteccion;

        // Lanzar un rayo en la dirección de detección
        Ray rayo = new Ray(transform.position, direccionInicial);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayo, out hitInfo, distanciaMaxima))
        {
            // Si la distancia al objeto es menor que la distancia máxima, cambia el color de la esfera a rojo
            esferaMaterial.color = Color.red;
        }
        else
        {
            // Si no detectamos un objeto, cambia el color de la esfera a verde
            esferaMaterial.color = Color.green;
        }
    }
}
