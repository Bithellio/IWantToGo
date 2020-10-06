

using UnityEngine;
using UnityEngine.UI;

public class PopUpScript : MonoBehaviour
{
    
    [SerializeField]
    private string Text;

    [SerializeField]
    private LevelManager LevelManager;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="Player")
        {
            LevelManager.ShowTextbox(Text, transform.position);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            LevelManager.HideTextBox();
        }
    }
}
