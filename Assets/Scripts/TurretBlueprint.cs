using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]  //linha para fazer o conteudo da classe aparecer no Inspector do Shop
public class TurretBlueprint  //nao e monobehaviour pq esse script nao ira ser conectado a um GameObject
{
    public GameObject prefab;
    public int cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;

    public int GetSellAmount()
    {
        return cost / 2;
    }
}
