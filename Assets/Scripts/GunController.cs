using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    //Ȱ��ȭ����
    public static bool isActivate= false;

    //���� ������ ��
    [SerializeField]
    private Gun currentGun;

    private float currentFireRate;

    //���º���
    private bool isReload = false;
    [HideInInspector]
    public bool isFIneSightMode = false;

    //���� ������ ��
    [SerializeField]
    private Vector3 originPos;
    private AudioSource audioSource;

    //������ �浿 ���� �޾ƿ�
    private RaycastHit hitlnfo;

    [SerializeField]
    private Camera theCam;
    private Crosshair theCrosshair;


    [SerializeField]
    private GameObject hit_effect_prefad;

    // Update is called once per frame
    private void Start()
    {
        originPos = Vector3.zero;
        audioSource= GetComponent<AudioSource>();
        theCrosshair = FindObjectOfType<Crosshair>();


 
    }

    void Update()
    {

        if (isActivate)
        {
            GunFireRateCalc();
            TryFire();
            TryReload();
            TryFineSight();
        }
       
    }

    //����ӵ� ����
    private void GunFireRateCalc()
    {
        if (currentFireRate > 0)
            currentFireRate -= Time.deltaTime; 

    }

    private void TryFire()
    {
        if(Input.GetButton("Fire1") && currentFireRate <= 0 && !isReload)
        {
            Fire();
        }
    }

    //�߻��� ���
    private void Fire()
    {
        if(!isReload)
        {
            if (currentGun.currentBulletCount > 0)
                Shoot();
            else
            {
                CancelFinSight();
                StartCoroutine(ReloadCoroutine());

            }
                
        }
       


    }

    //�߻��� ���
    private void Shoot()
    {
        theCrosshair.FireAnimation();
        currentGun.currentBulletCount--;
        currentFireRate = currentGun.fireRate; //����ӵ� ����
        PlaySE(currentGun.fire_Sound);
        currentGun.muzzleFlash.Play();

        Hit(); //�ٸ��� �ҰŸ� ������Ʈ ���� ����
        //�ѱ� �ݵ� �ڷ�ƾ
        StopAllCoroutines();
        StartCoroutine(RetroActionCoroutine());


    }


    //�´� ����
    private void Hit()
    {
        if(Physics.Raycast(theCam.transform.position, theCam.transform.forward + 
            new Vector3(Random.Range(-theCrosshair.GetAccuracy() - currentGun.accuracy, theCrosshair.GetAccuracy() + currentGun.accuracy),
                        Random.Range(-theCrosshair.GetAccuracy() - currentGun.accuracy, theCrosshair.GetAccuracy() + currentGun.accuracy),
                        0)
            , out hitlnfo, currentGun.range))
        {
            var clone = Instantiate(hit_effect_prefad, hitlnfo.point, Quaternion.LookRotation(hitlnfo.normal));
            Destroy(clone, 2f);
        }
    }

    private void TryReload()
    {
        if(Input.GetKeyDown(KeyCode.R) && !isReload && currentGun.currentBulletCount < currentGun.reloadBulletCount)
        {
            CancelFinSight();
            StartCoroutine(ReloadCoroutine());
        }
    }

    public void CanvelReload()
    {
        if(isReload)
        {
            StopAllCoroutines();
            isReload= false;
        }
    }
    //���� �ڷ�ƾ
    IEnumerator ReloadCoroutine()
    {
        if(currentGun.currentBulletCount >= 0 )
        {
            isReload = true;
            currentGun.anim.SetTrigger("Reload");

            currentGun.currentBulletCount += currentGun.currentBulletCount;
            currentGun.currentBulletCount = 0;

            yield return new WaitForSeconds(currentGun.reloadTime);

            if(currentGun.carryBulletCount >= currentGun.reloadBulletCount)
            {
                currentGun.currentBulletCount = currentGun.reloadBulletCount;
                currentGun.carryBulletCount -= currentGun.reloadBulletCount;
            }
            else
            {
                currentGun.currentBulletCount = currentGun.carryBulletCount;
                currentGun.carryBulletCount = 0;
            }

            isReload = false;
        }
        else
        {
            Debug.Log("������ �Ѿ��� �����ϴ�.");
        }
    }

    //������
    private void TryFineSight()
    {
        if(Input.GetButtonDown("Fire2") && !isReload)
        {
            FineSight();
        }
    }

    public void CancelFinSight()
    {
        if (isFIneSightMode)
            FineSight();
            
    }

    //������ ����
    private void FineSight()
    {
        isFIneSightMode = !isFIneSightMode;
        currentGun.anim.SetBool("FineSightMode", isFIneSightMode);
        theCrosshair.FineSightAnimation(isFIneSightMode);
        
        if(isFIneSightMode )
        {
            StopAllCoroutines();
            StartCoroutine(FineSughtActivateCoroutine());

        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(FineSughtDeactivateCoroutine());
        }

    }

    //������ Ȱ��ȭ
    IEnumerator FineSughtActivateCoroutine()
    {
        while(currentGun.transform.localPosition != currentGun.fineSightOriginPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.fineSightOriginPos, 0.2f);
            yield return null;
        }
    }

    //������ ��Ȱ��ȭ
    IEnumerator FineSughtDeactivateCoroutine()
    {
        while (currentGun.transform.localPosition != originPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.2f);
            yield return null;
        }
    }

    //�ݵ�
    IEnumerator RetroActionCoroutine()
    {
        Vector3 recoilBack = new Vector3(currentGun.retroActionForce, originPos.y, originPos.z);
        Vector3 retroActionRecoilBack = new Vector3(currentGun.retroActionFineSightForce, currentGun.fineSightOriginPos.y, currentGun.fineSightOriginPos.z);
        
        if(!isFIneSightMode)
        {
            currentGun.transform.localPosition = originPos;

            //�ݵ�����
            while(currentGun.transform.localPosition.x <= currentGun.retroActionForce -0.02f)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, recoilBack, 0.4f);
                yield return null;
            }

            //����ġ

            while(currentGun.transform.localPosition != originPos)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.1f);
                yield return null;
            }


        }
        else
        {
            currentGun.transform.localPosition = currentGun.fineSightOriginPos;
            //�ݵ�����
            while (currentGun.transform.localPosition.x <= currentGun.retroActionFineSightForce - 0.02f)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, retroActionRecoilBack, 0.4f);
                yield return null;
            }

            //����ġ

            while (currentGun.transform.localPosition != currentGun.fineSightOriginPos)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.fineSightOriginPos, 0.1f);
                yield return null;
            }
        }

    }

    //����
    private void PlaySE(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }

    public Gun GetGun()
    {
        return currentGun;
    }

    public bool GetFineSightMode()
    {
        return isFIneSightMode;
    }

    public void GunChange(Gun _gun)
    {
        if(WeaponManager.currentWeapon!= null)
        {
            WeaponManager.currentWeapon.gameObject.SetActive(false);
        }

        currentGun = _gun;
        WeaponManager.currentWeapon = currentGun.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentGun.anim;



        currentGun.transform.localPosition = Vector3.zero;
        currentGun.gameObject.SetActive(true);
        isActivate= true;
    }
}
