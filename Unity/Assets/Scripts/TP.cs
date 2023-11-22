using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TP : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("car")) 
        {
            GameObject car = other.gameObject;
            CarMeshAgent agent = car.GetComponent<CarMeshAgent>();
            agent.agent.enabled = false;
            car.transform.position = spawnPosition.position;
            car.transform.rotation = spawnPosition.rotation;
            if (agent.currentFloor < agent.destinationFloor) 
            {
                agent.currentFloor++;
            }
            if (agent.currentFloor > agent.destinationFloor)
            {  
                agent.currentFloor--;
            }

            agent.agent.enabled = true;


        }
    }
}
