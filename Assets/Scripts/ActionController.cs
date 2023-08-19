using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range; //????? ?? ??

    private bool pickupActivated = false; //?? ??? ? ?? ??


    private RaycastHit hitinfo;

    //??? ????? ????? ??? ???
    [SerializeField]
    private LayerMask layerMask;

    //??? ????
    [SerializeField]
    private Text actionText;
    [SerializeField]
    private Inventory theinventory;


    void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickUp();
        }
    }

    private void CheckItem()
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitinfo, range, layerMask))
        {
            if(hitinfo.transform.tag == "Item")
            {
                ItemlnfoAppear();
            }
        }
        else
        {
            lnfoDisappear();
        }
    }

    private void CanPickUp()
    {
        if(pickupActivated)
        {
            if(hitinfo.transform != null)
            {
                Debug.Log(hitinfo.transform.GetComponent<ItemPickUp>().item.itemName + " ?????? ");
                theinventory.Acquireltem(hitinfo.transform.GetComponent<ItemPickUp>().item );
                Destroy(hitinfo.transform.gameObject);
                lnfoDisappear();
            }
        }
    }
    private void ItemlnfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitinfo.transform.GetComponent<ItemPickUp>().item.itemName + " ?? " + "<color=yellow>" + "(E)" + "</color>";
    }

    private void lnfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }
}
