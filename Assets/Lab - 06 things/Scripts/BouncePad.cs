using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float bounce = 8f;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Rigidbody body = other.attachedRigidbody;
        Vector3 v = body.linearVelocity;
        body.linearVelocity = new Vector3(v.x, 0f, v.z); // stop falling first
        body.AddForce(Vector3.up * bounce, ForceMode.VelocityChange);
    }
}