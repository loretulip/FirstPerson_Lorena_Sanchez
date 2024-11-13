using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class SistemaInteracciones : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float distanciaMax;
    [SerializeField] Animator anim = null;

    private Transform interactuableActual;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, distanciaMax))
        {
            if (hit.transform.CompareTag("Interactuable"))
            {
                //Activar outline
                interactuableActual = hit.transform;
                interactuableActual.GetComponent<Outline>().enabled = true;
                Debug.Log("Detectado");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    //scriptCaja.abrir;
                    //Abrir();
                }
            }
        }
        else if (interactuableActual) // Si tenia un interactuable...
        {
            // Lo apago
            interactuableActual.GetComponent<Outline>().enabled = false;
            // Lo anulo
            interactuableActual = null;
        }
              
        // Lanzar raycast por cada frame (update) desde el centro de la cámara hacia delante
        // Si hemos hitteado algo, preguntar si ese algo lleva el tag "Interactuable"
        // Si es así, poner Debug.Log("Detectado")
    }
   
}
