using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class WaterEffect : MonoBehaviour
{
    private Volume efecto;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Volume>();
    }

    // Update is called once per frame
    void Update()
    {
        // Busca en tu profeile si tienes el efecto LensDistortion
        if (efecto.profile.TryGet(out LensDistortion distortion))
        {

        }
    }
}
