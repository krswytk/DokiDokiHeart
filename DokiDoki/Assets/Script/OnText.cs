using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnText : MonoBehaviour
{
    public Text Htext;
    public Text Otext;
    public Image HImage;
    private float Heart;
    private float O_2;
    SerialLight SL;

    ///hertanim
    [SerializeField] private Sprite[] Hearts;
    private float timer;
    ///

    // Start is called before the first frame update
    void Start()
    {
        SL = GetComponent<SerialLight>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Heart = SL.GetH();
        O_2 = SL.GetO();
        Htext.text = Heart.ToString();
        Otext.text = O_2.ToString();
        //Debug.Log(Heart + " , " + O_2);
        timer += Time.deltaTime;
        if (timer > 1) timer -= 1;
        HA();
    }

    private void HA()
    {
        int c;
        if (timer < 0.2) c = 1;
        else if (timer < 0.4) c = 2;
        else if (timer < 0.6) c = 3;
        else if (timer < 0.8) c = 4;
        else c = 5;


        switch (c)
        {
            case 1: HImage.sprite = Hearts[0]; break;
            case 2: HImage.sprite = Hearts[1]; break;
            case 3: HImage.sprite = Hearts[2]; break;
            case 4: HImage.sprite = Hearts[3]; break;
            case 5: HImage.sprite = Hearts[4]; break;
        }

    }

}
