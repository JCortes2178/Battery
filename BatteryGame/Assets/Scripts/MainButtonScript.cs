using UnityEngine;
using UnityEngine.SceneManagement;
public class MainButtonScript : MonoBehaviour
{
    public void GoToMainMenu()
    {
        int mainSceneIndex = 0;
        SceneManager.LoadScene(mainSceneIndex);
    }
}
