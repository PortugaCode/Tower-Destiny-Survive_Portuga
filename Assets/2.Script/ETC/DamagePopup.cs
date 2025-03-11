using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class DamagePopup : MonoBehaviour
{

    [SerializeField] private PoolUniqueID poolUniqueID;

    private const float DISAPPEAR_TIMER_MAX = 0.75f;
    private float disappearTimer;


    private TextMeshPro textMesh;
    private Color textColor;
    [SerializeField] private Color Basic_textColor;

    private void Awake()
    {
        TryGetComponent<TextMeshPro>(out  textMesh);
    }




    public void SetUp(int damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());
        disappearTimer = DISAPPEAR_TIMER_MAX;

        textColor = Basic_textColor;
        textMesh.color = textColor;
    }

    private void Update()
    {
        float moveYSpeed = 0.7f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;


/*        if(disappearTimer > DISAPPEAR_TIMER_MAX * 0.5f)
        {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }*/


        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0)
            {
                SpawnManager.Instance.EnqueueData(poolUniqueID, this.gameObject);
            }
        }
    }
}
