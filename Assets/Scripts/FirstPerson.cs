using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FirstPerson : MonoBehaviour
{
    [SerializeField] public GameObject pantallaMuerte;
    [SerializeField] public GameObject pantallaHUD;
    [SerializeField] public GameObject pantallaSangre;

    [Header("Movimiento")]
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float escalaGravedad;
    [SerializeField] private float alturaSalto;
    private bool isGrounded = true;
    private Rigidbody rb;

    [Header("Detección Suelo")]
    [SerializeField] private Transform pies;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private float radio;

    [Header("Vida")]
    [SerializeField] private int vidaMax;
    [SerializeField] private int vidaActual;
    [SerializeField] private Image barraVida;
    [SerializeField] private TMP_Text textoVida;

    [Header("Estado del Jugador")]
    CharacterController controller;
    private Camera cam;
    public bool tieneLlave = false;

    private Vector3 movimientoVertical;


    public int VidaActual { get => vidaActual; set => vidaActual = value; }

    // -------------------- Funciones de Inicialización --------------------
    void Start()
    {
        pantallaSangre.SetActive(false);
        vidaActual = vidaMax;
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    // -------------------- Funciones de Vida --------------------
    public void RestaurarVida(int cantidad)
    {
        vidaActual = Mathf.Min(vidaActual + cantidad, vidaMax); // No superar la vida máxima
        Debug.Log("Vida restaurada a: " + vidaActual);
    }

    private void ActualizacionHUD()
    {
        float rellenoBarra = (float)vidaActual / vidaMax;
        barraVida.fillAmount = rellenoBarra;
        textoVida.SetText(vidaActual + " / " + vidaMax);
    }

    public void RecibirDanho(int danhoRecibido)
    {
        vidaActual -= danhoRecibido;
        if (vidaActual < 0)
        {
            ActualizacionHUD();
        }
    }

    public void RecogerVida(int vidaRecuperada)
    {
        vidaActual = Mathf.Min(vidaActual + vidaRecuperada, vidaMax);
        ActualizacionHUD();
    }

    public void Muerte()
    {
        Time.timeScale = 0.3f;
        pantallaMuerte.SetActive(true);
        pantallaHUD.SetActive(false);
    }

    // -------------------- Funciones de Movimiento --------------------
    void Update()
    {
        if (vidaActual<=50)
        {
            pantallaSangre.SetActive(true);
        }
        if (vidaActual <= 0)
        {
            Muerte();
        }        

        ActualizacionHUD();
        MovimientoJugador();
        AplicarGravedad();
        DeteccionSuelo();
    }

    private void MovimientoJugador()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector2 input = new Vector2(h, v).normalized;

        transform.eulerAngles = new Vector3(0, cam.transform.eulerAngles.y, 0);

        // Si existe input...
        if (input.sqrMagnitude > 0)
        {
            float anguloRotacion = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            transform.eulerAngles = new Vector3(0, anguloRotacion, 0);

            Vector3 movimiento = Quaternion.Euler(0, anguloRotacion, 0) * Vector3.forward;
            controller.Move(movimiento * velocidadMovimiento * Time.deltaTime);
        }
    }

    // -------------------- Funciones de Física --------------------
    private void AplicarGravedad()
    {
        // Mi movimiento vertical en la y va aumentándose a cada cierta escala por segundo
        movimientoVertical.y += escalaGravedad * Time.deltaTime;
        controller.Move(movimientoVertical * Time.deltaTime);
    }

    private void DeteccionSuelo()
    {
        Collider[] collsDetectados = Physics.OverlapSphere(pies.position, radio, queEsSuelo);

        if (collsDetectados.Length > 0)
        {
            movimientoVertical.y = 0;
            Saltar();
        }
    }

    private void Saltar()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Espacio");
            movimientoVertical.y = Mathf.Sqrt(-2f * escalaGravedad * alturaSalto);
        }
    }

    // -------------------- Funciones de Colisiones --------------------
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("ParteEnemigo"))
        {
            Rigidbody rbEnemigo = hit.gameObject.GetComponent<Rigidbody>();
            Vector3 direccionFuerza = hit.transform.position - transform.position;
            rbEnemigo.AddForce(direccionFuerza.normalized * 50, ForceMode.Impulse);
        }
    }

    // -------------------- Funciones de Llave --------------------
    public void ObtenerLlave()
    {
        tieneLlave = true;
        Debug.Log("Llave obtenida.");
    }

    public bool TieneLlave()
    {
        return tieneLlave;
    }

    // -------------------- Funciones Auxiliares --------------------
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pies.position, radio);
    }



}
