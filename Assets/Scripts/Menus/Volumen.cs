using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volumen : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] float volumenSlider;
    [SerializeField] Image imagenMute;
    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = slider.value;
        RevisarMute();
    }
    public void ChangeSLider(float valor)
    {
        volumenSlider = valor;
        PlayerPrefs.SetFloat("volumenAudio", volumenSlider);
        AudioListener.volume = slider.value;
        RevisarMute();
    }

    void RevisarMute()
    {
        if (volumenSlider == 0)
        {
            imagenMute.enabled = true;
        }
        else
        {
            imagenMute.enabled = false;
        }
    }

}
