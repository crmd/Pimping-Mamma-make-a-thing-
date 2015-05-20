using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public float chanceToPay;
	public int minPayment;
	public int maxPayment;
	public Sprite lightOff;
	public Sprite lightOn;
	private SpriteRenderer light;
	public float maxUseDuration;
	private float timer;
	private bool inUse;
	public float maxTimeBeforeUse;
	private Animator anim;

	// Use this for initialization
	void Start () {
		light = transform.Find("Light").GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		timer = Random.Range(0.0f, maxTimeBeforeUse);
	}
	
	// Update is called once per frame
	void Update () {
		if(timer <= 0)
		{
			if(inUse)
			{
				//NPC leaves room
				anim.SetTrigger("NPCLeave");
				light.sprite = lightOff;
				if(Random.Range (0.0f, 1.0f) > chanceToPay)
				{
					//Pay out all the hella cash money
					//Random.Range (minPayment, maxPayment);
				}
				else
				{
					//They don't pay
				}
				timer = Random.Range (0.0f, maxTimeBeforeUse);
				inUse = false;
			}
			else
			{
				//NPC enters room
				anim.SetTrigger("NPCEnter");
				light.sprite = lightOn;
				timer = Random.Range (0.0f, maxUseDuration);
				inUse = true;
			}
		}
		timer -= Time.deltaTime;
	}
}
