using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaGun_Controller : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private Transform shootPos;
    [SerializeField] private Transform[] shootPos_direction;

    [Header("Offset")]
    [SerializeField] private float offset;

    private float delay = 1.0f;
    private float skillDelay = 5.0f;
    private bool isSkillShot = false;

    private Vector2 mousePos;

    private bool isGameStart = false;

    private void Start()
    {
        GameManager.Instance.OnFightAction -= GameStart;
        GameManager.Instance.OnFightAction += GameStart;
    }

    //마우스 커서 위치로 로테이션
    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if(isGameStart)
        {
            delay -= Time.deltaTime;
            skillDelay -= Time.deltaTime;
        }


        if (skillDelay <= 0)
        {
            isSkillShot = true;
            skillDelay = 5.0f;
        }


        if (Input.GetMouseButtonDown(0) && delay <= 0)
        {
            if(isSkillShot)
            {
                for (int i = 0; i < shootPos_direction.Length; i++)
                {
                    SpawnManager.Instance.PoolingBullet(shootPos, shootPos_direction[i].position - shootPos.position, isSkillShot);
                }
                delay = 1.0f;
                isSkillShot = false;
                return;
            }

            for(int i = 0; i < shootPos_direction.Length; i++)
            {
                SpawnManager.Instance.PoolingBullet(shootPos, shootPos_direction[i].position - shootPos.position, isSkillShot);
            }

            delay = 1.0f;
        }
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - offset;

        transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
    }

    private void GameStart(bool isGameStart)
    {
        this.isGameStart = isGameStart;
    }

}
