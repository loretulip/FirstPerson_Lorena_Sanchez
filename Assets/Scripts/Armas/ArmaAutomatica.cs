using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class ArmaAutomatica : MonoBehaviour
{
    [SerializeField] private ParticleSystem system;
    [SerializeField] private ArmaSO datos;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip disparoM4;

    private float timer;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        timer = datos.cadenciaAtaque;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            timer += Time.deltaTime;
            if (Input.GetMouseButton(0) && timer >= datos.cadenciaAtaque)
            {

                system.Play();
                audioSource.PlayOneShot(disparoM4);
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hitInfo, datos.distanciaAtaque))
                {
                    if (hitInfo.transform.CompareTag("ParteEnemigo"))
                    {
                        hitInfo.transform.GetComponent<ParteDeEnemigo>().RecibirDanho(datos.danhoAtaque);
                    }

                }
                timer = 0;

            }
        }
        {
        }
    }
         
}