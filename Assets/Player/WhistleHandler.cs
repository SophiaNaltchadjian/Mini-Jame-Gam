using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class WhistleHandler : MonoBehaviour
{
    private bool hasWhistle;
    private bool isFreeze;
    private Animator animator;
    private Player player;
    private List<Freezable> freezables = new List<Freezable>();
   
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        RefreshFreezables();
    }

    public void RefreshFreezables()
    {
        
        freezables.Clear();
        freezables.AddRange(FindObjectsByType<Freezable>(FindObjectsSortMode.None));
        foreach (Freezable freezable in freezables)
        {
            if (freezable.mode == WeatherMode.FreezeOnly)
            {
                freezable.gameObject.SetActive(false);
            }
            else
            {
                freezable.gameObject.SetActive(true);
            }
        }
    }

    public void Collect()
    {
        AudioClip audioToPlay = player.playerAudioClips[1];
        player.playerAudioSource.clip = audioToPlay;
        player.playerAudioSource.Play();
        hasWhistle = true;
    }

    public bool CanBlow()
    {
       
        return hasWhistle;
    }

    public void BlowWhistle()
    {
        isFreeze = !isFreeze;
        animator.SetBool("IsFreeze", isFreeze);

        foreach (Freezable f in freezables)
        {
            f.OnWhistle(isFreeze);
        }

        player.CheckSlidingObject();
    }
}
