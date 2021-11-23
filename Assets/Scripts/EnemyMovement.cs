using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]    //depende do componente Enemy
public class EnemyMovement : MonoBehaviour
{
    private Transform target; //alvo de movimento do inimigo
    private int wavepointIndex = 0;

    private Enemy enemy;  //variavel enemy para guardar o componente

    void Start ()
    {
        enemy = GetComponent<Enemy>(); //guardando o componente Enemy na variavel enemy

        target = Waypoints.points[0]; //seta a direcao do inimigo assim que ele spawna
    }

    void Update ()
    {
        Vector3 dir = target.position - transform.position;  //calcula a direcao do proximo alvo
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);  //move o inimigo

        if (Vector3.Distance(transform.position, target.position) <= 0.4f) //testando se o inimigo esta proximo do alvo
        {
            GetNextWaypoint(); //seta o proximo alvo
        }

        enemy.speed = enemy.startSpeed;  //reseta a velocidade do inimigo a cada update
    }

    void GetNextWaypoint() //metodo que seta o proximo alvo de movimento do inimigo
    {
        if (wavepointIndex >= Waypoints.points.Length - 1) //teste se o inimigo esta chegando no fim do caminho (array)
        {
            EndPath();  //terminar o caminho
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex]; //setando proximo alvo de movimento
    }

    void EndPath() //terminar o caminho
    {
        PlayerStats.Lives--; //diminui a vida
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject); //remove o objeto do inimigo do jogo
    }
}
