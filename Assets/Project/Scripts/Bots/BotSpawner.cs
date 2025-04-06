using System.Collections.Generic;
using UnityEngine;
using Spawners;

public class BotSpawner : Spawner<Bot>
{
    [SerializeField] private List<Transform> _pointsSpawn = new List<Transform>();

    private Vector3 _spawnPosition;    

    private void Start()
    {
        CreateMaxCountBots();
    }

    public override void PerformOnGet(Bot bot)
    {
        bot.gameObject.SetActive(true);
        bot.transform.position = _spawnPosition;
        bot.SetHome(_spawnPosition);
        bot.Removed += RemoveObject;
    }

    public override void OnRelease(Bot bot)
    {
        bot.gameObject.SetActive(false);
        bot.Removed -= RemoveObject;
    }

    public void CreateBot()
    {
        GetObject();
    }

    private void CreateMaxCountBots()
    {
        for (int i = 0; i < _pointsSpawn.Count; i++)
        {
            _spawnPosition = _pointsSpawn[i].transform.position;
            CreateBot();
        }
    }
}
