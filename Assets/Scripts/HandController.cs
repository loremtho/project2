using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : CloseWeaponController
{

    //활성화여부
    public static bool isActivate = false;

    [SerializeField]
    private QuickSlotController theQuickSlot;
    // Update is called once per frame
    void Update()
    {
        if (isActivate && !Inventory.inventoryActivated)
        {
            if(QuickSlotController.go_HandItem == null)
            TryAttack();
            else
            TryEating();
        }

    }

    private void TryEating()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            currentCloseWeapon.anim.SetTrigger("Eat");
            theQuickSlot.EatItem();
        }
    }


    protected override IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                isSwing = false;
                Debug.Log(hitlnfo.transform.name);
            }
            yield return null;
        }
    }


    public override void CloseWeaponChange(CloseWeapon _closeWeapon)
    {
        base.CloseWeaponChange(_closeWeapon);
        isActivate = true;

    }
}
