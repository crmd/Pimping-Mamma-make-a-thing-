using UnityEngine;
using System.Collections;

public class SideDoor : MonoBehaviour {

	public bool canGoUp;
	public bool canGoDown;

	private GameObject wButton;
	private GameObject sButton;

	private bool playerAtDoor;

	// Use this for initialization
	void Start () {
		wButton = transform.Find("wButton").gameObject;
		sButton = transform.Find("sButton").gameObject;
	}

	void FixedUpdate()
	{
		if(playerAtDoor)
		{
			if(canGoUp)
			{
				wButton.SetActive(true);
			}
			if(canGoDown)
			{
				sButton.SetActive(true);
			}
			playerAtDoor = false;
		}
		else
		{
			wButton.SetActive(false);
			sButton.SetActive(false);
		}
	}

	void OnTriggerStay2D()
	{
		playerAtDoor = true;
	}
}
