using System;

using UnityEngine;
using UnityEngine.UI;

[ RequireComponent (typeof (RectTransform)) ]

public abstract class Axe : MonoBehaviour
	{
		protected GameObject _line, _text;
						
		void Awake ()
			{
				(_line = new GameObject ()).name = "Line";				
				_line.transform.SetParent (gameObject.transform, false);
				
				_line.AddComponent <Image> ().color = Color.red;

				(_text = new GameObject ()).name = "Signature";
				_text.transform.SetParent (gameObject.transform, false);

				Text t = _text.AddComponent <Text> ();
				
				t.fontSize = 9;       			
				t.alignment = TextAnchor.MiddleCenter;
				t.font = (Font) Resources.GetBuiltinResource (typeof (Font), "Arial.ttf");
			}
		
		abstract public Axe anchors (float position);

		/** not used yet, this is a candidate for an exception */
		public Axe value (float v)
			{
				_text.GetComponent <Text> ().text = v.ToString (); 
				return this;
			}
	}

public class XAxe : Axe
	{
		override public Axe anchors (float position)
			{
				/** setting anchors for the axis container, which includes a line and a caption to it;
				 *  they will be aligned below ... */
				RectTransform transform = GetComponent <RectTransform> ();
				
				/** the anchor point is defined as the midpoint of the bottom edge of the container
				 *  rectangle along the x-axis */
				transform.pivot = new Vector2 (0.5f, 0.0f); 
				/** the x-dimension is defined as the width of the axis label text;
				 *  15.0 matches Arial font size 9 */
				transform.sizeDelta = new Vector2 (15.0f, 0.0f); 
				
				/** determine the position of the grid line along the y-axis and snap its edges to the
				 *  height of the parent element; for this, the y coordinate for the minimum anchor is
				 *  defined as 0.0f ... */
				transform.anchorMin = new Vector2 (position, 0.0f);
				/** ... and for maximu anchor as 1.0f */
				transform.anchorMax = new Vector2 (position, 1.0f);
		
				/** ... now we bind the elements that the container contains - the line and the caption
				 *  to it; their parent is the container; get the line component */
				transform = _line.GetComponent <RectTransform> ();
				
				/** we anchor it in the center of the height so that the same distance
				 *  from the edges of the parent container will recede from the top and bottom */
				transform.pivot = new Vector2 (0.0f, 0.5f);
				
				/** stretch the line from the center to the top and bottom of the parent container ... */
				transform.anchorMin = new Vector2 (0.5f, 0.0f);
				/** ... at the center of the face, which is located along the x-axis */
				transform.anchorMax = new Vector2 (0.5f, 1.0f);

				/** the length of the line is determined by 20.0 less than the length of the container
				 *  for the axis */
				transform.sizeDelta = new Vector2 (1.0f, -20.0f);

				/** ... now get the line signature component */
				transform = _text.GetComponent <RectTransform> (); 

				/** bind the caption to the line across the entire width of the container, and
				 * the height is determined by the height of the text how to get it */
				transform.anchorMin = new Vector2 (0.0f, 0.0f);
				transform.anchorMax = new Vector2 (1.0f, 0.0f);

				/** the width of the caption corresponds to the Arial font and its size 9,
				 *  it also determines the width of the parent container */
				transform.sizeDelta = new Vector2 (0.0f, 15.0f); 
				
				return this;
			}	
	}
	
public class YAxe : Axe
	{
	override public Axe anchors (float position)
			{	
				/** setting anchors for the axis container, which includes a line and a caption to it;
				 *  they will be aligned below ... */
				RectTransform transform = GetComponent <RectTransform> ();

				/** the anchor point is defined as the midpoint of the bottom edge of the container
				 *  rectangle along the y-axis */
				transform.pivot = new Vector2 (0.0f, 0.5f); 
				/** the x-dimension is defined as the width of the axis label text;
				 *  15.0 matches Arial font size 9 */
				transform.sizeDelta = new Vector2 (0.0f, 10.0f); 
				
				/** determine the position of the grid line along the y-axis and snap its edges to the
				 *  height of the parent element; for this, the y coordinate for the minimum anchor is
				 *  defined as 0.0f ... */
				transform.anchorMin = new Vector2 (0.0f, position);
				/** ... and for maximu anchor as 1.0f */
				transform.anchorMax = new Vector2 (1.0f, position);

				/** ... now we bind the elements that the container contains - the line and the caption
				 *  to it; their parent is the container; get the line component */
				transform = _line.GetComponent <RectTransform> ();

				/** we anchor it in the center of the height so that the same distance
				 *  from the edges of the parent container will recede from the top and bottom */
				transform.pivot = new Vector2 (0.5f, 0.0f);
				
				/** stretch the line from the center to the top and bottom of the parent container ... */
				transform.anchorMin = new Vector2 (0.0f, 0.5f);
				/** ... at the center of the face, which is located along the y-axis */
				transform.anchorMax = new Vector2 (1.0f, 0.5f);

				/** the length of the line is determined by 20.0 less than the length of the container
				 *  for the axis */
				transform.sizeDelta = new Vector2 (-20.0f, 1.0f); 

				/** ... now get the line signature component */
				transform = _text.GetComponent <RectTransform> (); 

				/** bind the caption to the line across the entire width of the container, and
				 * the height is determined by the height of the text how to get it */
				transform.anchorMin = new Vector2 (0.0f, 0.0f);
				transform.anchorMax = new Vector2 (0.0f, 1.0f);

				/** the width of the caption corresponds to the Arial font and its size 9,
				 *  it also determines the width of the parent container */
				transform.sizeDelta = new Vector2 (10.0f, 0.0f); 
					
				return this;
			}
	}

