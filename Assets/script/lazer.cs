using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazer : MonoBehaviour
{

    SpriteRenderer sprite;
   
    // Start is called before the first frame update
    void OnEnable()
    {
        sprite = GetComponent<SpriteRenderer>();
        transform.rotation = transform.parent.transform.rotation;//������ ������ ���� �߻�
        
        
        GetComponent<Animator>().Play("lazer");
        Invoke("Destroy", 2f);//2�� �� Ǯ��
        transform.SetParent(null);//�߻��� ȸ�� ����
    }


    void Destroy()
    {
        transform.SetParent(GameManager.gm.pool.transform);
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(sprite.color == Color.black)
        sprite.color = Color.white;// ����ȿ���� Ÿ�̹��� ������ �������� �ȵǴ� ������ �����ϱ� ���� ������� ����

    }
 
    }

