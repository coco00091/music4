using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//유도블래스터용 스크립트
public class Attacker : makelazer
{
    [SerializeField]
    GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("player");
        
    }

    // Update is called once per frame

  

    protected override IEnumerator make()
    {
        

        float angle = Mathf.Atan2(player.transform.position.y-transform.parent.position.y,player.transform.position.x-transform.parent.transform.position.x) * Mathf.Rad2Deg;
        
        transform.parent.transform.rotation = Quaternion.AngleAxis(angle+90, Vector3.forward);
        yield return new WaitForSeconds(1);
        StartCoroutine(base.make());
        yield return new WaitForSeconds(2f);
        var obj2 = GameObject.FindGameObjectsWithTag("chaser");//소환한 원그리기 블래스터를 모두 잡아옴
        foreach (var obj in obj2)
        {
            obj.tag = "blaster";//재활용을 위해 태그 재정비
            obj.transform.SetParent(GameObject.FindGameObjectWithTag("pool").transform);//풀링
            obj.SetActive(false);
        }

    }
}
