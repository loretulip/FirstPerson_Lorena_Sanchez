using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEditor.ShaderData;
using UnityEditor.SearchService;
using UnityEngine.Rendering.Universal;

public class MenuVictoria : MonoBehaviour
{
   
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
    }  
    
    
    public void Salir()
    {
        Application.Quit();
    }
    public void MenuPrincipal()
    {
        SceneManager.LoadScene(0);
    }
   



}
