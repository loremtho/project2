using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : CloseWeaponController
{
    //활성화여부
    public static bool isActivate = true;

    private void Start()
    {
        WeaponManager.currentWeapon = currentCloseWeapon.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentCloseWeapon.anim;
    }
    // Update is called once per frame
    void Update()
    {
        if (isActivate)
        {
            TryAttack();
        }

    }
    protected override IEnumerator HitCoroutine()
    {
        while(isSwing)
        {
            if(CheckObject())
            {
                if(hitlnfo.transform.tag == "Rock")
                {
                    hitlnfo.transform.GetComponent<Rock>().Mining();                   
                }
                else if(hitlnfo.transform.tag == "Twig")
                {
                    hitlnfo.transform.GetComponent<Twig>().Damage(this.transform);                   
                }
                else if(hitlnfo.transform.tag == "Grass")
                {
                    hitlnfo.transform.GetComponent<Grass>().Damage();                   
                }
                else if(hitlnfo.transform.tag == "Tree")
                {
                    hitlnfo.transform.GetComponent<Tree>().Chop(hitlnfo.point, transform.eulerAngles.y);                   
                }
                isSwing= false;
                Debug.Log(hitlnfo.transform.name);
            }
            yield return null;
        }
    }

    public override void CloseWeaponChange(CloseWeapon _closeWeapon)
    {
        base.CloseWeaponChange(_closeWeapon);
        isActivate= true;
    }
}
