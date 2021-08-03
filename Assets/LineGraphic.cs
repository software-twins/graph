using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
 
 /***
    * as basis are taken the source, shown in example and located on:
    *      https://russianblogs.com/article/87471758041/
    *
    * the full project with various charts is located on:
    *      https://github.com/monitor1394/unity-ugui-XCharts     
    */

public enum LineType
    {
        Straight,
        Smooth
    }

[ RequireComponent ( typeof( CanvasRenderer ) ) ]
public class LineGraphic : MaskableGraphic
	{
        /** 
          * adding a one point to a chart _line
          */
        public void add_point ( Vector3 value )
            {
                _line.add_point ( value );
                
                /* comment as indicated earlier when declaring a variable 
                 * _refresh = true; */
            }

        /**
          * adding a set of points to a chart _line
          */
        public void add_point ( List < Vector3 > points )
            {
                _line.add_point ( points );
                
                /* comment as indicated earlier when declaring a variable 
                 * _refresh = true; */
            }

        /**
          * set the thickness for the _line chart
          */
        public void size ( float size )
            {
                _line._size = size;

                /* comment as indicated earlier when declaring a variable 
                 * _refresh = true; */
            }
    
        /**
          * setting the color for the chart _line
          */
        public void color ( Color color )
            {
                _line._line_color = color;

                /* comment as indicated earlier when declaring a variable 
                 * _refresh = true; */
            }
    
        /**
          * setting the smoothness for the chart _line
          */
        //public void smoothness ( float value )
        //    {
        //        if ( _line_type == LineType.Smooth )
        //                ( (SmoothLine) _line ).smoothness = value;
 
                /* comment as indicated earlier when declaring a variable 
                 * _refresh = true; */
        //    }
    
        /**
          * setting the smoothness style for the chart _line
          */
        //public void smooth_style ( float value )
        //    {
        //        if ( LineType == LineType.Smooth )
        //                ( (SmoothLine) _line).lineSmoothStyle = value;

                /* comment as indicated earlier when declaring a variable 
                 * _refresh = true; */
        //    }       	

        [ SerializeField ]
		private LineType _line_type = LineType.Straight; // Smooth;
    	
		[ SerializeField ]
        private Line _line = new StraightLine ();
    	
		/* [ SerializeField ] */
        protected RectTransform _transform;
        /** 
          * the purpose is not clear, while it is commented out, 
          * it was only found that this variable, _protected float width_, is used in the CheckRereshState method,
          * that is called in the _Update_ method
          *  
          * protected float _width = 1.0f;
          */  
    	
        /**
          * the analysis of the state of the variable _private bool refresh_ is carried out
          * only in the _CheckRefreshState_  method, we will also try to comment it out
          *
          * [ NonSerialized ]
          * private bool _refresh = true;
          *
          * we also comment on the assignment of values to this variable _refresh_ along the way of source text
          */
    
        /**
          * since the variable _width_ is commented out, then the corresponding get/set method 
          * are commented for now too
          *        
          * public float width 
          *      { 
          *          get { return _width; } 
          *      }
          */
        
        public LineType line_type
            {
                get { return _line_type; }

                set
                    {
                        _line_type = value;
                                               
                        if ( value == LineType.Straight )
                                _line = new StraightLine ();
                        else
                                _line = new SmoothLine ();
                        
                        /**
                          * comment as indicated earlier when declaring a variable 
                          *_refresh = true;
                          */
                    }
            }
 
        protected override void Awake ()
            {
                base.Awake ();

                _transform = gameObject.GetComponent < RectTransform > ();

                // if ( LineType == LineType.Smooth )
                //         _line = new SmoothLine ();
            }

        protected override void OnPopulateMesh ( VertexHelper vh )
            {
                vh.Clear ();

                _line.draw_line ( vh, _transform.rect.width, _transform.rect.height );
            }

        private void Update ()
            {
				
            	  CheckRefreshChart ();
            	  
            }
	       
        /**
          * the purpose of the _width_ variable, which is used in the method below, is not clear;
          * the logic of this method _CheckRefreshChart_ is generally incomprehensible,
          * for example, these two lines, in particular, second arguments _width_ - 1 and _width_
          *
          * rectTransform.SetSizeWithCurrentAnchors ( RectTransform.Axis.Horizontal, width - 1 );
          * rectTransform.SetSizeWithCurrentAnchors ( RectTransform.Axis.Horizontal, width );
          *
          * so this method is also commented out and let's we try to run the script without it
          * 
		  *  protected void CheckRefreshChart ()
          *     {
          *         if ( _refresh )
          *             {
          *                 int width = ( int ) _width;
		  *
          *                 rectTransform.SetSizeWithCurrentAnchors ( RectTransform.Axis.Horizontal, width - 1 );
          *                 rectTransform.SetSizeWithCurrentAnchors ( RectTransform.Axis.Horizontal, width );
		  *
          *                 _refresh = false;
          *             }
          *     }
          */
              
       

        protected override void OnRectTransformDimensionsChange ()   
            {
				SetVerticesDirty ();
			}
    }
 
