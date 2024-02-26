using UnityEngine;

public class FoodProvider : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _gridArea;
    [SerializeField] private PickUp _pickUp;
    [SerializeField] private Transform _wallPrefab;
    [SerializeField] private SnakeHead _snakeHead;

    private float _offset = 0.5f;

    private void Start()
    {
        Vector3 gridAreaCenter = _gridArea.bounds.center;
        Vector3 gridAreaSize = _gridArea.bounds.size;

        Transform northWall = Instantiate(_wallPrefab, gridAreaCenter + new Vector3(0, Mathf.RoundToInt(gridAreaSize.y / 2 + _offset) , 0), Quaternion.identity);
        northWall.localScale = new Vector3(gridAreaSize.x, 1, 1);

        Transform southWall = Instantiate(_wallPrefab, gridAreaCenter + new Vector3(0, Mathf.RoundToInt(-gridAreaSize.y / 2 - _offset) , 0), Quaternion.identity);
        southWall.localScale = new Vector3(gridAreaSize.x, 1, 1);

        Transform eastWall = Instantiate(_wallPrefab, gridAreaCenter + new Vector3(Mathf.RoundToInt(gridAreaSize.x / 2 + _offset), 0, 0), Quaternion.identity);
        eastWall.localScale = new Vector3(1, gridAreaSize.y, 1);

        Transform westWall = Instantiate(_wallPrefab, gridAreaCenter + new Vector3(Mathf.RoundToInt(-gridAreaSize.x / 2 - _offset), 0, 0), Quaternion.identity);
        westWall.localScale = new Vector3(1, gridAreaSize.y, 1);
    }

    public void SpawnFood()
    {
        int maxAttempts = 100;
        int attempts = 0;

        while (attempts < maxAttempts)
        {
            Vector2 pos = RandomFoodPosition();

            if (!IsFoodPositionOccupied(pos))
            {
                var food = Instantiate(_pickUp, pos, Quaternion.identity);
                return;
            }

            attempts++;
        }

        Debug.Log("you win!!");
        _snakeHead.WinGame();
    }

    private bool IsFoodPositionOccupied(Vector2 position)
    {
        foreach (Transform segment in _snakeHead._snakeBody)
        {
            if ((Vector2)segment.position == position)
            {
                return true;
            }
        }
        return false;
    }

    private Vector2 RandomFoodPosition()
    {
        Bounds bounds = this._gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(Mathf.Round(x), Mathf.Round(y));
    }
}