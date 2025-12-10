using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float hitRadius = 0.5f;
    public string enemyTag = "Enemy"; // tag que identifica enemigos

    private Vector3 direction;

    void Start()
    {
        direction = transform.forward;
    }

    void Update()
    {
        float dt = Time.timeScale == 0 ? Time.unscaledDeltaTime : Time.deltaTime;
        Vector3 move = direction * speed * dt;

        // mover la bala
        transform.position += move;

        // detectar enemigos manualmente
        Collider[] hits = Physics.OverlapSphere(transform.position, hitRadius);
        foreach (Collider col in hits)
        {
            if (col.CompareTag(enemyTag))
            {
                Destroy(col.gameObject);
                Destroy(gameObject); // destruir la bala al impactar
                break; // solo destruir un enemigo por frame
            }
        }
    }
}
