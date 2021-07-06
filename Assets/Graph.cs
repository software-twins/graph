using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

[ RequireComponent (typeof (RectTransform)) ]

public class Graph : MonoBehaviour 
	{
		void Start ()
			{
				/** setting up background rect, then ... */
                GameObject background = _image ( new Vector4 (0.0f, 0.0f, 1.0f, 0.05f), new Vector2 (0.0f, 0.0f), 
                                                                                        new Vector2 (1.0f, 1.0f) );

				/** of the next two cycles, the parameters of the cycles are 
				 * 10 and 7 determine the number of grid lines; these values are still
				 * built-in and are not moved to parameters 
				 */
						
				/** ... as part of background setting up horizontal lines of value grid and ... */
				for ( int i = 0; i < 10 ; i ++  )
						_x (_axe (background, _rect.width / 10.0f * i), 0.05f + 0.10f * i);
				
				/**... as part of background setting up vertical lines of value grid; */
				for (int i = 0; i < 7; i ++ )
						_y (_axe (background, i * 5), 0.07f + 0.14f * i);

				_data_rect = _image ( new Vector4 (0.0f, 0.0f, 1.0f, 0.15f), new Vector2 (0.05f, 0.07f), 
                                                                             new Vector2 (0.95f, 0.91f) );
                            
                for ( int i = 0; i < 30; i ++ )
                       _points.AddLast (new LinkedListNode < GameObject > (_point (i / 30.0f, 0.5f + 0.3f * Mathf.Cos (i))));

                LinkedListNode < GameObject > item = _points.First;
                
                while ( item.Next != null )
                    {
                        _line (item.Value.GetComponent < RectTransform > ().anchorMin, item.Next.Value.GetComponent < RectTransform > ().anchorMin); 
                        item = item.Next;
                    }
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

		public Rect _rect = new Rect (0.0f, 0.0f, 2.0f, 100.0f);

        /** private method sections */	    
        private GameObject _image (Vector4 color, Vector2 min, Vector2 max)
            {
                GameObject b = new GameObject ("I." + color.ToString ());
                b.transform.SetParent (gameObject.transform, false);

                /** add image as background ... */
                b.AddComponent <Image> ().color = color; //new Vector4 (0.0f, 0.0f, 1.0f, 0.05f); 

                /** ... and setting for him anchors, for this first we receive rectTransform component... */
                RectTransform transform = b.GetComponent <RectTransform> ();

                /** ... and set anchors values; here we set the image to the full length and width of the
                 * parent component */
                transform.anchoredPosition = transform.pivot = transform.sizeDelta = new Vector2 (0.0f, 0.0f);              
                /** the anchor point is set to the bottom left corner, and since the anchors are anchored
                 *  to the edges of the parent object and the background image should overlap the
                 *  entire parent object, the property SizeDelta is set to 0.0f */
                transform.anchorMin = min; //new Vector2 (0.0f, 0.0f);
                transform.anchorMax = max; //new Vector2 (1.0f, 1.0f);

                return b;
            }

    	private GameObject _axe (GameObject parent, float value)
			{
				GameObject axe = new GameObject ();
				axe.transform.SetParent (parent.transform, false); 

				GameObject line = new GameObject ("Image");
				line.transform.SetParent (axe.transform, false);
				
				line.AddComponent <Image> ().color = Color.red;
						
				GameObject text = new GameObject ("Signature");
				text.transform.SetParent (axe.transform, false);

				Text t = text.AddComponent <Text> ();
				
				t.fontSize = 9;       			
				t.alignment = TextAnchor.MiddleCenter;
				t.font = (Font) Resources.GetBuiltinResource (typeof (Font), "Arial.ttf");
				t.text = value.ToString ();

				return axe;
			}

		private void _x (GameObject source, float position)
			{
				source.name = "X.Line." + position.ToString ();

				RectTransform transform;

				transform = source.AddComponent <RectTransform> ();
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
						
				transform = source.transform.GetChild (0).GetComponent <RectTransform> ();
				
				/** the anchor point is defined as the midpoint of the bottom edge of the container
				 *  rectangle along the x-axis */
				transform.pivot = new Vector2 (0.5f, 0.5f); 
				/** the x-dimension is defined as the width of the axis label text;
				 *  15.0 matches Arial font size 9 */
				transform.sizeDelta = new Vector2 (1.0f, -20.0f); 
				
				/** determine the position of the grid line along the y-axis and snap its edges to the
				 *  height of the parent element; for this, the y coordinate for the minimum anchor is
				 *  defined as 0.0f ... */
				transform.anchorMin = new Vector2 (0.5f, 0.0f);
				/** ... and for maximu anchor as 1.0f */
				transform.anchorMax = new Vector2 (0.5f, 1.0f);

				transform = source.transform.GetChild (1).GetComponent <RectTransform> ();
				
				/** the anchor point is defined as the midpoint of the bottom edge of the container
				 *  rectangle along the x-axis */
				//	transform.pivot = new Vector2 (0.5f, 0.0f); 
				/** the x-dimension is defined as the width of the axis label text;
				 *  15.0 matches Arial font size 9 */
				transform.sizeDelta = new Vector2 (0.0f, 15.0f); 
				
				/** determine the position of the grid line along the y-axis and snap its edges to the
				 *  height of the parent element; for this, the y coordinate for the minimum anchor is
				 *  defined as 0.0f ... */
				transform.anchorMin = new Vector2 (0.0f, 0.0f);//position, 0.0f);
				/** ... and for maximu anchor as 1.0f */
				transform.anchorMax = new Vector2 (1.0f, 0.0f);
			}

		private void _y (GameObject source, float position)
			{
				source.name = "Y.Line." + position.ToString ();

				RectTransform transform;

				transform = source.AddComponent <RectTransform> ();
				/** the anchor point is defined as the midpoint of the bottom edge of the container
				 *  rectangle along the x-axis */
				transform.pivot = new Vector2 (0.0f, 0.5f); 
				/** the x-dimension is defined as the width of the axis label text;
				 *  15.0 matches Arial font size 9 */
				transform.sizeDelta = new Vector2 (0.0f, 15.0f); 

				/** determine the position of the grid line along the y-axis and snap its edges to the
				 *  height of the parent element; for this, the y coordinate for the minimum anchor is
				 *  defined as 0.0f ... */
				transform.anchorMin = new Vector2 (0.0f, position);
				/** ... and for maximu anchor as 1.0f */
				transform.anchorMax = new Vector2 (1.0f, position);
						
				transform = source.transform.GetChild (0).GetComponent <RectTransform> ();
				
				/** the anchor point is defined as the midpoint of the bottom edge of the container
				 *  rectangle along the x-axis */
				transform.pivot = new Vector2 (0.5f, 0.5f); 
				/** the x-dimension is defined as the width of the axis label text;
				 *  15.0 matches Arial font size 9 */
				transform.sizeDelta = new Vector2 (-20.0f, 1.0f); 
				
				/** determine the position of the grid line along the y-axis and snap its edges to the
				 *  height of the parent element; for this, the y coordinate for the minimum anchor is
				 *  defined as 0.0f ... */
				transform.anchorMin = new Vector2 (0.0f, 0.5f);
				/** ... and for maximu anchor as 1.0f */
				transform.anchorMax = new Vector2 (1.0f, 0.5f);

				transform = source.transform.GetChild (1).GetComponent <RectTransform> ();
				
				/** the anchor point is defined as the midpoint of the bottom edge of the container
				 *  rectangle along the x-axis */
				//	transform.pivot = new Vector2 (0.5f, 0.0f); 
				/** the x-dimension is defined as the width of the axis label text;
				 *  15.0 matches Arial font size 9 */
				transform.sizeDelta = new Vector2 (15.0f, 0.0f); 
				
				/** determine the position of the grid line along the y-axis and snap its edges to the
				 *  height of the parent element; for this, the y coordinate for the minimum anchor is
				 *  defined as 0.0f ... */
				transform.anchorMin = new Vector2 (0.0f, 0.0f);//position, 0.0f);
				/** ... and for maximu anchor as 1.0f */
				transform.anchorMax = new Vector2 (0.0f, 1.0f);
			}

        private GameObject _point (float x, float y)
            {
                GameObject point = new GameObject ("Point." + x.ToString () + y.ToString ());
                point.transform.SetParent (_data_rect.transform, false);
            
                CircleGraphic circle = point.AddComponent <CircleGraphic> ();

                circle.detail = 8;
                circle.mode = CircleGraphic.Mode.FillInside;

                RectTransform transform = point.GetComponent <RectTransform> ();
              
                transform.anchorMin = transform.anchorMax = new Vector2 (x, y);
                transform.pivot = new Vector2 (0.5f, 0.5f);
                transform.sizeDelta = new Vector2 (4.0f, 4.0f);
            
                return point;
            }

        private GameObject _line (Vector2 a, Vector2 b)
            {
                GameObject line = new GameObject ("Line", typeof (Image)); 
                line.transform.SetParent (_data_rect.transform, false);

                line.GetComponent < Image > ().color = new Color (0.0f, 1.0f, 0.0f, 0.5f); 
                
                RectTransform transform = line.GetComponent < RectTransform > ();
                transform.anchorMin = transform.anchorMax = a; //new Vector2 (0.f, 0.5f);
               // transform.anchoredPosition = new Vector2 (0.5f, 0.5f); //a;// + direction * distance * 0.5f;

                transform.sizeDelta = new Vector2 (150.0f, 3.0f);
                Debug.Log (a);
              
              //  transform.localEulerAngles = new Vector3 (0.0f, 0.0f, GetAngleFromVectorFloat (direction));
                
                return line;
            }

        private float GetAngleFromVectorFloat (Vector3 dir)
            {
                dir = dir.normalized;

                float n = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
                if ( n < 0 ) 
                        n += 360;
               
                return n;
            }

          /** field section */
        private GameObject _data_rect;

        private LinkedList < GameObject > _points = new LinkedList < GameObject > ();
	}