using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class GameManager : MonoBehaviour
{
//    [SerializeField]
//    GameObject blast_with_par;
//    GameObject obj;
//    float circleangle;
//    Vector2 circleedge;
      [SerializeField]
      float angleplus2, pattern3_delay, pattern3_firstdelay, circlerad2, pattern3_maxcount,pattern3_trycount,pattern3_trydelay;
    [SerializeField]
    GameObject blaster_with_par2;

   
    //    [SerializeField]
    //    GameObject default_blasts;

    
    IEnumerator dongbang()
    {
      
        while (true)
        {
            yield return new WaitForSeconds(pattern3_firstdelay);


            for (int j = 0; j < pattern3_trycount; j++)
            {


                yield return new WaitForSeconds(pattern3_trydelay);
                
                for (int i = 0; i < pattern3_maxcount; i++)
                {
                    circleangle += angleplus2;//���� �׸��� �����͸� ��ȯ�ϱ� ���� �� ������ ����
                    circleedge = new Vector2(Mathf.Sin(Mathf.Deg2Rad * circleangle), Mathf.Cos(Mathf.Deg2Rad * circleangle));//������-> ���� ->�����ڻ���(����Ƽ������ Sin,Cos ������ radian�� ������ ������� ����)�� �̿��� ���ͷ� ��ȯ

                    MakeVec("blaster", circleedge * circlerad2, ref obj, ref blast_with_par);//������ ����
                    float angle = Mathf.Atan2(obj.transform.position.y, obj.transform.position.x) * Mathf.Rad2Deg;
                   // float angle = Vector3.Angle (obj.transform.position,Vector2.zero);
                    obj.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);//                 
                    obj.AddComponent<movetocenter>();
                    yield return new WaitForSeconds(pattern3_delay);
                }

                
               

            }
        }
    }

    IEnumerator dongbang2()
    {
      
        while (true)
        {
            yield return new WaitForSeconds(1);
            print("�ڷ�ƾ ����");

            for (int j = 0; j < pattern3_trycount; j++)
            {


                yield return new WaitForSeconds(pattern3_trydelay);

                for (int i = 0; i < 3; i++)
                {
                    circleangle += angleplus2;//���� �׸��� �����͸� ��ȯ�ϱ� ���� �� ������ ����
                    circleedge = new Vector2(Mathf.Sin(Mathf.Deg2Rad * circleangle), Mathf.Cos(Mathf.Deg2Rad * circleangle));//������-> ���� ->�����ڻ���(����Ƽ������ Sin,Cos ������ radian�� ������ ������� ����)�� �̿��� ���ͷ� ��ȯ

                    MakeVec("chaser", circleedge * circlerad2, ref obj, ref blaster_with_par2);//������ ����
                    obj.transform.GetChild(0).GetComponent<Attacker>().lazermake();
                    yield return new WaitForSeconds(pattern3_delay);
                }




            }
        }
    }
}