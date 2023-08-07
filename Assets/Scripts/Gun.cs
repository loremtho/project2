using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public string gunName;
    public float range;
    public float accuracy;
    public float fireRate;
    public float reloadTime;

    public int damage;

    public int reloadBulletCount; //재장전 개수
    public int currentBulletCount; //탄창
    public int maxBulletCount; //최대 소유
    public int carryBulletCount; 

    public float retroActionForce;
    public float retroActionFineSightForce;

    public Vector3 fineSightOriginPos;
    public Animator anim;
    public ParticleSystem muzzleFlash;

    public AudioClip fire_Sound;



}
