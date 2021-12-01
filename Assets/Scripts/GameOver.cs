using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    

    public string menuSceneName = "MainMenu";

    public SceneFader sceneFader;

    public void Retry()  //funcionalidade do botao Retry
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name); //carrega a scene utilizando SceneManager do unity
        //restaurando a scene atual atraves do nome dela
    }  

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}
