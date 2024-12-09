using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private int danhoAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private LayerMask queEsPlayer;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float vidas;

    private NavMeshAgent agent;
    private FirstPerson player;
    private Animator anim;
    private bool enRangoAtaque = false;
    private bool ventanaAbierta = false;
    private bool atacando = false;

    public float Vidas { get => vidas; set => vidas = value; }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindObjectOfType<FirstPerson>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return; // Evitar errores si el jugador no existe.

        float distancia = Vector3.Distance(transform.position, player.transform.position);

        if (distancia <= radioAtaque) // Si está en rango de ataque
        {
            if (!atacando)
            {
                Atacar();
            }
        }
        else // Si está fuera de rango de ataque
        {
            Perseguir();
        }

        if (ventanaAbierta)
        {
            DetectarJugador();
        }
    }

    private void Perseguir()
    {
        enRangoAtaque = false;
        atacando = false;

        anim.SetBool("Attacking", false);
        agent.isStopped = false;
        agent.SetDestination(player.transform.position);
    }

    private void Atacar()
    {
        enRangoAtaque = true;
        atacando = true;

        agent.isStopped = true; // Detener al enemigo.
        anim.SetBool("Attacking", true);

        // Asegurar que el enemigo mire al jugador.
        Vector3 direccionAPlayer = (player.transform.position - transform.position).normalized;
        direccionAPlayer.y = 0;
        transform.rotation = Quaternion.LookRotation(direccionAPlayer);
    }

    private void DetectarJugador()
    {
        Collider[] collsDetectados = Physics.OverlapSphere(attackPoint.position, radioAtaque, queEsPlayer);

        if (collsDetectados.Length > 0)
        {
            foreach (Collider col in collsDetectados)
            {
                FirstPerson jugador = col.GetComponent<FirstPerson>();
                if (jugador != null)
                {
                    jugador.RecibirDanho(danhoAtaque);
                }
            }
            ventanaAbierta = false; // Cerrar ventana de ataque.
        }
    }

    // Métodos llamados desde los eventos de animación.
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
        atacando = false;
        anim.SetBool("Attacking", false);
        Perseguir(); // Reanudar persecución.
    }

    public void Morir()
    {
        agent.enabled = false;
        anim.enabled = false;
        Destroy(gameObject, 10); // Destruir al enemigo tras 10 segundos.
    }

    private void OnDrawGizmosSelected()
    {
        // Visualizar el rango de ataque en la escena.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, radioAtaque);
    }
}
