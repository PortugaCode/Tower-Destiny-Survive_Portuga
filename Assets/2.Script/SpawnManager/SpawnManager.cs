using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public enum SpawnSate
{
    None = 0,
    Spawn,
    Stop
}
public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    //===========================================

    private const int ENEMY_MAX = 50;




    [Header("PoolData")]
    [SerializeField] private ObjectPoolData objectPoolData;

    [Header("SpawnPoint")]
    [SerializeField] private Transform[] spawnPoint;

    [Header("SpawnState")]
    [SerializeField] private SpawnSate spawnSate;

    public void SetSpawnState(SpawnSate spawnSate)
    {
        this.spawnSate = spawnSate;
    }


    [SerializeField] private float spawnTime;
    [SerializeField] private float spawnDelay = 2.0f;

    public int EnemyCount = 0;



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
                if(EnemyCount < ENEMY_MAX)
                {
                    PoolingEnemy();
                    EnemyCount++;
                }
                break;

            case SpawnSate.Stop:
                break;
        }
    }



    //======================�ܺ� ����==========================================

    public void EnqueueData(PoolUniqueID poolUniqueID, GameObject gameObject)
    {
        gameObject.SetActive(false);
        poolDic[poolUniqueID].Enqueue(gameObject);
    }

    public void PoolingEnemy()
    {
        int rand = UnityEngine.Random.Range(0, 3);
        int randomEnemy = UnityEngine.Random.Range(0, 4);
        GameObject enemyClone;

        // ť�� ENEMY MAX ��ŭ ä������ ���� ��
        if (poolDic[(PoolUniqueID)rand].Count <= 0)
        {
            enemyClone = Instantiate(objectPoolData.GetPrefab((PoolUniqueID)rand, randomEnemy)
                , spawnPoint[rand].position, Quaternion.identity);
            enemyClone.transform.SetParent(spawnPoint[rand]);
            
            return;
        }

        enemyClone = poolDic[(PoolUniqueID)rand].Dequeue();
        enemyClone.transform.position = spawnPoint[rand].position;
        enemyClone.SetActive(true);
    }

}
