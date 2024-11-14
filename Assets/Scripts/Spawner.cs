using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
            //Saca una copia de u enemigo en el punto 0 con rotaci�n 0,0,0
            //          QU�             D�NDE           C�MO
            Instantiate(enemigoPrefab, puntosSpawn[Random.Range(0,puntosSpawn.Length)].position, Quaternion.identity);
            yield return new WaitForSeconds(2);
        }
    }

}
