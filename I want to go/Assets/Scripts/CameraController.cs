using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private GameObject Player;

    [SerializeField]
    [Range(0, 10)]
    private float Offset;

    [SerializeField]
    [Range(0,10)]
    private float OffsetSmoothing;

    private Vector3 _playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _playerPosition = new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);

        if(Player.transform.localScale.x > 0f)
        {
            _playerPosition = new Vector3(_playerPosition.x + Offset, _playerPosition.y, _playerPosition.z);
        }
        else
        {
            _playerPosition = new Vector3(_playerPosition.x - Offset, _playerPosition.y, _playerPosition.z);
        }

        transform.position = Vector3.Lerp(transform.position, _playerPosition, OffsetSmoothing * Time.deltaTime);


    }
}
