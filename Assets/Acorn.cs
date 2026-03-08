using UnityEngine;
using UnityEngine.SceneManagement;
public class Acorn : MonoBehaviour
{
    public GameObject winPanel;
    private AudioController musicController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            winPanel.SetActive(true);
            musicController.audioSourceMusic.Stop();
        }
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
