
using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    void Update()
    {
        // Rotate the coin and make it bob up and down
        transform.Rotate(0f, 0f, 100f * Time.deltaTime);

        transform.localPosition = new Vector3(
            0f,
            0.15f + Mathf.Sin(Time.time * 2f) * 0.1f,
            0f
        );
    }
}
