﻿using System;
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
    	[ SerializeField ]
        private LineType _line_type = LineType.Smooth;
    	
		[ SerializeField ]
        private Line _line = new SmoothLine ();
    	
		[ SerializeField ]
        protected float _width = 0.1f;
    	
        [ NonSerialized ]
        private bool _refresh = true;
    
        public float width 
            { 
                get { return _width; } 
            }
        
        public LineType LineType
            {
                get { return _line_type; }
                set
                    {
                        _line_type = value;
                                               
                        if ( value == LineType.Straight )
                                _line = new StraightLine ();
                        else
                                _line = new SmoothLine ();
                        
                        _refresh = true;
                    }
            }
 
        protected override void Awake ()
            {
                base.Awake ();

                _width = rectTransform.sizeDelta.x;
            
                if ( LineType == LineType.Smooth )
                        _line = new SmoothLine ();
            }

        protected override void OnPopulateMesh ( VertexHelper vh )
            {
                vh.Clear ();
                _line.draw_line ( vh );

				Debug.Log ( "OnPopulateMesh" );
            }

        private void Update ()
            {
                CheckRefreshChart ();
            }

        protected void CheckRefreshChart ()
            {
                if ( _refresh )
                    {
                        int width = ( int ) _width;

                        rectTransform.SetSizeWithCurrentAnchors ( RectTransform.Axis.Horizontal, width - 1 );
                        rectTransform.SetSizeWithCurrentAnchors ( RectTransform.Axis.Horizontal, width );

                        _refresh = false;
                    }
            }
    
        public void AddPoint ( Vector3 v3 )
            {
                _line.AddPoint ( v3 );
                _refresh = true;
            }

        public void AddPoint ( List <Vector3> points )
            {
                _line.AddPoint ( points );
                _refresh = true;
            }
 
        public void SetSize ( float size )
            {
                _line._size = size;
                _refresh = true;
            }
    
        public void SetColor ( Color color )
            {
                _line._line_color = color;
                _refresh = true;
            }
    
        public void SetSmoothness ( float smoothness )
            {
                if ( _line_type == LineType.Smooth )
                        ( (SmoothLine) _line ).smoothness = smoothness;
 
                _refresh = true;
            }
    
        public void SetLineSmoothStyle ( float smoothStyle )
            {
                if ( LineType == LineType.Smooth )
                        ( (SmoothLine) _line).lineSmoothStyle = smoothStyle;
                _refresh = true;
            }   

        protected override void OnRectTransformDimensionsChange ()   
            {
                Debug.Log ( "OnRectTransformDimensionsChange()" );
			}
    }
 
[System.Serializable]
public class Line
    {
        [ SerializeField ]
        protected List < Vector3 > _data_points = new List <Vector3> ();

        [ SerializeField ]
        public float _size = 1;
        
        [ SerializeField ]
        public Color _line_color = Color.green;
        
        public virtual void draw_line ( VertexHelper vh )
            {
            }
    
        public void AddPoint ( Vector3 p )
            {
                _data_points.Add ( p );
            }
    
        public void AddPoint ( List <Vector3> points )
            {
                _data_points.AddRange ( points );
            }        

        protected void draw ( VertexHelper vh, Vector3 point_a, Vector3 point_b, float size, Color32 color )
            {
                if ( point_a == point_b ) 
                        return;

                Vector3 v = Vector3.Cross ( point_a - point_b, Vector3.forward ).normalized * size;
                
                _vertex [0].position = point_a - v;
                _vertex [1].position = point_b - v;
                _vertex [2].position = point_b + v;
                _vertex [3].position = point_a + v;
 
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
        public override void draw_line ( VertexHelper vh )
            {
                for ( int i = 0; i < _data_points.Count; i ++ )
                        if ( i < _data_points.Count - 1 )
                                draw ( vh, _data_points [i], _data_points [i + 1], _size, _line_color );
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
  
        public override void draw_line ( VertexHelper vh )
            {
                Vector3 lp  = Vector3.zero;
                Vector3 np  = Vector3.zero;
                Vector3 llp = Vector3.zero;
                Vector3 nnp = Vector3.zero;
 
                for ( int i = 0; i < _data_points.Count; i ++ )
                    {
                        if ( i < _data_points.Count - 1 )
                            {
                                llp = i > 1 ? _data_points [i - 2] : lp;
                                nnp = i < _data_points.Count - 1 ? _data_points  [i + 1] : np;

				               	bezier_list (ref bezierPoints, _data_points [i], _data_points [i + 1], llp, nnp, smoothness, lineSmoothStyle);
                                
                                for ( int j = 0; j < bezierPoints.Count; j ++ )
                                    {
                                        if ( j < bezierPoints.Count - 1 )
                                                draw ( vh, bezierPoints [j], bezierPoints [j + 1], _size, _line_color );
                                            
                                    }
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
 
 