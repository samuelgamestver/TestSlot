using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
	public List<GameObject> PointList;

	public float speed = 1f;

	public bool pusk;

	public bool muve;

	int pointcount;


	public void FirstPos()
	{
		transform.localPosition = PointList[0].transform.localPosition;
	}

	void FixedUpdate()
	{
		if(pusk)
		{
			if(transform.localPosition != PointList[pointcount].transform.localPosition)
			{
				transform.localPosition = Vector3.MoveTowards(transform.localPosition, PointList[pointcount].transform.localPosition, speed);
			}
			else
			{
				if(muve)
				{
					if(pointcount < PointList.Count-1)
						pointcount++;
					else
					{
						transform.parent.GetComponent<Colomn>().GeneratedObject.RemoveAt(0);
						Destroy(gameObject);
					}
				}
				else 
				{
					if (pointcount < 1)
						pointcount = 1;
					else
						pusk = false;
				}

					//pusk = false;
			}
		}

			
	}
}
		