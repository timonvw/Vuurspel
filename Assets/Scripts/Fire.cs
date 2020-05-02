using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //a mimmickt het verder staan dan bepaalde waarde
        //s mimmickt de druk waarde

        if (MessageListener.Instance.buttonPressed == true && GameMaster.Instance.DimAllowed == true)
        {
            GameMaster.Instance.SpawnWater(this.transform.position);
            GameMaster.Instance.SetWaterSplach(true);

            this.transform.localScale -= new Vector3(GameMaster.Instance.fireDimTime, GameMaster.Instance.fireDimTime, GameMaster.Instance.fireDimTime);

            if (this.transform.localScale.x <= 0.05f)
            {
                GameMaster.Instance.SetWaterSplach(false);
                GameMaster.Instance.score += 69 * System.DateTime.Now.Second;
                GameMaster.Instance.fireDimmedCounter += 1;
                GameMaster.Instance.getWater = true;
                GameMaster.Instance.SetSettings();
                
                //fire spawnen
                GameMaster.Instance.DimAllowed = false;
                GameMaster.Instance.SpawnFire();

                Destroy(this.gameObject);
            }
            //0.05
        }
        else
        {
            this.transform.localScale += new Vector3(GameMaster.Instance.fireDimTime, GameMaster.Instance.fireDimTime, GameMaster.Instance.fireDimTime);

            if (this.transform.localScale.x >= 0.65f)
            {
                GameMaster.Instance.EndGame();
                GameMaster.Instance.getWater = false;
                UIManager.Instance.EmptyUIText();
                Destroy(this.gameObject);
            }

            GameMaster.Instance.SetWaterSplach(false);
        }
    }
}
