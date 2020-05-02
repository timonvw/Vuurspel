using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    #region Singleton
    private static GameMaster _instance;
    public static GameMaster Instance
    {
        get
        {
            if (_instance == null)
            {
                //uit nog niet nodig
                //GameObject go = new GameObject("GameMaster");
                //go.AddComponent<GameMaster>();
            }

            return _instance;
        }
    }
    #endregion

    //List of spawn locations
    [SerializeField]private List<GameObject> ListFireSpawns = new List<GameObject>();
    [SerializeField]private List<GameObject> ListCloudSpawns = new List<GameObject>();

    //prefabs
    [SerializeField]private GameObject Fire;
    [SerializeField]private GameObject FireEnd;
    [SerializeField]private GameObject Splash;
    [SerializeField]private GameObject SplashStartPos;
    [SerializeField]private GameObject SplashBegin;
    [SerializeField]private List<GameObject> ListClouds = new List<GameObject>();

    public float fireDimTime {get; private set;}
    public float fireSpawnTime {get; private set;}
    public float runTime {get; private set;}
    public int fireDimmedCounter {get; set;}
    public int score {get; set;}
    public bool getWater {get; set;}
    public bool DimAllowed {get; set;}

    private float runTimeCounter;

    // Use this for initialization
    void Start ()
    {
        //start values
        _instance = this;
        GameStart();
    }

    public void GameStart()
    {
        score = 0;
        fireDimTime = 0.001f;
        fireDimmedCounter = 0;
        fireSpawnTime = 5f;
        runTime = 5f;
        runTimeCounter = runTime;
        getWater = false;
        //dimmen
        DimAllowed = true;
        FireEnd.SetActive(false);
        //UIManager.Instance.EmptyUIText();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //water halen
        if (getWater == true)
        {
            //runTimeCounter -= Time.deltaTime;
            UIManager.Instance.GetWaterCounter("");

            if (runTimeCounter >= 0)
            {
                if (MessageListener.Instance.distanceRange >= 200)
                {
                    //SpawnFire();
                    runTimeCounter = runTime;
                    UIManager.Instance.EmptyUIText();
                    DimAllowed = true;
                    getWater = false;
                }
            }
            else
            {
                //EndGame();
                //getWater = false;
            }
        }
    }

    public void SetSettings()
    {
        switch (fireDimmedCounter)
        {
            case 2:
                fireDimTime = 0.0015f;
                fireSpawnTime = 2.5f;
                runTime = 4f;
                runTimeCounter = runTime;
                break;
            case 4:
                fireDimTime = 0.0018f;
                fireSpawnTime = 1.25f;
                runTime = 3f;
                runTimeCounter = runTime;
                break;
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartSpawnClouds(2.5f));
        SpawnFire();
    }

    public void EndGame()
    {
        UIManager.Instance.EmptyUIText();
        FireEnd.SetActive(true);
        StartCoroutine(End());
    }

    //spawn vuur op een random plek
    public void SpawnFire()
    {
        var randomPos = Random.Range(0, 6);

        Instantiate(Fire,ListFireSpawns[randomPos].transform.position, Quaternion.identity);
    }

    //spawn a random wolk at an random position
    public void SpawnCloud()
    {
        var randomCloud = Random.Range(0, 2);
        var randomPos = Random.Range(0, 4);

        Instantiate(ListClouds[randomCloud], ListCloudSpawns[randomPos].transform.position, Quaternion.identity);
    }

    //spawn een water druppel en geef de positie mee van het vuur
    public void SpawnWater(Vector3 position)
    {
        GameObject splash = Instantiate(Splash, SplashStartPos.transform.position, Quaternion.identity);
        splash.SendMessage("SetPosition", position);
    }

    public void SetWaterSplach(bool on)
    {
        SplashBegin.SetActive(on);
    }
    
    //Spawn 2 wolken with an interval in het begin van het spel
    private IEnumerator StartSpawnClouds(float pauseTime)
    {
        SpawnCloud();
        yield return new WaitForSeconds(pauseTime);
        SpawnCloud();
    }

    private IEnumerator End()
    {
        yield return new WaitForSeconds(2);
        UIManager.Instance.EndGame();
    }
}
