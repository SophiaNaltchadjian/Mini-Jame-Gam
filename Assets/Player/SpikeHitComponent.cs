using UnityEngine;
using UnityEngine.SceneManagement;
public class SpikeHitComponent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool IsGameOver = false;
    public GameObject gameOverMenu;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Spike>())
        {
            Debug.Log("GameOver");
            IsGameOver = true;
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
            
        }
    }

    // Update is called once per frame

}
