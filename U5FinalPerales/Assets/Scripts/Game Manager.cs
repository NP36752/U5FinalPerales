using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 5.0f;
    public bool isGameActive;
    public bool isPaused = false;
    private int score;
    public int lives;
    public GameObject titleScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public GameObject pauseScreen;
    public Button restartButton;


    public void StartGame(int difficulty)
    {
        StartCoroutine(SpawnTarget());
        spawnRate /= difficulty;
        score = 0;
        UpdateScore(0);
        isGameActive = true;
        titleScreen.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(true);
        lives++;
        UpdateLives();
    }
    
    IEnumerator SpawnTarget()
    {
        while (isGameActive && lives > 0)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    
    public void UpdateScore(int scoreToAdd)
    {
        score+= scoreToAdd;
        scoreText.text = "Score:" + score;
        
    }

    public void UpdateLives()
    {
       if (isGameActive)
        {
            lives--;
            livesText.text = "Lives:" + lives;
            if (lives == 0)
            {
                GameOver();
            }
        }
    }
    
    public void GameOver()
    {
        if (lives == 0)
        {
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            isGameActive = false;
        }        
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGameActive && !isPaused)
        {
            isPaused = true;
            isGameActive = false;
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.Space) && !isGameActive && isPaused)
        {
            isGameActive = true;
            isPaused = false;
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
        }
    }
}
