using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f; //velocidade de movimento inicial

    [HideInInspector]
    public float speed;  //velocidade de movimento

    public float startHealth = 100f;  
    private float health;
    private bool isEnemyDead = false;

    public int enemyValue = 50;  //valor do inimigo em dinheiro

    public GameObject enemyDeathEffectPrefab; //referencia para o prefab efeito de morte

    [Header("Unity Stuff")]
    public Image healthBar;

    void Start()
    {
        speed = startSpeed; //setando a velocidade como a velocidade inicial
        health = startHealth;
    }

    public void TakeDamage(float amount) //metodo que recebe o dano e retira vida do inimigo
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth; 

        if(health <= 0)  //testando se o inimigo tem vida
        {
            Die();
            isEnemyDead = true;
        }
    }

    public void Slow(float percent)  //metodo para diminuir a velocidade do inimigo
    {
        speed = startSpeed * (1f - percent); //aplicando o slow
    }

    void Die()  //metodo que remove o inimigo do jogo
    {
        if(isEnemyDead)
            return;

        PlayerStats.Money += enemyValue; //adicionando dinheiro ao jogador por ter matado o nimigo
        
        GameObject effect = (GameObject)Instantiate(enemyDeathEffectPrefab, transform.position, Quaternion.identity); //instanciando o efeito de morte
        Destroy(effect, 5f);  //destruindo efeito de morte apos 5 segundos

        WaveSpawner.EnemiesAlive--;
        
        Destroy(gameObject);  //destruindo o inimigo
    }
}
