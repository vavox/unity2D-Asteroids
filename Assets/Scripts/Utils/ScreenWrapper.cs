using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    #region Fields
    float colliderRadius;// = 0.7f;
    #endregion
    
    #region Unity methods
    // Start is called before the first frame update
    void Start()
    {
       colliderRadius = gameObject.GetComponent<CircleCollider2D>().radius;
    }

    void OnBecameInvisible()
    {
        Vector2 position = transform.position;

        // check left, right, top, and bottom sides
        if (position.x + colliderRadius * transform.localScale.x < ScreenUtils.ScreenLeft ||
            position.x - colliderRadius * transform.localScale.x > ScreenUtils.ScreenRight)
        {
            position.x *= -1;
        }
        if (position.y - colliderRadius * transform.localScale.x > ScreenUtils.ScreenTop ||
            position.y + colliderRadius * transform.localScale.x < ScreenUtils.ScreenBottom)
        {
            position.y *= -1;
        }

        // move ship
        transform.position = position;    
    }
    #endregion
}
