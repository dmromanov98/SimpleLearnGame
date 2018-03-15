using System.Collections.Generic;
using UnityEngine;

public class GeneratingMap : MonoBehaviour {

    public GameObject player;

    [Tooltip("Different types of objects that can be present on the map")]
    public List<GameObject> gameObjectsOnMap;
    [Tooltip("Scale X")]
    public float gameObjectXScale = 2;

    public GameObject plane;
    [Header("Plane settings")]
    [Tooltip("Length of plane")]
    public float lengthZScale = 20;
    [Tooltip("How many lines of obstacles or something else on plane")]
    public float linesOnPlane = 5;
    
    [HideInInspector]
    private Vector3 shift;
    [HideInInspector]
    public List<GameObject> HedgesOnMap;
    [HideInInspector]
    public List<GameObject> planes;
    [HideInInspector]
    private bool timetoAdd = false;

    public void Start()
    {
        //задание размеров plane(А)
        plane.transform.localScale = new Vector3((gameObjectXScale/10)*linesOnPlane,1,lengthZScale);
        //задание позиции player(А)
        player.transform.position = new Vector3(0, player.transform.localScale.y/2, 3+plane.transform.position.z - plane.transform.localScale.z * 5);
        
        //размер смещения между центрами Plane(ОВ)
        shift = new Vector3(0, 0, plane.transform.localScale.z * 10);

        GameObject platform1 = Instantiate(plane);
        planes.Add(platform1);
        planes[0].transform.position = new Vector3(0, 0, 0);

        GameObject platform2 = Instantiate(plane);
        planes.Add(platform2);
        planes[1].transform.position = planes[0].transform.position + shift;

        //генерация объектов
        GeneratingHedges();

        Debug.Log("Pos1 : " + planes[0].transform.position + " Pos2 : " + planes[1].transform.position + " Shift : " + shift);

    }

    //генерация объектов
    private void GeneratingHedges()
    {
        //ВОЗМОЖНО УДАЛЕНИЕ КОЛИЧЕСТВА БЛОКОВ И ЦИКЛА WHILE!


        //подсчет количества возможных позиций блоков
        bool[,] HedgesArr = new bool[(int)(plane.transform.localScale.z / (player.transform.localScale.z)), (int)linesOnPlane];

        for (int i = 0; i < HedgesArr.GetLength(0); i++)
        {

            int colBlocksOnThisLine = Random.Range(2, HedgesArr.GetLength(1));

            for (int j = 1; j < colBlocksOnThisLine; j++)
            {
                bool added = true;

                //позиция блока в массиве
                int pos = Random.Range(0, HedgesArr.GetLength(1));

                //проверка добавлен ли туда блок
                if (HedgesArr[i, pos] == false)
                    HedgesArr[i, pos] = true;
                else
                    added = false;

                //если добавлен блок
                while (!added)
                {
                    pos = Random.Range(0, HedgesArr.GetLength(1));
                    if (HedgesArr[i, pos] == false)
                    {
                        HedgesArr[i, pos] = true;
                        added = true;
                    }
                }
                Instantiate(gameObjectsOnMap[0], new Vector3(2*pos-4,
                    gameObjectsOnMap[0].transform.localScale.y/2,planes[0].transform.localScale.z*5-i*10),
                    new Quaternion(0, 0, 0,0));
            }
        }

        Debug.Log(HedgesArr.GetLength(0) + " " + HedgesArr.GetLength(1));
    }

    public void Update()
    {
       //Проверка пробежал ли игрок plane , который мы хотим перенести
       if(((planes[0].transform.position.z + planes[0].transform.localScale.z*5) <= player.transform.position.z &&
           planes[1].transform.position.z >= player.transform.position.z && !timetoAdd)||
           ((planes[1].transform.position.z + planes[1].transform.localScale.z * 5) <= player.transform.position.z &&
           planes[0].transform.position.z >= player.transform.position.z && !timetoAdd))
        {
            Debug.Log("Need add");
            timetoAdd = true;
        }

       //перенос
       if(planes[0].transform.position.z >= planes[1].transform.position.z && timetoAdd)
        {
            Debug.Log("add1 " + planes[0].transform.position + " " + planes[1].transform.position + shift);
            planes[1].transform.position = planes[0].transform.position + shift;
            timetoAdd = false;
        }

        //перенос
        if (planes[1].transform.position.z >= planes[0].transform.position.z && timetoAdd)
        {
            Debug.Log("add2 " + planes[1].transform.position + " " + planes[0].transform.position + shift);
            planes[0].transform.position = planes[1].transform.position + shift;
            timetoAdd = false;
        }
    }
}
