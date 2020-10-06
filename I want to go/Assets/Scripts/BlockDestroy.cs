using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroy : MonoBehaviour
{

    [SerializeField]
    private LevelManager levelManager;


    void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            if (collision.GetType() == typeof(CircleCollider2D))
            {

                if (gameObject.tag == "GoldBlock")
                {

                    levelManager.IncreaseGoldScore();
                }

                if (gameObject.tag == "ChockBlock")
                {

                    levelManager.IncreaseChocScore();
                }


                gameObject.SetActive(false);
            }
            
        }
    }
}
