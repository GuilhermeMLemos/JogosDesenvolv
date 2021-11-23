using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;  //cor nova ao passar o mouse
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset; //position offset do node setado no Inspector

    [HideInInspector]
    public GameObject turret; //armazena a torre
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;  //variavel para armazenar o renderer e nao ter que chamar o GetComponent toda hora
    private Color startColor; //armazena a cor inicial

    BuildManager buildManager;

    void Start ()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color; //armazenando a cor original do Node

        buildManager = BuildManager.instance; //instanciando o Build Manager
    }

    public Vector3 GetBuildPosition() //metodo que retorna a posicao exata do Node mais um offset
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown() //funcao que detecta clicks do mouse
    {
        if(EventSystem.current.IsPointerOverGameObject()) //testando se o mouse esta em cima de um objeto de UI
            return;

        if(turret != null) //testando se ja existe uma torre no Node
        {
            buildManager.SelectNode(this); //caso existe uma torre, passa o node para o SelectNode do build manager
            return;
        }

        buildManager.DeselectNode();

        if(!buildManager.CanBuild) //teste para saber se pode construir 
            return;

        //construir uma torre
        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if(PlayerStats.Money < blueprint.cost) //testando se o jogador tem dinheiro suficiente
        {
            Debug.Log("Dinheiro insuficiente!");
            return;
        }
        PlayerStats.Money -= blueprint.cost; //retirando o dinheiro

        //Instanciando e armazenando o GameObject em uma variavel
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity); //Metodo do Unity para instanciar um GameObject no jogo
        turret = _turret; //armazenando a torre no Node para mostrar que ja existe uma torre posicionada

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffectPrefab, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Torre construída!");
    }

    public void UpgradeTurret()
    {
        if(PlayerStats.Money < turretBlueprint.upgradeCost) //testando se o jogador tem dinheiro suficiente
        {
            Debug.Log("Dinheiro insuficiente!");
            return;
        }
        PlayerStats.Money -= turretBlueprint.upgradeCost; //retirando o dinheiro

        Destroy(turret); //removendo a torre antiga

        //Instanciando e armazenando o GameObject em uma variavel uma nova torre melhorada
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity); 
        turret = _turret; //armazenando a torre no Node para mostrar que ja existe uma torre posicionada

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffectPrefab, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;

        Debug.Log("Torre melhorada!");
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();
        
        GameObject effect = (GameObject)Instantiate(buildManager.sellEffectPrefab, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(turret);
        turretBlueprint = null;

        if(isUpgraded)
        {
            isUpgraded = false;
        }
    }

    void OnMouseEnter()  //funcao do unity que sera chamada quando o mouse passar por cima do objeto
    {
        if(EventSystem.current.IsPointerOverGameObject())  //testando se o mouse esta em cima de algum objeto UI
            return;
        
        if(!buildManager.CanBuild) //teste para fazer com que o node so seja destacado caso exista uma torre para construir
            return;
        
        if(buildManager.HasMoney) //testando o dinheiro do jogador
        {
            rend.material.color = hoverColor;  //trocando a cor do node 
        }else
        {
            rend.material.color = notEnoughMoneyColor;  //trocando para a cor caso nao possua dinheiro necessario
        } 
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;  //restaurando a cor original
    }

}
