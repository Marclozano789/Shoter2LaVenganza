using UnityEngine;
using UnityEngine.AI;

public class BubbleEnemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Rigidbody rb;
    private Transform bubble;

    private Vector3 originalVelocity;

    public void Trap(Transform bubbleTransform)
    {
        bubble = bubbleTransform;

        // Desactivar NavMeshAgent temporalmente
        agent = GetComponent<NavMeshAgent>();
        if (agent != null) agent.enabled = false;

        // Detener Rigidbody si existe
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            originalVelocity = rb.linearVelocity;
            rb.isKinematic = true;
        }
    }

    private void Update()
    {
        if (bubble != null)
        {
            // Mantener al enemigo en el centro de la burbuja
            transform.position = bubble.position;
        }
        else
        {
            // Restaurar movimiento al desaparecer la burbuja
            if (agent != null) agent.enabled = true;
            if (rb != null) rb.isKinematic = false;

            Destroy(this);
        }
    }




}
