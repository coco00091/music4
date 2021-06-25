using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    [SerializeField]
    //한박자 마다 증가 (lazerpattern_edge 번째 직후 레이저 타이밍은 쏘지 않게한다.)
    float lazerpattern;
    [SerializeField]
    float lazerpattern2; //레이저 몇번쐈는지
    [SerializeField]
    float lazerpattern_edge;//lazerpattern 에 활용할 코드(3을 넣을 경우 3번까지 쏘고 한박 무시)

    bool handler; // 왼손 오른손 변환
    bool IsAct; // 한박의 1/3 마다 true
    [SerializeField]
    float first_delay; //맨처음 얼마나 뜸 들일지
    public int count; // 한박의 1/3 마다 1증가
    [SerializeField]
    int highest_count; //한박의 1/4이 몇번 쳐지게 할 것인지
    [SerializeField]
    GameObject left, right; //왼손과 오른손 오브젝트
    Animator leftani,rightani;//왼손과 오른손의 애니메이터


    
    class Setting
    {
        
        public float fd { get; private set; } = 0;
        public float r { get; private set; } = 0;
        public Setting(float fd,float r)
        {
            this.fd = fd;
            this.r = r;
        }
    }

    Dictionary<int, Setting> patterndic = new Dictionary<int, Setting>();// 게임 난이도(속도) 에따라 처음 딜레이와 한박의 크기를 얼마나 할지 다르게 설정해줘야해서 만든 설정 세팅



    private void Start()
    {

        StartCoroutine(extremerotate());
        StartCoroutine(dongbang());
        StartCoroutine(dongbang2());

        StartCoroutine(bulleter());//총알 쏘기
        StartCoroutine(rythm());//1/4박 돌리기
        leftani = left.GetComponent<Animator>();
        rightani = right.GetComponent<Animator>();


        //dic관련
        patterndic.Add(4,new Setting(20, 0.47f));//4배속 설정
        patterndic.Add(2,new Setting(10, 0.47f));//2배속 설정
        patterndic.Add(1,new Setting(5, 0.47f));//일반 설정
        patterndic.Add(0, new Setting(0, 0f));//게임오버시 설정

        

        first_delay = patterndic[(int)Time.timeScale].fd;//맨처음 제거할 패턴 수
        ryth= patterndic[(int)Time.timeScale].r;//박자리듬 

    }


    void makelazerin()
    {
        List<bool> luck =new List<bool>() { false, false, false, true };//네개의 회전 블래스터중 하나의 블래스터만 사용
        if (lazerpattern2%12==0)//3번쨰로 쏠 때마다(마지막 박수때) 두개의 true를 지정
            luck[0] = true;


        bool[] lucky =new bool[4];
        for (int i = 0; i < 4; i++)
        {
            int rand = Random.Range(0, luck.Count);
            lucky[i] = luck[rand];
            luck.RemoveAt(rand);

        }
            // lucky 배열에 랜덤하게 하나에 true 를 담음


        
        var m= GameObject.FindGameObjectsWithTag("defaultblaster");//4개의 회전 블래스터
        if (m.Length == 0) return;//못찾으면 끄기
        makelazer[] mk = new makelazer[4];//그 블래스터 속의 makerlazer 클래스(함수 사용을 위한)
        for (int i = 0; i < 4; i++)
            mk[i] = m[i].GetComponent<makelazer>();

        for(int i=0;i<4;i++)
        {
            
            if(lucky[i])
             mk[i].lazermake();//true에 해당하는 블래스터 발동
        }
    }
    private void LateUpdate()
    {
        if(IsAct)
        { 
            
            IsAct = false;
            if((count%3==0))//1/3*3박자 (1박자) 마다
            {
                lazerpattern++;//한박자 마다

                if (!(lazerpattern%4==0))//4번쨰 박자는 쉬어감
                {
                    lazerpattern2++;//공격 발동시 
                    if (lazerpattern2 % 3 == 0)//3번째 공격 마다
                    {                    
                        int num = Random.Range(1, 4);
                                                   
                        if (default_blasts.activeSelf)
                            lmr(num == 1, num == 2, num == 3);// 위쪽 공격도 추가
                       
                    }

                    if (handler)//오른손 애니
                {
                    rightani.Play("rightattack");
                    makelazerin();
                    
                    
                    handler = !handler;
                }

                 else//왼손 애니
                {
                    leftani.Play("leftattack");
                    makelazerin();
                    
                    handler = !handler;

                }
                    StartCoroutine(colorchange());//반전 효과

                }
                
            }
           
        }
        
    }


    IEnumerator rythm()
    {
        yield return new WaitForSeconds(first_delay);
        for(int i=0;i<highest_count;i++)
        {
            IsAct = true;
            count++;
            yield return new WaitForSeconds(ryth);
            
        }


    }
}
