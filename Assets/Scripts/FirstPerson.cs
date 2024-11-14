using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class FirstPerson : MonoBehaviour
{
    [SerializeField] private int vida;

    [Header ("Movimiento")]
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float escalaGravedad;
    [SerializeField] private float alturaSalto;

    [Header("Deteccion Suelo")]
    [SerializeField] private Transform pies;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private float radio;

    CharacterController controller;
    private Camera cam;

    private Vector3 movimientoVertical;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
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
        //Tengo que lanzar una bola de detección en mis pies para detectar si hay suelo
        Collider[] collsDetectados = Physics.OverlapSphere(pies.position, radio, queEsSuelo);
        if(collsDetectados.Length > 0)
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movimientoVertical.y = Mathf.Sqrt(-2 * escalaGravedad * alturaSalto);
        }
    }
    public void RecibirDanho(int danhoRecibido)
    {
        vida -= danhoRecibido;
        if(vida < 0)
        {
            Destroy(gameObject);
        }
    }
}
