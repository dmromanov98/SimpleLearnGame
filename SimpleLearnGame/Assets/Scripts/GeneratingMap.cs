using System.Collections.Generic;
using UnityEngine;

public class GeneratingMap : MonoBehaviour {

    [Tooltip("Different types of objects that can be present on the map")]
    public List<GameObject> gameObjects;

    [Space]
    public GameObject player;
    public GameObject plane;

    [HideInInspector]
    public Vector3 shift;
    [HideInInspector]
    public List<GameObject> ObstaclesOnMap;
    [HideInInspector]
    public List<GameObject> planes;
    [HideInInspector]
    private bool timetoAdd = false;

    public void Start()
    {
        //смещение plane ов
        shift = new Vector3(0, 0, plane.transform.localScale.z * 10);

        GameObject platform1 = Instantiate(plane);
        planes.Add(platform1);
        planes[0].transform.position = new Vector3(0, 0, 0);

        GameObject platform2 = Instantiate(plane);
        planes.Add(platform2);
        planes[1].transform.position = planes[0].transform.position + shift;

        
        
        Debug.Log("Pos1 : " + planes[0].transform.position + "Pos2 : " + planes[1].transform.position);

    }

    public void Update()
    {

       if(planes[0].transform.position.z >= player.transform.position.z ||
           planes[1].transform.position.z >= player.transform.position.z)
        {
            Debug.Log("Need add");
            timetoAdd = true;
        }

       if(planes[0].transform.position.z <= player.transform.position.z && timetoAdd)
        {
            Debug.Log("add1 " + planes[0].transform.position + " " + planes[1].transform.position + shift);
            
            planes[1].transform.position = planes[0].transform.position + shift;
            timetoAdd = false;
        }

       if(planes[1].transform.position.z <= player.transform.position.z && timetoAdd)
        {
            Debug.Log("add2");

            planes[0].transform.position = planes[1].transform.position + shift;
            timetoAdd = false;
        }
    }
}
