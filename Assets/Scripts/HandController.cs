using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : CloseWeaponController
{

    //활성화여부
    public static bool isActivate = false;
    public static Item currentKit; //설치하려는 킷 (연금테이블)

    private bool isPreview = false;

    private GameObject go_preview; // 설치할 키트 프리뷰.
    private Vector3 previewPos; //설치할 키트 위치.
    [SerializeField]
    private float rangeAdd; //건축시 추가 사정거리.


    [SerializeField]
    private QuickSlotController theQuickSlot;
    // Update is called once per frame
    void Update()
    {
        if (isActivate && !Inventory.inventoryActivated)
        {
            if(currentKit == null)
            {
                if (QuickSlotController.go_HandItem == null)
                    TryAttack();
                else
                    TryEating();
            }
            else
            {
                if(!isPreview)
                    InstallPreviewKit();
                PreviewPositionUpdate();
                Build();
            }
        }

    }

    private void InstallPreviewKit()
    {
        isPreview= true;
        go_preview = Instantiate(currentKit.kitPreviewPrefab , transform.position, Quaternion.identity);
    }

    private void PreviewPositionUpdate()
    {
        if(Physics.Raycast(transform.position, transform.forward, out hitlnfo, currentCloseWeapon.range + rangeAdd /*왜 안댐? layerMask*/ ))
        {
            previewPos = hitlnfo.point;
            go_preview.transform.position = previewPos;
        }

    }

    private void Build()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(go_preview.GetComponent<PreviewObject>().isBuildable())
            {
                //요건 또 왜 없지? theQuickSlot.DecreaseSelectedItem();
                GameObject temp = Instantiate(currentKit.kitPrefab, previewPos, Quaternion.identity);
                Destroy(go_preview);
                currentKit = null;
                isPreview = false;

            }
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
