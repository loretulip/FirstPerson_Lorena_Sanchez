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
        if(Input.GetKeyDown(KeyCode.Alpha1))
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

        CambiarArma(0);

    }

    private void CambiarArma(int nuevoIndice)
    {
        if (nuevoIndice >= arma.Length || nuevoIndice < arma.Length)
        {

            // Desactivo el arma que actualmente llevo equipada
            arma[indiceArmaActual].SetActive(false);

            // Cambio el índice
            indiceArmaActual = nuevoIndice;

            arma[indiceArmaActual].SetActive(true);
        }
    }
}
