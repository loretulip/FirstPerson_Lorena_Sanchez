using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuertaFinal : MonoBehaviour
{
    private bool puertaAbierta = false;
    private Animator animator;
    public GameObject mensaje;

    void Start()
    {
        animator = GetComponent<Animator>();
        mensaje.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
            }
            else
            {
                mensaje.SetActive(true);
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        else
        {
            Debug.Log("La puerta ya está abierta");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            mensaje.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    
}
