using UnityEngine;

public class Ball : MonoBehaviour
{
    //config params
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 17f;
    [SerializeField] Paddle paddle;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.8f;
    //state
    Vector2 paddleToBallVector;
    private bool hasStarted = false;

    // Cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidbody2D;
    void Start()
    {
        paddleToBallVector = transform.position - paddle.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButton(0))
        {
            myRigidbody2D.velocity = new Vector2(xPush, yPush);
            hasStarted = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePossition = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePossition + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));
        if (hasStarted)
        {
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidbody2D.velocity += velocityTweak;
        }
    }
}
