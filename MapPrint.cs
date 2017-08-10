using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;


public class MapPrint : MonoBehaviour
{

    //public wall wall;
    public GameObject box;
    public GameObject star;
    public GameObject target;
    
    public  string[,] map;
    public  int[,] sPos;
    public int FloorMask;
    public int step=1;
    void Start()
    {
        map = new string[100, 100];
        sPos = new int[100, 100];//用来记录步数
        ReadMapFile();
        FloorMask = LayerMask.GetMask("floor");
        Draw();
        SetSmap(step);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Setmap();
        }
        //StartCoroutine(SetSmap(step));
        SetSmap(step);
        //Debug.Log("bushu" + sPos[20, 20]);
        step++;
        for (int i = 1; i < 100; i++)
        {
            for (int j = 1; j < 100; j++)
            {
                if (sPos[i, j] == step)
                {
                    Debug.Log("X=" + i + "Y=" + j);
                }
            }
        }
        
    
    }

    public void ReadMapFile()
    {
        string path = Application.dataPath + "//" + "map.txt";
        if (!File.Exists(path))
        {
            return;
        }

        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        StreamReader read = new StreamReader(fs, Encoding.Default);
        string strReadline;
        int y = 0;

        while ((strReadline = read.ReadLine()) != null)
        {
            for (int x = 0; x < strReadline.Length; ++x)
            {
                
                if (strReadline[x] == '#')
                {
                    map[x, y] = "wall";
                }
                if (strReadline[x] == '^')
                {
                    map[x, y] = "star";
                    sPos[x, y] = 1;
                }
                if (strReadline[x] == '*')
                {
                    map[x, y] = "stop";
                    
                }


            }
            y += 1;
        }
     
    }
    
    public void Setmap()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, FloorMask))
        {
            Vector3 mousemap = hit.point;
            Instantiate(target, mousemap, Quaternion.identity);
        }
    }

    void Draw()
    {
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                if (map[i, j] == "wall")
                {
                    Instantiate(box, new Vector3(-i, 0.5f, j), Quaternion.identity);
                    
                }
                if (map[i, j] == "star")
                {
                    
                    Instantiate(star, new Vector3(-i, 0.5f, j), Quaternion.identity);
                    
                    
                }
                if (map[i, j] == "stop")
                {
                    Instantiate(star, new Vector3(-i, 0.5f, j), Quaternion.identity);
                    
                    
                }

            }
           
        }
       
    }
    void SetSmap(int n)//寻找自己周围的四个坐标 n是步数
    {
        for (int i = 1; i <100; i++)
        {
            for (int j = 1; j <100; j++)
            { 
                if (sPos[i, j] == n)
                {
                    if (map[i - 1, j] == "stop" || map[i + 1, j] == "stop" || map[i, j - 1]=="stop" || map[i, j + 1]=="stop")
                    {
                       return;
                    }
                    if (map[i - 1, j] == null)
                    {
                        sPos[i - 1, j] = (n + 1);
                        Instantiate(target, new Vector3(-i+1, 0f, j), Quaternion.identity);
                        //yield return new WaitForSeconds(0.01f);
                    }
                    if (map[i + 1, j] == null)
                    {
                        sPos[i + 1, j] = (n + 1);
                        Instantiate(target, new Vector3(-i -1, 0f, j), Quaternion.identity);
                        //yield return new WaitForSeconds(0.01f);
                    }
                    if (map[i , j-1] == null)
                    {
                        sPos[i , j-1] = (n + 1);
                        Instantiate(target, new Vector3(-i , 0f, j-1), Quaternion.identity);
                        //yield return new WaitForSeconds(0.01f);
                    }
                    if (map[i , j+1] == null)
                    {
                        sPos[i , j+1] = (n + 1);
                        Instantiate(target, new Vector3(-i, 0f, j+1), Quaternion.identity);
                        //yield return new WaitForSeconds(0.01f);
                    }
                }
            }
        }
        //SetSmap(n + 1);
        //yield return null;
    }
    
}
