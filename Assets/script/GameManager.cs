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

   


    public static GameManager gm; //�ܺο��� ��,�Լ��� ���� ����ϵ��� �̱��� ����
    
    private void Awake()
    {

        timescale_check = Time.timeScale;
        gm = this;//�̱��� �Ҵ�
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
        if (!(pool.transform.childCount == 0))//�ڽ��� ������
        {
          

            if (CheckisGarbage(tag_name, ref final))//Ư�� �±װ� �ִ� ������Ʈ �ϳ��� �Ծ��ش�
                SetChild(transform, in final);//������Ʈ�� ������ �������ش�
            else
            {
                final = Instantiate(prefab, transform);// ���� ���ϸ� ����
                //final.transform.SetParent(null);
            }


        }
        else
        {
            final =Instantiate(prefab,transform);//�ڽ��� ������ ����
            //final.transform.SetParent(null);

        }
            

    }
    bool CheckisGarbage(string tag_name,ref GameObject final)
    {

        int count = pool.transform.childCount;
        GameObject obj;
        for (int i = 0; i < count; i++)//Ǯ�Ŵ��� �ڽĵ��� �ϳ��ϳ� ������
        {
            obj = pool.transform.GetChild(i).gameObject;
            if (obj.tag == tag_name)//�Ѿ��� �ִ��� ����
            {

                
                final = obj;//�Ѿ��� �ִٸ� �Դ´�
                return true;
            }
            

        }
        return false;//������ ��ã���� �����Ѵ�


    }
    void SetChild(Transform transform,in GameObject final,bool notpar=false)
    {
        final.transform.SetParent(transform);
        final.transform.position = transform.position;
        final.SetActive(true);
        if(notpar)
            final.transform.SetParent(null);

    }

    

    //���Ͱ� ���� Ǯ���ڵ�

    public void MakeVec(string tag_name, Vector3 pos, ref GameObject final, ref GameObject prefab)
    {
        if (!(pool.transform.childCount == 0))//�ڽ��� ������
        {


            if (CheckisGarbageVec(tag_name, ref final))//Ư�� �±װ� �ִ� ������Ʈ �ϳ��� �Ծ��ش�
                SetChildVec(pos, in final);//������Ʈ�� ������ �������ش�
            else
            {
                final = Instantiate(prefab,pos,Quaternion.identity);// ���� ���ϸ� ����
                //final.transform.SetParent(null);
            }


        }
        else
        {
            final = Instantiate(prefab, pos,Quaternion.identity);//�ڽ��� ������ ����
            //final.transform.SetParent(null);

        }


    }
    bool CheckisGarbageVec(string tag_name, ref GameObject final)
    {

        int count = pool.transform.childCount;
        GameObject obj;
        for (int i = 0; i < count; i++)//Ǯ�Ŵ��� �ڽĵ��� �ϳ��ϳ� ������
        {
            obj = pool.transform.GetChild(i).gameObject;
            if (obj.tag == tag_name)//�Ѿ��� �ִ��� ����
            {


                final = obj;//�Ѿ��� �ִٸ� �Դ´�
                return true;
            }


        }
        return false;//������ ��ã���� �����Ѵ�


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
        //���� ����

        var obj = GameObject.FindObjectsOfType<SpriteRenderer>();//�����ϴ� ��� ��� Į�� ������

        foreach (SpriteRenderer sprite in obj)
        {

            if (sprite.tag == "bullet"||sprite.tag=="defaultblaster"||sprite.tag=="blast"||sprite.tag=="teeth") continue;//�����λ� �ٲ��� �ʾƾ��� ���� �ȹٲ�
            if (sprite.color == Color.black)
                sprite.color = Color.white;
            else if (sprite.color == Color.white)
                sprite.color = Color.black;
            //��� Į���� ��� ���� ������Ŵ
        }
    }
    IEnumerator colorchange()
    {
        //Volume.SetActive(true);// �ϱ׷���ȿ��
        // ��½ �ϱ� ���� �Լ��� �ڷ�ƾ���� ������
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
            life_hp += 0.3f;//�� ���� ������ �� 30�� ȸ��
            if (life_hp > 1)
                life_hp = 1;//�ʰ� �� ����
        }

        if (life_hp < 0)//���ӿ���
        {
            timetrigger = false;
            Panel.SetActive(true);

        }
            

        
        life.fillAmount =life_hp;
        //�� ��
        
      
        leftarm.fillAmount = Mathf.Abs(leftarm_hp);
        if(leftarm_hp<0)
            leftarm.fillAmount = Mathf.Abs(leftarm_hp)/3;

        rightarm.fillAmount = Mathf.Abs(rightarm_hp);
        if(rightarm_hp<0)
            rightarm.fillAmount = Mathf.Abs(rightarm_hp)/3;
        // �� ��

        head.fillAmount = head_hp;
        // �� ��ü ��


        //�� ���̳ʽ��� �� �ٲ�
        if (leftarm_hp < 0)
            leftarm.color = Color.magenta;
        else
            leftarm.color = Color.red;

        if (rightarm_hp < 0)
            rightarm.color = Color.magenta;
        else
            rightarm.color = Color.red;

        // �Ӹ� ��ȣ�� �� ��ȯ
        if (!(leftarm_hp < 0 && rightarm_hp < 0))
            head.color = Color.blue;
        else
            head.color = Color.red;
            
        


            
        
    }

    IEnumerator bulleter()
    {
        while(true)
        {
            Make("bullet", player.transform, ref final, ref bulletprefab);//�Ѿ� �߻�
            yield return new WaitForSeconds(0.1f);

        }
        
    }
}
