using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject blast;

    [SerializeField]
    float lmr_x, lmr_y,up;

    [SerializeField]
    float splash_delay;

    GameObject left_obj, middle_obj, right_obj;

    [SerializeField]
    float downspeed;


    public void lmr(bool left,bool middle,bool right)
    {

        List<GameObject> list = new List<GameObject>();//위에 소환 할 블래스터를 담을 배열

        if (left)
        {
           // left_obj= Instantiate(blast, new Vector3(-lmr_x, lmr_y+up, 0), Quaternion.identity);
            MakeVec("blast", new Vector3(-lmr_x, lmr_y + up, 0),ref left_obj,ref blast);//블래스터를 만들고
            list.Add(left_obj);//리스트에 추가
            left_obj.transform.rotation = Quaternion.identity;//각도가 틀어진 회전패턴을 불러올 경우 각도 문제생길 수 있음.
        }
           
        if(middle)
        { 
           // middle_obj= Instantiate(blast, new Vector3(0, lmr_y+up, 0), Quaternion.identity);
            MakeVec("blast", new Vector3(0, lmr_y + up, 0), ref middle_obj, ref blast);
            list.Add(middle_obj);
            middle_obj.transform.rotation = Quaternion.identity;//각도가 틀어진 회전패턴을 불러올 경우 각도 문제생길 수 있음.
        }
           
        if(right)
        {
           // right_obj = Instantiate(blast, new Vector3(lmr_x, lmr_y+up, 0), Quaternion.identity);
            MakeVec("blast", new Vector3(lmr_x, lmr_y + up, 0), ref right_obj, ref blast);
            list.Add(right_obj);
            right_obj.transform.rotation = Quaternion.identity;//각도가 틀어진 회전패턴을 불러올 경우 각도 문제생길 수 있음.
        }
                   
        StartCoroutine(go_down(list));//아래로 움직이기.
        StartCoroutine(wait_and_shoot(left, middle, right));//쏘기
    }

    IEnumerator go_down(List<GameObject> obj)
    {
              
        foreach(GameObject blaster in obj )
        {
            
            
            while (!Mathf.Approximately(blaster.transform.position.y,lmr_y))
            {
                float rp = Mathf.Lerp(blaster.transform.position.y, lmr_y, downspeed);
                blaster.transform.position = new Vector2(blaster.transform.position.x, rp);
                yield return null;

            }
          
        }
    }
    IEnumerator wait_and_shoot(bool left, bool middle, bool right)//위쪽에서 쏘는 정규 블래스터
    { 
        print("됐다");    

        if (left)
            left_obj.GetComponent<makelazer>().lazermake();
        print("원");
        if (middle)
            middle_obj.GetComponent<makelazer>().lazermake();
        print("투");
        if (right)
            right_obj.GetComponent<makelazer>().lazermake();
        print("쓰리");

        yield return new WaitForSeconds(2.6f);//레이저 쏠 때까지 기다려준다.레이저 쏘는데 1.6초(0.8+0.8)/1초 텀
        
        if (left)
        {           
            left_obj.transform.SetParent(pool.transform);
            left_obj.SetActive(false);                  
        }

        if (middle)
        {
            middle_obj.transform.SetParent(pool.transform);
            middle_obj.SetActive(false);          
        }

        if (right)
        {
            right_obj.transform.SetParent(pool.transform);
            right_obj.SetActive(false);         
        }

        //풀매니저에 풀링

        
    }
  
   
}