using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreateTable : MonoBehaviour {
    public static int size = 10;
    private int width = 0;
    private int height = 0;
    public Vector2[,] pos_array;

    private UnityEngine.Object bcg_obj;
    private UnityEngine.Object obstacle_obj;
    private GameObject table;
    private GameObject obstacle_parent;
    //判断是否有障碍物
    public static bool[,] if_obstacle;




    void Awake()
    {
        width = size;
        height = size;
        Camera.main.orthographicSize = Convert.ToSingle(width / 2);
        bcg_obj = Resources.Load("bcg", typeof(GameObject));
        obstacle_obj = Resources.Load("obstacle", typeof(GameObject));
        table = GameObject.Find("table");
        obstacle_parent = GameObject.Find("obstacle");
        pos_array = new Vector2[height, width];
        if_obstacle= new bool[height, width];

    }



    //初始化棋盘
   public void initializeTable()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                float x= Convert.ToSingle(-width / 2 + j) + 0.5f;
                float y = Convert.ToSingle(-height / 2 + i) + 0.5f;
                pos_array[i, j] = new Vector2(x,y);
                if_obstacle[i, j] = false;
                GameObject bcg_gameobj = Instantiate(bcg_obj) as GameObject;
                bcg_gameobj.transform.position = pos_array[i, j];
                bcg_gameobj.transform.parent = table.transform;
            }
        }
    }

    //销毁障碍物
    public void DestroyObstacle() {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if_obstacle[i, j] = false;
            }
        }
                for (int i = 0; i < obstacle_parent.transform.childCount; i++)
        {
            Destroy(obstacle_parent.transform.GetChild(i).gameObject);
        }
    }

    //点击屏幕产生一个障碍物
    public void click_create_obstacle()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 click_pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if ((click_pos.x > pos_array[i, j].x - 0.5f) && (click_pos.x < pos_array[i, j].x + 0.5f) && (click_pos.y > pos_array[i, j].y - 0.5f) && (click_pos.y < pos_array[i, j].y + 0.5f))
                    {
                        if (if_obstacle[i, j] == false)
                        {
                            if_obstacle[i, j] = true;
                            GameObject life_gameobj = Instantiate(obstacle_obj) as GameObject;
                            life_gameobj.transform.position = pos_array[i, j];
                            life_gameobj.transform.parent =obstacle_parent.transform;
                        }
                    }
                }
            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
