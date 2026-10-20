
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public Transform respawnPoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);

                if (playerHealth.CurrentHealth <= 0)
                {
                    return;
                }
            }

            other.transform.position = respawnPoint.position;
            Debug.Log("You hit a trap! Back to start.");
        }

        Rigidbody playerRb = other.GetComponent<Rigidbody>();

        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
        }

        other.transform.position = respawnPoint.position;
    }
}
