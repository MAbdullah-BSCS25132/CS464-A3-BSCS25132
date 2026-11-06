using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float multiplier = 1.8f;
    public float duration = 5f;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        BallController ball = other.GetComponent<BallController>();
        if (ball != null) ball.BoostSpeed(multiplier, duration);

        Destroy(gameObject);
    }
}