[System.Serializable]
public class Line
    {
        [ SerializeField ]
        protected List < Vector3 > _data_points = new List < Vector3 > ();

        [ SerializeField ]
		/**
		  * 	here size variable determines the thickness of the line; 
		  * 		it is used in the _draw_ and _draw_line_ methods, when the mesh vertice _vh_ are calculate, and
		  * 		is setting up in the external calling code through calling the _SetSize_ method;
		  * 		this method (_SetSize_) is defined in another class _LineGraph_, which includes _Line_ class as 
		  * 		an aggregate and in which _Line_ is included as part of
		  */
        public float _size = 1.0f;
        
        [ SerializeField ]
        public Color _line_color = Color.green;
        
        public virtual void draw_line ( VertexHelper vh, float width, float height )
            {
            }
    
        public void add_point ( Vector3 p )
            {
                _data_points.Add ( p );
            }
    
        public void add_point ( List < Vector3 > points )
            {
                _data_points.AddRange ( points );
            }        

        protected void draw ( VertexHelper vh, Vector3 a, Vector3 b, float size, Color32 color )
            {
                if ( a == b ) 
                		return;

				
				Vector3 v = Vector3.Cross (a - b, Vector3.forward).normalized * size;
                
                _vertex [0].position = a - v;
                _vertex [1].position = b - v;
                _vertex [2].position = b + v;
                _vertex [3].position = a + v;
 
                for ( int i = 0; i < 4; i ++ )
                    {
                        _vertex [i].color = color;
                        _vertex [i].uv0 = Vector2.zero;
                    }
                
                vh.AddUIVertexQuad ( _vertex );
            }
    
		private static UIVertex [] _vertex = new UIVertex [4];  
	}

public class StraightLine : Line
    {
        public override void draw_line ( VertexHelper vh,  float width, float height )
            {
				for ( int i = 0; i < _data_points.Count; i ++ )
						if ( i < _data_points.Count - 1 )
							{
								Vector3 a = new Vector3 ( _data_points [i].x * width  / 10.0f, 
														  _data_points [i].y * height / 98.0f, 
														   0.0f );
								
								Vector3 b = new Vector3 ( _data_points [i + 1].x * width  / 10.0f,
														  _data_points [i + 1].y * height / 98.0f,
														   0.0f );

								//draw (vh, _data_points [i], _data_points [i + 1], _size, _line_color);
								Debug.Log (_size );
								draw ( vh, a, b, _size, _line_color );
							}
            }
    }

