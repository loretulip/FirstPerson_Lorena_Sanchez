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
            // Si se interact�a con la caja de munici�n
            if (hit.transform.TryGetComponent(out CajaMunicion scriptCaja))
            {
                interactuableActual = hit.transform;
                interactuableActual.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    scriptCaja.Abrir(player); // Se pasa el jugador al m�todo Abrir
                }
            }

            // Si se interact�a con la puerta
            else if (hit.transform.TryGetComponent(out PuertaFinal puerta))
            {
                interactuableActual = hit.transform;
                interactuableActual.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    puerta.Interactuar(player); // Llamamos al m�todo Interactuar de la puerta
                }
            }

            // Si ten�a un objeto interactuable y ya no est� interactuando con �l
            else if (interactuableActual)
            {
                interactuableActual.GetComponent<Outline>().enabled = false;
                interactuableActual = null;
            }
        }
    }
}
