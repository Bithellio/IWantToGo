using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowScript : MonoBehaviour
{

    [SerializeField]
    [Range(0f, 20f)]
    private float Velocity;

    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private GameObject Reticle;



    private float vely = 0f;

    private Rigidbody2D _rigidbody;
    private PlayerController _controller; 

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _controller = Player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_controller.CanMoveSpawn)
        {
            if (Input.GetButtonDown("Fire1"))
            {

                var target = new Vector3(Reticle.transform.position.x, Reticle.transform.position.y, transform.position.z);

                var difference = Player.transform.position - target;

                var rotate = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;


                gameObject.SetActive(true);
                transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
                transform.rotation = Quaternion.Euler(0f, 0f, rotate);

                SoundManagerScript.PlaySound("throw");
                _rigidbody.velocity = (Reticle.transform.position - transform.position).normalized * Velocity;
            }
            _controller.RespawnPoint = transform.position;

        }
    
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision, GetComponent<BoxCollider2D>());
        }
    }
}
