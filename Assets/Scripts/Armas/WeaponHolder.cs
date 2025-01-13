using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] GameObject[] arma;


    int indiceArmaActual = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale>0)
        {
            CambioArmaTeclado();
            CambioArmaScroll();
        }
        else
        {
        }
        
    }

    private void CambiarArma(int nuevoIndice)
    {
        if (nuevoIndice >= 0 && nuevoIndice < arma.Length)
        {

            // Desactivo el arma que actualmente llevo equipada
            arma[indiceArmaActual].SetActive(false);

            // Cambio el índice
            indiceArmaActual = nuevoIndice;

            arma[indiceArmaActual].SetActive(true);
        }
    }
    private void CambioArmaTeclado()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CambiarArma(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CambiarArma(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CambiarArma(2);
        }
    }
    private void CambioArmaScroll()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel > 0)
        {
            CambiarArma(indiceArmaActual - 1);
        }
        else if (scrollWheel < 0)
        {
            CambiarArma(indiceArmaActual + 1);
        }
    }

}
