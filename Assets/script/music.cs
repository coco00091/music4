using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class music : MonoBehaviour
{
    public InputField input;


    [SerializeField]
    List<AudioClip> clips;

    AudioSource audiosource;

    //public TMPro.TMP_InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Z))
        {
            audiosource.clip = clips[0];
            audiosource.Play();

        }
        if (Input.GetKey(KeyCode.X))
        {
           
            audiosource.clip = clips[1];
            audiosource.Play();

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            audiosource.clip = clips[2];
            audiosource.Play();

        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Event now = Event.KeyboardEvent("C");
            
            input.ProcessEvent(now);
           
             
        }



    } 
    IEnumerator play()
    {
        yield return new WaitForSeconds(3f);

    }
}
