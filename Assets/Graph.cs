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
                GameObject background = _image ( new Vector4 (0.0f, 0.0f, 1.0f, 0.05f), new Vector2 (0.0f, 0.0f), new Vector2 (1.0f, 1.0f) );

				/** of the next two cycles, the parameters of the cycles are 
				 * 10 and 7 determine the number of grid lines; these values are still
				 * built-in and are not moved to parameters 
				 */
						
				/** ... as part of background setting up horizontal lines of value grid and ... */
				for ( int i = 0; i < 10 ; i ++  )
						_x (_axe (background, _rect.width / 10.0f * i), 0.05f + 0.10f * i);
				
				/**... as part of background setting up vertical lines of value grid; */
				for (int i = 0; i < 7; i ++ )
                        _y (_axe (background, _rect.height / 7.0f * i), 0.07f + /** max value  */ 98.0f / _rect.height / 7.0f * i);

                //Debug.Log ("----" + _rect.height);

				_data_rect = _image ( new Vector4 (0.0f, 0.0f, 1.0f, 0.15f), new Vector2 (0.05f, 0.07f), new Vector2 (0.95f, 0.91f) );
                            
                for ( int i = 0; i < 10; i ++ )
                        _points.AddLast (new LinkedListNode < GameObject > (_point (i / 10.0f, _values [i] / _rect.height))); // 0.5f + 0.3f * Mathf.Cos (i))));

                LinkedListNode < GameObject > item = _points.First;

               // int j = 0;

              /*  RectTransform transform = _data_rect.GetComponent < RectTransform > ();
                
                while ( item.Next != null )
                    {
                         Vector2 a = new Vector2 (50 + j * 50 , ( _values [j] / 100.0f ) * transform.sizeDelta.y);
                         Vector2 b = new Vector2 (50 + (j + 1) * 50, ( _values [j + 1] / 100.0f ) * transform.sizeDelta.y);
                        
                        Debug.Log (" while loop " + a + " " + b );                           
 
                        _line ( a, b ); 
                        item = item.Next;
                        j ++ ;
                    }*/

                RectTransform transform = _data_rect.GetComponent < RectTransform > ();
                
                float graphHeight = transform.rect.height;
                float yMaximum = 100f;
                float xSize = 50f;

                Debug.Log ( " _data-rect " + transform.sizeDelta.y );

                Vector2 lastCircleGameObject = new Vector2 (-1.0f, -1.0f); //null;
                for ( int i = 0; i < _values.Count-1; i ++ )
                    {
                        float xPosition = xSize + i * xSize;
                        float yPosition = ( _values [i] / yMaximum ) * graphHeight;
                        
                        Vector2 circleGameObject = new Vector2 ( xPosition, yPosition );
                        if ( lastCircleGameObject != new Vector2 (-1.0f, -1.0f) ) 
                                _line ( lastCircleGameObject, circleGameObject, 
                                                new Vector2 ((i - 1) / 10.0f, _values [i - 1 ] / _rect.height), 
                                                new Vector2 (i       / 10.0f, _values [i]      / _rect.height)); // 0.5f + 0.3f * Mathf.Cos (i)))); );

                        lastCircleGameObject = circleGameObject;
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

        List < int > _values = new List < int > () { 5, 98, 56, 45, 30, 22, 17, 15, 13, 17 };

        private GameObject _point (float x, float y)
            {
                GameObject point = new GameObject ("Point." + x.ToString () + y.ToString ());
                point.transform.SetParent (_data_rect.transform, false);
            
                CircleGraphic circle = point.AddComponent <CircleGraphic> ();

                circle.detail = 8;
                circle.mode = CircleGraphic.Mode.FillInside;

                RectTransform transform = point.GetComponent <RectTransform> ();
              
                transform.anchorMin = transform.anchorMax = new Vector2 (x, y);
                //transform.anchoredPosition = new Vector2 (x, y);
                
                transform.pivot = new Vector2 (0.5f, 0.5f);
                transform.sizeDelta = new Vector2 (4.0f, 4.0f);
            
                return point;
            }

        private GameObject _line (Vector2 a, Vector2 b, Vector2 a1, Vector2 b1)
            {
                GameObject line = new GameObject ("Line", typeof (Image)); 
                line.transform.SetParent (_data_rect.transform, false);

                line.GetComponent < Image > ().color = new Color (0.0f, 1.0f, 0.0f, 0.5f); 

                RectTransform transform = line.GetComponent < RectTransform > ();
                
                float distance = Vector2.Distance (a, b); // * 300.0f;
                
                Debug.Log (distance + " " +  a + " " + b);
              //  transform.sizeDelta = new Vector2 (distance, 3.0f);

                Vector2 direction = (b - a).normalized;
                
                //transform.anchorMin = transform.anchorMax = new Vector2 (0.0f, 0.0f);
                
                //transform.anchoredPosition = a + direction * distance * 0.5f;
                                
                //transform.localEulerAngles = new Vector3 (0.0f, 0.0f, GetAngleFromVectorFloat (direction));

                transform.anchorMin = new Vector2 (0, 0);
                transform.anchorMax = new Vector2 (0, 0);
                
                transform.sizeDelta = new Vector2 (50, 3.0f);
                transform.anchoredPosition = a1 ; //+ direction * distance;// * .5f;

               // transform.pivot = new Vector2 (0.0f, 0.0f);
               // transform.localEulerAngles = new Vector3 (0, 0, 90);// GetAngleFromVectorFloat (direction));
                
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