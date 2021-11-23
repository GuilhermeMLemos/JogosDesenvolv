using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;  //alvo
    private Enemy targetEnemy;  //variavel para guardar o componente Enemy do alvo

    [Header("General")]
    public float range = 15f;  //alcance da torre

    [Header("Use Bullets (default)")]  //atributos da torre caso utilize projeteis
    public GameObject bulletPrefab;
    public float fireRate =  1f;
    private float fireCountdown = 0f;

    [Header("Use Laser")]  //atributos caso utilize laser
    public bool useLaser = false;

    public int damageOverTime = 30;
    public float slowAmount = 0.5f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Unity Setup Fields")] //configuracoes do unity
    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); //metodo do unity que chama repetidamente uma funcao com um periodo determinado
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); //pegando o objeto com a tag Enemy e colocando em um array
        float shortestDistance = Mathf.Infinity;  //declarando variaveis de menor distancia e inimigo mais proximo
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies) //para cada item dentro do array
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);  //calculando distancia
            if(distanceToEnemy < shortestDistance) //trocando a menor distancia
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range) //testando caso o alvo esteja no alcance
        {
            target = nearestEnemy.transform; //o alvo passa a ser o inimigo mais proximo
            targetEnemy = nearestEnemy.GetComponent<Enemy>();  //guardando em uma variavel o componente Enemy (script) do inimigo
        }else
        {
            target = null;  //caso contrario a torre perde o alvo
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            if(useLaser)  //caso atire com laser
            {
                if(lineRenderer.enabled)  //e o laser esteja ativo (atirando)
                {
                    lineRenderer.enabled = false;  //desative o laser
                    impactEffect.Stop(); //desativa o efeito de impacto
                    impactLight.enabled = false; //desativa a iluminacao do laser
                }    
            }
            return;
        }

        //Mirar no alvo
        LockOnTarget(); 

        if(useLaser)  //teste se a torre atira com laser
        {
            Laser();
        }else //senao atira com projetil
        {
            if(fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }    
    }

    void LockOnTarget() //metodo para rotacionar torre em direcao ao alvo
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;   
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()  //atirar com laser
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime); //causando dano por segundo no inimigo
        targetEnemy.Slow(slowAmount);

        if(!lineRenderer.enabled)
        {
            lineRenderer.enabled = true; //ativando o laser caso esteja desativado
            impactEffect.Play(); //ativa o efeito de impacto
            impactLight.enabled = true; //ativa a iluminacao do laser
        } 
              
        lineRenderer.SetPosition(0, firePoint.position); //seta a posicao inicial (0) do laser (linha)
        lineRenderer.SetPosition(1, target.position); //seta a final (1)

        Vector3 dir = firePoint.position - target.position; //pegando um vetor que aponte do inimigo ate o ponto de tiro fa torre

        impactEffect.transform.rotation = Quaternion.LookRotation(dir); //fazendo o efeito apontar para a torre

        impactEffect.transform.position = target.position + dir.normalized; //fazendo o efeito de impacto seguir a posicao do alvo
    }

    void Shoot() //atirar com projetil
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); //instanciando o projetil e guardando em uma variavel
        Bullet bullet = bulletGO.GetComponent<Bullet>(); //pegando o componente Bullet

        if(bullet != null) //testando se o projetil existe
        {
            bullet.Seek(target); //metodo de Bullet para perseguir o alvo
        }
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
