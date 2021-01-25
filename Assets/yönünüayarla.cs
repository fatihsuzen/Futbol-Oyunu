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
    public Slider slide;
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
    public float laaaa;
    int x = -1;

    public bool trbool = true;
        

    public SphereCollider topuncol;
    public MeshCollider plane;
    
    private void Start()
    {   
       
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
        trajpool = new GameObject[trajuzunluk];
        trajpoolrb = new Rigidbody[trajuzunluk];
        for(int i = 0; i < trajpool.Length; i++)
        {
            trajpool[i] = Instantiate(traj, new Vector3(999, 999, 999), Quaternion.identity);
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
            if (Input.GetMouseButton(0))
            {
                mousex += Input.GetAxis("Mouse X") * dönmehızı;          
                transform.rotation = Quaternion.Euler(mousey, -mousex, 0);
            
            }
            if (Input.GetMouseButtonUp(0))
            {            
                Way.SetActive(false);
                rb.constraints = RigidbodyConstraints.None;
                //trbool = true;
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
            {   
                SceneManager.firstShot = true;
                rb.AddForce(transform.forward * SceneManager.atışgücü );
                trbool = false;                 
            }
        }           
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

}
