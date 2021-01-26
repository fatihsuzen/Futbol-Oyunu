using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public static bool playerInHole=false;
    public static bool botInHole=false;
    public List<GameObject> SpawnPoint = new List<GameObject>();
    public Text number;
    public Text BotScoreText;
    public Text PlayerScoreText;
    public static int playerScore,botScore;

    void Start()
    {
        PlayerScoreText = GameObject.Find("PlayerScoreText").GetComponent<Text>();
        BotScoreText = GameObject.Find("BotScoreText").GetComponent<Text>();
    }
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
            playerScore++;
            TextUpdate();        
        }
        if(other.gameObject.tag == "Bot")
        {
            //Botskor++
            StartCoroutine(Spawn(other.gameObject, SpawnPoint[Random.Range(0, SpawnPoint.Count)].transform.position));
            //other.transform.position = SpawnPoint[Random.Range(0,SpawnPoint.Count)].transform.position;
            Bot.TargetInt++;
            botInHole=true;
            botScore++;
            TextUpdate();
        }
       
    }
    void TextUpdate()
    {
        PlayerScoreText.text = playerScore.ToString();
        BotScoreText.text = botScore.ToString();
    }
    IEnumerator Spawn(GameObject other,Vector3 pos)
    {
        yield return new WaitForSeconds(1f);
        other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        other.transform.position =pos;

    }
}
