using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    PlayerController playerController;
    Boss boss;

    //Statci Instance
    public static GameManager instance;
    
    public bool isGameActive;
    public GameObject startPanel;
    public GameObject aboutPanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject winnerPanel;
    public GameObject healthBarUI;
    public GameObject playerPrefab;
    public GameObject chestPrefab;
    public GameObject bossPrefab;
    

    void Awake()
    {
        // static instance 

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        isGameActive = false;

        Time.timeScale = 0.0f;

        //References

        playerController = playerPrefab.GetComponent<PlayerController>();
        boss = bossPrefab.GetComponent<Boss>();

        //UI Panel
        startPanel.gameObject.SetActive(true);
        aboutPanel.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(false);
        healthBarUI.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        chestPrefab.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();

        if (boss.currnentHealth <= 0) 
        {
            chestPrefab.SetActive(true);
        }
    }

    public void StartGame() 
    {
        // start game code goes here
        isGameActive = true;
        startPanel.gameObject.SetActive(false);
        healthBarUI.SetActive(true);

        Time.timeScale = 1.0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }

	public void GameOver()
	{
		if (playerController.currnentHealth <= 0)
		{
			// Game over code goes here
			gameOverPanel.SetActive(true);
            Time.timeScale = 0.0f;

		}
	}

    public void GameWon()
    {
        winnerPanel.SetActive(true);
        Time.timeScale = 0.0f;
    }

	public void ShowAboutPanel() 
    {
        startPanel.SetActive(false);
        aboutPanel.SetActive(true);
    }

    public void ReturnToMainMenu() 
    {
        aboutPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    public void PauseGame() 
    {
        Time.timeScale = 0.0f;
        pausePanel.SetActive(true);
    }

    public void ContinueGame() 
    {
        Time.timeScale = 1.0f;
        pausePanel.SetActive(false);
    }
    public void QuitGame() 
    {
        Application.Quit();
    }

  
}
