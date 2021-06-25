using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject blast_with_par;
    GameObject obj;
    float circleangle;
    Vector2 circleedge;
    [SerializeField]
    float angleplus, pattern2_delay, pattern2_firstdelay, circlerad, pattern2_maxcount;

    [SerializeField]
    GameObject default_blasts;

    IEnumerator extremerotate()
    {
        while (true)
        {

            yield return new WaitForSeconds(pattern2_firstdelay);

            default_blasts.SetActive(false);//회전 블래스터 꺼주기
            for (int i = 0; i < pattern2_maxcount; i++)
            {
                circleangle += angleplus;//원을 그리며 블래스터를 소환하기 위한 원 내각도 증가
                circleedge = new Vector2(Mathf.Sin(Mathf.Deg2Rad * circleangle), Mathf.Cos(Mathf.Deg2Rad * circleangle));//내각도-> 라디안 ->사인코사인을 이용해 벡터로 변환
                MakeVec("blaster", circleedge * circlerad, ref obj, ref blast_with_par);//블래스터 생성
                float angle = Mathf.Atan2(obj.transform.position.y, obj.transform.position.x) * Mathf.Rad2Deg;//Vector3.Angle(obj.transform.position,Vector3.zero);
                obj.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);//
                obj.transform.GetChild(0).GetComponent<makelazer>().lazermake();//레이저 발사
                obj.tag = "pattern2";//나중에 생성된 블래스터 삭제할 때 잡아오기 위한 용도
                yield return new WaitForSeconds(pattern2_delay);
            }

            var catcher = GameObject.FindGameObjectsWithTag("pattern2");//소환한 원그리기 블래스터를 모두 잡아옴
            foreach (var obj in catcher)
            {
                obj.tag = "blaster";//재활용을 위해 태그 재정비
                obj.transform.SetParent(pool.transform);//풀링
                obj.SetActive(false);
            }
            default_blasts.SetActive(true);//회전 블래스터 켜주기

        }
    }
}
    

