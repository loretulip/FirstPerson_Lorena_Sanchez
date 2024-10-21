using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPerson : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float smoothing;
    [SerializeField] private float velocidadRotación;
    CharacterController controller;
    private Camera cam;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector2 input = new Vector2(h, v).normalized;
        //Vector3 movimiento = new Vector3(h, 0, v).normalized;



        //Si existe input...
        if (input.sqrMagnitude > 0)
        {
            float anguloRotacion = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float anguloSuave = Mathf.SmoothDampAngle(transform.eulerAngles.y, anguloRotacion, ref velocidadRotación, smoothing);
            
            transform.eulerAngles = new Vector3(0, anguloSuave, 0);

            Vector3 movimiento = Quaternion.Euler(0, anguloRotacion, 0) * Vector3.forward;
            controller.Move(movimiento * velocidadMovimiento * Time.deltaTime);

            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }

    }
}
