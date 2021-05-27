using System;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Axes : MonoBehaviour
	{
		private GameObject [] y_axes;
		
		void Awake ()
			{
				y_axes = new GameObject [10];
				Debug.Log ("Start " + y_axes + "-" + y_axes.Length);
				
				for ( int i = 0; i < y_axes.Length; i ++ )
					{
						(y_axes [i] = new GameObject ()).name = "YAxis." + i.ToString ();
						
						y_axes [i].transform.parent = gameObject.transform;
						y_axes [i].AddComponent <Axe> ().direction (new Vector2 (1.0f, 0.0f)); 
					}
			}

		public void geometry (Rect rect)
			{
				float length = rect.height - 10;

				Debug.Log (y_axes + "-");
				/** provide text position and size using RectTransform */
		int i = 0;
        		foreach (GameObject item in y_axes)
					{
						Debug.Log ("! " + item + " " + item.GetComponent <Axe> ());
						
						Vector2 v  = new Vector2 (rect.width / y_axes.Length * i ++ , 0.0f);					
	
						item.GetComponent <Axe> ().geometry (v, length);
					}
						
			}
	
		public Axes initialize ()
			{
				return this;
			}
	}