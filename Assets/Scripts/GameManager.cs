using System;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    public static GameManager instance { get; private set; }
    private bool isGameOver;
    private bool isGameWin = false;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform startPosition;
    [SerializeField] private CinemachineCamera cinemachineCamera;
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private GameObject GameWinUI;

    private CoinController[] coins;
    private JumpGemController[] jumpGems;
    private CheckPointController[] checkPoints;
    private CheckPointController lastCheckPoint = null;
    
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        coins = FindObjectsOfType<CoinController>();
        jumpGems = FindObjectsOfType<JumpGemController>();
        checkPoints = FindObjectsOfType<CheckPointController>();
        UpdateScoreText();
        SpawnPlayer();
        GameOverUI.SetActive(false);
        GameWinUI.SetActive(false);
    }

    public void SpawnPlayer(bool respawnLastCheckPoint = true)
    {
        GameObject newPlayerGameObject;
        if(respawnLastCheckPoint)
            newPlayerGameObject = Instantiate(playerPrefab, lastCheckPoint ? lastCheckPoint.transform.position : startPosition.position, Quaternion.identity); 
        else
            newPlayerGameObject = Instantiate(playerPrefab, startPosition.position, Quaternion.identity); 
        cinemachineCamera.Follow = newPlayerGameObject.transform;
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        isGameOver = true;
        score = 0;
        GameOverUI.SetActive(true);
    }

    public void GameWin()
    {
        isGameWin = true;
        score = 0;
        GameWinUI.SetActive(true);
    }

    public bool IsGameWin()
    {
        return isGameWin;
    }

    public void RestartGame()
    {
        isGameOver = false;
        score = 0;
        UpdateScoreText();
        SpawnPlayer(false);
        GameOverUI.SetActive(false);
        ResetCoins();
        ResetJumpGems();
        ResetCheckPoints();
    }
    
    public void RestartGameFromLastCheckPoint()
    {
        isGameOver = false;
        UpdateScoreText();
        SpawnPlayer(true);
        GameOverUI.SetActive(false);
        ResetJumpGems();
    }
    
    public void BackToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
    
    public void LoadLevel(int levelNumber)
    {
        string levelName = "Level " + levelNumber;
        SceneManager.LoadScene(levelName);
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
    
    private void ResetCoins()
    {
        foreach (CoinController coin in coins)
        {
            coin.gameObject.SetActive(true);
        }
    }
    
    private void ResetJumpGems()
    {
        foreach (JumpGemController jumpGem in jumpGems)
        {
            jumpGem.gameObject.SetActive(true);
        }
    }
    
    private void ResetCheckPoints()
    {
        foreach (CheckPointController checkPoint in checkPoints)
        {
            checkPoint.DisableCheckPoint();
        }
    }
    
    public void SetLastCheckPoint(CheckPointController checkPoint)
    {
        lastCheckPoint = checkPoint;
        foreach (CheckPointController oldCheckPoint in checkPoints)
        {
            if (String.CompareOrdinal(oldCheckPoint.name, checkPoint.name) < 0)
            {
                oldCheckPoint.SetCheckpoint();
            }
        }
    }
}