using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaManual : MonoBehaviour
{
    [SerializeField] private ArmaSO datos;
    [SerializeField] private ParticleSystem system;
    [SerializeField] private LayerMask queEsEnemigo;

    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        // came s la camara principal de la escena "Main Camera"
        cam=Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clic");
            system.Play(); // Ejecutar sistema partículas
            if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hitInfo, datos.distanciaAtaque))
            {
                if (hitInfo.transform.CompareTag("ParteEnemigo"))
                {
                    hitInfo.transform.GetComponent<ParteDeEnemigo>().RecibirDanho(datos.danhoAtaque);
                }
               
            }

        }
        

    }
}
