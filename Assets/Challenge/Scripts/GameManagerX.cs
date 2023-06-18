using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerX : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartButton;
    public int duration = 60;
    private int time;

    public List<GameObject> targetPrefabs;

    private int score;
    private float spawnRate = 1.5f;
    public bool isGameActive;

    private float spaceBetweenSquares = 2.5f; 
    private float minValueX = -3.75f; //  x value of the center of the left-most square
    private float minValueY = -3.75f; //  y value of the center of the bottom-most square
    
    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        StartCoroutine(TimeUpdate());
        score = 0;
        time = duration;
        UpdateScore(0);
        titleScreen.SetActive(false);
    }
    
    IEnumerator TimeUpdate()
    {
        while (isGameActive)
        {
            UpdateTime();
            yield return new WaitForSeconds(1);
        }
    }
    
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, 4);

            if (isGameActive)
            {
                Instantiate(targetPrefabs[index], RandomSpawnPosition(), targetPrefabs[index].transform.rotation);
            }
        }
    }
    
    public Vector3 RandomSpawnPosition()
    {
        float spawnPosX = minValueX + (RandomSquareIndex() * spaceBetweenSquares);
        float spawnPosY = minValueY + (RandomSquareIndex() * spaceBetweenSquares);

        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);
        Debug.Log(spawnPosition);
        return spawnPosition;
    }
    
    int RandomSquareIndex()
    {
        return Random.Range(0, 4);
    }
    
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateTime()
    {
        time -= 1;
        timeText.text = "Time: " + time;
        if(time == 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

}
