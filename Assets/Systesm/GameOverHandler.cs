using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Vector3 startPos;

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<SpikeHitComponent>())
        {
            if (player.GetComponent<SpikeHitComponent>().IsGameOver == true)
            {
                player.GetComponent<SpikeHitComponent>().IsGameOver = false;
                player.gameObject.transform.position = startPos;
            }
        }
    }
}
