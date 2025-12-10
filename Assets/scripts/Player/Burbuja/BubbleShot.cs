using UnityEngine;
using System.Collections;

public class BubbleShot : MonoBehaviour
{
    public GameObject bubblePrefab;
    public Transform spawnPoint;
    public float bubbleSpeed = 20f;
    public float shotRate = 0.5f;
    private float shotRateTime = 0;
    public KeyCode shootKey = KeyCode.R;



    void Update()
    {
        if (bubblePrefab == null || spawnPoint == null) return;

        if (Input.GetKeyDown(shootKey))
        {
            // Funciona incluso con Time.timeScale = 0
            if (Time.unscaledTime > shotRateTime)
            {
                GameObject bubble = Instantiate(bubblePrefab, spawnPoint.position, spawnPoint.rotation);

               

                Bubble bubbleScript = bubble.GetComponent<Bubble>();
                if (bubbleScript != null)
                {
                    bubbleScript.floatSpeed = bubbleSpeed; // le pasamos la velocidad
                }
                

                shotRateTime = Time.unscaledTime + shotRate;

                Destroy(bubble, 5f);

                //StartCoroutine(UpdateBubbleTransform(bubble));
            }
        }
    }

    private IEnumerator UpdateBubbleTransform(GameObject bubble)
    {
        while (bubble != null)
        {
            bubble.transform.position = spawnPoint.position;
            bubble.transform.rotation = spawnPoint.rotation;
            yield return null; // se ejecuta cada frame, incluso con Time.timeScale = 0
        }
    }
}

