using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }



    [SerializeField] private ParallaxControler parallaxControler;
    [SerializeField] private SpawnManager spawnManager;


    public Action<bool> OnFightAction;
    public Action<bool> OnGameEndAction;

    public void FightAction(bool isFight)
    {
        OnFightAction?.Invoke(isFight);
        //spawnManager.SetSpawnState(SpawnSate.Spawn);
    }

    public void GameEndAction(bool isGameEnd)
    {
        OnGameEndAction?.Invoke(isGameEnd);
        spawnManager.SetSpawnState(SpawnSate.Stop);
    }

    public void StartSpawn()
    {
        spawnManager.SetSpawnState(SpawnSate.Spawn);
    }


}
