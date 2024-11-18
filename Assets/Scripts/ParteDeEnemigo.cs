using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class ParteDeEnemigo : MonoBehaviour

{
    [SerializeField] private Enemigo mainScript;
    [SerializeField] private float multiplicadorDanho;

    public void RecibirDanho(float danhoRecibido)
    {
        mainScript.Vidas -= danhoRecibido;

        if (mainScript.Vidas <= 0)
        {
            mainScript.Morir();
        }
    }
    public void Explotar()
    {
        mainScript.GetComponent<Animator>().enabled = false;
        mainScript.GetComponent<NavMeshAgent>().enabled = false;
        mainScript.enabled = false;
    }
}
