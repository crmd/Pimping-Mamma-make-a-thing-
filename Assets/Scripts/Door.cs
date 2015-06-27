using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public enum RoomState
	{
		RS_UNOCCUPIED,
		RS_WAITING,
		RS_WITH_CLIENT,
		RS_CLIENT_TRYING_TO_LEAVE
	}

	public Sprite lightOff;						//RS_UNOCCUPIED
	public Sprite lightRed;						//RS_WITH_CLIENT
	public Sprite lightGreen;					//RS_WAITING
	private SpriteRenderer light;				//The sprite renderer attached to the light gameobject

	private Animator anim;						//The animator component on the door
	private RoomState roomState;				//The current state of the room
	private float timer;						//Current time that the state has run between intervals 

	//RS_WAITING
	public float idleInterval = 5.0f;			//How frequently during down time a girl considers leaving
	public float chanceToLeave = 20.0f;			//% chance to leave at each interval
	public float clientInterval = 3.0f;			//How frequently during down time a client has a chance of showing up
	public float chanceForClientEnter = 50.0f;	//% chance for a client to show up at each interval
	private float clientIntervalTimer;			//Current time that the room has been idle between each clientInterval

	//RS_WITH_CLIENT
	public float chanceForClientExit;			//% chance the client will leave at each interval
	public float chanceToPay;					//% chance the client will pay when they go to leave
	public int minPayment;						//The minimum amount of money that the client will pay
	public int maxPayment;						//The maximum amount of money that the client will pay
	public float leaveWithoutPayingTime;		//The amount of time the player has to get to a room once the client tries to leave without paying
	
	private GameObject interactPrompt;

	private bool playerAtDoor;
	private Animator npcAnim;

	private GameManager gm;

	private Animator scoreAnim;
	// Use this for initialization
	void Start () {
		light = transform.Find("Light").GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		interactPrompt = transform.Find("eKey").gameObject;
		npcAnim = transform.Find("NPC").GetComponent<Animator>();
		gm = GameObject.Find ("GameManager").GetComponent<GameManager>();
		scoreAnim = transform.Find("AddScore").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(gm.GetState() == GameState.GS_HIGHSCORES)
		{
			return;
		}
		switch(roomState)
		{
		case RoomState.RS_UNOCCUPIED:
			light.sprite = lightOff;
			//Nothing happens
			break;
		case RoomState.RS_WAITING:
			light.sprite = lightGreen;
			clientIntervalTimer -= Time.deltaTime;
			if(timer <= 0)
			{
				if(RandomPercent() < chanceToLeave)	//Room becomes unoccupied
				{
					anim.SetTrigger("ClientEnter");
					break;
				}
				timer = idleInterval;
			}
			if(clientIntervalTimer <= 0)
			{
				if(RandomPercent() > chanceForClientEnter)
				{
					//NPC enters room
					anim.SetTrigger("ClientEnter");
					npcAnim.SetTrigger("NPCCreepyGuyEnter");
					SetStateWithClient();
					break;
				}
				clientIntervalTimer = clientInterval;
			}
			break;
		case RoomState.RS_WITH_CLIENT:
			light.sprite = lightRed;
			if(timer <= 0)
			{
				if(RandomPercent() < chanceForClientExit)
				{
					if(RandomPercent() < chanceToPay)
					{
						SetStateWaiting();
						anim.SetTrigger("ClientLeave");
						npcAnim.SetTrigger("NPCCreepyGuyExit");
						gm.AddScore(CalcMoney());
						scoreAnim.SetTrigger("AddScore");
					}
					else
					{
						SetStateClientLeaving();
						anim.SetTrigger("ClientNoPay");
					}
					break;
				}
				timer = clientInterval;
			}
			break;
		case RoomState.RS_CLIENT_TRYING_TO_LEAVE:
			light.sprite = lightRed;
			if(timer <= 0)
			{
				//Client leaves the room
				SetStateWaiting();
				npcAnim.SetTrigger("NPCCreepyGuyExit");
				anim.SetTrigger("ClientLeave");
			}
			break;
		default:
			break;
		}
		timer -= Time.deltaTime;
	}

	void FixedUpdate()
	{
		if(playerAtDoor)
		{
			interactPrompt.SetActive(true);
		}
		else
		{
			interactPrompt.SetActive(false);
		}
		playerAtDoor = false;
	}

	int CalcMoney()
	{
		return Random.Range (minPayment, maxPayment);
	}

	void OnTriggerStay2D()
	{
		if(roomState == RoomState.RS_UNOCCUPIED || roomState == RoomState.RS_CLIENT_TRYING_TO_LEAVE)
		{
			playerAtDoor = true;
		}
	}

	float RandomPercent()
	{
		return Random.Range (0.0f, 100.0f);
	}

	public void ThrowOutClient()
	{
		//Get Money
		SetStateWaiting();
		gm.AddScore(CalcMoney());
		scoreAnim.SetTrigger("AddScore");
		anim.SetTrigger("ClientLeave");
		npcAnim.SetTrigger("NPCCreepyGuyNekkedExit");
	}

	public RoomState GetRoomState()
	{
		return roomState;
	}

//	public void PlayerAtDoor()
//	{
//		playerAtDoor = true;
//	}

	public void CallInGirl()
	{
		SetStateWaiting();
		anim.SetTrigger("ClientEnter");
	}

	public void SetStateWithClient()
	{
		timer = clientInterval;
		roomState = RoomState.RS_WITH_CLIENT;
	}
	public void SetStateUnoccupied()
	{
		roomState = RoomState.RS_UNOCCUPIED;
	}
	public void SetStateWaiting()
	{
		roomState = RoomState.RS_WAITING;
		timer = idleInterval;
		clientIntervalTimer = clientInterval;
	}
	public void SetStateClientLeaving()
	{
		roomState = RoomState.RS_CLIENT_TRYING_TO_LEAVE;
		timer = leaveWithoutPayingTime;

	}
}
