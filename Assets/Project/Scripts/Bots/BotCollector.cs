using UnityEngine;

[RequireComponent(typeof(ResourceLoader), typeof(ResourceLoader), typeof(BotCollisonHandler))]
public class BotCollector : Bot
{
    private ResourceLoader _resourceLoader;
    private BotCollectorMover _botCollectorMover;
    private BotCollisonHandler _botCollisonHandler;
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
        _botCollisonHandler = GetComponent<BotCollisonHandler>();
    }

    private void OnEnable()
    {
        _botCollisonHandler.CollisionDetected += Load;
        _botCollectorMover.Moved += Move;
        _botCollectorMover.Stayed += Stay;
    }

    private void OnDisable()
    {
        _botCollisonHandler.CollisionDetected += Load;
        _botCollectorMover.Moved -= Move;
        _botCollectorMover.Stayed += Stay;
    }

    public Resource Unload()
    {
        Stay();
        
        _isLoad = false;
        
        return _resourceLoader.UnloadProcess();
    }

    private void Load(Resource resource)
    {    
        Stay();
       
        if (_resourceLoader.LoadProcess(resource))
        {
            _isLoad = true;
            _botCollectorMover.GoHome();
        }      
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
