using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [System.Serializable]
public class Sound{
    public string name; //곡의 이름
    public AudioClip clip; //곡
}

public class SoundManager : MonoBehaviour
{

    static public SoundManager instance;

    #region singleton
    void Awake() 
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion singleton

    public AudioSource[] audioSourcesEffects;
    public AudioSource audioSourceBgm;

    public string [] playSoundName;

    public Sound[] effectSounds;
    public Sound[] bgmSound;

    void Start() 
    {
        playSoundName = new string[audioSourcesEffects.Length];
    }


    public void PlaySE(string _name) // 곡 재생 함수
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if(_name == effectSounds[i].name)
            {
                for(int j = 0; j < effectSounds.Length; j++)
                {
                    if(!audioSourcesEffects[j].isPlaying)
                    {
                        playSoundName[j] = effectSounds[i].name;
                        audioSourcesEffects[j].clip = effectSounds[i].clip;
                        audioSourcesEffects[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 가용 AudioSource가 사용중입니다.");
                return;
            }
        }
        Debug.Log(_name + "사운드가 SoundManager에 등록되지 않았습니다.");
    }

    public void StopAllSE() //곡 정지 함수
    {
        for (int i = 0; i < audioSourcesEffects.Length; i++)
        {
            audioSourcesEffects[i].Stop();
        }
    }

    public void StopSE(string _name)
    {
        for (int i = 0; i < audioSourcesEffects.Length; i++)
        {
            if(playSoundName[i] == _name)
            {
                audioSourcesEffects[i].Stop();
                return;
            }
        }
        Debug.Log("재생 중인" + _name + "사운드가 없습니다.");
    }

}
