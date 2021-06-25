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

        List<GameObject> list = new List<GameObject>();//���� ��ȯ �� �����͸� ���� �迭

        if (left)
        {
           // left_obj= Instantiate(blast, new Vector3(-lmr_x, lmr_y+up, 0), Quaternion.identity);
            MakeVec("blast", new Vector3(-lmr_x, lmr_y + up, 0),ref left_obj,ref blast);//�����͸� �����
            list.Add(left_obj);//����Ʈ�� �߰�
            left_obj.transform.rotation = Quaternion.identity;//������ Ʋ���� ȸ�������� �ҷ��� ��� ���� �������� �� ����.
        }
           
        if(middle)
        { 
           // middle_obj= Instantiate(blast, new Vector3(0, lmr_y+up, 0), Quaternion.identity);
            MakeVec("blast", new Vector3(0, lmr_y + up, 0), ref middle_obj, ref blast);
            list.Add(middle_obj);
            middle_obj.transform.rotation = Quaternion.identity;//������ Ʋ���� ȸ�������� �ҷ��� ��� ���� �������� �� ����.
        }
           
        if(right)
        {
           // right_obj = Instantiate(blast, new Vector3(lmr_x, lmr_y+up, 0), Quaternion.identity);
            MakeVec("blast", new Vector3(lmr_x, lmr_y + up, 0), ref right_obj, ref blast);
            list.Add(right_obj);
            right_obj.transform.rotation = Quaternion.identity;//������ Ʋ���� ȸ�������� �ҷ��� ��� ���� �������� �� ����.
        }
                   
        StartCoroutine(go_down(list));//�Ʒ��� �����̱�.
        StartCoroutine(wait_and_shoot(left, middle, right));//���
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
    IEnumerator wait_and_shoot(bool left, bool middle, bool right)//���ʿ��� ��� ���� ������
    { 
        print("�ƴ�");    

        if (left)
            left_obj.GetComponent<makelazer>().lazermake();
        print("��");
        if (middle)
            middle_obj.GetComponent<makelazer>().lazermake();
        print("��");
        if (right)
            right_obj.GetComponent<makelazer>().lazermake();
        print("����");

        yield return new WaitForSeconds(2.6f);//������ �� ������ ��ٷ��ش�.������ ��µ� 1.6��(0.8+0.8)/1�� ��
        
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

        //Ǯ�Ŵ����� Ǯ��

        
    }
  
   
}