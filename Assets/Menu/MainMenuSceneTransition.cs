using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneTransition : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
