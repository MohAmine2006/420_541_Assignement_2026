
using UnityEngine;

// A score boost pickup. When the player touches it, it doubles points,
// plays a sound and particles, hides, and comes back after a delay.
public class ScoreBoostPickup : MonoBehaviour
{
    [SerializeField] private float duration = 10f;
    [SerializeField] private float respawnDelay = 30f;
    [SerializeField] private GameObject visuals;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private GameObject pickupEffect;

    private void OnTriggerEnter(Collider other)
    {
        // Only the player has a ScoreBoostPowerUp component
        ScoreBoostPowerUp scoreBoost = other.GetComponent<ScoreBoostPowerUp>();
        if (scoreBoost == null) return;

        scoreBoost.ActivateScoreBoost(duration);

        // Sound and particles
        GetComponent<AudioSource>().PlayOneShot(pickupSound);
        Instantiate(pickupEffect, transform.position, Quaternion.identity);

        // Disappear, and reappear after respawnDelay seconds
        visuals.SetActive(false);
        GetComponent<Collider>().enabled = false;
        Invoke(nameof(Reappear), respawnDelay);
    }

    private void Reappear()
    {
        visuals.SetActive(true);
        GetComponent<Collider>().enabled = true;
    }
}
