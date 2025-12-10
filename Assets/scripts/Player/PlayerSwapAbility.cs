using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class PlayerSwapAbility : MonoBehaviour
{
    public float maxSwapDistance = 30f;
    public KeyCode swapKey = KeyCode.Q;

    void Update()
    {
        if (Input.GetKeyDown(swapKey))
            TrySwap();
    }

    void TrySwap()
    {
        // Encontrar todos los objetos con TAG Enemy
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
            return;

        // Seleccionar al enemigo más cercano
        GameObject closest = enemies
            .OrderBy(e => Vector3.Distance(transform.position, e.transform.position))
            .FirstOrDefault();

        if (closest == null)
            return;

        float dist = Vector3.Distance(transform.position, closest.transform.position);
        if (dist > maxSwapDistance)
            return;

        // Guardar posiciones originales
        Vector3 playerPos = transform.position;
        Vector3 enemyPos = closest.transform.position;

        // Calcular direcciones ANTES de mover
        Vector3 dirJugador = (enemyPos - playerPos).normalized; // hacia el enemigo
        Vector3 dirEnemigo = (playerPos - enemyPos).normalized; // hacia el jugador

        // Mover jugador
        transform.position = enemyPos;
        if (dirJugador != Vector3.zero)
            transform.forward = dirJugador;

        // Mover enemigo
        NavMeshAgent agent = closest.GetComponent<NavMeshAgent>();
        if (agent != null)
            agent.Warp(playerPos);
        else
            closest.transform.position = playerPos;

        if (dirEnemigo != Vector3.zero)
            closest.transform.forward = -dirEnemigo; // de espaldas al jugador
    }

}
