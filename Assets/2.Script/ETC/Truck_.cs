using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck_ : MonoBehaviour
{
    [SerializeField] private bool isFight;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform rayPoint;

    private void Update()
    {
        RaycastHit2D raycastHit2D = Physics2D.CircleCast((Vector2)rayPoint.position, 2f, Vector2.left, 0f, layerMask);
        if (raycastHit2D && isFight == false)
        {
            if (raycastHit2D.collider.CompareTag("Enemy"))
            {
                isFight = true;
                GameManager.Instance.FightAction(isFight);
            }

            return;
        }
        if(!raycastHit2D && isFight == true)
        {
            isFight = false;
            GameManager.Instance.FightAction(isFight);

            return;
        }
            
    }
}
