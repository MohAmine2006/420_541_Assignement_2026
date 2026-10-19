
using UnityEngine;

// Gives the player a double jump for a limited time.
public class JumpBoostPowerUp : MonoBehaviour
{
    public float JumpTimeLeft { get; private set; }

    private void Update()
    {
        if (JumpTimeLeft <= 0f) return;

        JumpTimeLeft -= Time.deltaTime;

        if (JumpTimeLeft <= 0f)
        {
            JumpTimeLeft = 0f;
        }
    }

    public void ActivateJumpBoost(float duration)
    {
        JumpTimeLeft = duration;
    }
}
