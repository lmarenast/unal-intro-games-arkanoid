using UnityEngine;

public enum BlockType
{
    Small,
    Big
}

public enum BlockColor
{
    Green,
    Blue,
    Orange,
    Red,
    Purple
}

public class BlockTile : MonoBehaviour
{
    private const string BLOCK_BIG_PATH = "Sprites/BlockTiles/Big/Big_{0}_{1}";
    
    [SerializeField] 
    private BlockType _type = BlockType.Big;
    
    [SerializeField]
    private int _score = 10;
    
    public int Score => _score;
    private int _id;
    private BlockColor _color = BlockColor.Blue;
    private SpriteRenderer _renderer;
    private Collider2D _collider;
    
    private int _totalHits = 1;
    private int _currentHits = 0;

    public void SetData(int id, BlockColor color)
    {
        _id = id;
        _color = color;
    }
    
    public void Init()
    {
        _currentHits = 0;
        _totalHits = _type == BlockType.Big ? 2 : 1;

        _collider = GetComponent<Collider2D>();
        _collider.enabled = true;
        
        _renderer = GetComponentInChildren<SpriteRenderer>();

        _renderer.sprite =GetBlockSprite(_type, _color, 0);
    }
    
    public void OnHitCollision(ContactPoint2D contactPoint)
    {
        _currentHits++;
        if (_currentHits >= _totalHits)
        {
            _collider.enabled = false;
            gameObject.SetActive(false);
            ArkanoidEvent.OnBlockDestroyedEvent?.Invoke(_id);
            SpawnPowerUps(contactPoint);
        }
        else
        {
            _renderer.sprite = GetBlockSprite(_type, _color, _currentHits);
        }
    }
    
    static Sprite GetBlockSprite(BlockType type, BlockColor color, int state)
    {
        string path = string.Empty;
        if (type == BlockType.Big)
        {
            path = string.Format(BLOCK_BIG_PATH, color, state);
        }

        if (string.IsNullOrEmpty(path))
        {
            return null;
        }

        return Resources.Load<Sprite>(path);
    }

    private void  SpawnPowerUps(ContactPoint2D contactPoint)
    {
        if(Random.value<0.5f)
        {
            float randomPUp;
            randomPUp = Random.value;
            if(randomPUp<0.15f)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/PowerUpSlow"),contactPoint.point,Quaternion.identity);
            }
            else if(randomPUp<0.3f)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/PowerUpFast"),contactPoint.point,Quaternion.identity);
            }
            else if(randomPUp<0.45f)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/PowerUpSizeHigher"),contactPoint.point,Quaternion.identity);
            }
            else if(randomPUp<0.6f)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/PowerUpSizeMinor"),contactPoint.point,Quaternion.identity);
            }
            else if(randomPUp<0.7f)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/PowerUp50"),contactPoint.point,Quaternion.identity);
            }
            else if(randomPUp<0.8f)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/PowerUp100"),contactPoint.point,Quaternion.identity);
            }
            else if(randomPUp<0.9f)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/PowerUp250"),contactPoint.point,Quaternion.identity);
            }
            else if(randomPUp<=1f)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/PowerUp500"),contactPoint.point,Quaternion.identity);
            }
                
            }

    }
}