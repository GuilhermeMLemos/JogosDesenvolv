using UnityEngine;

public class DeleteSave : MonoBehaviour
{
    public void DeletePrefs()
    {
        PlayerPrefs.SetInt("levelReached", 1);
    }
}
