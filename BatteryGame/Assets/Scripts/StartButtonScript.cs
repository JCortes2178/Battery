using UnityEngine;
using UnityEngine.SceneManagement;
public class StartButtonScript : MonoBehaviour
{
    
    public void ChangeScene()
    {
        int SceneIndex = 0; //TODO need to change this when we update the build settings
        SceneManager.LoadScene(SceneIndex);
    }

}
