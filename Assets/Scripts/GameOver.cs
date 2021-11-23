using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text roundsText;

    public string menuSceneName = "MainMenu";

    public SceneFader sceneFader;

    void OnEnable()  //metodo do unity similar ao Start() porem so e chamado quando o objeto esta ativo
    {
        roundsText.text = PlayerStats.Rounds.ToString();  //alterando texto da UI de rounds sobrevividos
    }

    public void Retry()  //funcionalidade do botao Retry
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name); //carrega a scene utilizando SceneManager do unity
        //restaurando a scene atual atraves do Index dela nas configuracoes de build
    }  

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}
