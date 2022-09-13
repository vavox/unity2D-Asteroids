using UnityEngine;
using UnityEngine.Events;

public class ShipController : MonoBehaviour
{
    #region Fields
    [SerializeField]
    GameObject prefabBullet;

    Rigidbody2D shipRigidBody;

    Vector2 thrustDirection = new Vector2(1, 0);

    DrawHealthPointsEvent drawHealthPointsEvent = new DrawHealthPointsEvent();

    const float ThrustForce = 10f;
    const float RotateDegreesPerSecond = 180;
    
    // Health tracker
    short hearthsHealth = 3;
    #endregion

    #region Public properties
    // Get health points   
    public short HearthsHealth
    {
        get { return hearthsHealth; }
    }
    #endregion

    #region Unity methods
    // Start is called before the first frame update
    void Start()
    {
        shipRigidBody = GetComponent<Rigidbody2D>();

        EventManager.AddDrawHealthPointsInvoker(this);
        drawHealthPointsEvent.Invoke(hearthsHealth);
    }

    // Update is called once per frame
    void Update()
    {
        // Ship rotation
        transform.Rotate(Vector3.forward, RotateDegreesPerSecond * GetInput("Rotate") * Time.deltaTime);

        // Change thrust direction to match ship rotation
        float zRotation = transform.eulerAngles.z * Mathf.Deg2Rad;
        thrustDirection.x = Mathf.Cos(zRotation);
        thrustDirection.y = Mathf.Sin(zRotation);

        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            AudioManager.Play(AudioClipName.PlayerShot);
            Vector3 bulletPosition = new Vector3(thrustDirection.x, thrustDirection.y, transform.position.z);
            GameObject bullet = Instantiate(prefabBullet, transform.position + bulletPosition/2, Quaternion.identity);
            Bullet script = bullet.GetComponent<Bullet>();
            script.ApplyForce(thrustDirection);
        }
    }

    // Fixed Update (called 50 times per second)
    void FixedUpdate()
    {
        // Ship thrust movement
        shipRigidBody.AddForce(ThrustForce * thrustDirection * GetInput("Thrust"), ForceMode2D.Force);
        
        if(GetInput("Rotate") == 0 && GetInput("Thrust") != 0) 
        { shipRigidBody.freezeRotation = true; }
        else { shipRigidBody.freezeRotation = false; } 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Asteroid"))
        {
            if(!CanDecreaseHealth())
            { 
                AudioManager.Play(AudioClipName.PlayerDeath);
                Destroy(gameObject); 
                EventManager.RemoveDrawHealthPointsInvoker(this);
            }
        }
    }
    #endregion

    #region Private methods
    // Get input value by input type 
    float GetInput(string inputType)
    {
        if(inputType == null)
        {
            Debug.Log("Wrong Input Type!");
            return 0f;   
        }   
        
        return Input.GetAxis(inputType);
    }

    bool CanDecreaseHealth()
    {
        if(hearthsHealth > 1)
        {
            AudioManager.Play(AudioClipName.PlayerHit);
            hearthsHealth--;
            drawHealthPointsEvent.Invoke(hearthsHealth);
            return true;
        }
        else
        {
            hearthsHealth--;
            drawHealthPointsEvent.Invoke(hearthsHealth);
            return false;
        }
    }
    #endregion

    #region Event methods
    public void AddDrawHealthPointsListener(UnityAction<int> listener)
    {
        drawHealthPointsEvent.AddListener(listener);
    }
    #endregion
}
