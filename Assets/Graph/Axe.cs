using System;

using UnityEngine;
using UnityEngine.UI;

[ RequireComponent (typeof (RectTransform)) ]

public class Axe : MonoBehaviour
	{
		private GameObject line, text;
				
		void Awake ()
			{
				(line = new GameObject ()).name = "Line";
				
				line.transform.SetParent (gameObject.transform, false);
				line.AddComponent <Image> ().color = Color.red;

				RectTransform transform = line.GetComponent <RectTransform> ();
				transform.anchoredPosition = new Vector2 (0.0f, 0.0f); 
transform.pivot = new Vector2 (0.5f, 0.0f); 
				
				transform.anchorMin = new Vector2 (0.0f, 0.0f);
				transform.anchorMax = new Vector2 (0.0f, 1.0f);

				transform.sizeDelta = new Vector2 (1.0f, 0.0f); 

				(text = new GameObject ()).name = "Signature";

				text.transform.SetParent (gameObject.transform, false);

				Text t = text.AddComponent <Text> ();
				
       			t.font = (Font) Resources.GetBuiltinResource (typeof (Font), "Arial.ttf");
        		t.text = "P";
       			t.fontSize = 18;
        		t.alignment = TextAnchor.MiddleCenter;

      			 // Provide Text position and size using RectTransform.
      	        transform = t.GetComponent <RectTransform> ();
        		
				transform.anchoredPosition = transform.pivot = new Vector2 (0.0f, 0.0f); 
				
				transform.anchorMin = new Vector2 (0.0f, 0.0f);
				transform.anchorMax = new Vector2 (1.0f, 0.0f);

				//transform.sizeDelta = new Vector2 (1.0f, 0.0f); 
			}

		private float angle = 0.0f;
			
		public Axe direction (Vector2 d)
			{
				_direction = d;
				return this;
			}

		private Vector2 _direction;
	}
	

