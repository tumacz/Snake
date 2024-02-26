using UnityEngine;
using UnityEngine.InputSystem;

public class DirectionManager : MonoBehaviour
{
    [SerializeField] SnakeHead _snakeHead;

    private Vector2 _upDirection = Vector2.up;
    private Vector2 _downDirection = Vector2.down;
    private Vector2 _rightDirection = Vector2.right;
    private Vector2 _leftDirection = Vector2.left;

    private SnakeInput _snakeInput;
    private InputAction _up;
    private InputAction _down;
    private InputAction _left;
    private InputAction _right;

    private bool _directionChanged = false;

    private void Awake()
    {
        _snakeInput = new SnakeInput();
    }

    private void Start()
    {
        _up = _snakeInput.Snake.Up;
        _down = _snakeInput.Snake.Down;
        _left = _snakeInput.Snake.Left;
        _right = _snakeInput.Snake.Right;

        _up.started += ctx => ManageCurrentDirection(_upDirection);
        _down.started += ctx => ManageCurrentDirection(_downDirection);
        _left.started += ctx => ManageCurrentDirection(_leftDirection);
        _right.started += ctx => ManageCurrentDirection(_rightDirection);
    }

    private void OnEnable()
    {
        _snakeInput.Enable();
    }

    private void ManageCurrentDirection(Vector2 direction)
    {
        if (!_directionChanged)
        {
            if (_snakeHead._snakeBody.Count > 1)
            {
                if (direction != -_snakeHead._direction)
                    SetCurrentDirection(direction);
            }
            else
            {
                SetCurrentDirection(direction);
            }
        }
    }
    private void OnDestroy()
    {
        _snakeInput.Disable();
    }

    public void FinishMovement()
    {
        _directionChanged = false;
    }

    private void SetCurrentDirection(Vector2 direction)
    {
        _snakeHead._direction = direction;
        _directionChanged = true;
    }
}