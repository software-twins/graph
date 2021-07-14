using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
 
public enum LineType
    {
        Straight,
        Smooth
    }
 
public class LineGraphic : MaskableGraphic
	{
    	[SerializeField]
        private LineType _line_type = LineType.Straight;
    	
		[SerializeField]
        private Line line = new StraightLine ();
    	
		[SerializeField]
        protected float _width;
    	
        [NonSerialized]
        private bool _refresh = false;
    
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
                                line = new StraightLine ();
                        else
                                line = new SmoothLine ();
                        
                        _refresh = true;
                    }
            }
 
        protected override void Awake ()
            {
                base.Awake ();

                _width = rectTransform.sizeDelta.x;
            
                if ( LineType == LineType.Smooth )
                        line = new SmoothLine ();
            }

        protected override void OnPopulateMesh ( VertexHelper vh )
            {
                vh.Clear ();
                line.DrawLine ( vh );
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
                line.AddPoint ( v3 );
                _refresh = true;
            }

        public void AddPoint ( List <Vector3> points )
            {
                line.AddPoint (points);
                _refresh = true;
            }
 
        public void SetSize ( float size )
            {
                line.size = size;
                _refresh = true;
            }
    
        public void SetColor ( Color color )
            {
                line.lineColor = color;
                _refresh = true;
            }
    
        public void SetSmoothness ( float smoothness )
            {
                if (_line_type == LineType.Smooth)
                        ( (SmoothLine) line).smoothness = smoothness;
 
                _refresh = true;
            }
    
        public void SetLineSmoothStyle (float smoothStyle)
            {
                if ( LineType == LineType.Smooth )
                        ( (SmoothLine) line).lineSmoothStyle = smoothStyle;
                _refresh = true;
            }   
    }
 
[System.Serializable]
public class Line
    {
        [SerializeField]
        protected List <Vector3> dataPoints = new List <Vector3> ();

        [SerializeField] public float size = 1;
        [SerializeField] public Color lineColor = Color.black;
        
        public virtual void DrawLine(VertexHelper vh)
            {
            }
    
        public void AddPoint(Vector3 p)
            {
                dataPoints.Add(p);
            }
    
        public void AddPoint(List<Vector3> points)
            {
                dataPoints.AddRange(points);
            }
    }

public class StraightLine : Line
    {
        public override void DrawLine(VertexHelper vh)
            {
                for (int i = 0; i < dataPoints.Count; i++)
                    {
                        if ( i < dataPoints.Count - 1 )
                            {
                                UIDrawLine.DrawLine(vh, dataPoints[i], dataPoints[i + 1], size, lineColor);
                            }
                    }
            }
    }

public class SmoothLine : Line
    {
        /// <summary>
             // кривая гладкость. Чем меньше значение, гладкая кривая, но количество вершин увеличится.
        /// </summary>
        [SerializeField] public float smoothness = 2;
        /// <summary>
             /// Кривая коэффициент сглаживания. Кривизна кривой может быть изменена путем регулировки гладкого коэффициента для получения другой кривой с небольшим изменением внешнего вида.
        /// </summary>
        [SerializeField] public float lineSmoothStyle = 2;
  
        private List <Vector3> bezierPoints = new List <Vector3> ();
    
        public override void DrawLine ( VertexHelper vh )
            {
                Vector3 lp  = Vector3.zero;
                Vector3 np  = Vector3.zero;
                Vector3 llp = Vector3.zero;
                Vector3 nnp = Vector3.zero;
 
                for ( int i = 0; i < dataPoints.Count; i ++ )
                    {
                        if ( i < dataPoints.Count - 1 )
                            {
                                llp = i > 1 ? dataPoints[i - 2] : lp;
                                nnp = i < dataPoints.Count - 1 ? dataPoints[i + 1] : np;
                                UIDrawLine.GetBezierList(ref bezierPoints, dataPoints[i], dataPoints[i + 1], llp, nnp, smoothness, lineSmoothStyle);
                                for ( int j = 0; j < bezierPoints.Count; j ++ )
                                    {
                                        if (j < bezierPoints.Count - 1)
                                            {
                                                UIDrawLine.DrawLine(vh, bezierPoints[j], bezierPoints[j + 1], size, lineColor);
                                            }
                                    }
                            }
                    }
            }
    }
 
 