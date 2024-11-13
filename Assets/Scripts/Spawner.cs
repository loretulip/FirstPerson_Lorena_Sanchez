using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] puntosSpawn;
    [SerializeField] private Enemigo enemigoPrefab;

    void Start()
    {
       
    }
    IEnumerator SpawnearEnemigo()
    {
        yield return new WaitForSeconds(2);
        //Saca una copia de u enemigo en el punto 0 con rotaci�n 0,0,0
        //          QU�             D�NDE           C�MO
        Instantiate(enemigoPrefab, puntosSpawn[0].position, Quaternion.identity);
    }
}
