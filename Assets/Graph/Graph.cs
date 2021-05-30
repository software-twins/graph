using System;

using UnityEngine;
using UnityEngine.UI;

[ RequireComponent (typeof (RectTransform)) ]

public class Graph : CanvasElement
	{
		private GameObject background, axes;
			
		int points;

		void Awake ()
			{
				//GameObject obj;
				(background = new GameObject ()).name = "Background";
				
				background.transform.SetParent (gameObject.transform, false);
				background.AddComponent <Image> ().color = new Vector4 (0.0f, 0.0f, 1.0f, 0.05f); 

				RectTransform transform = background.GetComponent <RectTransform> ();

				transform.anchoredPosition = transform.pivot = transform.sizeDelta = new Vector2 (0.0f, 0.0f); 
				
				transform.anchorMin = new Vector2 (0.0f, 0.0f);
				transform.anchorMax = new Vector2 (1.0f, 1.0f);

				(axes = new GameObject ()).name = "Axes";
				
				axes.transform.SetParent (gameObject.transform, false);
				axes.AddComponent <Axes> ().initialize ();

				transform = axes.GetComponent <RectTransform> ();

				transform.anchoredPosition = transform.pivot = transform.sizeDelta = new Vector2 (0.0f, 0.0f); 
				
				transform.anchorMin = new Vector2 (0.0f, 0.0f);
				transform.anchorMax = new Vector2 (1.0f, 1.0f);
			}

		public Graph initialize (Canvas canvas)
			{
				//background.transform.parent = canvas.transform;				
				return this;
			}
	}