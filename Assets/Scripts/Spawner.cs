using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] puntosSpawn;
    [SerializeField] private Enemigo enemigoPrefab;

    void Start()
    {
        StartCoroutine(SpawnearEnemigo());
    }

    private IEnumerator SpawnearEnemigo()
    {
        while (true)
        {
            // Solo spawnea enemigos si el Time.timeScale es igual a 1
            if (Time.timeScale == 1)
            {
                Instantiate(enemigoPrefab, puntosSpawn[Random.Range(0, puntosSpawn.Length)].position, Quaternion.identity);
            }
            yield return new WaitForSeconds(2); // Espera 2 segundos
        }
    }
}
