using UnityEngine;
using UnityEngine.SceneManagement;
public class GameStateScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame()
    {
        //TODO: right now game index is 0, will need to update this when we switch it
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        //TODO make this switch to an game over scene
        //When we do this switch we need to put gameOverScence after MainScene in Build Settings
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //For now we just reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
