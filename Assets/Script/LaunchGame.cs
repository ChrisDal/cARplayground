
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchGame : MonoBehaviour
{
    public GameObject CoinsSpawner; 
    public GameObject car; 
    // Start is called before the first frame update
    void Start()
    {
        if (CoinsSpawner == null){
            Debug.Log("Fill Prefab."); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchOn()
    {
        Transform tCar  = car.GetComponent<Transform>(); 
        GameObject child = Instantiate(CoinsSpawner,  tCar.position + new Vector3(0.0f, 3.0f,0.0f), 
                    tCar.rotation); 

        GameVariables.isRunning = true; 
    }
}
