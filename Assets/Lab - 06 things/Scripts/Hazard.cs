using UnityEngine;

public class Hazard : MonoBehaviour
{
    public int damage = 1;
    public float knockback = 10f;
    public AudioClip warningSound;
    [Range(0f, 1f)] public float volume = 1f;
    void OnTriggerEnter(Collider other) { Hit(other); } // first touch
    void OnTriggerStay(Collider other) { Hit(other); }  // standing inside

    void Hit(Collider other)
    {

        if (!other.CompareTag("Player")) return;
        if (!other.TryGetComponent(out Health health)) return;


        // knockback only happens if the hit really landed
        if (health.TakeDamage(damage))
        {
            Rigidbody body = other.attachedRigidbody;
            Vector3 away = body.position - transform.position;
            away.y = 0f;

            if (health.current > 0)
            {
                if (warningSound != null)
                    AudioSource.PlayClipAtPoint(warningSound, Camera.main.transform.position, volume);
                Vector3 v = body.linearVelocity;
                body.linearVelocity = new Vector3(0f, v.y, 0f);
                body.AddForce(away.normalized * knockback, ForceMode.Impulse);
            }
        }
    }
}