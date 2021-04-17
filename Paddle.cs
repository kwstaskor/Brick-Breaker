using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;
    // Cache references
    GameSession gameSession;
    Ball ball;
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
    }
    void Update()
    {
        var paddlePossition = new Vector2(transform.position.x, transform.position.y);
        paddlePossition.x = Mathf.Clamp(GetXPos(), minX, maxX);
        transform.position = paddlePossition;
    }


    private float GetXPos()
    {
        if (gameSession.IsAutoPlayEnabled())
        {
            return ball.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
