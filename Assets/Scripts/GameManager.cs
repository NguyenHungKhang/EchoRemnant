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

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateScoreText();
        SpawnPlayer();
        GameOverUI.SetActive(false);
        GameWinUI.SetActive(false);
    }

    public void SpawnPlayer()
    {
        GameObject newPlayerGameObject = Instantiate(playerPrefab, startPosition.position, Quaternion.identity);
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
        SpawnPlayer();
        GameOverUI.SetActive(false);
        SceneManager.LoadScene("Game");
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}