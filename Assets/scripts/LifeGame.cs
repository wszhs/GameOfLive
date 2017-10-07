using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LifeGame : MonoBehaviour {

    private bool starting = false;
    public int size = 10;
    private  int width = 0;
    private  int height = 0;
    private Vector2[,] pos_array;

    //初始化生命的位置
    private string[] life_pos_number = {"4#2","5#3","5#4","4#4","3#4"};

    //判断是否有生命
    bool[,] if_life;


    private UnityEngine.Object bcg_obj;
    private UnityEngine.Object life_obj;
     void Awake()
    {
        width = size;
        height = size;
        Camera.main.orthographicSize = Convert.ToSingle(width / 2);
        GameObject.Find("Camera").GetComponent<Camera>().orthographicSize = Convert.ToSingle(width / 2);
        bcg_obj = Resources.Load("bcg", typeof(GameObject));
        life_obj = Resources.Load("pic", typeof(GameObject));

        pos_array = new Vector2[height,width];
        if_life = new bool[height, width];

        initializeTable();
       
    }

    //初始化棋盘
    void initializeTable() {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                float x = Convert.ToSingle(-width / 2 + j) + 0.5f;
                float y = Convert.ToSingle(-height / 2 + i) + 0.5f;
                pos_array[i, j] = new Vector2(x, y);
                if_life[i, j] = false;

                GameObject bcg_gameobj = Instantiate(bcg_obj) as GameObject;
                bcg_gameobj.transform.position = pos_array[i, j];
                bcg_gameobj.transform.parent = GameObject.Find("table").transform;
            }
        }
    }


    //初始化生命棋子
    void initializeLife() {

        foreach (string i in life_pos_number) {
            //Debug.Log(i);
            int x =Convert.ToInt32(i.Split('#')[0]);
            int y= Convert.ToInt32(i.Split('#')[1]);
            if_life[x, y] = true;
            GameObject life_gameobj = Instantiate(life_obj) as GameObject;
            life_gameobj.transform.position = pos_array[x, y];
            life_gameobj.transform.parent = GameObject.Find("life").transform;
        }
       
    }

    //生命演化的规则
    void life_rule() {

        bool[,] if_middle_life = new bool[height,width];
        for (int i = 0; i < height; i++) {
            for (int j = 0; j < width; j++) {
                int life_flag = 0;                //周围生命的个数
                for (int n = -1; n < 2; n++) {
                    for (int m = -1; m < 2; m++) {
                        if (!(n==0&&m==0)&& (i + n >= 0) && (i + n < height) && (j + m >= 0) && (j + m < width)) {
                            if (if_life[i + n,j+m] == true) {
                                life_flag++;
                            }
                        }
                    }
                }
                if (life_flag == 2) {
                    if_middle_life[i, j] = if_life[i, j];
                }
                else
                    if (life_flag == 3)
                {
                    if_middle_life[i, j] = true;
                }
                else {
                    if_middle_life[i, j] = false;
                }
            }
        }
        if_life = if_middle_life;
        GameObject go = GameObject.Find("life");
        for (int i = 0; i < go.transform.childCount; i++)
        {
            Destroy(go.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < height; i++) {
            for (int j = 0; j < width; j++) {
                if (if_life[i, j] == true) {
                    GameObject life_gameobj = Instantiate(life_obj) as GameObject;
                    life_gameobj.transform.position = pos_array[i, j];
                    life_gameobj.transform.parent = GameObject.Find("life").transform;
                }
            }
        }

    }

    //点击屏幕产生一个生命
    void click_create_life() {
        if (Input.GetMouseButton(0))
        {
            Vector3 click_pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    if ((click_pos.x > pos_array[i, j].x - 0.5f) && (click_pos.x < pos_array[i, j].x + 0.5f) && (click_pos.y > pos_array[i, j].y - 0.5f) && (click_pos.y < pos_array[i, j].y + 0.5f)) {
                        if (if_life[i, j] == false) {
                            if_life[i, j] = true;
                            GameObject life_gameobj = Instantiate(life_obj) as GameObject;
                            life_gameobj.transform.position = pos_array[i, j];
                            life_gameobj.transform.parent = GameObject.Find("life").transform;
                        }
                    }
                }
            }
        }
    }
    // Use this for initialization

    void Start () {
        //initializeLife();
       // InvokeRepeating("life_rule", 1.0f,1.0f);
        
    }
	
	// Update is called once per frame
	void Update () {
        if (starting == false)
        {
            click_create_life();
        }
        if (Input.GetKeyDown(KeyCode.Space)&&starting==false) {
            starting = true;
            GameObject.Find("Camera").SendMessage("startrecord");
            InvokeRepeating("life_rule", 1.0f, 1.0f);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && starting == true)
        {
            starting = false;
            GameObject.Find("Camera").SendMessage("stoprecord");
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    if_life[i, j] = false;
                }
            }
            CancelInvoke();
            GameObject go = GameObject.Find("life");
            for (int i = 0; i <go.transform.childCount; i++)
            {
                Destroy(go.transform.GetChild(i).gameObject);
            }

        }
    }

    private void OnGUI()
    {
        GUIStyle fontStyle = new GUIStyle();
        fontStyle.normal.background = null;    //设置背景填充  
        fontStyle.normal.textColor = new Color(1, 0, 0);   //设置字体颜色  
        fontStyle.fontSize = 50;       //字体大小  
        if (starting == false) {
            GUI.Label(new Rect(100, 100, 600, 100), "请按鼠标左键选择初始生命",fontStyle);
        }
        else {
            GUI.Label(new Rect(100, 100, 600, 100), "生命系统正在运行。。",fontStyle);
        }
    }
}
