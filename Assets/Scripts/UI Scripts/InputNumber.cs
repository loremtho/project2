using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputNumber : MonoBehaviour
{
    private bool activated =false;

    [SerializeField]
    private Text text_Priview;
    [SerializeField]
    private Text text_Input;
    [SerializeField]
    private InputField if_text;

    [SerializeField]
    private GameObject go_Base;

    [SerializeField]
    private ActionController thePlayer;


    void Update() 
    {
        if(activated)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            OK();
            else if(Input.GetKeyDown(KeyCode.Escape))
            Cancel();
        }
    }
    public void Call()
    {
        go_Base.SetActive(true);
        activated = true;
        if_text.text = "";
        text_Priview.text = DragSlot.instance.dragSlot.itemCount.ToString();
    }

    public void Cancel()
    {
        go_Base.SetActive(false);
        activated = false;
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    public void OK()
    {
        DragSlot.instance.SetColor(0);

        int num;
        if(text_Input.text != "")
        {
            if(CheckNumber(text_Input.text))
            {
                num = int.Parse(text_Input.text);
                if(num > DragSlot.instance.dragSlot.itemCount)
                   num = DragSlot.instance.dragSlot.itemCount;
            }
            else
            {
                num = 1;
            }
        }
        else
        num = int.Parse(text_Priview.text);
        StartCoroutine(DropItemCoroutine(num));
        activated = false;
    }

    IEnumerator DropItemCoroutine(int _num)
    {
        for (int i = 0; i < _num; i++)
        {
            if(DragSlot.instance.dragSlot.item.itemPrefab != null)
            {
                Instantiate(DragSlot.instance.dragSlot.item.itemPrefab, thePlayer.transform.position + thePlayer.transform.forward, Quaternion.identity);
            }
            DragSlot.instance.dragSlot.SetSlotCount(-1);
            yield return new WaitForSeconds(0.05f);
        }

        //현재 아이템을 들고있고, 그 아이템의 모든 개수를 버릴 경우 아이템 파괴
        if(int.Parse(text_Priview.text) == _num)
        {
            if(QuickSlotController.go_HandItem != null)
                Destroy(QuickSlotController.go_HandItem);
        }
        DragSlot.instance.dragSlot = null;
        go_Base.SetActive(false);
        activated = false;
    }

    private bool CheckNumber(string _argString)
    {
        char[] _tempCharArray = _argString.ToCharArray();
        bool isNumber = true;
        for (int i = 0; i < _tempCharArray.Length; i++)
        {
            if(_tempCharArray[i] >= 48 && _tempCharArray[i] <= 57)
            continue;
            isNumber = false;
        }

        return isNumber;
    }

}
