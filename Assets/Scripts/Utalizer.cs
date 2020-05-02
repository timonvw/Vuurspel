using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using UnityEngine.UI;

public class Utalizer : MonoBehaviour
{
    #region Singleton
    private static Utalizer _instance;
    public static Utalizer Instance
    {
        get
        {
            if (_instance == null)
            {
                //uit nog niet nodig
                //GameObject go = new GameObject("Utalizer");
                //go.AddComponent<Utalizer>();
            }

            return _instance;
        }
    }
    #endregion

    //vars sonar
    private readonly string dataPathSonar = "../Data/sonardata.txt";
    [SerializeField] private string textSonarData;

    //get set vars
    public bool DisablePressure { get; set; }
    public bool DisableSonar { get; set; }

    //vars pressure
    private readonly string dataPathPressure = "../Data/pressuredata.txt";
    [SerializeField] private string textPressureData;

    void Awake()
    {
        //start values
        _instance = this;
        DisablePressure = false;
        DisableSonar = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (DisableSonar)
        {
             SonarHandler();
        }

        if (DisablePressure)
        {
            PressureHandler();
        }
    }

    private void SonarHandler()
    {
        //read distance in cm from file
        textSonarData = System.IO.File.ReadAllText(dataPathSonar);
    }

    private void PressureHandler()
    {
        //read button pressed from file
        textPressureData = System.IO.File.ReadAllText(dataPathPressure);
    }

    public int GetDistance()
    {
        return int.Parse(textSonarData);
    }

    public bool GetButtonPressed()
    {
        if (textPressureData == "true")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
