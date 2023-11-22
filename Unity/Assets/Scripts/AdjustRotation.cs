using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AdjustRotation : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform carTransform;
    private CarMeshAgent carMeshAgent;
    void Start()
    {
        carMeshAgent = GetComponent<CarMeshAgent>();
        carTransform = gameObject.transform;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject==carMeshAgent.wayPoint)
        {
            transform.rotation = other.transform.rotation;
            transform.position = other.transform.position;
            carMeshAgent.SecondDestination();
        }
    }

}
