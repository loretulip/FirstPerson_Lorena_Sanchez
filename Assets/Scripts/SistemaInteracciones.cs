using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaInteracciones : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private float distanciaInteraccion;
    private Transform interactuableActual;
    public FirstPerson player; // Referencia al jugador (FirstPerson)

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, distanciaInteraccion))
        {
            // Si se interactúa con la caja de munición
            if (hit.transform.TryGetComponent(out CajaMunicion scriptCaja))
            {
                interactuableActual = hit.transform;
                interactuableActual.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    scriptCaja.Abrir(player); // Se pasa el jugador al método Abrir
                }
            }

            // Si se interactúa con la puerta
            else if (hit.transform.TryGetComponent(out PuertaFinal puerta))
            {
                interactuableActual = hit.transform;
                interactuableActual.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    puerta.Interactuar(player); // Llamamos al método Interactuar de la puerta
                }
            }

            // Si tenía un objeto interactuable y ya no está interactuando con él
            else if (interactuableActual)
            {
                interactuableActual.GetComponent<Outline>().enabled = false;
                interactuableActual = null;
            }
        }
    }
}
