using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colomn : MonoBehaviour
{
	[SerializeField] GameObject[] PrefSlots;

	public List<GameObject> Points;

	public List<GameObject> GeneratedObject;

	private bool play;

	[SerializeField] int Speed = 5;

	private int frame;

	[SerializeField] float TimeWork = 5;

	private float TimeToStop;

	[SerializeField] bool checkResult;

	public static Action CheckResult;

    void Start()
    {
		for (int p = 0; p < gameObject.transform.childCount; p++)
		{
			if(gameObject.transform.GetChild(p).tag == "Point")
				Points.Add(gameObject.transform.GetChild(p).gameObject);
		}
    }


	public void Pusk()
	{
		if(GeneratedObject.Count >0)
		{
			for(int i = 0; i<GeneratedObject.Count; i++)
				Destroy(GeneratedObject[i]);

			GeneratedObject.Clear();
		}

		//GeneratedObject.Clear();

		play = true;

		TimeToStop = Time.realtimeSinceStartup + TimeWork;
	}

	void FixedUpdate()
    {
        if(play)
		{
			frame++;

			if(frame == Speed)
			{
				int rand = UnityEngine.Random.Range(0, PrefSlots.Length);

				GameObject InsSlot = Instantiate(PrefSlots[rand]);

				InsSlot.transform.SetParent(gameObject.transform);
				//InsSlot.transform.localPosition = Vector3.zero;
				InsSlot.transform.localScale = Vector3.one;

				GeneratedObject.Add(InsSlot);

				for (int i = 0; i < Points.Count; i++)
				{
					InsSlot.GetComponent<Slot>().PointList.Add(Points[i]);
				}

				InsSlot.GetComponent<Slot>().speed = 400/Speed;

				InsSlot.GetComponent<Slot>().FirstPos();

				InsSlot.GetComponent<Slot>().pusk = true;
				InsSlot.GetComponent<Slot>().muve = true;

				frame = 0;
			}

			if (Time.realtimeSinceStartup > TimeToStop)
			{
				play = false;

				for (int gen = 0; gen < GeneratedObject.Count; gen++)
				{
					GeneratedObject[gen].GetComponent<Slot>().muve = false;
				}

				if(checkResult)
					CheckResult?.Invoke();
			}
		}
    }
}
