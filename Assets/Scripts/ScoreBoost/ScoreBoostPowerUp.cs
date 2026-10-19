
using UnityEngine;

public class ScoreBoostPowerUp : MonoBehaviour
{
    private float scoreMultiplier = 1f;
    private float boostTimer = 0f;

    public void ActivateScoreBoost(float duration)
    {
        scoreMultiplier = 2f;
        boostTimer = duration;
    }

    private void Update()
    {
        if (boostTimer > 0f)
        {
            boostTimer -= Time.deltaTime;

            if (boostTimer <= 0f)
            {
                scoreMultiplier = 1f;
            }
        }
    }

    public int GetScore(int points)
    {
        return Mathf.RoundToInt(points * scoreMultiplier);
    }
}
