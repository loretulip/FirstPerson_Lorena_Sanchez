using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class WaterEffect : MonoBehaviour
{
    [SerializeField] private float velocidad;
    private Volume efecto;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Volume>();
    }

    // Update is called once per frame
    void Update()
    {
        // Time.deltaTime = Tiempo entre dos frames consecutivos
        // Time.time = Tiempo de juego total
        // Debug.Log(Mathf.Cos(velocidad * Time.time));

        // Busca en tu profeile si tienes el efecto LensDistortion
        if (efecto.profile.TryGet(out LensDistortion distortion))
        {
            // Es como: Vector3 sitio = new Vector3 (0, 0, 0);  
            FloatParameter xValue = new FloatParameter(1 + Mathf.Cos(velocidad * Time.time) / 2);
            FloatParameter yValue = new FloatParameter(1 + Mathf.Sin(velocidad * Time.time) / 2);
           distortion.xMultiplier.SetValue(xValue);
           distortion.yMultiplier.SetValue(yValue);
        }
    }
}
