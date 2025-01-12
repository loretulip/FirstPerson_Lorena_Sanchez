using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuertaFinal : MonoBehaviour
{
    private bool puertaAbierta = false;
    private Animator animator;
    public GameObject mensaje;
    public Text mensajeTexto;
    private bool esperandoInput = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        mensaje.SetActive(false);
    }

    public void Interactuar(FirstPerson player)
    {
        if (!puertaAbierta)
        {
            if (player.tieneLlave)
            {
                if (animator != null)
                {
                    animator.SetTrigger("Abrir");
                }

                puertaAbierta = true;
                Debug.Log("¡Has abierto la puerta con la llave!");
            }
            else
            {
                mensajeTexto.text = "¡Necesitas una llave para abrir esta puerta!";
                mensaje.SetActive(true);
                Time.timeScale = 0f;
                esperandoInput = true;
            }
        }
        else
        {
            Debug.Log("La puerta ya está abierta.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && puertaAbierta)
        {
            SceneManager.LoadScene(2);
        }
    }

    void Update()
    {
        if (esperandoInput && Input.GetKeyDown(KeyCode.E))
        {
            mensaje.SetActive(false);
            Time.timeScale = 1f;
            esperandoInput = false;
        }
    }
}
