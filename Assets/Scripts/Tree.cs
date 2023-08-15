using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    //깎일 나무조각들
    [SerializeField]
    private GameObject[] go_treePieces;
    [SerializeField]
    private GameObject go_treeCenter;

    [SerializeField]
    private GameObject go_Log_prefab;

    //쓰러질 때 랜덤으로 가해질 힘의 세기
    [SerializeField]
    private float force;

    //자식 트리
    [SerializeField]
    private GameObject go_ChildTree;

    //부모 트리 쓰러지면 캡슐 콜라이더 제거
    [SerializeField]
    private CapsuleCollider parentCol;
    
    //자식 트리 쓰러질 때 필요한 컴포넌트 활성화 및 중력 활성화
    [SerializeField]
    private CapsuleCollider childCol;
    [SerializeField]
    private Rigidbody childRigid;

    //파편 효과
    [SerializeField]
    private GameObject go_hit_effect_prefab;
    //파편 제거 시간
    [SerializeField]
    private float debrisDestroyTime;
    //나무 제거 시간
    [SerializeField]
    private float destroyTime;

    //필요한 사운드
    [SerializeField]
    private string chop_Sound;
    [SerializeField]
    private string falldown_Sound;
    [SerializeField]
    private string logChange_Sound;




    public void Chop(Vector3 _pos, float angleY)
    {
        Hit(_pos);

        AngleCalc(angleY);

        if(CheckTreePiece())
        {
            return;
        }

        FallDownTree();
    }

    //적중 이펙트
    private void Hit(Vector3 _pos)
    {
        SoundManager.instance.PlaySE(chop_Sound);

        GameObject clone = Instantiate(go_hit_effect_prefab, _pos, Quaternion.Euler(Vector3.zero));
        Destroy(clone, debrisDestroyTime);
    }

    private void AngleCalc(float _angleY)
    {
        Debug.Log(_angleY);
        if(0 <= _angleY && _angleY <= 70)
        {
            DestroyPiece(2);
        }
        else if(70 <= _angleY && _angleY <= 140)
        {
            DestroyPiece(3);
        }
        else if(140 <= _angleY && _angleY <= 210)
        {
            DestroyPiece(4);
        }
        else if(210 <= _angleY && _angleY <= 280)
        {
            DestroyPiece(0);
        }
        else if(280 <= _angleY && _angleY <= 360)
        {
            DestroyPiece(1);
        }
    }

    private void DestroyPiece(int _num)
    {
        if(go_treePieces[_num].gameObject != null)
        {
            GameObject clone = Instantiate(go_hit_effect_prefab, go_treePieces[_num].transform.position, Quaternion.Euler(Vector3.zero));
            Destroy(clone, debrisDestroyTime);
            Destroy(go_treePieces[_num].gameObject);
        }
    }

    private bool CheckTreePiece()
    {
        for (int i = 0; i < go_treePieces.Length; i++)
        {
            if(go_treePieces[i].gameObject != null)
            {
                return true;
            }
        }
        return false;
    }

    private void FallDownTree()
    {

        SoundManager.instance.PlaySE(falldown_Sound);
        Destroy(go_treeCenter);

        parentCol.enabled = false;
        childCol.enabled = true;
        childRigid.useGravity = true;

        childRigid.AddForce(Random.Range(-force, force), 0f, Random.Range(-force, force));

        StartCoroutine(LogCoroutine());
    }

    IEnumerator LogCoroutine()
    {
        yield return new WaitForSeconds(destroyTime);

        SoundManager.instance.PlaySE(logChange_Sound);

        Instantiate(go_Log_prefab, go_ChildTree.transform.position + (go_ChildTree.transform.up * 3f), Quaternion.LookRotation(go_ChildTree.transform.up));
        Instantiate(go_Log_prefab, go_ChildTree.transform.position + (go_ChildTree.transform.up * 6f), Quaternion.LookRotation(go_ChildTree.transform.up));
        Instantiate(go_Log_prefab, go_ChildTree.transform.position + (go_ChildTree.transform.up * 9f), Quaternion.LookRotation(go_ChildTree.transform.up));

        Destroy(go_ChildTree.gameObject);
    }

    public Vector3 GetTreeCenterPosition()
    {
        return go_treeCenter.transform.position;
    }
}
