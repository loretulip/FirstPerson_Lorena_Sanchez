using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bazooka : MonoBehaviour
{
    [SerializeField] private GameObject gatitoPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private AudioSource granadaGato;
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
            // Creo una instancia con la misma orientacion que el ca�on
            Instantiate(gatitoPrefab,spawnPoint.position,spawnPoint.rotation);
        }
    }
    private void ReproducirSonido(AudioClip clip)
    {
        Debug.Log("Maullido");
        granadaGato.PlayOneShot(clip);
    }
}
