using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnHeart : MonoBehaviour
{
    public Text Htext;
    public Text Otext;
    public Image HImage;
    private float Heart;
    private float O_2;
    SerialLight SL;

    ///hertanim
    [SerializeField] private Sprite[] Hearts;
    [SerializeField] private float time = 2;//1周にかかる秒数
    private float timer;
    private float Atimer;
    private int num;
    /// <summary>
    /// 
    /// 
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        SL = GetComponent<SerialLight>();
        timer = 0;
        Atimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Heart = SL.GetH();
        O_2 = SL.GetO();

        Ontext();//テキスト表記を行う関数
        timer += Time.deltaTime;


        HA(time);//ドキドキを行う関数
    }

    private void HA(float time)
    {
        float s = time / Hearts.Length;//1枚の描写にかけていい時間

        if (timer > Atimer)
        {
            Atimer = (timer + s);
            num++;
            if (num >= Hearts.Length) num = 0;
            //Debug.Log("Atimer" + Atimer + "  " + "timer" + timer);
            HImage.sprite = Hearts[num]; 
        }

    }
    private void Ontext()
    {
        Htext.text = "Heart:" + Heart.ToString();
        Otext.text = "  O_2:" + O_2.ToString();
        //Debug.Log(Heart + " , " + O_2);
    }

    public void SetH(float f)
    {
        time = f;
    }

}

