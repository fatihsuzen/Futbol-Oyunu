using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bot : MonoBehaviour
{
    public Vector3 target;
    public Rigidbody rb;
    public GameObject Camera;
    public float speed=10f;
    public GameObject Gosterge;
    public float Distance;
    public float ShotPower;
    public float TolShotPower;
    public float MinShotPower,MaxShotPower;
    public static int TargetInt = 0;
    Vector3 Temp;
    private GameObject[] bottrajpool;
    private Rigidbody[] bottrajpoolrb;
    public int trajuzunluk = 20;
    public GameObject traj;
    public float zaman = 0.5f;
    private float beklemesüresi = 0;
    public float bekleme = 0.2f;
    private int sayac = 0;
    public bool trbool=false;
    bool bikere = false;
    bool bikere2= false;

    private void Start()
    {
        bottrajpool = new GameObject[trajuzunluk];
        bottrajpoolrb = new Rigidbody[trajuzunluk];
        for (int i = 0; i < bottrajpool.Length; i++)
        {
            bottrajpool[i] = Instantiate(traj, new Vector3(999, 999, 999),  Quaternion.Euler(new Vector3(0,90,0)));
            bottrajpool[i].SetActive(false);
            bottrajpool[i].name = "BOT" + i;
        }
        for (int i = 0; i < bottrajpoolrb.Length; i++)
        {
            bottrajpoolrb[i] = bottrajpool[i].GetComponent<Rigidbody>();
        }
    }
    void OnEnable()
    {
        Temp = transform.position;
        bikere = true;
        Camera.transform.DOMove(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z - 1.63f), 2f).OnComplete(() => bikere = false);
        Camera.transform.parent = transform;
        Invoke("SimulatedShotOne",3f);
        
    }
    public void SimulatedShotOne()
    {
        TurnToTarget();
        Invoke("SimulatedShotTwo",Random.Range(4f,6f));
        Invoke("kışkışkamera", 2.5f);

      
    }  
    public void SimulatedShotTwo()
    {  
        Calculate(12);
        trbool = true;
        InvokeRepeating("Shot",1.5f,0.05f);
    } 
    public void NextTurn()
    {
        transform.position = Temp;
        Invoke("SimulatedShotOne",1f);
    }   

    public void TurnToTarget()
    {  
        target = SceneManager.holeList[TargetInt].transform.position;

        bikere2 = true;
        
        //transform.LookAt(new Vector3(target.x,target.y+0.3f,target.z));
        Invoke("Rot",3f);      
    }   
    void Rot()
    {
        bikere2 = false;
        transform.DOLocalRotate(new Vector3(-45,transform.eulerAngles.y,transform.eulerAngles.z) ,1f );
    }
    public void Shot()
    {  
         if(ShotPower>MinShotPower&&ShotPower<MaxShotPower)
         {
            //rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
            rb.AddForce(transform.forward * TolShotPower);
            //rb.constraints = RigidbodyConstraints.None;
            trbool = false;
            CancelInvoke("Shot");
            Invoke("TurnPlayer",5);
         }
    }
    void TurnPlayer()
    {   
        transform.rotation = Quaternion.Euler(0,0,0);
        GameObject.Find("Player").GetComponent<yönünüayarla>().enabled = true;              
        gameObject.GetComponent<Bot>().enabled = false;
    }   
    public void Calculate(int difficulty)
    {
        Distance = Vector3.Distance(transform.position,SceneManager.holeList[TargetInt].transform.position);

        if(Distance>=0&&Distance<1)
        ShotPower = Distance * 1250f;
        else if(Distance>=1&&Distance<2)
        ShotPower = Distance * 1100f;
        else if(Distance>=2&&Distance<3)
        ShotPower = Distance *1050f;
        else if(Distance>=3&&Distance<4)
        ShotPower = Distance * 800f;
        else if(Distance>=4&&Distance<6)
        ShotPower = Distance * 692f;
        else if(Distance>=6&&Distance<8)
        ShotPower = Distance * 590f;
        else if(Distance>=8&&Distance<9.5f)
        ShotPower = Distance * 522f; 
        else if(Distance>=9.5f&&Distance<11f)
        ShotPower = Distance * 470f;
        else if(Distance>=11f&&Distance<12f)
        ShotPower = Distance * 430f;            
       
         switch (difficulty)
         {
           
          case 1:
             MinShotPower = ShotPower-400;
             MaxShotPower = ShotPower+400;
              break;
          case 2:
             MinShotPower = ShotPower-300;
             MaxShotPower = ShotPower+300; 
              break;

          case 3:
              MinShotPower = ShotPower-280;
              MaxShotPower = ShotPower+280;
              break;
          case 4:
             MinShotPower = ShotPower-250;
             MaxShotPower = ShotPower+250;
              break;

          case 5:
              MinShotPower = ShotPower-200;
              MaxShotPower = ShotPower+200;
              break;
          case 6:
             MinShotPower = ShotPower-180;
             MaxShotPower = ShotPower+180;
              break;
          case 7:
              MinShotPower = ShotPower-150;
              MaxShotPower = ShotPower+150;
              break;
          case 8:
             MinShotPower = ShotPower-100;
             MaxShotPower = ShotPower+100;
              break;

          case 9:
              MinShotPower = ShotPower-70;
              MaxShotPower = ShotPower+70;
              break;
          case 10:
             MinShotPower = ShotPower-50;
             MaxShotPower = ShotPower+50;
              break;

          case 11:
              MinShotPower = ShotPower-30;
              MaxShotPower = ShotPower+30;
              break;
          case 12:
             MinShotPower = ShotPower-10;
             MaxShotPower = ShotPower+10;
              break;
          default:
              break;
         }  
         TolShotPower = Random.Range(MinShotPower,MaxShotPower);

    }
    public void Trajector()
    {
        if (beklemesüresi < Time.time)
        {
            bottrajpool[sayac].SetActive(true);
            bottrajpool[sayac].transform.position = transform.position;
            bottrajpoolrb[sayac].velocity = Vector3.zero;
            bottrajpoolrb[sayac].AddForce(transform.forward * TolShotPower);
            StartCoroutine(kapa(bottrajpool[sayac], zaman));
            sayac++;
            beklemesüresi = bekleme + Time.time;
            if (sayac == bottrajpool.Length)
                sayac = 0;
        }

    }
    IEnumerator kapa(GameObject r, float time)
    {
        yield return new WaitForSeconds(time);
        r.SetActive(false);
    }
    private void Update()
    {
        if (bikere2)
        {
            Vector3 targetPoint = target;
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3.0f);
        }
        if (bikere)
        {
            Camera.transform.LookAt(new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z));
        }
        if (trbool)
        {
            Trajector();
        }
        else
        {
            for (int i = 0; i < bottrajpool.Length; i++)
            {
                bottrajpool[i].SetActive(false);
            }
        }
    }
    void kışkışkamera()
    {
        transform.GetChild(1).parent = null;
        Camera.transform.DOMove(new Vector3(13.95f, 4.89f, 4.4f), 2f).OnComplete(() => Gosterge.SetActive(true)); 
        Camera.transform.DORotate(new Vector3(20f, -90f, 0), 2f).OnComplete(() => SceneManager.firstShot = false); 
    }
}
