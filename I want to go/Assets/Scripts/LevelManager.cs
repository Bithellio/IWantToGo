using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    private float RespawnDelay;

    [SerializeField]
    private PlayerController GamePlayer;

    public int Coinscore;

    [SerializeField]
    private List<GameObject> Coins; 

    // Start is called before the first frame update
    void Start()
    {
        GamePlayer = FindObjectOfType<PlayerController>(); 

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Respawn()
    {               
        StartCoroutine("RespawnCoroutine");
        GamePlayer.SetPlayerDeath(false);
    }


    private IEnumerator WaitForAnimation(Animation animation)
    {
        do
        {
            yield return null;
        } while (animation.isPlaying);
    }

    public IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(RespawnDelay);
        GamePlayer.gameObject.SetActive(false);        
        GamePlayer.transform.position = GamePlayer.RespawnPoint;
        GamePlayer.gameObject.SetActive(true);
    }
}
