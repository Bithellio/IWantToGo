using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{


    public int CoinValue;
    public GameObject MainCollider; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (gameObject.active)
        {
            Debug.Log("Active");
        }
        if (collision.tag == "Player")
        {
            gameObject.SetActive(false);         
        }

    }
        
}
