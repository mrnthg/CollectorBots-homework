using UnityEngine;

[RequireComponent(typeof(ResourceLoader), typeof(ResourceLoader))]
public class BotCollector : Bot
{
    private ResourceLoader _resourceLoader;
    private BotCollectorMover _botCollectorMover;
    private bool _isLoad;
    private bool _isMove;

    public bool IsLoad => _isLoad;
    public bool IsMove => _isMove;

    private void Awake()
    {
        _isLoad = false;
        _isMove = false;
        _resourceLoader = GetComponent<ResourceLoader>();
        _botCollectorMover = GetComponent<BotCollectorMover>();
    }

    private void OnEnable()
    {
        _botCollectorMover.Moved += Move;
        _botCollectorMover.Stayed += Stay;
        _resourceLoader.Loaded += Load;
        _resourceLoader.Unloaded += Unload;
    }

    private void OnDisable()
    {
        _botCollectorMover.Moved -= Move;
        _botCollectorMover.Stayed += Stay;
        _resourceLoader.Loaded -= Load;
        _resourceLoader.Unloaded -= Unload;
    }

    private void Load()
    {    
        _isLoad = true;
        Stay();
    }

    private void Unload()
    {
        _isLoad = false;       
        Stay();
    }

    private void Move()
    {
        _isMove = true;
    }

    private void Stay()
    {
        _isMove = false;
    }
}
