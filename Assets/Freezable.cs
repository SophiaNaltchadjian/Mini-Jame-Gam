using UnityEngine;

public enum WeatherMode
{
    FreezeOnly,
    SnowOnly,
    Both
}

public class Freezable : MonoBehaviour
{
    public WeatherMode mode = WeatherMode.Both;

    [Tooltip("Sprite shown during freeze state (used when mode is FreezeOnly or Both)")]
    public Sprite freezeSprite;

    [Tooltip("Sprite shown during snow state (used when mode is SnowOnly or Both)")]
    public Sprite snowSprite;

    private SpriteRenderer spriteRenderer;
    public bool IsFrozen { get; private set; }

    private AudioController musicController;

    void Awake()
    {
        musicController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnWhistle(bool isFreeze)
    {
        if (isFreeze)
        {
            AudioClip Currentmusic = musicController.musics[1];
            musicController.audioSourceMusic.clip = Currentmusic;
            musicController.audioSourceMusic.Play();

            
        }
        else
        {
            AudioClip Currentmusic = musicController.musics[0];
            musicController.audioSourceMusic.clip = Currentmusic;
            musicController.audioSourceMusic.Play();
          
        }

        IsFrozen = isFreeze;
        isFreeze = IsFrozen;
        switch (mode)
        {
            case WeatherMode.FreezeOnly:
                gameObject.SetActive(isFreeze); 
                break;
            case WeatherMode.SnowOnly:
                gameObject.SetActive(!isFreeze);
                break;
            case WeatherMode.Both:
                if (spriteRenderer != null)
                spriteRenderer.sprite = isFreeze ? freezeSprite : snowSprite;
                break;
        }
    }
}
