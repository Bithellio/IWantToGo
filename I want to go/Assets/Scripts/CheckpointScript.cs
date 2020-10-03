using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{

    [SerializeField]
    private Sprite NotActivatedSprite;

    [SerializeField]
    private Sprite ActivatedSprite;

    [SerializeField]
    private bool CheckpointReached;

    private SpriteRenderer _checkpointSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _checkpointSpriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _checkpointSpriteRenderer.sprite = ActivatedSprite;
            CheckpointReached = true;
        }
    }
}
