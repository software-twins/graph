using System;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ RequireComponent (typeof (RectTransform)) ]

public class Axes : MonoBehaviour
	{
		private GameObject [] y_axes, x_axes;
		
		void Awake ()
			{
				y_axes = new GameObject [9];
				
				for ( int i = 0; i < y_axes.Length; i ++ )
					{
						(y_axes [i] = new GameObject ()).name = "YAxis." + i.ToString ();
						y_axes [i].transform.SetParent (gameObject.transform, false);
						
						Axe a = y_axes [i].AddComponent <XAxe> ().anchors ((i + 1) / 10.0f);
					}
						
				x_axes = new GameObject [9];

				for ( int i = 0; i < x_axes.Length; i ++ )
					{
						(x_axes [i] = new GameObject ()).name = "XAxis." + i.ToString ();
						x_axes [i].transform.SetParent (gameObject.transform, false);
						
						Axe a = x_axes [i].AddComponent <YAxe> ().anchors ((i + 1) / 10.0f);
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
	}