using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    [Header("FlashMat")]
    [SerializeField] private Material FlashMat;

    [Header("Duration")]
    [SerializeField] private float duration = 0.1f;

   
    private SpriteRenderer[] spriteRenderer;

    
    private Material originalMaterial;


    private Coroutine flashRoutine;
    private WaitForSeconds waitForSeconds;


    private void Start()
    {
        spriteRenderer = this.gameObject.transform.GetComponentsInChildren<SpriteRenderer>();

        originalMaterial = spriteRenderer[0].material;
        waitForSeconds = new WaitForSeconds(duration);
    }

    private void OnEnable()
    {
        if(originalMaterial != null)
        {
            for (int i = 0; i < spriteRenderer.Length; i++)
            {
                spriteRenderer[i].material = originalMaterial;
            }
        }

    }


    public void Flash()
    {
        if(flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(Flash_Co());
    }




    private IEnumerator Flash_Co()
    {
        for(int i = 0; i < spriteRenderer.Length; i++)
        {
            spriteRenderer[i].material = FlashMat;
        }

        yield return waitForSeconds;

        for (int i = 0; i < spriteRenderer.Length; i++)
        {
            spriteRenderer[i].material = originalMaterial;

            flashRoutine = null;
        }
    }

}
