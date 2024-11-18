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
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward.normalized * fuerza, ForceMode.Impulse);
        Destroy(gameObject,tiempoVida);
    }

    // Update is called once per frame
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
                coll.GetComponent<ParteDeEnemigo>().Explotar(); //deshabilito el mov del enemigo impactado
                coll.GetComponent<Rigidbody>().isKinematic = false; // dejo los huesos en dinamico
                // explosion
                coll.GetComponent<Rigidbody>().AddExplosionForce(50, transform.position, radioExplosion, 3.5f,ForceMode.Impulse);
            }
        }
    }   
}
