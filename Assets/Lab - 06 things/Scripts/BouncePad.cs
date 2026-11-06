using UnityEngine;

public class BouncePad : MonoBehaviour
{

    public AudioClip bounceSound;
    [Range(0f, 1f)] public float volume = 1f;
    public float bounce = 8f;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (bounceSound != null)
            AudioSource.PlayClipAtPoint(bounceSound, Camera.main.transform.position, volume);

        Rigidbody body = other.attachedRigidbody;
        Vector3 v = body.linearVelocity;
        body.linearVelocity = new Vector3(v.x, 0f, v.z); // stop falling first
        body.AddForce(Vector3.up * bounce, ForceMode.VelocityChange);
    }
}