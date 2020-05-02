using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    private Vector3 movePosition;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position,
            new Vector3(movePosition.x, movePosition.y + 0.1f, movePosition.z), 3 * Time.deltaTime);

        this.transform.localScale -= new Vector3(0.008F, 0.008f, 0.008f);

        if (this.transform.position == new Vector3(movePosition.x, movePosition.y + 0.1f, movePosition.z))
        {
            Destroy(this.gameObject);
        }
    }

    public void SetPosition(Vector3 position)
    {
        movePosition = position;
    }
}
