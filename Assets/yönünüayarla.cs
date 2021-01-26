using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class yönünüayarla : MonoBehaviour
{

    float mousex, mousey;
    public GameObject Camera;
    public GameObject Way;
    public GameObject Gosterge;
    public Rigidbody rb;
    public float dönmehızı;
    public float atışgücü;
    public float miny, maxy;
    public GameObject Stick;
    bool yönet = true;

    public GameObject traj;
    public int trajuzunluk;

    private GameObject[] trajpool;
    private Rigidbody[] trajpoolrb;
    private int sayac = 0;
    public float zaman = 0.5f;
    private float beklemesüresi=0;
    public float bekleme = 5f;
    public float Distance;
    int x = -1;
    int p = 0;
    public bool trbool = true;
    bool bikere = false;
    private int a = 0;

    public SphereCollider topuncol;
    public MeshCollider plane;

    void OnEnable()
    {
        
        if (a != 0)
        {
            bikere = true;
            Camera.transform.DOMove(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z - 1.63f), 2f).OnComplete(() => bikere = false);
        }
        else
        {
            a++;
        }       
        sayac = 0;
        zaman = 0.5f;
        beklemesüresi=0;
        bekleme = 0.2f;
        x = -1;
        p = 0;
        trbool = true;
        yönet = true;
        mousex = 0;
        mousey = 0;
        SceneManager.firstShot = true;
        Way.SetActive(true);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    void OnDisable()
    {   
        transform.rotation = Quaternion.Euler(0, 0, 0);
        CancelInvoke();
    }
    private void Start()
    {   
        Invoke("Calculate",1);
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
        trajpool = new GameObject[trajuzunluk];
        trajpoolrb = new Rigidbody[trajuzunluk];
        for(int i = 0; i < trajpool.Length; i++)
        {
            trajpool[i] = Instantiate(traj, new Vector3(999, 999, 999), Quaternion.Euler(new Vector3(0,90,0)));
            trajpool[i].SetActive(false);
        }
        for (int i = 0; i < trajpoolrb.Length; i++)
        {
            trajpoolrb[i] = trajpool[i].GetComponent<Rigidbody>();
        }
    }
    private void Update()
    {
        if(SceneManager.firstShot)
        {
            if (bikere)
            {
                Camera.transform.LookAt(new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z));
            }
            if (Input.GetMouseButton(0) && p==0)
            {              
                Camera.transform.parent = transform;
                mousex += Input.GetAxis("Mouse X") * dönmehızı;          
                transform.rotation = Quaternion.Euler(mousey, -mousex, 0);
            
            }
            if (Input.GetMouseButtonUp(0)&&p==0)
            {   p++;         
                Way.SetActive(false);
                rb.constraints = RigidbodyConstraints.None;
                trbool = true;
                transform.GetChild(2).parent = null;
                Camera.transform.DOMove(new Vector3(13.95f,4.89f,4.4f), 2f).OnComplete( () => Gosterge.SetActive(true));;                 
                Camera.transform.DORotate(new Vector3(20f,-90f,0), 2f).OnComplete( () => SceneManager.firstShot= false);;              
                mousey = -45;
                transform.rotation = Quaternion.Euler(mousey,-mousex, 0);
                
            }
        }
        else
        {
            if (trbool)
            {
                Trajector();
            }
            else
            {
                for (int i = 0; i < trajpool.Length; i++)
                {
                    trajpool[i].SetActive(false);
                }
            }
            if (Input.GetMouseButtonDown(0))
            {   Debug.Log(SceneManager.atışgücü);
                SceneManager.firstShot = true;
                rb.AddForce(transform.forward * SceneManager.atışgücü );
                trbool = false;
                Gosterge.SetActive(false);                 
            }
            if(rb.velocity.z<0.001f&&!trbool&&p==1)
            {   p++;
                Invoke("CameraMoveStartPos",5);  
            }
        }           
    }
    void CameraMoveStartPos()
    {
        if(!GameController.playerInHole)
        {
        GameObject.Find("Bot").GetComponent<Bot>().enabled = true;  
        gameObject.GetComponent<yönünüayarla>().enabled = false;    
       
        }
        else
        {
             SceneManager.firstShot = true;     
        }
        
    }
    public void Calculate()
    {
        Distance = Vector3.Distance(transform.position,SceneManager.holeList[0].transform.position);
        //if(difficulty=>1&&difficulty<5)

    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "delik")
        {
            Physics.IgnoreCollision(topuncol,plane);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "delik")
        {
            Physics.IgnoreCollision(topuncol, plane,false);
        }
    }
    IEnumerator kapa(GameObject r,float time)
    {
        yield return new WaitForSeconds(time);
        r.SetActive(false);
    }
    public void Trajector()
    {   
        if (beklemesüresi < Time.time)
        {
            trajpool[sayac].SetActive(true);
            trajpool[sayac].transform.position = transform.position;
            trajpoolrb[sayac].velocity = Vector3.zero;
            trajpoolrb[sayac].AddForce(transform.forward * SceneManager.atışgücü);
            StartCoroutine(kapa(trajpool[sayac], zaman));
            sayac++;
            beklemesüresi = bekleme + Time.time;
            if (sayac == trajpool.Length)
                sayac = 0;
        } 
    }

}
