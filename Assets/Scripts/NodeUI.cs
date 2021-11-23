using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;  //referencia para a UI do node

    public Text upgradeCost;
    public Button upgradeButton;

    public Text sellAmount;

    private Node target;  //node alvo da UI

    public void SetTarget(Node _target)  //recebe o alvo do build manager
    {
        target = _target;  //seta alvo

        transform.position = target.GetBuildPosition();  //reposiciona o node UI na posicao do node com o metodo GetBuildPosition do node

        if(!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        }else
        {
            upgradeCost.text = "MAX";
            upgradeButton.interactable = false;
        }

        sellAmount.text = "$" + target.turretBlueprint.GetSellAmount();

        ui.SetActive(true);  //ativa a UI do node
    }

    public void Hide()  //desativa a UI do node
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
