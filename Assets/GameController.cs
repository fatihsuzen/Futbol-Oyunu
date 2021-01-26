using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public static bool playerInHole=false;
    public List<GameObject> SpawnPoint = new List<GameObject>();
    public Text number;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //playerskor++
            //StartCoroutine("Spawn");          
            StartCoroutine(Spawn(other.gameObject, SpawnPoint[Random.Range(0, SpawnPoint.Count)].transform.position));
            //Playercurrenthole++; 
            number.color = Color.red;
           
            playerInHole = true;          
        }
        if(other.gameObject.tag == "Bot")
        {
            //Botskor++
            StartCoroutine(Spawn(other.gameObject, SpawnPoint[Random.Range(0, SpawnPoint.Count)].transform.position));
            //other.transform.position = SpawnPoint[Random.Range(0,SpawnPoint.Count)].transform.position;
            Bot.TargetInt++;
        }
    }
    IEnumerator Spawn(GameObject other,Vector3 pos)
    {
        yield return new WaitForSeconds(1f);
        other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        other.transform.position =pos;

    }
}
