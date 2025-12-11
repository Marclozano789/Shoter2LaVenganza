using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PlayerSwapAbility : MonoBehaviour
{
    public float maxSwapDistance = 30f;
    public KeyCode swapKey = KeyCode.Q;
    public float swapDuration = 0.15f; // muy rápido, estilo Boogie Woogie

    private bool swapping = false;

    void Update()
    {
        if (Input.GetKeyDown(swapKey))
            TrySwap();
    }

    void TrySwap()
    {
        if (swapping)
            return;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
            return;

        // Encontrar el más cercano
        GameObject closest = null;
        float minDist = Mathf.Infinity;

        foreach (var e in enemies)
        {
            float d = Vector3.Distance(transform.position, e.transform.position);
            if (d < minDist)
            {
                closest = e;
                minDist = d;
            }
        }

        if (closest == null || minDist > maxSwapDistance)
            return;

        StartCoroutine(SwapSmooth(closest.transform));
    }

    IEnumerator SwapSmooth(Transform enemy)
    {
        swapping = true;

        // Guardar componentes
        NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();

        // Desactivar movimiento enemigo durante la transición
        if (agent != null)
        {
            agent.ResetPath();
            agent.isStopped = true;
        }

        // Guardar posiciones originales
        Vector3 startPlayer = transform.position;
        Vector3 startEnemy = enemy.position;

        Quaternion startRotPlayer = transform.rotation;
        Quaternion startRotEnemy = enemy.rotation;

        // Direcciones finales
        Vector3 finalPlayerDir = (startEnemy - startPlayer).normalized;
        Vector3 finalEnemyDir = (startPlayer - startEnemy).normalized;

        Quaternion finalRotPlayer = Quaternion.LookRotation(finalPlayerDir);
        Quaternion finalRotEnemy = Quaternion.LookRotation(finalEnemyDir);

        float t = 0f;

        // Interpolación suave (curva tipo EaseInOut)
        while (t < 1f)
        {
            t += Time.deltaTime / swapDuration;
            float lerp = Mathf.SmoothStep(0, 1, t);

            // Interpolación de posiciones
            transform.position = Vector3.Lerp(startPlayer, startEnemy, lerp);
            enemy.position = Vector3.Lerp(startEnemy, startPlayer, lerp);

            // Interpolación de rotación
            //transform.rotation = Quaternion.Slerp(startRotPlayer, finalRotPlayer, lerp);
            //enemy.rotation = Quaternion.Slerp(startRotEnemy, finalRotEnemy, lerp);

            // El jugador mira al enemigo (sin inclinarse)
            Vector3 lookDirPlayer = enemy.position - transform.position;
            lookDirPlayer.y = 0;
            lookDirPlayer.Normalize();
            if (lookDirPlayer.sqrMagnitude > 0.001f)
                transform.rotation = Quaternion.LookRotation(lookDirPlayer);

            // El enemigo mira al jugador (sin inclinarse)
            Vector3 lookDirEnemy = transform.position - enemy.position;
            lookDirEnemy.y = 0;
            lookDirEnemy.Normalize();
            if (lookDirEnemy.sqrMagnitude > 0.001f)
                enemy.rotation = Quaternion.LookRotation(lookDirEnemy);

            yield return null;
        }

        // Reactivar el NavMeshAgent
        if (agent != null)
            agent.isStopped = false;

        swapping = false;
    }

}
