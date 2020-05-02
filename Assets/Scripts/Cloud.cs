using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    //waardes
    private float speed;
    private int deletePos;

	// Use this for initialization
	void Start ()
    {
        if (this.transform.position.x < 0)
        {
            speed = Random.Range(0.5f, 1.5f);
            deletePos = 10;
        }
        else
        {
            speed = Random.Range(-0.5f, -1.5f);
            deletePos = -10;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		this.transform.Translate(speed * Time.deltaTime,0,0);

        if (deletePos == 10)
        {
            if (this.transform.position.x >= 10)
            {
                Delete();
            }
        }
        else
        {
            if (this.transform.position.x <= -10)
            {
                Delete();
            }
        }

        if (UIManager.Instance.backToMenu == true)
        {
            Destroy(this.gameObject);
        }
	}

    //wolk verwijderen en nieuwe spawnen
    private void Delete()
    {
        GameMaster.Instance.SpawnCloud();
        Destroy(this.gameObject);
    }
}
