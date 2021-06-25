using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    Text highestscore_text,score_text;
    
    float score,highestscore;

    delegate void del();
    del scoreupdater;

    
    void OnEnable()
    {
        
        if(Time.timeScale==2)
            scoreupdater = ScoreUpdate;

        if (Time.timeScale == 4)
            scoreupdater = ScoreUpdate_extreme;

        scoreupdater += () => { Time.timeScale *= 1 / 100f; };
        scoreupdater();

        
    }
    // Start is called before the first frame update
    void ScoreUpdate()
    {
        score = GameManager.gm.localtimer;
        highestscore = PlayerPrefs.GetFloat("highestscore");

        if (highestscore < score)
            PlayerPrefs.SetFloat("highestscore",score);

        score_text.text = string.Format($"{score:0.00} sec");
        highestscore_text.text = string.Format($"{highestscore:0.00} sec");
    }

    void ScoreUpdate_extreme()
    {
        score = GameManager.gm.localtimer;
        highestscore = PlayerPrefs.GetFloat("extreme highestscore");

        if (highestscore < score)
            PlayerPrefs.SetFloat("extreme highestscore", score);

        score_text.text = string.Format($"{score:0.00} sec");
        highestscore_text.text = string.Format($"{highestscore:0.00} sec");

        //현재 최고기록이 더 높더라도 이전 
    }
}
