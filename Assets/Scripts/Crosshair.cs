using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{

    [SerializeField]
    private Animator animator;

    private float gunAccuracy;

    [SerializeField]
    private GameObject go_crosshairHUD;
    [SerializeField]
    private GunController theGunController;


    public void WalkingAnimation(bool _flag)
    {
        if(!GameManager.isWater)
        {
            WeaponManager.currentWeaponAnim.SetBool("Walk", _flag);
            animator.SetBool("Walking", _flag);
        }
        
    }
    public void RunningAnimation(bool _flag)
    {
        if(!GameManager.isWater)
        {
            WeaponManager.currentWeaponAnim.SetBool("Run", _flag);
            animator.SetBool("Running", _flag);
        }
    }

    public void JumpingAnimation(bool _flag)
    {
        if(!GameManager.isWater)
        {
            animator.SetBool("Running", _flag);
        }
    }

    public void CrouchingAnimation(bool _flag)
    {
        if(!GameManager.isWater)
        {
            animator.SetBool("Crouching", _flag);
        }
    }
    public void FineSightAnimation(bool _flag)
    {
        if(!GameManager.isWater)
        {
            animator.SetBool("FineSight", _flag);
        }
    }

    public void FireAnimation()
    {
        if(!GameManager.isWater)
        {
            if (animator.GetBool("Walking"))
            {
                animator.SetTrigger("Walk_Fire");
            }
            else if (animator.GetBool("Crouching"))
            {
                animator.SetTrigger("Crouch_Fire");
            }  
            else
            {
                animator.SetTrigger("Idle_Fire");
            }
        }
            
    }

    public float GetAccuracy()
    {
        if (animator.GetBool("Walking"))
        {
            gunAccuracy = 0.06f;
        }
        else if (animator.GetBool("Crouching"))
        {
            gunAccuracy = 0.015f;
        }
        else if(theGunController.GetFineSightMode())
        {
            gunAccuracy = 0.001f;
        }
        else
        {
            gunAccuracy = 0.035f;
        }

        return gunAccuracy;

    }




}
