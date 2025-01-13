using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class CajaMunicion : MonoBehaviour
{
    private Animator anim;
    public bool tieneLlave = false;
    public Image imagenLlave;
    private bool cajaAbierta = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        imagenLlave.gameObject.SetActive(false); 
    }

    public void Abrir(FirstPerson player)
    {
        if (cajaAbierta) // Si la caja ya ha sido abierta, no hace nada más
        {
            return;
        }

        if (player == null)
        {
            Debug.LogError("La referencia al jugador es nula.");
            return;
        }

        anim.SetTrigger("Abrir");

        if (tieneLlave)
        {
            player.tieneLlave = true;
            imagenLlave.gameObject.SetActive(true);
            Debug.Log("Has obtenido la llave.");
        }
        else
        {
            int vidaRestaurada = Random.Range(5, 21);
            player.RestaurarVida(vidaRestaurada);
            Debug.Log("Has recibido una cura de " + vidaRestaurada + " puntos.");
        }

        cajaAbierta = true;
    }
}
