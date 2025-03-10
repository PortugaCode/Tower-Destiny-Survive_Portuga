using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;



public class ParallaxControler : MonoBehaviour
{
    [Header("ParallaxObjects")]
    [SerializeField] private ParallaxObject parallaxObjects_Layer_0;
    [SerializeField] private ParallaxObject parallaxObjects_Layer_1;

    [Header("ParallaxObjects_Speed")]
    [SerializeField] private float layer_0_BasicSpeed;
    [SerializeField] private float layer_1_BasicSpeed;

    [SerializeField] private float layer_0_FightSpeed;
    [SerializeField] private float layer_1_FightSpeed;

    private void Start()
    {
        GameManager.Instance.OnFightAction -= SetParallaxSpeed;
        GameManager.Instance.OnFightAction += SetParallaxSpeed;

        //SetParallaxSpeed(false);
    }

    public void SetParallaxSpeed(bool isFight)
    {
        if(isFight)
        {
            parallaxObjects_Layer_0.SetSpeed(layer_0_FightSpeed);
            parallaxObjects_Layer_1.SetSpeed(layer_1_FightSpeed);
            return;
        }

        parallaxObjects_Layer_0.SetSpeed(layer_0_BasicSpeed);
        parallaxObjects_Layer_1.SetSpeed(layer_1_BasicSpeed);
    }
}
