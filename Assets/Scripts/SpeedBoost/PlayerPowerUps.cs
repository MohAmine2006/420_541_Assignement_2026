
using UnityEngine;

// Manages timed power-ups for the player.
public class PlayerPowerUps : MonoBehaviour
{
    [SerializeField] private float boostMultiplier = 2f;

    public float SpeedTimeLeft { get; private set; }

    private CharacterMovement movement;

    private void Start()
    {
        movement = GetComponent<CharacterMovement>();
    }

    public void ActivateSpeedBoost(float duration)
    {
        SpeedTimeLeft = duration;
        movement.SpeedMultiplier = boostMultiplier;
    }

    private void Update()
    {
        if (SpeedTimeLeft <= 0f) return;

        SpeedTimeLeft -= Time.deltaTime;

        if (SpeedTimeLeft <= 0f)
        {
            SpeedTimeLeft = 0f;
            movement.SpeedMultiplier = 1f;
        }
    }
}
