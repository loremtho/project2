using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{

    //풀 체력
    [SerializeField]
    private int hp;
    //이펙트 제거 시간
    [SerializeField]
    private float destroyTime;
    //폭발력 세기
    [SerializeField]
    private float Expforce;
    //타격 효과
    [SerializeField]
    private GameObject go_hit_effect_prefab;


    [SerializeField]
    private Item item_leaf;
    [SerializeField]
    private int leafCount;
    private Inventory theInven;

    private Rigidbody[] rigidbodies;
    private BoxCollider[] boxColliders;

    [SerializeField]
    private string hit_Sound;

    // Start is called before the first frame update
    void Start()
    {
        theInven = FindObjectOfType<Inventory>();
        rigidbodies = this.transform.GetComponentsInChildren<Rigidbody>();
        boxColliders = transform.GetComponentsInChildren<BoxCollider>();
    }

    public void Damage()
    {
        hp--;

        Hit();

        if(hp <= 0)
        {
            Destrcution();
        }
    }

    private void Hit()
    {
        SoundManager.instance.PlaySE(hit_Sound);

        var clone = Instantiate(go_hit_effect_prefab, transform.position + Vector3.up, Quaternion.identity);
        Destroy(clone, destroyTime);
    }

    private void Destrcution()
    {
        theInven.Acquireltem(item_leaf, leafCount);

        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].useGravity = true;
            rigidbodies[i].AddExplosionForce(Expforce, transform.position, 1f);
            boxColliders[i].enabled = true;
        }

        Destroy(this.gameObject, destroyTime);
    }

}
