using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBlockScript : MonoBehaviour
{

    [SerializeField]
    private LevelManager GameLevelManager;

    [SerializeField]
    private int AmountToOpen;

    [SerializeField]
    private GameObject ReplacementTextBox;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        GameLevelManager.HideTextBox();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           var openScore =  GameLevelManager.GetScore(tag);

            if(openScore >= AmountToOpen)
            {
                ReplacementTextBox.SetActive(true);
                gameObject.SetActive(false);
            }
            else
            {

            }
        }
    }
}
