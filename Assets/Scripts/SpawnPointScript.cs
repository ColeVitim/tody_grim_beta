using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointScript : MonoBehaviour
{

    UnityEngine.Object EnemyRef;
    public string NameOfEnemy;
    
    void Start ()
    {
        EnemyRef = Resources.Load(NameOfEnemy);

        Death();
 

    }


    public void Death()
    {
           /* GameObject EnemyResp = (GameObject)Instantiate(EnemyRef);
            EnemyResp.transform.position = transform.position;*/
        StartCoroutine(death());
    }

    IEnumerator death()
    {
        Debug.Log("esperando");
        yield return new WaitForSeconds(5);
        GameObject EnemyResp = (GameObject)Instantiate(EnemyRef);
        EnemyResp.transform.position = transform.position;
                Debug.Log("esperando2");

        
    }
    
}
