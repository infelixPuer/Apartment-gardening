using UnityEngine;

public class Plantpot : MonoBehaviour
{
    [SerializeField] private Sprite[] tomatoSprites;
    [SerializeField] private Sprite[] wateredTomatoSprites;
    
    private SpriteRenderer _renderer;
    private Sprite _initialSprite;

    private int _stage;

    public bool isPlanted { get; private set; }
    public bool isGrown { get; private set; }
    private bool isWatered { get; set; }

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _initialSprite = _renderer.sprite;
    }

    public void Grow()
    {
        if (!CheckForEndOfArray(_stage))
        {
            return;
        }

        if (_stage == 0)
        {
            isPlanted = true;
            _renderer.sprite = tomatoSprites[_stage];
            _stage++;
        }
        else if (isWatered)
        {
            _renderer.sprite = tomatoSprites[_stage];
            _stage++;
            isWatered = false;
        }

        if (_stage == tomatoSprites.Length)
        {
            isGrown = true;
        }
    }

    public void Water()
    {
        _renderer.sprite = wateredTomatoSprites[_stage - 1];
        isWatered = true;
    }

    public void Harvest()
    {
        _stage = 0;
        _renderer.sprite = _initialSprite;
        isPlanted = false;
        isGrown = false;
    }

    private bool CheckForEndOfArray(int stage)
    {
        return stage < tomatoSprites.Length;
    }
}
