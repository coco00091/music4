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

            default_blasts.SetActive(false);//ȸ�� ������ ���ֱ�
            for (int i = 0; i < pattern2_maxcount; i++)
            {
                circleangle += angleplus;//���� �׸��� �����͸� ��ȯ�ϱ� ���� �� ������ ����
                circleedge = new Vector2(Mathf.Sin(Mathf.Deg2Rad * circleangle), Mathf.Cos(Mathf.Deg2Rad * circleangle));//������-> ���� ->�����ڻ����� �̿��� ���ͷ� ��ȯ
                MakeVec("blaster", circleedge * circlerad, ref obj, ref blast_with_par);//������ ����
                float angle = Mathf.Atan2(obj.transform.position.y, obj.transform.position.x) * Mathf.Rad2Deg;//Vector3.Angle(obj.transform.position,Vector3.zero);
                obj.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);//
                obj.transform.GetChild(0).GetComponent<makelazer>().lazermake();//������ �߻�
                obj.tag = "pattern2";//���߿� ������ ������ ������ �� ��ƿ��� ���� �뵵
                yield return new WaitForSeconds(pattern2_delay);
            }

            var catcher = GameObject.FindGameObjectsWithTag("pattern2");//��ȯ�� ���׸��� �����͸� ��� ��ƿ�
            foreach (var obj in catcher)
            {
                obj.tag = "blaster";//��Ȱ���� ���� �±� ������
                obj.transform.SetParent(pool.transform);//Ǯ��
                obj.SetActive(false);
            }
            default_blasts.SetActive(true);//ȸ�� ������ ���ֱ�

        }
    }
}
    

