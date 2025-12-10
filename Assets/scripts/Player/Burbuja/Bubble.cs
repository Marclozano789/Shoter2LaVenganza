using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float radius = 2f;          // radio de la burbuja
    public LayerMask enemyMask;
    public float floatHeight = 2f;     // altura de flotación
    public float floatSpeed = 2f;      // velocidad de flotación
    public bool trapEnemies = true;

    private void Update()
    {
        // Flotar hacia arriba
        //transform.position += Vector3.up * floatSpeed * Time.deltaTime;
        transform.position += transform.forward * floatSpeed * Time.deltaTime;
        transform.position += Vector3.up * 0.2f * Mathf.Sin(Time.time * 3f) * Time.unscaledDeltaTime;

        if (trapEnemies)
        {
        Debug.Log("paco");

            // Detectar enemigos dentro de la burbuja, aqui problemas
            Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyMask);
            foreach (Collider col in hits)
            {
                if (col.GetComponent<BubbleEnemy>() == null)
                {
                    col.gameObject.AddComponent<BubbleEnemy>().Trap(transform); //Problemas ??
                }
            }


        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }




}
