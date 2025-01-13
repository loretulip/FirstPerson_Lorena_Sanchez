using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MensajeInicial : MonoBehaviour
{
    [SerializeField] private GameObject mensajeUI; 

    void Start()
    {
        ActivarMensajeInicial();
    }
   
    void Update()
    {
        
        if (mensajeUI.activeSelf && Input.GetKeyDown(KeyCode.X))
        {
            CerrarMensaje();
        }
    }

    private void ActivarMensajeInicial()
    {
        Time.timeScale = 0; 
        mensajeUI.SetActive(true); 
        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.None;
    }

    private void CerrarMensaje()
    {
        Time.timeScale = 1; 
        mensajeUI.SetActive(false); 
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked;
    }
}
