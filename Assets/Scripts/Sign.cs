using UnityEngine;
using System.Collections;

public class Sign : MonoBehaviour {

	public float minFlashTime;
	public float maxFlashTime;
	private float timer;
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(timer <= 0)
		{
			anim.SetTrigger("Flash");
			timer = Random.Range(minFlashTime, maxFlashTime);
		}
		timer -= Time.deltaTime;
	}
}
