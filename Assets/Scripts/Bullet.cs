using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;  //transform do alvo

    public float speed = 70f;
    public int damage = 50;

    public float explosionRadius = 0f;
    public GameObject impactEffect;

    public void Seek(Transform _target) //setando o alvo do projetil
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime; //distancia percorrida em um frame

        if(dir.magnitude <= distanceThisFrame) //testando se o projetil colidiu
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World); //movendo o projetil
        transform.LookAt(target);  //fazendo o projetil 'olhar' para o alvo
    }

    void HitTarget()  //destruindo o alvo
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);

        if(explosionRadius > 0f)  //testando se o projetil e explosivo
        {
            Explode();
        }else
        {
            Damage(target);
        }
        
        Destroy(gameObject);  //destruindo o game object do projetil
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);  //funcao que gera uma esfera e checa se os colliders estao dentro da esfera
        //armazenando os colliders em um array de colliders
        foreach(Collider collider in colliders) 
        {
            if(collider.GetComponent<Collider>().tag == "Enemy") //checando se e um inimigo
            {
                Damage(collider.GetComponent<Collider>().transform);
            }
        }
    }

    void Damage(Transform enemy)  //funcao que causa dano no alvo
    {
        Enemy e = enemy.GetComponent<Enemy>();  //"pegando" o componente script Enemy
        
        if(e != null)  //teste se o inimigo nao e null
        {
            e.TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
