using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
public class SearchAlg : MonoBehaviour
{
    public delegate void MyDelegate(PosPoint p);
    public event MyDelegate MyEvent;
    private PosPoint[] move_point = { new PosPoint(0,1), new PosPoint(1, 0), new PosPoint(0, -1), new PosPoint(-1, 0) };
    bool finished = false;
    bool[,] is_arrive;
    bool[,] is_obstacle;
    int size;
    public SearchAlg() {
        size = CreateTable.size;
        is_arrive = new bool[CreateTable.size, CreateTable.size];
        is_obstacle = CreateTable.if_obstacle;
        //is_obstacle= new bool[CreateTable.size, CreateTable.size];
    }
    private void Start()
    {
       
      // DFS_Search(new PosPoint(0,0),new PosPoint(1,1),new PosPoint(0,0));
    }
    //深度优先算法
    public void DFS_Search(PosPoint start_point,PosPoint target_point, PosPoint current_point)
    {
        if (current_point == start_point)
        {

            is_arrive[current_point.x, current_point.y] = true;
            Debug.Log("(" + current_point.x + "," + current_point.y + ")");
        }

        if (current_point == target_point)
        {
            Debug.Log("到达终点");
            finished = true;
            return;

        }
        else 
        {
            foreach (PosPoint i in move_point)
            {
                if (finished == false)
                {
                    PosPoint next_point = current_point + i;
                    if ((current_point + i).x < 0 || (current_point + i).x >= size || (current_point + i).y < 0 || (current_point + i).y >= size)
                        continue;
                    if (!is_obstacle[next_point.x, next_point.y] && !is_arrive[next_point.x, next_point.y])
                    {
                        current_point = next_point;
                        is_arrive[current_point.x, current_point.y] = true;
                        Debug.Log("(" + current_point.x + "," + current_point.y + ")");
                        MyEvent(current_point);
                        DFS_Search(start_point, target_point, current_point);
                        is_arrive[current_point.x, current_point.y] = false;

                    }

                }
            }
  
        }

    }

}
