using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    
    public GameObject ballFUS; 
    public float deltaSpawn; 
    
    // Start is called before the first frame update
    void Start()
    {
        deltaSpawn = GameVariables.spawnBallRate[GameVariables.difficulty]; 
        StartCoroutine(SpawnBalls()); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnBalls() {
        while (GameVariables.isRunning)
        {
            // wait for delta seconds 
            yield return new WaitForSeconds(deltaSpawn); 
            Transform parentTrf = transform.GetComponentInParent<Transform>(); 
            Quaternion rotToBall = new Quaternion();
            rotToBall.eulerAngles = new Vector3(0.0f, parentTrf.rotation.eulerAngles.y, 0.0f); 

            
            Transform carTrf = GameObject.FindGameObjectWithTag("Player").transform; 
            Ray rayToCar = new Ray(parentTrf.position, carTrf.position - parentTrf.position ); 
            Vector3 ballDir = new Vector3(); 
            ballDir = rayToCar.direction;
            ballDir.y = 0.0f; // go straight and not oriented towards the floor 
            ballDir.Normalize(); 
            Debug.DrawRay(rayToCar.origin, 3.0f*ballDir, Color.yellow, 2.0f); 
            
            GameObject ball = Instantiate(ballFUS,  transform.position, rotToBall);
            ball.GetComponent<BallController>().setDirection(ballDir);  
        }
        
    }

    // Rotation in EulerAngles != Editor Angles 
    // EulerAngles : [ 0: 360] => 270 == -90
    private Vector3 defineOrientation(Transform ptf)
    {
        Vector3 dir = new Vector3();  
        if (Mathf.Abs((ptf.rotation.eulerAngles.y - 270.0f)) < 0.1f )
        {
            dir = Vector3.forward; 
        }
        else if (Mathf.Abs((ptf.rotation.eulerAngles.y - 90.0f)) < 0.1f)
        {
            dir = Vector3.back; 
        }
        else if (Mathf.Abs(ptf.rotation.eulerAngles.y) < 0.1f && Mathf.Abs(ptf.rotation.eulerAngles.z - 270.0f) < 0.1f)
        {
            dir = Vector3.right; 
        }
        else if (Mathf.Abs(ptf.rotation.eulerAngles.y - 180.0f) < 0.1f && Mathf.Abs(ptf.rotation.eulerAngles.z -270.0f) < 0.1f)
        {
            dir = Vector3.left; 
        }
        else 
        {
            dir = Vector3.back; 
        }

        return dir; 
    }

    

   
}
