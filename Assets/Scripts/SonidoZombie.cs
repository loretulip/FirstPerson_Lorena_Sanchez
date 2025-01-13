using UnityEngine;

public class SonidoZombie : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Transform jugador;
    [SerializeField] private float maxDistance = 20f;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (distancia <= maxDistance)
        {
            audioSource.volume = 1 - (distancia / maxDistance);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.volume = 0;
        }
    }
}
