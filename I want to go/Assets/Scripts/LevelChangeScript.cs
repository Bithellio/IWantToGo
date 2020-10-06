using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChangeScript : MonoBehaviour
{

    [SerializeField]
    private string NextLevel; 


  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SceneManager.LoadScene(NextLevel);
        }
    }
}
