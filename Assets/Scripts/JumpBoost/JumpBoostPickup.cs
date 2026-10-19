
using UnityEngine;

// A jump boost pickup. When the player touches it, it gives the boost,
// plays a sound and particles, hides, and comes back a few seconds later.
public class JumpBoostPickup : MonoBehaviour
{
    [SerializeField] private float duration = 30f;
    [SerializeField] private float respawnDelay = 30f;
    [SerializeField] private GameObject visuals;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private GameObject pickupEffect;

    private void OnTriggerEnter(Collider other)
    {
        JumpBoostPowerUp jumpBoost = other.GetComponent<JumpBoostPowerUp>();
        if (jumpBoost == null) return;

        jumpBoost.ActivateJumpBoost(duration);

        GetComponent<AudioSource>().PlayOneShot(pickupSound);
        Instantiate(pickupEffect, transform.position, Quaternion.identity);

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
