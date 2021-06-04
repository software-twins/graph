using System;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ RequireComponent (typeof (RectTransform)) ]

public class Axes : MonoBehaviour
	{
		private GameObject [] _y_axes, _x_axes;
		
		void Awake ()
			{
				_y_axes = new GameObject [9];
				
				for ( int i = 0; i < _y_axes.Length; i ++ )
					{
						(_y_axes [i] = new GameObject ()).name = "YAxis." + i.ToString ();
						_y_axes [i].transform.SetParent (gameObject.transform, false);
						
						float position = 0.05f + (float) i / _y_axes.Length;
						_y_axes [i].AddComponent <XAxe> ().value (i * 100.0f).anchors (position);
					}
						
				_x_axes = new GameObject [9];

				for ( int i = 0; i < _x_axes.Length; i ++ )
					{
						(_x_axes [i] = new GameObject ()).name = "XAxis." + i.ToString ();
						_x_axes [i].transform.SetParent (gameObject.transform, false);

						float position = 0.05f + (float) i / _x_axes.Length;
						_x_axes [i].AddComponent <YAxe> ().value (i * 20.0f).anchors (position);
					}
			}
	
		public Axes anchors ()
			{
				RectTransform transform = GetComponent <RectTransform> ();

				transform.anchoredPosition = transform.pivot = transform.sizeDelta = new Vector2 (0.0f, 0.0f); 
				
				transform.anchorMin = new Vector2 (0.0f, 0.0f);
				transform.anchorMax = new Vector2 (1.0f, 1.0f);
				
				return this;
			}

		public Axes value (float x, float y)
			{
				return this;
			}
	
	}