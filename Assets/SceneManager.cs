using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SceneManager : MonoBehaviour
{
    [Header("Textler")]
    public static Text[] texts;
    public static bool firstShot = true;
    public static bool secondShot = false;
    public static GameObject[] holeList = new GameObject[5];
    public GameObject[] HoleList = new GameObject[5];
    public static float atışgücü;
    public float Atışgücü;
    public GameObject Plane;
    public GameObject Hole;
    public GameObject Stick;
    public int PlaneX;
    public int PlaneZ;
    public int HolePiece;
    int HoleCounter = 0;
    int x = -1;
    void Start()
    {
        texts = new Text[5];
        InvokeRepeating("Rotation",0,8.1f);
        InvokeRepeating("Power",1,0.1f);
        LevelManager();
    }


    void LevelManager()
    {
        for(int x = 0; x<=PlaneX;x+=2)
        {
             for(int z = 0; z<=PlaneZ;z+=2)
             {
                if(Random.Range(0,5)==0 && HoleCounter<HolePiece && z>2)
                {  
                    GameObject ForTarget = Instantiate(Hole, new Vector3(x, Hole.transform.position.y, z), Quaternion.Euler(new Vector3(90,0,0)));
                    texts[HoleCounter] = ForTarget.transform.GetChild(4).transform.GetChild(0).gameObject.GetComponent<Text>();
                    texts[HoleCounter].text = (HoleCounter + 1).ToString();
                    ForTarget.transform.GetChild(0).name = "Target"+HoleCounter;
                    holeList[HoleCounter]=ForTarget;
                    HoleList[HoleCounter]=ForTarget;
                    HoleCounter++;

                   
                }
                else
                {   
                    if(x==PlaneZ &&HoleCounter<HolePiece)
                    {
                        GameObject ForTarget = Instantiate(Hole, new Vector3(x, Hole.transform.position.y, z), Quaternion.Euler(new Vector3(90,0,0)));
                    texts[HoleCounter] = ForTarget.transform.GetChild(4).transform.GetChild(0).gameObject.GetComponent<Text>();
                    texts[HoleCounter].text = (HoleCounter + 1).ToString();
                    ForTarget.transform.GetChild(0).name = "Target"+HoleCounter;
                    holeList[HoleCounter]=ForTarget;
                    HoleList[HoleCounter]=ForTarget;
                    HoleCounter++;
                    Debug.Log("sa");
                    }
                    else
                    Instantiate(Plane, new Vector3(x, Plane.transform.position.y, z), Quaternion.Euler(new Vector3(90,0,0))); 
                }
             }
        }
    }

    void Power()
    {    
        if(90f-(Stick.transform.eulerAngles.z)>=0&&90f-(Stick.transform.eulerAngles.z)<=90)
        {
            atışgücü = (90f-(Stick.transform.eulerAngles.z))*25f;
        }
        else if(450f+(Stick.transform.eulerAngles.z*x)>90)
        {      
           atışgücü = (450f+(Stick.transform.eulerAngles.z*x))*25f;  
        }  
        Atışgücü = atışgücü;   
    }   
    public void Rotation()
    {
        Stick.transform.DORotate(new Vector3(Stick.transform.eulerAngles.x,Stick.transform.eulerAngles.y,90f), 4f).SetEase( Ease.Linear )
        .OnComplete( () =>  Stick.transform.DORotate(new Vector3(Stick.transform.eulerAngles.x,Stick.transform.eulerAngles.y,-89f),4f).SetEase( Ease.Linear ));;
    }
}
