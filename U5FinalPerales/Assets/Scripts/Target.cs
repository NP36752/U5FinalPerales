using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -6;
    public AudioClip nukeBallSound;
    public AudioClip eightBallSound;
    public AudioClip eyeBallSound;
    public AudioClip skullSound;
    private AudioSource ballSounds;
    private GameManager gameManager;
    public int pointValue;
    public ParticleSystem explosionParticle;
    bool outOfBounds = false;
    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        ballSounds = GetComponent<AudioSource>();
    }
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
    public void OnMouseDown()
    {
        Destroy(gameObject);
        gameManager.UpdateScore(pointValue);
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        if (gameObject.CompareTag("Bad"))
        {
            gameManager.UpdateLives();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            outOfBounds = true;
            if (!gameObject.CompareTag("Bad"))
            {
                gameManager.UpdateLives();
            }
        }
    }
    private void OnDestroy()
    {
        if (gameObject.CompareTag("Bad") && !outOfBounds)
        {
            ballSounds.PlayOneShot(skullSound);
        }
        if (gameObject.CompareTag("Eyeball") && !outOfBounds)
        {
            ballSounds.PlayOneShot(eyeBallSound);
        }
        if (gameObject.CompareTag("NukeBall") && !outOfBounds)
        {
            ballSounds.PlayOneShot(nukeBallSound);
        }
        if (gameObject.CompareTag("8Ball") && !outOfBounds)
        {
            ballSounds.PlayOneShot(eightBallSound);
        }
        // i gave up on adding sound effects when the balls are destroyed i cant figure it out
    }
}
