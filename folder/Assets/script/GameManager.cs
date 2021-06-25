using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public partial class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject Volume;
    float timescale_check;
    [SerializeField]
    GameObject Panel;
    GameObject final;
    [SerializeField]
    Camera cam;

    [SerializeField]
    Text text;
    [SerializeField]
    public float localtimer{get; private set;}
    bool timetrigger=true;
    
    public GameObject pool;

    [SerializeField]
    GameObject bulletprefab;

    [SerializeField]
    Image head,leftarm,rightarm,life;
    [SerializeField]
    public float head_hp= 1, rightarm_hp=1, leftarm_hp=1, life_hp= 1;

    [SerializeField]
    float headheal, armheal;

    [SerializeField]
    GameObject player;

    [SerializeField]
    float ryth;

   


    public static GameManager gm; //외부에서 변,함수를 쉽게 사용하도록 싱글톤 생성
    
    private void Awake()
    {

        timescale_check = Time.timeScale;
        gm = this;//싱글톤 할당
    }
    // Start is called before the first frame update
   



   
    private void FixedUpdate()
    {
        
        if(head_hp<1)
            head_hp += headheal;
        if (leftarm_hp < 1)
            leftarm_hp += armheal;
        if (rightarm_hp < 1)
            rightarm_hp += armheal;

    }
    // Update is called once per frame
    void Update()
    {
                   
        if (Input.GetKeyDown(KeyCode.Q))
            StartCoroutine(colorchange());


        if (timetrigger)
            localtimer += Time.deltaTime/Time.timeScale;
        text.text = string.Format("{0:0.00}",localtimer);

        //

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 4;
            SceneManager.LoadScene("ingame");
        }

        
        
        if (Input.GetKeyDown(KeyCode.Return))
            Time.timeScale *=2;

       
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("ingame");
        }
       
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Time.timeScale = 4;
            SceneManager.LoadScene("ingame");
        }





    }
    public void home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("startscene");
    }
    public void restart()
    {
        Time.timeScale = timescale_check;
        SceneManager.LoadScene("ingame");        
    }

    public void Make(string tag_name, Transform transform,ref GameObject final,ref GameObject prefab,bool notpar=false)
    {
        if (!(pool.transform.childCount == 0))//자식이 있으면
        {
          

            if (CheckisGarbage(tag_name, ref final))//특정 태그가 있는 오브젝트 하나를 먹어준다
                SetChild(transform, in final);//오브젝트를 얻으면 세팅해준다
            else
            {
                final = Instantiate(prefab, transform);// 얻지 못하면 생성
                //final.transform.SetParent(null);
            }


        }
        else
        {
            final =Instantiate(prefab,transform);//자식이 없으면 생성
            //final.transform.SetParent(null);

        }
            

    }
    bool CheckisGarbage(string tag_name,ref GameObject final)
    {

        int count = pool.transform.childCount;
        GameObject obj;
        for (int i = 0; i < count; i++)//풀매니저 자식들을 하나하나 봐가며
        {
            obj = pool.transform.GetChild(i).gameObject;
            if (obj.tag == tag_name)//총알이 있는지 본다
            {

                
                final = obj;//총알이 있다면 먹는다
                return true;
            }
            

        }
        return false;//끝까지 못찾으면 포기한다


    }
    void SetChild(Transform transform,in GameObject final,bool notpar=false)
    {
        final.transform.SetParent(transform);
        final.transform.position = transform.position;
        final.SetActive(true);
        if(notpar)
            final.transform.SetParent(null);

    }

    

    //벡터값 전용 풀링코드

    public void MakeVec(string tag_name, Vector3 pos, ref GameObject final, ref GameObject prefab)
    {
        if (!(pool.transform.childCount == 0))//자식이 있으면
        {


            if (CheckisGarbageVec(tag_name, ref final))//특정 태그가 있는 오브젝트 하나를 먹어준다
                SetChildVec(pos, in final);//오브젝트를 얻으면 세팅해준다
            else
            {
                final = Instantiate(prefab,pos,Quaternion.identity);// 얻지 못하면 생성
                //final.transform.SetParent(null);
            }


        }
        else
        {
            final = Instantiate(prefab, pos,Quaternion.identity);//자식이 없으면 생성
            //final.transform.SetParent(null);

        }


    }
    bool CheckisGarbageVec(string tag_name, ref GameObject final)
    {

        int count = pool.transform.childCount;
        GameObject obj;
        for (int i = 0; i < count; i++)//풀매니저 자식들을 하나하나 봐가며
        {
            obj = pool.transform.GetChild(i).gameObject;
            if (obj.tag == tag_name)//총알이 있는지 본다
            {


                final = obj;//총알이 있다면 먹는다
                return true;
            }


        }
        return false;//끝까지 못찾으면 포기한다


    }
    void SetChildVec(Vector3 pos, in GameObject final)
    {
        final.transform.SetParent(null);
        final.transform.position = pos;
        final.SetActive(true);
    }



    //



    public void colorchanger()
    {
        if (cam.backgroundColor == Color.black)
            cam.backgroundColor = Color.white;
        else
            cam.backgroundColor = Color.black;
        //배경색 반전

        var obj = GameObject.FindObjectsOfType<SpriteRenderer>();//존재하는 모든 흑백 칼라를 가져옴

        foreach (SpriteRenderer sprite in obj)
        {

            if (sprite.tag == "bullet"||sprite.tag=="defaultblaster"||sprite.tag=="blast"||sprite.tag=="teeth") continue;//디자인상 바뀌지 않아야할 색은 안바꿈
            if (sprite.color == Color.black)
                sprite.color = Color.white;
            else if (sprite.color == Color.white)
                sprite.color = Color.black;
            //흑백 칼라의 모든 것을 반전시킴
        }
    }
    IEnumerator colorchange()
    {
        //Volume.SetActive(true);// 일그러짐효과
        // 번쩍 하기 위해 함수를 코루틴으로 가져옴
        for (int i = 0; i < 2; i++)
        {
            colorchanger();          
            yield return new WaitForSeconds(0.1f);
            colorchanger();
            yield return new WaitForSeconds(0.1f);


        }
        Volume.SetActive(false);

    }

    public void UpdateUI()
    {
        if(head_hp<0)
        {
            head_hp = 1;
            life_hp += 0.3f;//적 뒤질 때마다 피 30퍼 회복
            if (life_hp > 1)
                life_hp = 1;//초과 힐 방지
        }

        if (life_hp < 0)//게임오버
        {
            timetrigger = false;
            Panel.SetActive(true);

        }
            

        
        life.fillAmount =life_hp;
        //내 피
        
      
        leftarm.fillAmount = Mathf.Abs(leftarm_hp);
        if(leftarm_hp<0)
            leftarm.fillAmount = Mathf.Abs(leftarm_hp)/3;

        rightarm.fillAmount = Mathf.Abs(rightarm_hp);
        if(rightarm_hp<0)
            rightarm.fillAmount = Mathf.Abs(rightarm_hp)/3;
        // 팔 피

        head.fillAmount = head_hp;
        // 적 본체 피


        //팔 마이너스시 색 바뀜
        if (leftarm_hp < 0)
            leftarm.color = Color.magenta;
        else
            leftarm.color = Color.red;

        if (rightarm_hp < 0)
            rightarm.color = Color.magenta;
        else
            rightarm.color = Color.red;

        // 머리 보호막 색 전환
        if (!(leftarm_hp < 0 && rightarm_hp < 0))
            head.color = Color.blue;
        else
            head.color = Color.red;
            
        


            
        
    }

    IEnumerator bulleter()
    {
        while(true)
        {
            Make("bullet", player.transform, ref final, ref bulletprefab);//총알 발사
            yield return new WaitForSeconds(0.1f);

        }
        
    }
}
