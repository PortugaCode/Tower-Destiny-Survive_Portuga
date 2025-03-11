using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Transform pfDamagePopup;

    private void Start()
    {
        Transform damagePopupTransform = Instantiate(pfDamagePopup, Vector2.zero, Quaternion.identity);
        DamagePopup damagePopup;
        damagePopupTransform.TryGetComponent<DamagePopup>(out damagePopup);

        damagePopup.SetUp(56);

    }
}