public class SmoothLine : Line
    {
        /// <summary>
             // кривая гладкость. Чем меньше значение, гладкая кривая, но количество вершин увеличится.
        /// </summary>
        [ SerializeField ] 
        public float smoothness = 1;
        /// <summary>
             /// Кривая коэффициент сглаживания. Кривизна кривой может быть изменена путем регулировки гладкого коэффициента для получения другой кривой с небольшим изменением внешнего вида.
        /// </summary>
        [ SerializeField ] 
        public float lineSmoothStyle = 2;
  
        public override void draw_line ( VertexHelper vh, float width, float height )
            {
                Vector3 lp  = Vector3.zero;
                Vector3 np  = Vector3.zero;
                Vector3 llp = Vector3.zero;
                Vector3 nnp = Vector3.zero;
 
				for (int i = 0; i < _data_points.Count; i++)
					{
						if ( i < _data_points.Count - 1 )
							{
								Vector3 a = new Vector3 ( _data_points [i].x * width  / 10.0f, 
					                                      _data_points [i].y * height / 98.0f, 
					                                       0.0f);
				
								Vector3 b = new Vector3 ( _data_points [i + 1].x * width  / 10.0f, 
					                                      _data_points [i + 1].y * height / 98.0f, 
					                                       0.0f);

								llp = i > 1 ? new Vector3 ( _data_points [i - 2].x * width / 10.0f,
															_data_points [i - 2].y * height / 98.0f,
															 0.0f)
											: lp;
                                
								nnp = i < _data_points.Count - 1 ? b : np;

								bezier_list ( ref bezierPoints, a, b, llp, nnp, smoothness, lineSmoothStyle );
                                
								for ( int j = 0; j < bezierPoints.Count; j ++ )
										if ( j < bezierPoints.Count - 1 )
												draw ( vh, bezierPoints [j], bezierPoints [j + 1], _size, _line_color);
							}
					}
            }   

        private List <Vector3> bezierPoints = new List <Vector3> ();

        private void bezier_list ( ref List <Vector3> pos_list, Vector3 sp, Vector3 ep, Vector3 lsp, Vector3 nep, float smoothness = 2f, float k = 2.0f)
            {
                float distance = Mathf.Abs ( sp.x - ep.x );
                
                Vector3 cp1, cp2;

                var direction = ( ep - sp ).normalized;
                var diff = distance / k;
        
                if ( lsp == sp )
                    {
                        cp1 = sp + distance / k * direction * 1;
                        cp1.y = sp.y;
                        cp1 = sp;
                    }
                else
                    {
                        cp1 = sp + ( ep - lsp ).normalized * diff;
                    }

                if ( nep == ep )
                        cp2 = ep;
                else 
                        cp2 = ep - ( nep - sp ).normalized * diff;
        
                distance = Vector3.Distance ( sp, ep );
                
                int segment = (int) ( distance / (smoothness <= 0 ? 2f : smoothness) );
                
                if ( segment < 1 ) 
                        segment = (int) (distance / 0.5f);
                
                if ( segment < 4 )
                        segment = 4;
                
                bezier_list_2 (ref pos_list, sp, ep, segment, cp1, cp2);
            }
    
        private void bezier_list_2 ( ref List <Vector3> pos_list, Vector3 sp, Vector3 ep, int segment, Vector3 cp, Vector3 cp2 )
            {
                pos_list.Clear ();

                if ( pos_list.Capacity < segment + 1 )
                        pos_list.Capacity = segment + 1;
                    
                for ( int i = 0; i < segment; i ++ )
                        pos_list.Add ( bezier_2 (i / (float)  segment, sp, cp, cp2, ep) );
                    
                pos_list.Add ( ep );
            }
    
        private Vector3 bezier_2 ( float t, Vector3 sp, Vector3 p1, Vector3 p2, Vector3 ep )
            {
                t = Mathf.Clamp01 ( t );
                var oneMinusT = 1f - t;
                return oneMinusT * oneMinusT * oneMinusT * sp + 3f * oneMinusT * oneMinusT * t * p1 + 3f * oneMinusT * t * t * p2 + t * t * t * ep;
            }
    }
 
 