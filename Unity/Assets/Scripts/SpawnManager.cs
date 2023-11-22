using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject[] carPrefabs;
    [SerializeField] private GameObject spawnPoint;
    private float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.Find("spawnPoint1");
        spawnTime = 2;
        InvokeRepeating("SpawnCar",0,spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCar() 
    { 
        int randomIndex = Random.Range(0, carPrefabs.Length);
        Instantiate(carPrefabs[randomIndex], spawnPoint.transform.position, carPrefabs[randomIndex].transform.rotation);

    }

}
