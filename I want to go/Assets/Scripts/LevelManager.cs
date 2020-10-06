using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    private float RespawnDelay;

    [SerializeField]
    private PlayerController GamePlayer;

    [SerializeField]
    private GameObject TextBoxField;

    [SerializeField]
    private GameObject TipBoxField;

    [SerializeField]
    private List<GameObject> GoldBlocks;

    [SerializeField]
    private List<GameObject> ChocolateBlocks;

    [SerializeField]
    private GameObject WjCanvas;

    [SerializeField]
    private GameObject SpawnThrowCanvas;

    [SerializeField]
    private GameObject DoubleJumpCanvas;

    [SerializeField]
    private GameObject DashCanvas;

    [SerializeField]
    private GameObject SlideCanvas;

    [SerializeField]
    private GameObject GoldCookiesCanvas;

    [SerializeField]
    private GameObject ChocCookiesCanvas;

    public int GoldScore = 0 ;
    public int ChocScore = 0;


    // Start is called before the first frame update
    void Start()
    {
        GamePlayer = FindObjectOfType<PlayerController>(); 

    }

    // Update is called once per frame
    void Update()
    {

        if (GamePlayer.CanWallJump == true)
        {
            WjCanvas.SetActive(true);
        }
        if (GamePlayer.CanDoubleJump == true)
        {
            DoubleJumpCanvas.SetActive(true);
        }
        if(GamePlayer.CanMoveSpawn == true)
        {
            SpawnThrowCanvas.SetActive(true); 
        }
        if(GamePlayer.CanDash == true)
        {
            DashCanvas.SetActive(true); 
        }
        if(GamePlayer.CanSlide == true)
        {
            SlideCanvas.SetActive(true);
        }
    }


    public void Respawn()
    {               
        StartCoroutine("RespawnCoroutine");
        GamePlayer.SetPlayerDeath(false);
    }

    public void ShowTextbox(string text, Vector3 position)
    {
        if(TextBoxField == null)
        {
            TextBoxField = Instantiate(TextBoxField, position, transform.rotation);
        }
        TextBoxField.transform.position = position;
        TextBoxField.SetActive(true);
        var child = TextBoxField.transform.Find("Text");
        child.GetComponent<Text>().text = text;
    }

    public void HideTextBox()
    {
        TextBoxField.SetActive(false);
    }




    public void ShowTipbox(string text, Vector3 position)
    {
        if (TipBoxField == null)
        {
            TipBoxField = Instantiate(TipBoxField, position, transform.rotation);
        }
        TipBoxField.transform.position = position;
        TipBoxField.SetActive(true);
        var child = TipBoxField.transform.Find("Text");
        child.GetComponent<Text>().text = text;
    }

    public void HideTipBox()
    {
        TipBoxField.SetActive(false);
    }



    public IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(RespawnDelay);

        // Reset Gold Blocks
        foreach(var block in GoldBlocks)
        {
            block.SetActive(true); 
        }
        foreach ( var block in ChocolateBlocks)
        {
            block.SetActive(true);
        }

        GamePlayer.gameObject.SetActive(false);        
        GamePlayer.transform.position = GamePlayer.RespawnPoint;
        GamePlayer.gameObject.SetActive(true);
    }

 
    public void IncreaseChocScore()
    {
        ChocCookiesCanvas.SetActive(true);

         ChocScore++;
        var text = ChocCookiesCanvas.GetComponentInChildren<Text>();
        text.text = ChocScore.ToString(); 
    }

    public void IncreaseGoldScore()
    {
        GoldCookiesCanvas.SetActive(true);

        GoldScore++;
        var text = GoldCookiesCanvas.GetComponentInChildren<Text>();
        text.text = GoldScore.ToString();
    }


    public int GetScore(string tag)
    {
        if (tag == "GoldLock")
        {
            return GoldScore;
        }
        if(tag == "ChockLock")
        {
            return ChocScore;
        }
        return 0;
    }
}
