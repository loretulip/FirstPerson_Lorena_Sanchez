using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ApuntesCorutinas : MonoBehaviour
{
    private bool corrutinaAbierta = false;
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S) && corrutinaAbierta == false) 
        {
            StartCoroutine(SemaforoInfinito());
            corrutinaAbierta = true;
        }
    }

    IEnumerator SemaforoInfinito()
    {
        while (1 == 1) 
        {
            Debug.Log("Verde");
            yield return new WaitForSeconds(2);
            Debug.Log("Amarillo");
            yield return new WaitForSeconds(2);
            Debug.Log("Rojo");
            yield return new WaitForSeconds(2);
        }
    }
}
