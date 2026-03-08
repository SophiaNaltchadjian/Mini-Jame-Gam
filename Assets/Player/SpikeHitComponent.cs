using UnityEngine;

public class SpikeHitComponent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool IsGameOver = false;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Spike>())
        {
            Debug.Log("GameOver");
            IsGameOver = true;
        }
    }

    // Update is called once per frame

}
