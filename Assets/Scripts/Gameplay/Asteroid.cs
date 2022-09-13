using UnityEngine;
using UnityEngine.Events;

public class Asteroid : MonoBehaviour
{
    #region Fields
    [SerializeField]
    Sprite[] rockSprites = new Sprite[3];

    SpriteRenderer spriteRenderer;
    CircleCollider2D asteroidCollider;

    AddPointsEvent addPointsEvent = new AddPointsEvent();

    float angle;

    const int Points = 50; 
    #endregion

    #region Unity methods
    // Start is called before the first frame update
    void Start()
    {
        // Setting random rock sprite
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Intializing Asteroid
        Initialize((Direction)Random.Range(0, 3));

        asteroidCollider = GetComponent<CircleCollider2D>();
        asteroidCollider.enabled = true;

        EventManager.AddAddPointsInvoker(this);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            addPointsEvent.Invoke(Points);
            AudioManager.Play(AudioClipName.AsteroidHit);
            Destroy(collision.gameObject);
            
            if(transform.localScale.x < 0.5f)
            {
                Destroy(gameObject);
                EventManager.RemoveAddPointsInvoker(this);
            }
            else
            {
                ShrinkAsteroid();
            }
        }
    }
    #endregion

    #region Private methods
    public void Initialize(Direction direction)
    {
        // Setting random rock sprite
        SetSprite();

        // Setting random angle based on direction
        SetDirection(direction);

        // Setting random velocity 
        SetVelocity();
    }

    void SetSprite()
    {
        spriteRenderer.sprite = rockSprites[Random.Range(0, rockSprites.Length)];
    }

    void SetVelocity()
    {
        const float MinImpulseForce = 1f;
        const float MaxImpulseForce = 3f;
       // float angle = Random.Range(0, 2 * Mathf.PI);
        Vector2 direction = new Vector2(
            Mathf.Cos(angle), Mathf.Sin(angle));
        float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);
        GetComponent<Rigidbody2D>().AddForce(
            direction * magnitude,
            ForceMode2D.Impulse);
    }

    // Setting random direction angle based on direction
    void SetDirection(Direction direction)
    {
        // set random angle based on direction
        float randomAngle = Random.value * 30f * Mathf.Deg2Rad;
        if (direction == Direction.Up)
        {
            angle = 75 * Mathf.Deg2Rad + randomAngle;
        }
        else if (direction == Direction.Left)
        {
            angle = 165 * Mathf.Deg2Rad + randomAngle;
        }
        else if (direction == Direction.Down)
        {
            angle = 255 * Mathf.Deg2Rad + randomAngle;
        }
        else
        {
            angle = -15 * Mathf.Deg2Rad + randomAngle;
        }

    }


    void ShrinkAsteroid()
    {
        // shrink asteroid to half size
        Vector3 scale = transform.localScale;
        scale.x /= 2;
        scale.y /= 2;

        transform.localScale = scale;

        // clone twice and destroy original
        GameObject newAsteroid = Instantiate<GameObject>(gameObject, transform.position, Quaternion.identity);
        newAsteroid.GetComponent<Asteroid>().SetVelocity();
        Vector2 newPosition = new Vector2(transform.position.x + 0.7f, transform.position.y + 0.7f);
        newAsteroid = Instantiate<GameObject>(gameObject, newPosition, Quaternion.identity);
        newAsteroid.GetComponent<Asteroid>().SetVelocity();
        Destroy(gameObject);
    }
    #endregion

    #region Event methods
    public void AddAddPointsListener(UnityAction<int> listener)
    {
        addPointsEvent.AddListener(listener);
    }
    #endregion
}
