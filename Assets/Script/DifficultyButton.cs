using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    
    private Button button; 
    public ManagerGame gameManager; 
    public int levelDifficulty; 
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>(); 
        button.onClick.AddListener(setDifficulty); // Call on click 
        gameManager = GameObject.Find("World").GetComponent<ManagerGame>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setDifficulty()
    {
        Debug.Log("Difficulty Choosen: " + gameObject.name + "."); 
        gameManager.StartGame(levelDifficulty); 
    }
}
