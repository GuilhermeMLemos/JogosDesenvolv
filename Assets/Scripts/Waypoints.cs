using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] points;  //array estatico do tipo transform

    void Awake()  //metodo do unity que e chamado antes do Start
    {
        points = new Transform[transform.childCount];  //aloca o array do tipo transform com length = quantidade de filhos de Waypoints
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);  //cada posicao do array recebe cada filho de Waypoints de acordo com o indice
        }
    }
}
