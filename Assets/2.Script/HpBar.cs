using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [Header("AIAgent")]
    [SerializeField] private AIAgent agent;

    [Header("HP UI")]
    [SerializeField] private Slider hpbar;

    [Header("health")]
    [SerializeField] private float health;

    [Header("Canvas")]
    [SerializeField] private GameObject canvasObj;

    private void Start()
    {
        agent.OnDamage -= HitDamage;
        agent.OnDamage += HitDamage;
    }

    private void OnEnable()
    {
        health = agent.health;
        canvasObj.SetActive(false);
    }

    private void HitDamage(float damage)
    {
        canvasObj.SetActive(true);

        health -= damage;
        if(health <= 0)
        {
            health = 0;
            canvasObj.SetActive(false);
        }

        hpbar.value = health;
    }

}
