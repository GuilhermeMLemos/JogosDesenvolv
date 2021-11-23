using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    
    public Wave[] waves; 

    public Transform spawnPoint;   //referencia para a posicao (transform) do ponto de spawn

    public float timeBetweenWaves = 5f;   //tempo entre as ondas de inimigo
    private float countdown = 2f;  //countdown de spawn da proxima onda

    public Text waveCountdownText;  //referencia para o texto do countdown na UI

    private int waveIndex = 0;  //index da onda de inimigo
    
    private void Start()
    {
        this.enabled = true;
    }
    
    void Update ()
    {
        if(EnemiesAlive > 0)  //proxima onda de inimigos so comecara caso nao haja mais inimigo
        {
            return;
        }

        if (countdown <= 0f)  //caso o countdown seja zero
        {
            StartCoroutine(SpawnWave());  //chamando uma Coroutine, spawnando a onda
            countdown = timeBetweenWaves;  //resetando o countdown
            return;
        }

        countdown -= Time.deltaTime;  //countdown e diminuido a cada segundo

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);  //limitando o countdown com o minimo de 0 segundo

        waveCountdownText.text = string.Format("{0:00.00}", countdown);  //alterando o texto UI do countdown para um formato de "relogio"
    }

    IEnumerator SpawnWave () //funcao do tipo Coroutine (IEnumerator)
    {
        PlayerStats.Rounds++;  //aumentando ondas sobrevividas do jogador

        Wave wave = waves[waveIndex];

        for (int i = 0; i < wave.spawnCount; i++)  //spawna uma quantidade de inimigos de acordo com spawnCount na classe Wave
        {
            SpawnEnemy(wave.enemy);  //spawnando o inimigo determinado na classe Wave
            yield return new WaitForSeconds(1f / wave.spawnRate);  //spawnando cada inimigo de mesma onda com uma diferença determinada pelo spawnRate da classe Wave
        }

        waveIndex++;  //aumentando index da onda

        if(waveIndex == waves.Length)
        {
            Win();
        }
    }

    void SpawnEnemy (GameObject enemy)  //funcao que spawna recebe um tipo de inimigo
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);  //instanciando o objeto do inimigo nas posicoes do spawn
        EnemiesAlive++;
    }

    void Win()
    {
        Debug.Log("NIVEL VENCIDO");
        this.enabled = false;  //desativa esse script
    }
}
