using System.Collections;
using UnityEngine;
using UnityEngine.UI; 
using TMPro; 
using UnityEngine.SceneManagement; 

/* Game Manager
Game Manager Script : 
Score (coins) and Time
Show GameOver and restart Button
*/
public class ManagerGame : MonoBehaviour
{
    // Private Var declaration
    Text timerText; 
    Text coinsText; 
    GameObject canvasObj; 
    public Transform childText; 
    public Transform childCoins; 

    public TextMeshProUGUI gameOverText; 
    public GameObject titleScreen; 
    public Button restartButton;
    public GameObject prefabArena; 
    private GameObject sandboxlabel; 


    
    // Start is called before the first frame update
    void Start()
    {
        titleScreen.gameObject.SetActive(true);
        sandboxlabel = GameObject.Find("Canvas/sandboxLbl");  
        sandboxlabel.SetActive(false); 
        prefabArena = GameObject.Find("Arena"); 
        prefabArena.SetActive(false); 
    }

    public void StartGame(int difficulty)
    {
        // Difficulty 
        GameVariables.difficulty = difficulty; 
        Debug.Log("Set Level Difficulty to " + GameVariables.difficulty.ToString()); 

        // Timer 
        Debug.Log("Current Time =" + GameVariables.currentTime.ToString());
        sandboxlabel.SetActive(true); 
        // get timer 
        childText = sandboxlabel.transform.GetChild(0); 
        timerText = childText.GetComponent<Text>(); 
        timerText.text = "Timer: " + GameVariables.currentTime.ToString() + "s";
        

        // Scoring 
        childCoins = sandboxlabel.transform.GetChild(1); 
        coinsText = childCoins.GetComponent<Text>(); 
        coinsText.text = "Score: 0"; 

        // Game Over Panel
        gameOverText.gameObject.SetActive(false); 
        restartButton.gameObject.SetActive(false); 
        
        titleScreen.gameObject.SetActive(false);
        Debug.Log("Canvas set."); 

        // Activated plane detection
        prefabArena.SetActive(true); 
         
    }

    // Update is called once per frame
    void Update()
    {
        if (GameVariables.currentTime <= 0)
        {
            GameVariables.isRunning = false; 
            GameOver();
        }
    }

    IEnumerator TimerTick()
    {
        Debug.Log("Current Time " + GameVariables.currentTime.ToString()); 
        while (GameVariables.currentTime > 0)
        {
            // attendre une seconde 
            yield return new WaitForSeconds(1);
            GameVariables.currentTime--; 
            timerText.text = "Timer: " + GameVariables.currentTime.ToString() + "s";
        }

    }

    

    public void StartTimer()
    {
        Debug.Log("In Start Coroutine will start");
        StartCoroutine(TimerTick());
    }

    public void UpdateCoins()
    {
        if (GameVariables.isRunning)
        {
            coinsText.text = "Score: " + CarController.collectedCoins.ToString();
        }
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);  
    }

    public void RestartGame()
    {
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false); 
        // Reset Score and Time 
        resetGameVariables(); 
        // reloads same scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        
         
    }

    void resetGameVariables()
    {
        GameVariables.currentTime = GameVariables.allowedTime;
        GameVariables.ncoins = 0;
        GameVariables.isRunning = false; 
        GameVariables.difficulty = -1; 
    }
}
