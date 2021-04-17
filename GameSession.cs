using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    //parms
    [Range(0.5f , 2f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 7;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool isAutoPlayEnabled;
    //state
    [SerializeField] int currentScore = 0;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
            DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        Cursor.visible = false;
        scoreText.text ="Score : " + currentScore.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }
    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        scoreText.text = "Score : " + currentScore.ToString();
    }
    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }
}
