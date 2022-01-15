using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    public ReticleController Reticle;
    public float m_speed = 1.0f;
    public float max_speed = 5.0f; 
    public float min_speed = 0.0f; 

    public static int collectedCoins = 0; 
    ManagerGame countingThings; 

    void Start()
    {
        // Game is starting when car exists 
        GameVariables.isRunning = true; 
        countingThings = GameObject.Find("World").GetComponent<ManagerGame>(); 
    }

    void Update()
    {
        if (! GameVariables.isRunning)
        {
            return; 
        }
        
        var trackingPosition = Reticle.transform.position;
        // Back on planes if falling 
        if (transform.position.y < -10.0f)
        {
            transform.position = trackingPosition; 
        }

        if (Vector3.Distance(trackingPosition, transform.position) < 0.1)
        {
            return;
        }

        

        var lookRotation = Quaternion.LookRotation(trackingPosition - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        transform.position = Vector3.MoveTowards(transform.position, trackingPosition, m_speed * Time.deltaTime);
    }


    void OnTriggerEnter(Collider other)
    {
        var coinController = other.GetComponent<CoinController>();
        if (coinController != null)
        {
            collectedCoins += coinController.scoreValue;
            GameVariables.ncoins -= 1;  
            countingThings.UpdateCoins(); 
            Destroy(other); 
           
            
        }
        else if (other.GetComponent<BallController>() != null)
        {
            collectedCoins += other.GetComponent<BallController>().scoreValue;
            countingThings.UpdateCoins(); 
            
        }
    }

}
