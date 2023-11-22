using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;

public class CarMeshAgent : MonoBehaviour
{
    public int currentFloor;
    public int destinationFloor;
    public GameObject wayPoint;
    public GameObject wayPoint2;
    private bool parkingReached;
    public NavMeshAgent agent;
    private Vector3 destination;

    // Start is called before the first frame update     

    void Start()
    {
        parkingReached = false;
        currentFloor = 1;
        agent = GetComponent<NavMeshAgent>();
        ApiManager.instance.RequestAssignedPosition(HandleAssignedPosition);
    }

    void HandleAssignedPosition(int[] position)
    {

        // Do something with the assigned position
        destinationFloor = position[0] - 1;
        int parkingSlot = position[1];
        string parkingString = destinationFloor + " " + parkingSlot;
        
        wayPoint = GameObject.Find(destinationFloor + "R" + parkingSlot);
        wayPoint2 = GameObject.Find(destinationFloor + "WayPoint" + parkingSlot);
        Debug.Log(wayPoint.name + " " + wayPoint2.name);
        destination = wayPoint.transform.position;

        if(wayPoint == null|| wayPoint2 == null)
        {
            Debug.Log("referencia numal en " + parkingString);
        }

    }


    // Update is called once per frame
    void Update()
    {
        
            if (currentFloor == destinationFloor && !parkingReached)
            {
                if (wayPoint == null)
                {
                    Debug.Log("Error en el de arriba");
                }
                destination = wayPoint.transform.position;
                parkingReached = true;
            }

            if (currentFloor < destinationFloor)
            {
                GoToNextLevel();
            }
        
            if (currentFloor > destinationFloor)
            {
                GoToPreviousLevel();
            }     

            agent.destination = destination;

    }

    public void GoToNextLevel()
    {

        string teleporterName = currentFloor + "TP" + (currentFloor+1);
        GameObject teleporter = GameObject.Find(teleporterName);    
        destination = teleporter.transform.position;
    }

    public void GoToPreviousLevel()
    {
        string teleporterName = currentFloor + "TP" + (currentFloor - 1);
        GameObject teleporter = GameObject.Find(teleporterName);
        destination = teleporter.transform.position;

    }

    public void SecondDestination() 
    {
        if (wayPoint2 == null)
        {
            Debug.Log("error en el de arriba");
        }

        destination = wayPoint2.transform.position;
    }



}
