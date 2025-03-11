using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Rigid Body 2D")]
    [SerializeField] private Rigidbody2D rig;

    [Header("Value")]
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    [Header("ID")]
    [SerializeField] private PoolUniqueID poolUniqueID;

    

    public Vector2 direction = Vector2.zero;
    public bool isSkillShot = false;
    private float autoDelete = 5.0f;


    private void OnEnable()
    {
        autoDelete = 5.0f;
        damage = UnityEngine.Random.Range(5.0f, 21.0f);
    }

    private void FixedUpdate()
    {
        autoDelete -= Time.deltaTime;
        if (autoDelete <= 0.0f && gameObject.activeSelf)
        {
            //ObjectPooling
            SpawnManager.Instance.EnqueueData(poolUniqueID, this.gameObject);
            return;
        }

        rig.velocity = direction * speed * Time.deltaTime;


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            AIAgent target;
            if (collision.gameObject.TryGetComponent<AIAgent>(out target))
            {
                //TakeDamage
                target.TakeDamage(damage);
                
                //ObjectPool
                if(!isSkillShot)
                {
                    if(target.health > 0)
                    {
                        target.rig.AddForce(Vector2.right * 1600f);
                    }
                    SpawnManager.Instance.EnqueueData(poolUniqueID, this.gameObject);
                }
            }

            return;
        }

        if (collision.CompareTag("Ground"))
        {
            //ObjectPool
            SpawnManager.Instance.EnqueueData(poolUniqueID, this.gameObject);
            return;
        }
    }
}
