using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    [SerializeField] private float secondPerRealTimeSecond; //게임 세계의 시간

  

    [SerializeField] private float fogDensityCalc; //증감량 비율
    [SerializeField] private float nightFogDensity; //밤상태의 Fog 밀도

    private float dayFogDensity; //낮상태의 fog 밀도
    private float currentFogDensity; //계산
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecond * Time.deltaTime);

        if(transform.eulerAngles.x >= 170)
        {
            GameManager.isNight= true;
        }
        else if(transform.eulerAngles.x <= 10)
        {
            GameManager.isNight = false;

        }

        if (GameManager.isNight)
        {
            if(currentFogDensity <= nightFogDensity)
            {
                currentFogDensity += 0.1f * fogDensityCalc * Time.deltaTime;
                RenderSettings.fogDensity = currentFogDensity;
            }
           
        }
        else
        {
            if (currentFogDensity >= dayFogDensity)
            {
                currentFogDensity -= 0.1f * fogDensityCalc * Time.deltaTime;
                RenderSettings.fogDensity = currentFogDensity;
            }
        }
    }
}
