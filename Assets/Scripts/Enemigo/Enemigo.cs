using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    [Header("Datos del Enemigo")]
    [SerializeField] private int danhoAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private LayerMask queEsPlayer;
    [SerializeField] private Transform attackPoint;
    [SerializeField] public bool danhoRealizado = false;
    [SerializeField] private float vidas;

    [Header("Componentes")]
    private Rigidbody[] huesos;
    private NavMeshAgent agent;
    private FirstPerson player;
    private Animator anim;

    [Header("Estados del Enemigo")]
    private bool ventanaAbierta = false;
    private bool atacando = false;

    public float Vidas { get => vidas; set => vidas = value; }

    // -------------------- Funciones de Inicializaci�n --------------------
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindObjectOfType<FirstPerson>();
        anim = GetComponent<Animator>();
        huesos = GetComponentsInChildren<Rigidbody>();

        CambiarEstadoHuesos(true);
    }

    // -------------------- Funciones de Actualizaci�n --------------------
    void Update()
    {
        if (player == null) return; // Evitar errores si el jugador no existe.

        float distancia = Vector3.Distance(transform.position, player.transform.position);

        if (distancia <= radioAtaque) // Si est� en rango de ataque
        {
            if (!atacando)
            {
                // L�gica para comenzar el ataque (si la animaci�n y el comportamiento lo requieren)
            }
        }
        else // Si est� fuera de rango de ataque
        {
            Perseguir();
        }

        if (ventanaAbierta)
        {
            DetectarJugador();
        }
    }

    // -------------------- Funciones de Movimiento --------------------
    private void Perseguir()
    {
        agent.SetDestination(player.transform.position);

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.isStopped = true;

            anim.SetBool("Attacking", true);

            EnfocarPlayer();
        }
    }

    private void EnfocarPlayer()
    {
        Vector3 direccionAPlayer = (player.transform.position - this.gameObject.transform.position).normalized;
        direccionAPlayer.y = 0;
        transform.rotation = Quaternion.LookRotation(direccionAPlayer);
    }

    // -------------------- Funciones de Ataque --------------------
    private void DetectarJugador()
    {
        Collider[] collsDetectados = Physics.OverlapSphere(attackPoint.position, radioAtaque, queEsPlayer);

        if (collsDetectados.Length > 0)
        {
            for (int i = 0; i < collsDetectados.Length; i++)
            {
                collsDetectados[i].GetComponent<FirstPerson>().RecibirDanho(danhoAtaque);
            }
            danhoRealizado = true;
        }
    }

    // M�todos llamados desde los eventos de animaci�n.
    private void AbrirVentanaAtaque()
    {
        ventanaAbierta = true;
    }

    private void CerrarVentanaAtaque()
    {
        ventanaAbierta = false;
    }

    private void FinAtaque()
    {
        agent.isStopped = false;
        anim.SetBool("Attacking", false);
        danhoRealizado = false;
    }

    // -------------------- Funciones de Muerte --------------------
    public void Morir()
    {
        //agent.enabled = false;
        anim.enabled = false;

        CambiarEstadoHuesos(false);
        Destroy(gameObject, 5); // Destruir al enemigo tras 5 segundos.
    }

    // -------------------- Funciones de F�sica --------------------
    private void CambiarEstadoHuesos(bool estado)
    {
        for (int i = 0; i < huesos.Length; i++)
        {
            huesos[i].isKinematic = estado;
        }
    }

    // -------------------- Funciones Auxiliares --------------------
    private void OnDrawGizmosSelected()
    {
        // Visualizar el rango de ataque en la escena.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, radioAtaque);
    }
}
