using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private float danhoAtaque;
    private NavMeshAgent agent;
    private FirstPerson player;
    private Animator anim;
    private bool ventanaAbierta = false;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radio;
    [SerializeField] private LayerMask personaje;


    // Start is called before the first frame update
    void Start()
    {
       agent = GetComponent<NavMeshAgent>();
       player = GameObject.FindObjectOfType<FirstPerson>();
       anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Perseguir();

        if(ventanaAbierta)
        {
            DetectarJugador();
        }

        // activar la animacion de ataque



    }

    private void DetectarJugador()
    {
        Collider[] collsDetectados = Physics.OverlapSphere(attackPoint.position, radio, personaje);
    }

    private void Perseguir()
    {
        agent.SetDestination(player.transform.position);

        // si la distancia que nos queda hacia el objeto cae por debajo del stoppingDistance
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // me paro ante él
            agent.isStopped = true;
            anim.SetBool("attacking", true);
        }
    }

    // Evento de animacion
    private void FinAtaque()
    {
        agent.isStopped = false;
        anim.SetBool("attacking", false);
    }
    private void Atacar()
    {

    }
    private void AbrirVentanaAtaque()
    {
        ventanaAbierta = true;
    }
    private void CerrarVentanaAtaque()
    {
        ventanaAbierta = false;
    }
}
