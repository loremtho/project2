using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    //기존위치
    private Vector3 originPos;

    //현재위치
    private Vector3 currentPos;

    //sway 한계
    [SerializeField]
    private Vector3 limitPos;

    //정조준 sway 한계
    [SerializeField]
    private Vector3 fineSightLimotPos;

    //부드러운 움직임 정도
    [SerializeField]
    private Vector3 smoothSway;

    //필요한 컴포넌트
    [SerializeField]
    private GunController theGunController;
    // Start is called before the first frame update
    void Start()
    {
        originPos = this.transform.localPosition;
    }

    
    void Update()
    {
        if(!Inventory.inventoryActivated)
        {
            TrySway();
        }
    }
    
    private void TrySway()
    {
        if(Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0)
            Swaying();
        else
            BackToOriginPos();
    }

    
    private void Swaying()
    {
        float _moveX = Input.GetAxisRaw("Mouse X");
        float _moveY = Input.GetAxisRaw("Mouse Y");

        if(!theGunController.isFIneSightMode)
        {
            currentPos.Set(Mathf.Clamp(Mathf.Lerp(currentPos.x, -_moveX, smoothSway.x), -limitPos.x, limitPos.x),
                     Mathf.Clamp(Mathf.Lerp(currentPos.y, -_moveY, smoothSway.x), -limitPos.y, limitPos.y),
                      originPos.z);
        }
        else
        {
            currentPos.Set(Mathf.Clamp(Mathf.Lerp(currentPos.x, -_moveX, smoothSway.y), -fineSightLimotPos.x, fineSightLimotPos.x),
                 Mathf.Clamp(Mathf.Lerp(currentPos.y, -_moveY, smoothSway.y), -fineSightLimotPos.y, fineSightLimotPos.y),
                  originPos.z);
        }
   

        transform.localPosition = currentPos;
    }
    
    private void BackToOriginPos()
    {
        currentPos = Vector3.Lerp(currentPos,originPos,smoothSway.x);
        transform.localPosition = currentPos;
    }
}
