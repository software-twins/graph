﻿using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class TestMouse : MonoBehaviour
	{
        private LineGraphic _line ; //= new LineGraphic ();

    	void Start ()
    		{
                _line = gameObject.AddComponent < LineGraphic > ();

                _line.SetSize (2);
     		}
 
    	void Update	()
    		{
        		if ( Input.GetMouseButton ( 0 ) )
        			{
            			//Debug.Log ( Input.mousePosition );
            			_line.AddPoint ( Input.mousePosition );
        			}
    		}
	}