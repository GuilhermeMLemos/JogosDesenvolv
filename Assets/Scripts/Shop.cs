using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;

    BuildManager  buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;  //referenciando a instancia do Build Manager
    }

    public void SelectStandardTurret() //selecionar torre inicial
    {
        Debug.Log("Standard Turret Selecionado");
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectMissileLauncher()  //selecionar o lancador de missil
    {
        Debug.Log("Missile Launcher Selecionado");
        buildManager.SelectTurretToBuild(missileLauncher);
    }

    public void SelectLaserBeamer()
    {
        Debug.Log("Laser Beamer Selecionado");
        buildManager.SelectTurretToBuild(laserBeamer);
    }
}
