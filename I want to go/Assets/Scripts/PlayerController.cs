using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float MoveSpeed = 5f;

    [SerializeField]
    [Range(0,10f)]
    private float JumpSpeed = 5f;

    [SerializeField]
    private bool CanDoubleJump;

    [SerializeField]
    private Transform GroundCheckPoint;

    [SerializeField]
    private bool CanWallJump;

    [SerializeField]
    private bool CanMoveSpawn;

    [SerializeField]
    private float GroundCheckRadius;

    [SerializeField]
    private LayerMask GroundLayer;

    [SerializeField]
    public Vector3 RespawnPoint;

    [SerializeField]
    public LevelManager GameLevelManager;

    private bool _isTouchingGround;
    private float _movement = 0f;
    private bool _hasJumped;

    private Rigidbody2D _rigidBody;
    private Animator _playerAnimation;
    private bool _isRight = true; 
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponent<Animator>();
        GameLevelManager = FindObjectOfType<LevelManager>();
        RespawnPoint = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        _isTouchingGround = Physics2D.OverlapCircle(GroundCheckPoint.position, GroundCheckRadius, GroundLayer);
        
        if(_isTouchingGround)
        {
            _hasJumped = false; 
        }

        _movement = Input.GetAxisRaw("Horizontal") * MoveSpeed;
        if (_movement != 0f )
        { 
            if(_movement > 0 && _isRight == false )
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y) ;
                _isRight = true;
                
            }
            else if(_movement < 0 && _isRight == true)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                _isRight = false;

            }
            _rigidBody.velocity = new Vector2(_movement, _rigidBody.velocity.y);
        }
        if(Input.GetButtonDown("Jump") && (_isTouchingGround || (CanDoubleJump && !_hasJumped)))
        {
            
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, JumpSpeed);
            _hasJumped = true;
        }
        if(Input.GetButtonDown("Respawn"))
        {
            _playerAnimation.SetBool("Dead", true);
            GameLevelManager.Respawn();
        }

        _playerAnimation.SetFloat("Speed", Mathf.Abs(_rigidBody.velocity.x));
        _playerAnimation.SetBool("OnGround", _isTouchingGround);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="FallDetector")
        {
            GameLevelManager.Respawn(); 
        }
        if(collision.tag =="WallJump")
        {
            GroundLayer |= (1 << LayerMask.NameToLayer("Walls"));
        }


    }


    public void SetPlayerDeath(bool set)
    {
        _playerAnimation.SetBool("Dead", set);
    }
}
