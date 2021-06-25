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
        transform.rotation = transform.parent.transform.rotation;//블래스터 각도에 맞춰 발사
        
        
        GetComponent<Animator>().Play("lazer");
        Invoke("Destroy", 2f);//2초 후 풀링
        transform.SetParent(null);//발사후 회전 금지
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
        sprite.color = Color.white;// 반전효과시 타이밍이 엇갈려 색복구가 안되는 오류를 방지하기 위해 흰색으로 고정

    }
 
    }

