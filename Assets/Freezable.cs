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

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnWhistle(bool isFreeze)
    {
        IsFrozen = isFreeze;
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
