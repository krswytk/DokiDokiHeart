using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Starting : MonoBehaviour
{
    SerialLight SL;
    /// 指を置いてください
    [SerializeField] private GameObject IMOB;
    [SerializeField] private GameObject HandOB;
    private Text HandText;

    private float H;
    private float O;

    private bool on;
    private bool stand_by;
    private bool Allstand_by;


    private float TotalTimer;
    private float Totalnum;
    private int num;
    private float ave;
    [SerializeField] private float Limiter = 20;//最初計測しない時間時間
    [SerializeField] private float LImit = 10;//平均心拍計算までの時間

    // Start is called before the first frame update
    void Start()
    {
        SL = GetComponent<SerialLight>();
        HandText = HandOB.GetComponent<Text>();
        HandText.text = "♡お指を置いてね♡";
        on = false;
        TotalTimer = 0;
        Totalnum = 0;
        stand_by = false;
        Allstand_by = false;
    }

    // Update is called once per frame
    void Update()
    {
        H = SL.GetH();
        O = SL.GetO();

        if (stand_by == false)
        {
            if (on == false) {
                if (H != 0) {
                    on = true;
                    HandText.text = "♡そのまま維持♡";
                    Debug.Log("計測開始");
                }
            }//一度値を取ったらスイッチをtrueに

            if (on == true)
            {
                TotalTimer += Time.deltaTime;
                if (TotalTimer > Limiter)
                {
                    Debug.Log("計測中");
                    Totalnum += H;
                    num++;
                }

                if (TotalTimer > LImit + Limiter && O != 0)
                { 
                    ave = Totalnum / num;
                    Debug.Log("AVE : " + ave);
                    stand_by = true;
                    HandText.text = "♡OKそのまま維持♡";
                    TotalTimer = 0;
                }
            }
        }
        else
        {
            TotalTimer += Time.deltaTime;
            if (TotalTimer > 3)
            {
                OFFIMOB();
                Allstand_by = true;
            }
        }

    }
    private void OFFIMOB()
    {
        IMOB.SetActive(false);
    }
    private void ONIMOB()
    {
        IMOB.SetActive(true);
    }
    public float GetAve()
    {
        return ave;
    }
    public bool GetST()
    {
        return Allstand_by;
    }
}
