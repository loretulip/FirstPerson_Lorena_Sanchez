using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Arma")]
public class ArmaSO : ScriptableObject // Contenedor de datos
{
    // Start is called before the first frame update

    public int balasCargador, balasBolsa;
    public float distanciaAtaque, danhoAtaque, cadenciaAtaque;

}
