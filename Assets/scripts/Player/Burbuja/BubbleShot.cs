using UnityEngine;

public class BubbleShot : MonoBehaviour
{
    public GameObject bubblePrefab;
    public Transform spawnPoint;
    public float bubbleSpeed = 20f;
    public float bubbleLifetime = 10f;
    public KeyCode shootKey = KeyCode.R;

    void Update()
    {
        if (Input.GetKeyDown(shootKey) && Time.timeScale > 0f) // No dispara en ZaWardo
        {
            GameObject bubble = Instantiate(bubblePrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody rb = bubble.GetComponent<Rigidbody>();
            if (rb != null)
                rb.linearVelocity = spawnPoint.forward * bubbleSpeed;

            Destroy(bubble, bubbleLifetime);
        }
    }
}

