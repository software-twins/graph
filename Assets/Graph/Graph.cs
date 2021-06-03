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

				/** add image as background ... */
				background.AddComponent <Image> ().color = new Vector4 (0.0f, 0.0f, 1.0f, 0.05f); 

				/** ... and setting for him anchors, for this first we receive rectTransform component... */
				RectTransform transform = background.GetComponent <RectTransform> ();

				/** ... and set anchors values; here we set the image to the full length and width of the
				 * parent component */
				transform.anchoredPosition = transform.pivot = transform.sizeDelta = new Vector2 (0.0f, 0.0f); 
				
				/** the anchor point is set to the bottom left corner, and since the anchors are anchored
				 *  to the edges of the parent object and the background image should overlap the
				 *  entire parent object, the property SizeDelta is set to 0.0f */
				transform.anchorMin = new Vector2 (0.0f, 0.0f);
				transform.anchorMax = new Vector2 (1.0f, 1.0f);

				/** add an object that will contain the grid lines ... */
				(axes = new GameObject ()).name = "Axes";
				axes.transform.SetParent (gameObject.transform, false);

				/** ... and add a script to it that implements the functions of the container of the grid lines;
				 *  for script elements, anchors are set in a separate method, which is implemented in the
				 *  script; for the background, this is not possible because the component is built-in, you can
				 *  create the Background class and implement this method in it, or you can leave it that way */
				axes.AddComponent <Axes> ().anchors ();
			}

		public Graph anchors (Vector2 min, Vector2 max)
			{
				RectTransform transform = GetComponent <RectTransform> ();

				transform.anchoredPosition = new Vector2 (10.0f, 10.0f); 
				transform.pivot = new Vector2 (0.0f, 0.0f);

				transform.anchorMin = min;
				transform.anchorMax = max;

				transform.sizeDelta = new Vector2 (-20.0f, -20.0f);
				
				return this;
			}
	}