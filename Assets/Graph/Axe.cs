using System;

using UnityEngine;
using UnityEngine.UI;

public class Axe : MonoBehaviour
	{
		private GameObject line, text;
				
		void Awake ()
			{
				(line = new GameObject ()).name = "Line";
				
				line.transform.parent = gameObject.transform;
				line.AddComponent <Image> ().color = Color.red;
	
				(text = new GameObject ()).name = "Signature";

				text.transform.parent = gameObject.transform;
				Text t = text.AddComponent <Text> ();
				
				t.color = Color.green;
				t.text = "100";
			}

		private float angle = 0.0f;
			
		public void geometry (Vector2 position, float length)
			{
				RectTransform transform = line.GetComponent <RectTransform> ();

				transform.anchoredPosition =  Vector2.Scale (position, transform.localScale);
				transform.sizeDelta = new Vector2 (length, 2f);
				transform.localEulerAngles = new Vector3 (0, 0, 90);

				//transform = text.GetComponent <RectTransform> ();
				
				//transform.anchoredPosition =  new Vector2 (rect.width / 2.0f, rect.height / 2.0f);
				//transform.sizeDelta = new Vector2 (rect.height, 2f);
				//transform.localEulerAngles = new Vector3 (0, 0, angle);
			}

		public Axe direction (Vector2 d)
			{
				_direction = d;
				return this;
			}

		private Vector2 _direction;
	}
	

