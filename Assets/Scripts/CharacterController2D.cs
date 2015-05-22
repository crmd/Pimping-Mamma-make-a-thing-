using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour {
	
	public float speed;				//
	private int curFloor = 0;		//
	public GameObject[] floors;		//
	public float offset;			//How far down from the centre point of a room should the player be positioned;
	public float leftWall = -2.2f;
	public float rightWall = 2.2f;
	private float xScale;
	
	public bool atSideDoor;
	
	private Animator anim;
	bool running;
	// Use this for initialization
	void Start () {
		transform.position = FloorPos();
		xScale = transform.localScale.x;
		anim = GetComponent<Animator>();
	}
	
	void FixedUpdate()
	{
		if(running)
		{
			anim.SetBool("running", true);
		}
		else
		{
			anim.SetBool("running", false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		running = false;
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			if(transform.position.x > leftWall)
			{
				transform.Translate(new Vector3(-1,0,0) * speed * Time.deltaTime);
			}
			transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
			running = true;
		}
		if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			if(transform.position.x < rightWall)
			{
				transform.Translate(new Vector3(1,0,0) * speed * Time.deltaTime);
			}
			transform.localScale = new Vector3(-xScale, transform.localScale.y, transform.localScale.z);
			running = true;
		}
		if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
		{
			if(atSideDoor)
			{
				if(curFloor < 2)
				{
					curFloor++;
				}
				transform.position = FloorPos();
			}
		}
		if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
		{
			if(atSideDoor)
			{
				if(curFloor > 0)
				{
					curFloor--;
				}
				transform.position = FloorPos();
			}
		}
	}
	
	void LateUpdate()
	{
		atSideDoor = false;
	}
	
	private Vector2 FloorPos()
	{
		return new Vector2(transform.position.x, floors[curFloor].transform.position.y - offset);
	}
	
	void OnTriggerStay2D(Collider2D col)
	{
		if(col.gameObject.tag == "SideDoor")
		{
			atSideDoor = true;
		}
		if(col.gameObject.tag == "Door")
		{
			Door door = col.gameObject.GetComponent<Door>();
			//door.PlayerAtDoor();
			if(door.GetRoomState() == Door.RoomState.RS_UNOCCUPIED)
			{
				//Display interact prompt
				if(Input.GetKey(KeyCode.E))
				{
					door.CallInGirl();
				}
			}
			else if(door.GetRoomState() == Door.RoomState.RS_CLIENT_TRYING_TO_LEAVE)
			{
				//Display interact prompt
				if(Input.GetKey(KeyCode.E))
				{
					door.ThrowOutClient();
				}
			}
		}
	}
}