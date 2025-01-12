using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class FirstPerson : MonoBehaviour
{
    [SerializeField] public GameObject pantallaMuerte;
    [SerializeField] public GameObject pantallaHUD;
    [SerializeField] public SistemaInteracciones sistemaInteracciones;


    [Header ("Movimiento")]
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float escalaGravedad;
    [SerializeField] private float alturaSalto;
    private bool isGrounded = true;
    private Rigidbody rb;


    [Header("Deteccion Suelo")]
    [SerializeField] private Transform pies;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private float radio;

    [Header("Vida")]
    [SerializeField] private int vidaMax;
    [SerializeField] private int vidaActual;
    [SerializeField] private Image barraVida;
    [SerializeField] private TMP_Text textoVida;


    CharacterController controller;
    private Camera cam;
    public bool tieneLlave = false;


    private Vector3 movimientoVertical;

    public int VidaActual { get => vidaActual; set => vidaActual = value; }

    // Start is called before the first frame update
    void Start()
    {
        vidaActual = vidaMax;
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();

    }
    public void RestaurarVida(int cantidad)
    {
        vidaActual = Mathf.Min(vidaActual + cantidad, vidaMax); // No superar la vida máxima
        Debug.Log("Vida restaurada a: " + vidaActual);
    }
    private void ActualizacionHUD()
    {
        float rellenoBarra = (float)vidaActual / vidaMax;
        barraVida.fillAmount = rellenoBarra;
        textoVida.SetText ( vidaActual+ " / " + vidaMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (vidaActual<=0)
        {
            Muerte();
        }
        ActualizacionHUD();
        float h = Input.GetAxisRaw("Horizontal"); 
        float v = Input.GetAxisRaw("Vertical");
        Vector2 input = new Vector2(h, v).normalized;


        transform.eulerAngles = new Vector3(0, cam.transform.eulerAngles.y, 0);

        //Si existe input...
        if (input.sqrMagnitude > 0) 
        {
            float anguloRotacion = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            transform.eulerAngles = new Vector3(0, anguloRotacion, 0);

            Vector3 movimiento = Quaternion.Euler(0, anguloRotacion, 0) * Vector3.forward;
            controller.Move(movimiento * velocidadMovimiento * Time.deltaTime);            
        }        
        AplicarGravedad();
        DeteccionSuelo();
    }

    private void AplicarGravedad()
    {
        //Mi movimiento vertical en la y va aumentándose a cada cierta escala por segundo
        movimientoVertical.y += escalaGravedad * Time.deltaTime;
        controller.Move(movimientoVertical * Time.deltaTime);
    }
    // Es como OnCollisionEnter PERO para un CharacterController
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.CompareTag("ParteEnemigo"))
        {
            Rigidbody rbEnemigo = hit.gameObject.GetComponent<Rigidbody>();
                                      // Enemigo                   Jugador
            Vector3 direccionFuerza = hit.transform.position - transform.position;
            rbEnemigo.AddForce(direccionFuerza.normalized * 50,ForceMode.Impulse);
        }
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
    //Para dibujar cualquier figura en la escena
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pies.position, radio);
    }
    private void Saltar()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Espacio");
            movimientoVertical.y = Mathf.Sqrt(-2f * escalaGravedad * alturaSalto);
        }
    }

    public void RecibirDanho(int danhoRecibido)
    {
        vidaActual -= danhoRecibido;
        if(vidaActual < 0)
        {
            Destroy(gameObject);
            ActualizacionHUD();
        }
    }

    public void Muerte()
    {
        Time.timeScale = 0.3f;
        pantallaMuerte.SetActive(true);
        pantallaHUD.SetActive(false);
    }
    public void RecogerVida(int vidaRecuperada)
    {
        vidaActual = Mathf.Min(vidaActual + vidaRecuperada, vidaMax);
        ActualizacionHUD();
    }

    public void ObtenerLlave()
    {
        tieneLlave = true;
        Debug.Log("Llave obtenida.");
    }

    public bool TieneLlave()
    {
        return tieneLlave;
    }


}
