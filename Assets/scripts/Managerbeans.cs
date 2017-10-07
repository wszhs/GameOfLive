using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


enum TimeProcess {
       READY,
       STARTING,
       END

}
enum SearchWay
{
    DFS=0,
    BFS=1
}

public class Managerbeans : MonoBehaviour
{
    private GameObject[] animal;

    private TimeProcess timeprocess;
    private CreateTable table;
    private GameObject target;

    Queue<PosPoint> qu = new Queue<PosPoint>();


    private void Awake()
    {
        animal = new GameObject[2];
        animal[0]= GameObject.Find("DFS_animal");
        animal[1] = GameObject.Find("BFS_animal");
        target = GameObject.Find("target");
        table = GetComponent<CreateTable>();
    }
    // Use this for initialization
    void Start()
    {
        timeprocess = TimeProcess.READY;
        table.initializeTable();
        animal[0].transform.localPosition = table.pos_array[0, 0];
        animal[1].transform.localPosition = table.pos_array[1, 0];
        target.transform.position = table.pos_array[CreateTable.size-1, CreateTable.size-1];
      

    }

    // Update is called once per frame
    void Update()
    {
        
        if (timeprocess == TimeProcess.READY)
        {
            table.click_create_obstacle();
        }
        if (Input.GetKeyDown(KeyCode.Space)&& (timeprocess!=TimeProcess.STARTING))
        {
            timeprocess = TimeProcess.STARTING;
            SearchAlg salg = new SearchAlg();
            salg.MyEvent += animalPos;
             salg.DFS_Search(new PosPoint(0, 0), new PosPoint(9, 9), new PosPoint(0, 0));

            InvokeRepeating("animalMove", 1.0f, 0.2f);

        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelInvoke();
            qu.Clear();
            animal[0].transform.localPosition = table.pos_array[0,0];

            timeprocess = TimeProcess.END;
            GetComponent<CreateTable>().DestroyObstacle();
            timeprocess = TimeProcess.READY;
        }

    }


    public  void animalPos(PosPoint p)
    {
        qu.Enqueue(p);
    }

    public void animalMove() {
        if (qu.Count != 0)
        {
            PosPoint p = qu.Dequeue();
            animal[0].transform.localPosition = table.pos_array[p.x, p.y];
        }
    }

}
