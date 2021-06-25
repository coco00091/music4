using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makelazer : MonoBehaviour
{
    [SerializeField]
    protected GameObject lazer;
    [SerializeField]
    protected Animator ani;
    protected GameObject final;
    protected Quaternion quaternion;
    [SerializeField]
    protected GameObject[] teeth = new GameObject[2];
    
    
    // Start is called before the first frame update
    void Start()
    {       
        ani = GetComponent<Animator>();        
    }

    public void lazermake()
    {
        StartCoroutine(make()); 
    }
   
    protected virtual IEnumerator make()
    {         
        yield return new WaitForSeconds(0.8f);//����� ȸ��
                   
        ani.Play("blastermove");
           
        yield return new WaitForSeconds(0.8f);//ȸ���� ���� ������
        teeth[0].GetComponent<Animator>().Play("teethmove");
        teeth[1].GetComponent<Animator>().Play("teethmove2");
        GameManager.gm.Make("lazer", transform, ref final,ref lazer);
    }
}
