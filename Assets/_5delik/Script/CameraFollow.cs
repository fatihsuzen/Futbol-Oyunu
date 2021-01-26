using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Camera;

    void Start()
    {
        InvokeRepeating("textsRotation",0.5f,0.22f);
    }
    public void textsRotation()
    {   
        for (int i = 0; i < SceneManager.texts.Length; i++)
        {
            SceneManager.texts[i].transform.LookAt(Camera.transform);
        }
    }
}
