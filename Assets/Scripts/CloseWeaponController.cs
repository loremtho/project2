using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CloseWeaponController : MonoBehaviour
{
    //�̿ϼ� Ŭ����
   

    [SerializeField] //���� ������ hand�� Ÿ�� ����
    protected CloseWeapon currentCloseWeapon;

    //������
    protected bool isAttack = false;
    protected bool isSwing = false;

    protected RaycastHit hitlnfo;





    protected void TryAttack()
    {
        if (Input.GetButton("Fire1"))
        {
            if (!isAttack)
            {
                //�ڷ�ƾ
                StartCoroutine(AttackCoroutine());
            }
        }
    }

    protected IEnumerator AttackCoroutine()
    {

        isAttack = true;
        currentCloseWeapon.anim.SetTrigger("Attack");

        yield return new WaitForSeconds(currentCloseWeapon.attackDelatA);
        isSwing = true;  //���� Ȱ��ȭ

        StartCoroutine(HitCoroutine());


        yield return new WaitForSeconds(currentCloseWeapon.attackDelatB);
        isSwing = false;

        yield return new WaitForSeconds(currentCloseWeapon.attackDelay - currentCloseWeapon.attackDelatA - currentCloseWeapon.attackDelatB);
        isAttack = false;
    }

    //�̿ϼ� = �߻� �ڷ�ƾ.
    protected abstract IEnumerator HitCoroutine();
   
    protected bool CheckObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitlnfo, currentCloseWeapon.range))
        {
            return true;
        }

        return false;
    }

    //�߰� ����
    public virtual void CloseWeaponChange(CloseWeapon _closeWeapon)
    {
        if (WeaponManager.currentWeapon != null)
            WeaponManager.currentWeapon.gameObject.SetActive(false);
        

        currentCloseWeapon = _closeWeapon;
        WeaponManager.currentWeapon = currentCloseWeapon.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentCloseWeapon.anim;



        currentCloseWeapon.transform.localPosition = Vector3.zero;
        currentCloseWeapon.gameObject.SetActive(true);
       
    }

}
