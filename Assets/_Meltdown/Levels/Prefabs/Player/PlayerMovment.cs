using System;
using UnityEngine;


public class PlayerMovment : MonoBehaviour
{
    public float jumpForce = 10f;
    
    private ControlsReader _controlsReader;
    private Rigidbody _rigidbody;
    private Vector3 _originalPosition;
    private Vector3 _originalScale; 


    enum PlayerState
    {
        IDLE,
        JUMPING,
        DUCKING,
    }

    private PlayerState _playerState = PlayerState.IDLE;

    void Start()
    {
        _controlsReader = GetComponent<ControlsReader>();
        _rigidbody = GetComponent<Rigidbody>();
        _originalPosition = GetComponent<Transform>().position;
        _originalScale = GetComponent<Transform>().localScale;
        _controlsReader.onEscapeKeyEvent += EscapeKey;
    }

    private void EscapeKey()
    {
        
       GameManager.Instance.LoadMainMenu();
       GameManager.Instance.UnloadMeltdown();
       _controlsReader.onEscapeKeyEvent -= EscapeKey;
    }

    void FixedUpdate()
    {
        Debug.Log(_playerState);
        
        if (_playerState == PlayerState.IDLE)
        {
            if (_controlsReader.isDucking)
            {
                _playerState = PlayerState.DUCKING;
                return;
            }

            if (_controlsReader.isJumping)
            {
                _playerState = PlayerState.JUMPING;
                return;
            }
        }
        switch (_playerState)
        {    
            case PlayerState.IDLE:
            {
                if (_controlsReader.isDucking)
                {
                    _playerState = PlayerState.DUCKING;
                    return;
                }

                if (_controlsReader.isJumping)
                {
                    _playerState = PlayerState.JUMPING;
                    return;
                }

                return;
            }
            
            case PlayerState.DUCKING:
            {
                Debug.Log("Ducking!");

                if (!_controlsReader.isDucking)
                {
                    transform.position = new Vector3(transform.position.x, _originalPosition.y, transform.position.z);
                    transform.localScale = new Vector3(transform.localScale.x, _originalScale.y, transform.localScale.z);
                    IdleStateReset();
                    Debug.Log(" Not Ducking!");
                    return;
                }
            
                transform.position = new Vector3(transform.position.x, .5f, transform.position.z);
                transform.localScale = new Vector3(transform.localScale.x, .5f, transform.localScale.z);
                return;
            }
            case PlayerState.JUMPING when _controlsReader.isJumping && GroundChecker():
                _rigidbody.AddForce(Vector3.up * jumpForce , ForceMode.Impulse);//needs to be in fixedupdate so that it doesn't become frame dependent and mess with the build version
                return;
            
            case PlayerState.JUMPING:
            {
                if (GroundChecker())
                {
                    IdleStateReset();
                }
                return;
            }
        }
    }
    
    private void IdleStateReset()
    {
        _playerState = PlayerState.IDLE;
    }


    private bool GroundChecker()
    {
        Transform playerTransform = transform;
        return Physics.Raycast(playerTransform.position, -playerTransform.up, 1.2f);
    }

    private void OnDestroy()
    {
        _controlsReader.onEscapeKeyEvent -= EscapeKey;
    }
}