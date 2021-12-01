using UnityEngine;
using UnityEngine.UI;

public class RoundsSurvived : MonoBehaviour
{
    public Text roundsText;

    void OnEnable()  //metodo do unity similar ao Start() porem so e chamado quando o objeto esta ativo
    {
        roundsText.text = PlayerStats.Rounds.ToString();  //alterando texto da UI de rounds sobrevividos
    }
}
