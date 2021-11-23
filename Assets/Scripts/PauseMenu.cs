using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI;

    public string menuSceneName = "MainMenu";

    public SceneFader sceneFader;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Toggle() //funcao para habilitar o menu de pause
    {
        if(GameManager.GameIsOver)
            return;

        pauseUI.SetActive(!pauseUI.activeSelf);   //pega o valor booleano de activeSelf do pauseUI e inverte

        if(pauseUI.activeSelf)
        {
            Time.timeScale = 0f; //trava o tempo
        }else
        {
            Time.timeScale = 1f; //retorna o tempo padrao
        }     
    }

    public void Retry()
    {
        Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Toggle();
        sceneFader.FadeTo(menuSceneName);
    }
}
