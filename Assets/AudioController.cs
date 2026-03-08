using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSourceMusic;
    public AudioClip[] musics;
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSourceMusic = GetComponent<AudioSource>();
        AudioClip Currentmusic = musics[0];
        audioSourceMusic.clip = Currentmusic;
        audioSourceMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
