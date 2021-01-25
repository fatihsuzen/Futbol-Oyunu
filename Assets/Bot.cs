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
    public int Distance;
    void Start()
    {
        Invoke("SimulatedShotOne",1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SimulatedShotOne()
    {
        TurnToTarget();
        Invoke("SimulatedShotTwo",Random.Range(3f,7f));
        Camera.transform.DOMove(new Vector3(13.95f,4.89f,4.4f), 2f).OnComplete( () => Gosterge.SetActive(true));;                 
        Camera.transform.DORotate(new Vector3(20f,-90f,0), 2f).OnComplete( () => SceneManager.firstShot= false);;  

    }  
    public void SimulatedShotTwo()
    { 
        Invoke("Shot",Random.Range(1f,5f));
    }    

    public void TurnToTarget()
    {  
        target = SceneManager.holeList[0].transform.position; 
        
        /*Vector3 direction = target - transform.position;
        Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(toRotation.x,toRotation.y+0.3f,toRotation.z), speed * Time.time); */
        
        transform.LookAt(new Vector3(target.x,target.y+0.3f,target.z));
        Invoke("Rot",0.1f);
        
      
    }   
    void Rot()
    {   
        transform.DOLocalRotate(new Vector3(-45,transform.eulerAngles.y,transform.eulerAngles.z) ,1f );
    }
    public void Shot()
    {
        rb.AddForce(transform.forward * SceneManager.atışgücü);
    }
}
