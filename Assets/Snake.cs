using System;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    [SerializeField] BoxCollider2D _head;
    [SerializeField] Transform _snakeBodyPrefab;
    [SerializeField] FoodProvider _foodProvider;
    [SerializeField] DirectionManager _directionManager;
    [SerializeField] float _timeBetweenJump;
    private Transform _headPosition;
    public Vector2 _direction;
    private float _timeToMove;
    private bool _canMove = true;

    public List<Transform> _snakeBody;

    private void Start()
    {

        _snakeBody = new List<Transform>();
        _snakeBody.Add(this.transform);
        _direction = Vector2.up;
        _headPosition = transform;
        _timeToMove = _timeBetweenJump;
        _foodProvider.SpawnFood();
    }

    private void Update()
    {
        if (_canMove)
        {
            if (_timeToMove <= 0)
            {
                MoveSnake();
                _timeToMove = _timeBetweenJump;
            }
            else
            {
                _timeToMove -= Time.deltaTime;
            }
        }
        else
        {
            Debug.Log("movementStoped");
        }
    }

    private void MoveSnake()
    {
        for (int i = _snakeBody.Count - 1; i > 0; i--)
        {
            _snakeBody[i].position = _snakeBody[i - 1].position;
        }
        for(int i = 1; i < _snakeBody.Count; i++)
        {
            if (_snakeBody[i].tag != "Obstacle")
            {
                _snakeBody[i].tag = "Obstacle";
            }
        }
        _headPosition.position = new Vector2(_headPosition.position.x + _direction.x, _headPosition.position.y + _direction.y);
        _directionManager.FinishMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Food")
        {
            _foodProvider.SpawnFood();
            collision.gameObject.GetComponent<PickUp>().OnPickedUp();
            Grow();
        }
        else if(collision.gameObject.tag == "Obstacle")
        {
            ResetGame();
        }
    }

    private void Grow()
    {
        Transform bodySegment = Instantiate(this._snakeBodyPrefab);
        bodySegment.position = _snakeBody[_snakeBody.Count - 1].position;

        _snakeBody.Add(bodySegment);
    }

    public void WinGame()
    {
        _canMove = false;
    }

    private void ResetGame()
    {
        for(int i = 1; i < _snakeBody.Count; i++)
        {
            Destroy(_snakeBody[i].gameObject);
        }
        _snakeBody.Clear();
        _snakeBody.Add(this.transform);
        this.transform.position = Vector3.zero;
        _direction = Vector2.up;
    }
}
