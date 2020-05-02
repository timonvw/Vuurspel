using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                //uit nog niet nodig
                //GameObject go = new GameObject("UIManager");
                //go.AddComponent<UIManager>();
            }

            return _instance;
        }
    }
    #endregion

    [SerializeField]private List<GameObject> cameraPositions = new List<GameObject>();
    [SerializeField]private GameObject camera;
    [SerializeField]private GameObject UIMenu;
    [SerializeField]private GameObject Counter;
    [SerializeField]private GameObject UIGame;
    [SerializeField]private GameObject UIEndGame;

    private float cameraSpeed = 5f;
    private bool startCameraMove = false;
    private bool inMenu = true;
    private bool startEndMenu = false;
    public bool backToMenu = false;

    public Text counterText;
    public Text counterTextShadow;
    public Text scoreText;
    public Text scoreTextShadow;
    public Text scoreEndText;
    public Text scoreEndTextShadow;

    public Text textFeedbackGame;
    public Text textFeedbackGameShadow;

    // Use this for initialization
    void Start ()
    {
        //start values
        _instance = this;

        Counter.SetActive(false);
        UIGame.SetActive(false);
        UIEndGame.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
	{
	    scoreText.text = "Punten: " + GameMaster.Instance.score.ToString();
	    scoreTextShadow.text = scoreText.text;

	    scoreEndText.text = GameMaster.Instance.score.ToString();
	    scoreEndTextShadow.text = scoreEndText.text;

        if (inMenu == true)
	    {
	        if (MessageListener.Instance.buttonPressed == true)
            {
                startCameraMove = true;
                UIMenu.SetActive(false);
            }

            if (startCameraMove == true)
            {
                camera.transform.position = Vector3.MoveTowards(camera.transform.position,
                    cameraPositions[1].transform.position, cameraSpeed * Time.deltaTime);

                if (camera.transform.position == cameraPositions[1].transform.position)
                {
                    StartGame();
                    inMenu = false;
                }
            }
        }

	    if (startEndMenu == true)
	    {
	        camera.transform.position = Vector3.MoveTowards(camera.transform.position,
	            cameraPositions[2].transform.position, cameraSpeed * Time.deltaTime);

	        if (camera.transform.position == cameraPositions[2].transform.position)
	        {
	            UIEndGame.SetActive(true);

                if (MessageListener.Instance.buttonPressed == true)
	            {
	                BackToMenu();
	                UIEndGame.SetActive(false);
                    startEndMenu = false;
	            }
	        }
	    }

	    if (backToMenu == true)
	    {
	        camera.transform.position = Vector3.MoveTowards(camera.transform.position,
	            cameraPositions[0].transform.position, cameraSpeed * Time.deltaTime);

	        if (camera.transform.position == cameraPositions[0].transform.position)
	        {
	            UIMenu.SetActive(true);
	            inMenu = true;
                GameMaster.Instance.GameStart();
                backToMenu = false;
	        }
        }

	    if (Input.GetKey(KeyCode.Escape))
	    {
	       Application.Quit();
	    }
    }

    public void EndGame()
    {
        startEndMenu = true;
        UIGame.SetActive(false);
    }

    private void BackToMenu()
    {
        backToMenu = true;
    }

    private void StartGame()
    {
        startCameraMove = false;
        Counter.SetActive(true);
        StartCoroutine(CountStartGame());
    }

    public void GetWaterCounter(string timeLeft)
    {
        textFeedbackGame.text = "Haal water! " + timeLeft;
        textFeedbackGameShadow.text = "Haal water! " + timeLeft;
    }

    public void EmptyUIText()
    {
        textFeedbackGame.text = "";
        textFeedbackGameShadow.text = "";
    }

    //Spawn 2 wolken with an interval in het begin van het spel
    private IEnumerator CountStartGame()
    {
        counterText.text = "3";
        counterTextShadow.text = "3";
        yield return new WaitForSeconds(1f);
        counterText.text = "2";
        counterTextShadow.text = "2";
        yield return new WaitForSeconds(1f);
        counterText.text = "1";
        counterTextShadow.text = "1";
        yield return new WaitForSeconds(1f);
        counterText.text = "Start !";
        counterTextShadow.text = "Start !";
        yield return new WaitForSeconds(1f);
        counterText.text = "";
        counterTextShadow.text = "";
        UIGame.SetActive(true);
        GameMaster.Instance.StartGame();
    }
}
