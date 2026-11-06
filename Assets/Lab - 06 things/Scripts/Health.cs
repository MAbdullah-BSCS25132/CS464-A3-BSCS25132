using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{

    public int maxHealth = 3;      // number of hearts
    public float iFrames = 1f;     // seconds of protection after a hit

    public int current;
    float invulnerableUntil;

    void Awake()
    {
        current = maxHealth;
    }

    // returns true if the hit really landed
    IEnumerator Flash()
    {
        Renderer r = GetComponentInChildren<Renderer>();
        Color old = r.material.color;
        r.material.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        r.material.color = old;
    }
    public bool TakeDamage(int amount)
    {
        if (Time.time < invulnerableUntil) return false; // still protected
        if (current <= 0) return false;                  // already dead

        current -= amount;
        invulnerableUntil = Time.time + iFrames;
        StartCoroutine(Flash());
        if (GameManager.instance != null) GameManager.instance.UpdateHearts();
        return true;
    }
    public void ResetHealth(float protectSeconds = 2f)
    {
        current = maxHealth;
        invulnerableUntil = Time.time + protectSeconds; // a moment of protection
    }
    public void Kill()
    {
        current = 0;
        if (GameManager.instance != null) GameManager.instance.UpdateHearts();
    }
}