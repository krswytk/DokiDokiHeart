using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Question : MonoBehaviour
{
    [SerializeField] private Text QuestionText;
    [SerializeField] private GameObject QS;
    private string[] Q =
    {
        "では始めます。すべていいえで答えてください",
        "問題①:あなたは今日朝ごはんを食べましたか？",
        "問題②:あなたは今日よく眠れましたか？",
        "問題③:あなたは昨日ゲームをしましたか？",
        "問題④:あなたはゲークリをやっていますか？",
        "問題⑤:あなたは徹夜で課題を作りましたか？"
    };
    private string syo = "あなたは正直な人ですね。";
    private string uso = "あなた嘘をついてますね？";


    Starting S;
    private bool stsw;
    private int num;
    [SerializeField] private float TimeToCalculate = 10;//1問に使う計測時間
    [SerializeField] private float DualTimeToCalculate = 5;//回答を表示する時間
    private float Timer;//時間を格納しておく
    private float Ave;
    OnHeart OH;
    SerialLight SL;
    private int HI,LOW;
    // Start is called before the first frame update
    void Start()
    {
        S = GetComponent<Starting>();
        SL = GetComponent<SerialLight>();
        stsw = false;
        QS.SetActive(false);//表示を伏せておく
        num = 0;
        OH = GetComponent<OnHeart>();
        Ave = S.GetAve();
        HI = 0;LOW = 0;//心拍数の高低記録
        num = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (set == false)
        {
            if (S.GetST() == true)
            {
                stsw = true;
                QS.SetActive(true);//表示を行う
                QuestionText.text = Q[num];
            }//初期の準備が終わったら 
        }

        if (stsw == true)//実際に問題を出し始める
        {
            Timer += Time.deltaTime;

            if(Timer > TimeToCalculate)
            {
                Debug.Log("計測終わり");
                if (HI < LOW)
                {
                    Debug.Log("正直者");
                    QuestionText.text = syo;
                }
                else if(HI > LOW)
                {
                    Debug.Log("嘘つき");
                    QuestionText.text = uso;
                }
                else
                {
                    Debug.Log("初回");
                }

                if (Timer > DualTimeToCalculate + TimeToCalculate)
                {
                    Debug.Log("次へ");
                    Timer = 0;
                    num++;
                    if(num == Q.Length)
                    {
                        QuestionText.text = "終わりです。あってましたか？";
                        RiStart();
                        return;
                    }
                    QuestionText.text = Q[num];
                    HI = 0;
                    LOW = 0;
                }
            }
            else if (num != 0)
            {
                if (Ave < SL.GetH())//平均より心拍が高い
                {
                    HI++;
                    OH.SetH(0.5f);
                }
                if (Ave > SL.GetH())//平均より心拍が低い
                {
                    LOW++;
                    OH.SetH(5);
                }
            }

        }
        else//欠点　問題を出し始める前でも反応してしまう。
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene(0);
            }

        }
    }

    [SerializeField] GameObject riset;
    bool set = false;

    private void RiStart()
    {
        stsw = false;
        riset.SetActive(true);
        set = true;
    }
}
