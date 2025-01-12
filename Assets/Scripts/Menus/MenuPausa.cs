using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEditor.ShaderData;
using UnityEditor.SearchService;
using UnityEngine.Rendering.Universal;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private AudioMixer audioMixer;
    bool pausa = false;

    //private UnityEngine.Rendering.Universal.DepthOfField depthOfField;

    //private float normalFocusDistance = 2.06f;
   // private float pausaFocusDistance = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Pausa();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Esc pulsado");
        }

    }
    public void Pausa()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pausa == false)
        {
            // SE MUESTRA CURSOR
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            menuPausa.SetActive(true);
            pausa = true;
            Debug.Log("Juego Pausado");
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pausa == true)
        {
            // NO SE MUESTRA CURSOR
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            menuPausa.SetActive(false);
            pausa = false;
            Debug.Log("Juego Reanudado");

        }
    }
    public void Reanudar()       
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (pausa == true)
        Time.timeScale = 1f;
        menuPausa.SetActive(false);
        pausa = false;
    }
    public void Reiniciar()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (pausa == true)
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        pausa = false;
    }
    public void Salir()
    {
        Application.Quit();
    }
    public void MenuPrincipal()
    {
        SceneManager.LoadScene(0);
    }
    public void PantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }
    public void CambiarVolumen(float volumen)
    {

    }
   


}
