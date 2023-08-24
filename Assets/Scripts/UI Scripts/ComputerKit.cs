using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Kit
{
    public string kitName;
    public string kitDescription;
    public string[] needItemName;
    public int[] needItemNumber;

    public GameObject go_Kit_Prefab;
}

public class ComputerKit : MonoBehaviour
{

    [SerializeField]
    private Kit[] kits;

    [SerializeField] private Transform tf_ItemAppearPos; // 생성될 아이템 위치
    [SerializeField] private GameObject go_BaseUi;

    private bool isCraft = false; // 중복실행 방지
    public bool isPowerOn = false; //전원 켜졌는지

    //필요한 컴포넌트
    private Inventory theinven;
    [SerializeField]
    private ComputerTooltip theToolTip;

    private AudioSource theAudio;
    [SerializeField] private AudioClip sound_ButtonClick;
    [SerializeField] private AudioClip sound_Beep;
    [SerializeField] private AudioClip sound_Activated;
    [SerializeField] private AudioClip sound_Output;

    void Start() {
        theinven = FindObjectOfType<Inventory>();
        theAudio = GetComponent<AudioSource>();
    }

    void Update() {

        if(isPowerOn)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            PowerOff();
        }
    }

    public void PowerOn()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPowerOn = true;
        go_BaseUi.SetActive(true);
    }
    public void PowerOff()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPowerOn = false;
        theToolTip.HideToolTip();
        go_BaseUi.SetActive(false);
    }

    public void ShowToolTip(int _buttonNum)
    {
        theToolTip.ShowToolTip(kits[_buttonNum].kitName, kits[_buttonNum].kitDescription, kits[_buttonNum].needItemName, kits[_buttonNum].needItemNumber);
    }

    public void HideToolTip()
    {
        theToolTip.HideToolTip();
    }

    private void PlaySE(AudioClip _clip)
    {
        theAudio.clip = _clip;
        theAudio.Play();
    }

    public void ClickButton(int _slotNumber)
    {
        PlaySE(sound_ButtonClick);
        if(!isCraft)
        {
            if(!CheckIngredient(_slotNumber)) //재료 체크
            return;

            isCraft = true;
            UseIngredient(_slotNumber); //재료 사용

            StartCoroutine(CraftCoroutine(_slotNumber)); // kit 생성
        }
    }

    private bool CheckIngredient(int _slotNumber)
    {
        for (int i = 0; i < kits[_slotNumber].needItemNumber.Length; i++)
        {
            if(theinven.GetItemCount(kits[_slotNumber].needItemName[i]) < kits[_slotNumber].needItemNumber[i])
            {
                PlaySE(sound_Beep);
                return false;
            }
        }
        return true;
    }

    private void UseIngredient(int _slotNumber)
    {
        for (int i = 0; i < kits[_slotNumber].needItemNumber.Length; i++)
        {
            theinven.SetItemCount(kits[_slotNumber].needItemName[i], kits[_slotNumber].needItemNumber[i]);
        }
    }

    private

    IEnumerator CraftCoroutine(int _slotNumber)
    {
        PlaySE(sound_Activated);
        yield return new WaitForSeconds(3f);
        PlaySE(sound_Output);

        Instantiate(kits[_slotNumber].go_Kit_Prefab, tf_ItemAppearPos.position, Quaternion.identity);
        isCraft = false;
    }

}
