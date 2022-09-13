using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Unity methods
    void Update()
    {
        DestroyBullet();
    }
    #endregion

    #region Private methods
    void DestroyBullet()
    {
        if(gameObject.transform.position.x < ScreenUtils.ScreenLeft || 
        gameObject.transform.position.x > ScreenUtils.ScreenRight ||
        gameObject.transform.position.y < ScreenUtils.ScreenBottom ||
        gameObject.transform.position.y > ScreenUtils.ScreenTop)
        {
            Destroy(gameObject);
        }
    }
    #endregion
    
    #region Public methods
    public void ApplyForce(Vector2 forceDirection)
    {
        const float forceMagnitude = 6;
        GetComponent<Rigidbody2D>().AddForce(
            forceMagnitude * forceDirection,
            ForceMode2D.Impulse);
    }
    #endregion
}
