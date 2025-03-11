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
        poolDic.Add(PoolUniqueID.BasicBullet, new Queue<GameObject>());
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



    //======================외부 참조==========================================

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

        // 큐에 아무것도 없다면?
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

    public void PoolingBullet(Transform point, Vector2 direction, bool skillshot)
    {
        GameObject bulletClone;

        if (poolDic[PoolUniqueID.BasicBullet].Count <= 0)
        {
            bulletClone = Instantiate(objectPoolData.GetPrefab(PoolUniqueID.BasicBullet), point.position, point.rotation);
            bulletClone.transform.SetParent(spawnPoint[^1]);

            if(bulletClone.TryGetComponent<Bullet>(out Bullet bullet))
            {
                bullet.direction = direction;
                bullet.isSkillShot = skillshot;
            }

            return;
        }

        bulletClone = poolDic[PoolUniqueID.BasicBullet].Dequeue();
        bulletClone.transform.position = point.position;
        bulletClone.transform.rotation = point.rotation;

        if (bulletClone.TryGetComponent<Bullet>(out Bullet bullet2))
        {
            bullet2.direction = direction;
            bullet2.isSkillShot = skillshot;
        }
        bulletClone.SetActive(true);
    }

}
