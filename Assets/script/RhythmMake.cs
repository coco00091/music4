using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    [SerializeField]
    //�ѹ��� ���� ���� (lazerpattern_edge ��° ���� ������ Ÿ�̹��� ���� �ʰ��Ѵ�.)
    float lazerpattern;
    [SerializeField]
    float lazerpattern2; //������ ���������
    [SerializeField]
    float lazerpattern_edge;//lazerpattern �� Ȱ���� �ڵ�(3�� ���� ��� 3������ ��� �ѹ� ����)

    bool handler; // �޼� ������ ��ȯ
    bool IsAct; // �ѹ��� 1/3 ���� true
    [SerializeField]
    float first_delay; //��ó�� �󸶳� �� ������
    public int count; // �ѹ��� 1/3 ���� 1����
    [SerializeField]
    int highest_count; //�ѹ��� 1/4�� ��� ������ �� ������
    [SerializeField]
    GameObject left, right; //�޼հ� ������ ������Ʈ
    Animator leftani,rightani;//�޼հ� �������� �ִϸ�����


    
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

    Dictionary<int, Setting> patterndic = new Dictionary<int, Setting>();// ���� ���̵�(�ӵ�) ������ ó�� �����̿� �ѹ��� ũ�⸦ �󸶳� ���� �ٸ��� ����������ؼ� ���� ���� ����



    private void Start()
    {

        StartCoroutine(extremerotate());
        StartCoroutine(dongbang());
        StartCoroutine(dongbang2());

        StartCoroutine(bulleter());//�Ѿ� ���
        StartCoroutine(rythm());//1/4�� ������
        leftani = left.GetComponent<Animator>();
        rightani = right.GetComponent<Animator>();


        //dic����
        patterndic.Add(4,new Setting(20, 0.47f));//4��� ����
        patterndic.Add(2,new Setting(10, 0.47f));//2��� ����
        patterndic.Add(1,new Setting(5, 0.47f));//�Ϲ� ����
        patterndic.Add(0, new Setting(0, 0f));//���ӿ����� ����

        

        first_delay = patterndic[(int)Time.timeScale].fd;//��ó�� ������ ���� ��
        ryth= patterndic[(int)Time.timeScale].r;//���ڸ��� 

    }


    void makelazerin()
    {
        List<bool> luck =new List<bool>() { false, false, false, true };//�װ��� ȸ�� �������� �ϳ��� �����͸� ���
        if (lazerpattern2%12==0)//3������ �� ������(������ �ڼ���) �ΰ��� true�� ����
            luck[0] = true;


        bool[] lucky =new bool[4];
        for (int i = 0; i < 4; i++)
        {
            int rand = Random.Range(0, luck.Count);
            lucky[i] = luck[rand];
            luck.RemoveAt(rand);

        }
            // lucky �迭�� �����ϰ� �ϳ��� true �� ����


        
        var m= GameObject.FindGameObjectsWithTag("defaultblaster");//4���� ȸ�� ������
        if (m.Length == 0) return;//��ã���� ����
        makelazer[] mk = new makelazer[4];//�� ������ ���� makerlazer Ŭ����(�Լ� ����� ����)
        for (int i = 0; i < 4; i++)
            mk[i] = m[i].GetComponent<makelazer>();

        for(int i=0;i<4;i++)
        {
            
            if(lucky[i])
             mk[i].lazermake();//true�� �ش��ϴ� ������ �ߵ�
        }
    }
    private void LateUpdate()
    {
        if(IsAct)
        { 
            
            IsAct = false;
            if((count%3==0))//1/3*3���� (1����) ����
            {
                lazerpattern++;//�ѹ��� ����

                if (!(lazerpattern%4==0))//4���� ���ڴ� ���
                {
                    lazerpattern2++;//���� �ߵ��� 
                    if (lazerpattern2 % 3 == 0)//3��° ���� ����
                    {                    
                        int num = Random.Range(1, 4);
                                                   
                        if (default_blasts.activeSelf)
                            lmr(num == 1, num == 2, num == 3);// ���� ���ݵ� �߰�
                       
                    }

                    if (handler)//������ �ִ�
                {
                    rightani.Play("rightattack");
                    makelazerin();
                    
                    
                    handler = !handler;
                }

                 else//�޼� �ִ�
                {
                    leftani.Play("leftattack");
                    makelazerin();
                    
                    handler = !handler;

                }
                    StartCoroutine(colorchange());//���� ȿ��

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
