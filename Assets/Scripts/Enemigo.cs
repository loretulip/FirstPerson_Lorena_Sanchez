using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private int danhoAtaque;
    private NavMeshAgent agent;
    private FirstPerson player;
    private Animator anim;
    private ArmaManual arma;
    private bool ventanaAbierta = false;
    private bool danhoRealizado = false;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radio;
    [SerializeField] private LayerMask personaje;
    [SerializeField] private float vidas;
    private Rigidbody[] huesos;

    public float Vidas { get => vidas; set => vidas = value; }



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindObjectOfType<FirstPerson>();
        anim = GetComponent<Animator>();
        arma = GetComponent<ArmaManual>();
        huesos = GetComponentsInChildren<Rigidbody>();

        CambiarEstadoHuesos(false);

    }

    // Activa el kinematic de todos los huesos para que no entren en conflicto con la animación
    private void CambiarEstadoHuesos(bool estado)
    {
        for (int i = 0; i < huesos.Length; i++)
        {
            huesos[i].isKinematic = estado;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Perseguir();
        // Solo si la ventana esta abierta y aun no ha hecho daño...
        if (ventanaAbierta && danhoRealizado == false)
        {
            DetectarJugador();
        }
        // activar la animacion de ataque
    }

    private void DetectarJugador()
    {
        Collider[] collsDetectados = Physics.OverlapSphere(attackPoint.position, radio, personaje);
        //Si al menos hemos detectado un collider...
        if (collsDetectados.Length > 0)
        {
            for (int i = 0; i < collsDetectados.Length; i++)
            {
                collsDetectados[i].GetComponent<FirstPerson>().RecibirDanho(danhoAtaque);
            }
            danhoRealizado = true;
        }
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
        danhoRealizado = false;
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
    public void Morir()
    {
        // Cuando muera el enemigo se desactiva el kinematic de los huesos, lo que hace que caiga por el ragdoll 
        CambiarEstadoHuesos(true);
        // "enabled" para Activar o Desactivar componentes
        // "SetActive" para Activar o Desactivar todo el objeto
        anim.enabled = false;
        agent.enabled = false;
        Destroy(gameObject, 10);
    }

    
}