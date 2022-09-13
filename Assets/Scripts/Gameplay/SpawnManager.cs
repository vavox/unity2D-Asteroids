using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    #region Fields
    [SerializeField] 
    GameObject asteroidPrefab;

    [SerializeField]
    GameObject shipPrefab;

    SetHudWaveNumber setHudWaveNumber = new SetHudWaveNumber();

    const float MinSpawnDelay = 1f;
    const float MaxSpawnDelay = 3f;

    const int MaxRocksAtScreen = 5;
    int waveNumber = 1;

    float spawnLeft = 0;
    float spawnRight = 0;
    float spawnUp = 0;
    float spawnDown = 0;
    #endregion

    #region Unity methods
    // Start is called before the first frame update
    void Start()
    {
        EventManager.AddSetHudWaveNumberInvoker(this);
        Instantiate(shipPrefab);
        setHudWaveNumber.Invoke(waveNumber); 
        SpawnAsteroidWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        int asteroidCount = FindObjectsOfType<Asteroid>().Length;
        if(asteroidCount == 0) 
        { 
            //AudioManager.Play(AudioClipName.IncreaseWave);
            waveNumber++;
            setHudWaveNumber.Invoke(waveNumber); 
            SpawnAsteroidWave(waveNumber);
        }
    }
    #endregion

    #region Private methods    
    void SpawnAsteroidWave(int asteroidsToSpawn)
    {
        for(int i = 0; i < asteroidsToSpawn; i++)
        {
            Instantiate(asteroidPrefab, GetRandomPosition(), asteroidPrefab.transform.rotation);
        }
    }

    Vector3 GetRandomPosition()
    {
        Direction direction = (Direction)Random.Range(0, 3);
        switch (direction){
            case Direction.Left:
                spawnLeft = ScreenUtils.ScreenRight;
                spawnRight = ScreenUtils.ScreenRight - ScreenUtils.ScreenRight/2;
                break;
            case Direction.Right:
                spawnLeft = ScreenUtils.ScreenLeft - ScreenUtils.ScreenLeft/2;
                spawnRight = ScreenUtils.ScreenLeft;
                break;
            case Direction.Up:
                spawnDown = ScreenUtils.ScreenBottom - ScreenUtils.ScreenBottom/2;
                spawnUp = ScreenUtils.ScreenBottom;
                break;
            case Direction.Down:
                spawnDown = ScreenUtils.ScreenTop;
                spawnUp = ScreenUtils.ScreenTop - ScreenUtils.ScreenTop/2;
                break;
            default:
                break;
        }

        return new Vector3(Random.Range(spawnLeft, spawnRight), Random.Range(-spawnDown, spawnUp), asteroidPrefab.transform.position.z);
    }
    #endregion

    #region Event methods
    public void AddSetHudWaveNumberListener(UnityAction<int> listener)
    {
        setHudWaveNumber.AddListener(listener);
    }
    #endregion
}
