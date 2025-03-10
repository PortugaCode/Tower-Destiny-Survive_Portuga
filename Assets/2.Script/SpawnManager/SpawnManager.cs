using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //===========================================

/*    private const int ENEMY_MAX = 20;
    private const int BULLET_MAX = 30;*/

    private enum SpawnSate
    {
        None = 0,
        Spawn,
        Stop
    }

    [Header("PoolData")]
    [SerializeField] private ObjectPoolData objectPoolData;

    [Header("SpawnPoint")]
    [SerializeField] private Transform[] spawnPoint;

    [Header("SpawnState")]
    [SerializeField] private SpawnSate spawnSate;

    [SerializeField] private float spawnTime;
    [SerializeField] private float spawnDelay = 2.0f;



    private Dictionary<PoolUniqueID, Queue<GameObject>> poolDic;

    private void Start()
    {
        initDic();
        spawnTime = spawnDelay;
    }
    private void Update()
    {
        if(spawnSate.Equals(SpawnSate.Spawn))
        {
            spawnTime -= Time.deltaTime;

            if (spawnTime <= 0.0f)
            {
                SpawnEnemy();
                spawnTime = spawnDelay;
            }
        }
    }

    private void initDic()
    {
        poolDic =new Dictionary<PoolUniqueID, Queue<GameObject>>();
        poolDic.Add(PoolUniqueID.Enemy_Top, new Queue<GameObject>());
        poolDic.Add(PoolUniqueID.Enemy_Middle, new Queue<GameObject>());
        poolDic.Add(PoolUniqueID.Enemy_Bottom, new Queue<GameObject>());
    }

    private void SpawnEnemy()
    {
        switch(spawnSate)
        {
            case SpawnSate.None :
                break;

            case SpawnSate.Spawn:
                PoolingEnemy();
                break;

            case SpawnSate.Stop:
                break;
        }
    }



    //======================외부 참조==========================================

    public void EnqueueData(PoolUniqueID poolUniqueID, GameObject gameObject)
    {
        gameObject.SetActive(false);
        poolDic[poolUniqueID].Enqueue(gameObject);
    }

    public void PoolingEnemy()
    {
        int rand = UnityEngine.Random.Range(0, 3);
        GameObject enemyClone;

        // 큐에 ENEMY MAX 만큼 채워있지 않을 때
        if (poolDic[(PoolUniqueID)rand].Count <= 0)
        {
            enemyClone = Instantiate(objectPoolData.GetPrefab((PoolUniqueID)rand), spawnPoint[rand].position, Quaternion.identity);
            enemyClone.transform.SetParent(spawnPoint[rand]);
            
            return;
        }

        enemyClone = poolDic[(PoolUniqueID)rand].Dequeue();
        enemyClone.transform.position = spawnPoint[rand].position;
        enemyClone.SetActive(true);
    }



}
