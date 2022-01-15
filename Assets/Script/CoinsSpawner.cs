using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    public GameObject[] ItemsPrefabs; 
    public GameObject m_arena; 
    public float squareWidth = 1.0f; 
    private float startDelay = 2.0f; 
    private float timer = 0.0f; 
    private float deltaSpawn = 0.0f;
    private float spawnIntervalRangeX = 1.0f; 
    private float spawnIntervalRangeY = 3.0f; 
    

    // Start is called before the first frame update
    void Start()
    {
        initPosition();   
        Invoke("SpawnItem", startDelay);  
        deltaSpawn = Random.Range(spawnIntervalRangeX, spawnIntervalRangeY);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (! GameVariables.isRunning)
        {
            return;
        }

        timer += Time.deltaTime;

        if (GameVariables.ncoins > GameVariables.nCoinsMax)
        {
            return; 
        }

        if (timer > deltaSpawn) 
        {
            SpawnItems(); 
            timer = 0.0f; 
            deltaSpawn = Random.Range(spawnIntervalRangeX, spawnIntervalRangeY);
        }

        
    }

    void SpawnItems()
    {
        
        // SpawnPosition 
        Transform carTransform = GameObject.FindGameObjectWithTag("Player").transform; 
        Vector3 spawnPosition = new Vector3(carTransform.position.x + Random.Range(-squareWidth/2.0f, squareWidth/2.0f),
                                         transform.position.y, 
                                         carTransform.position.z + Random.Range(-squareWidth/2.0f, squareWidth/2.0f));
        /*Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(-squareWidth/2.0f, squareWidth/2.0f),
                                         transform.position.y, 
                                         transform.position.z + Random.Range(-squareWidth/2.0f, squareWidth/2.0f)); */
        // Good or Trap item 
        float threshold = 1.0f - GameVariables.percentGoodCoins[GameVariables.difficulty]; // 70% de pieces = true
        bool goodOrBad = Random.Range(0.0f, 1.0f) > threshold; 
        int itemIndex = goodOrBad ? 0 : 1; 

        GameObject coin = Instantiate(ItemsPrefabs[itemIndex],  spawnPosition, 
                                        ItemsPrefabs[itemIndex].transform.rotation);
        GameVariables.ncoins += 1; // count items 
        CoinController ctrlC = coin.GetComponent<CoinController>(); 
        ctrlC.setGoodorBad(goodOrBad); 
    }

    

    void initPosition()
    {
        m_arena = GameObject.Find("Arena"); 
        if (m_arena)
        {
            Transform arenaTr = m_arena.transform; 
            transform.position = m_arena.transform.position + new Vector3(0.0f, 1.8f, 0.0f);
        }
    }
}

