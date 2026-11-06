using UnityEngine;
using System.Collections;
public class BallController : MonoBehaviour
{
    public float speed = 10f;
    float baseSpeed;
    Coroutine boostRoutine;
    Rigidbody rb;

    public AudioClip coinSound;
    public AudioClip redCoinSound;
    AudioSource audioSource;
    void Start()
    {
        baseSpeed = speed;
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    public void BoostSpeed(float multiplier, float seconds)
    {
        if (boostRoutine != null) StopCoroutine(boostRoutine); // refresh rule
        speed = baseSpeed * multiplier;
        boostRoutine = StartCoroutine(BoostTimer(seconds));
    }

    IEnumerator BoostTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        speed = baseSpeed;
        boostRoutine = null;
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal"); // A and D
        float z = Input.GetAxis("Vertical");   // W and S
        rb.AddForce(new Vector3(x, 0, z) * speed);
    }

    public int score = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            audioSource.PlayOneShot(coinSound);
            score++;
            GameManager.instance.CoinCollected(score);
        }
        else if (other.CompareTag("RedCoin"))
        {
            Destroy(other.gameObject);
            audioSource.PlayOneShot(redCoinSound);
            GameManager.instance.SpawnBlueCoins(transform.position);
        }
        else if (other.CompareTag("BlueCoin"))
        {
            Destroy(other.gameObject);
            audioSource.PlayOneShot(coinSound);
            GameManager.instance.BlueCoinCollected();
        }
    }
}