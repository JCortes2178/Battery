using UnityEngine;
using UnityEngine.SceneManagement;
public class GameStateScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame()
    {
        //TODO: right now game index is 0, will need to update this when we switch it
        int SceneIndex = 1;
        SceneManager.LoadScene(SceneIndex);
    }

    public void GameOver()
    {
        //TODO make this switch to an game over scene
        //For now we just reload the scene
        int GameOverIndex = SceneManager.GetActiveScene().buildIndex + 1; //2 right now
        SceneManager.LoadScene(GameOverIndex);
    }
    
    public void GoToMainMenu()
    {
        int mainSceneIndex = 0;
        SceneManager.LoadScene(mainSceneIndex);
    }
    
}
