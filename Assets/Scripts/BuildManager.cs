using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance; //variavel para armazenar a instancia unica do BuildManager para o jogo

    void Awake() //funcao que e chamada antes do start
    {
        if(instance != null) //testando se ja existe uma instancia do BuildManager pois so pode haver uma
        {
            Debug.Log("Mais de um BuildManager na scene!");
            return;
        }
        instance = this; 
    }

    public GameObject buildEffectPrefab; //referencia para o game object
    public GameObject sellEffectPrefab;

    private TurretBlueprint turretToBuild;  //torre a ser construida
    private Node selectedNode; //node a ser selecionado

    public NodeUI nodeUI; //referencia para o UI do node

    public bool CanBuild{ get{ return turretToBuild != null; } }  //variavel que nao pode ser setada e so e verdadeira quando turretToBuild e diferente de null
    public bool HasMoney{ get{ return PlayerStats.Money >= turretToBuild.cost; } }


    public void SelectNode(Node node) //recebe o node do Node
    {
        if(selectedNode == node) //se o node recebido ja e igual ao node selecionado
        {
            DeselectNode();  //entao desativa a ui
            return;
        }

        selectedNode = node;  //seta o node selecionado
        turretToBuild = null;  //caso esteja uma torre selecionada para construcao e removida

        nodeUI.SetTarget(node);  //muda o alvo da UI para o node selecionado
    }

    public void DeselectNode()
    {
        selectedNode = null;  //remove o node selecionado
        nodeUI.Hide();  //desativa a UI
    }

    public void SelectTurretToBuild(TurretBlueprint turret) //recebe a torre de Shop armazena na torre a ser construida
    {
        turretToBuild = turret;  //seta a torre a ser construida
        DeselectNode(); //caso esteja um node selecionado e removido
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
