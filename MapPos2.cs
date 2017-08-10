using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class MapPos2 : MonoBehaviour {
   
    public GameObject box;
    public GameObject star;
    public GameObject target;
    public int count;
    public int removeCount;
    public int[,] map;//用来记录地图的信息
    public Pos[,] sPos;//用来储存每个格子的步数
    List<Pos> SearchPos;//用来记录同样步数的坐标
    //public int FloorMask;
    public int step = 1;
    
    void Start ()
    {
        map = new int[30, 30];
        sPos = new Pos[30, 30];//用来记录步数
        SearchPos = new List<Pos>();//实例化一个列表
        ReadMapFile();
        Draw();
    }
 
    void Update()
    {
        DealList();
    }
    public void ReadMapFile()//读取TXT文件
    {
        string path = Application.dataPath + "//" + "map2.txt";
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

                if (strReadline[x] == '1')
                {
                    map[x, y] = 1;//墙的标记
                }
                if (strReadline[x] == '8')
                {
                    map[x, y] = 8;//开始的位置
                    sPos[x, y] = new Pos(x, y); //把坐标写到Pos类型的变量中;
                    sPos[x, y].step = 1;//记录开始的步数是1
                    SearchPos.Add(sPos[x, y]);
                }
                if (strReadline[x] == '9')
                {
                    map[x, y] = 9;//结束的位置
                }


            }
            y += 1;
        }
    }
    void Draw()
    {
       
        for (int i = 0; i < 30; i++)
        {
            for (int j = 0; j < 30; j++)
            {
                if (map[i, j] == 1)//画出墙
                {
                    Instantiate(box, new Vector3(i, 0.5f, -j), Quaternion.identity);
                    Debug.Log(11);
                }
                if (map[i, j] == 8)//画出起点
                {
                    Instantiate(star, new Vector3(i, 0.5f, -j), Quaternion.identity);
                    Debug.Log(11);
                }
                if (map[i, j] == 9)//画出结束点
                {
                    Instantiate(star, new Vector3(i, 0.5f, -j), Quaternion.identity);
                }
            }
        }
    }
    //处理列表里面的步数
    void DealList()
    {
        Debug.Log(SearchPos[0].step);
        int x;//用来记录横坐标
        int y;//用来记录纵坐标

        if (SearchPos.Count >= 1)
        {
           
            x=SearchPos[0].X ;
            y=SearchPos[0].Y ;
            
           
            if (map[x + 1, y] == 9 || map[x - 1, y] == 9 || map[x, y - 1] == 9 || map[x, y + 1] == 9)
            {
                Debug.Log("END");
                return;
            }
            if (map[x + 1, y] !=1 && sPos[x + 1, y]==null)
            {
                sPos[x + 1, y] = new Pos(x + 1, y);
                sPos[x + 1, y].step = sPos[x, y].step + 1;
                SearchPos.Add(sPos[x + 1, y]);
                Instantiate(target, new Vector3(x+1, 0f, -y), Quaternion.identity);
            }
            if (map[x - 1, y] != 1 && sPos[x - 1, y]==null)
            {
                sPos[x - 1, y] = new Pos(x - 1, y);//实例化
                sPos[x - 1, y].step = sPos[x, y].step + 1;
                SearchPos.Add(sPos[x - 1, y]);//增加到待处理列表
                Instantiate(target, new Vector3(x -1, 0f, -y), Quaternion.identity);
            }
            if (map[x , y-1] != 1 && sPos[x , y-1]==null)
            {
                sPos[x, y - 1] = new Pos(x, y - 1);
                sPos[x , y-1].step = sPos[x, y].step + 1;
                SearchPos.Add(sPos[x , y-1]);
                Instantiate(target, new Vector3(x , 0f, 1-y), Quaternion.identity);
            }
            if (map[x , y+1] != 1 && sPos[x , y+1]==null)
            {
                sPos[x, y + 1] = new Pos(x, y + 1);
                sPos[x , y+1].step = sPos[x, y].step + 1;
                SearchPos.Add(sPos[x , y+1]);
                Instantiate(target, new Vector3(x, 0f, -y-1), Quaternion.identity);
            }
            SearchPos.RemoveAt(0);
          
            
        }
        
    }
}
