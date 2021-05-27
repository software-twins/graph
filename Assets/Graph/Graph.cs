using System;

using UnityEngine;
using UnityEngine.UI;

public class Graph : CanvasElement
	{
		private GameObject background, axes;
			
		int points;

		void Awake ()
			{
				//GameObject obj;
				(background = new GameObject ()).name = "Background";
				
				background.transform.parent = gameObject.transform;
				background.AddComponent <Image> ().color = new Vector4 (0.0f, 0.0f, 1.0f, 0.05f); 

				(axes = new GameObject ()).name = "AxesContainer";
				
				axes.transform.parent = gameObject.transform;
				axes.AddComponent <Axes> ().initialize ();
			}

		override public void geometry (Rect rect)
			{
				/** provide text position and size using RectTransform */
        		RectTransform transform = background.GetComponent <RectTransform> ();
							
				/** set background box dimensions */
				transform.sizeDelta = new Vector2 (rect.width / 2.2f, rect.height / 2.2f);
				/** set background position */
				/** transform.localPosition = new Vector3 (rect.width / 2.0f, rect.height / 2.0f, 0.0f); */
				transform.position = Vector3.Scale (new Vector3 (rect.width / 4.0f, rect.height / 4.0f, 0.0f),
													transform.localScale);
			
				axes.GetComponent <Axes> ().geometry (rect);
			}

		public Graph initialize ()
			{
				return this;
			}
	}