using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granada : MonoBehaviour
{
    [SerializeField] int fuerza;
    [SerializeField] float tiempoVida;
    [SerializeField] float radioExplosion;
    [SerializeField] private LayerMask queEsExplotable;
    [SerializeField] GameObject explosionPrefab;

    [SerializeField] private AudioClip sonidoGato;
    private AudioSource audioSource;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward.normalized * fuerza, ForceMode.Impulse);
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = sonidoGato;
        audioSource.spatialBlend = 1f;
        audioSource.minDistance = 1f;
        audioSource.maxDistance = 15f;
        audioSource.Play();
        Destroy(gameObject, tiempoVida);
    }

    void Update()
    {
    }

    private void OnDestroy()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Collider[] collsDetectados = Physics.OverlapSphere(transform.position, radioExplosion, queEsExplotable);
        if (collsDetectados.Length > 0)
        {
            foreach (Collider coll in collsDetectados)
            {
                coll.GetComponent<ParteDeEnemigo>().Explotar();
                coll.GetComponent<Rigidbody>().isKinematic = false;
                coll.GetComponent<Rigidbody>().AddExplosionForce(50, transform.position, radioExplosion, 3.5f, ForceMode.Impulse);
            }
        }
    }
}
