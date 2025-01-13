using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bazooka : MonoBehaviour
{
    [SerializeField] private GameObject gatitoPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private AudioSource disparoBazooka;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DisparoGranada();
    }
    private void DisparoGranada()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // Creo una instancia con la misma orientacion que el cañon
            Instantiate(gatitoPrefab,spawnPoint.position,spawnPoint.rotation);
            disparoBazooka.Play();
        }
    }
    
}
