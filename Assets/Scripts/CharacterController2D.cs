using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour {

	public float speed;				//
	private int curFloor = 0;		//
	public GameObject[] floors;		//
	public float offset;			//How far down from the centre point of a room should the player be positioned;
	// Use this for initialization
	void Start () {
		transform.position = FloorPos();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Translate(new Vector3(-1,0,0) * speed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			transform.Translate(new Vector3(1,0,0) * speed * Time.deltaTime);
		}
		if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
		{
			if(curFloor < 2)
			{
				curFloor++;
			}
			transform.position = FloorPos();
		}
		if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
		{
			if(curFloor > 0)
			{
				curFloor--;
			}
			transform.position = FloorPos();
		}
	}

	private Vector2 FloorPos()
	{
		return new Vector2(transform.position.x, floors[curFloor].transform.position.y - offset);
	}
}
