using System;

using UnityEngine;
using UnityEngine.UI;

[ RequireComponent (typeof (CanvasScaler ), typeof (GraphicRaycaster)) ]

public class TemperatureCanvas : MonoBehaviour
	{
		private Canvas canvas;
		
		private GameObject graph;
		
	    // Start is called before the first frame update
    	void Start ()
    		{
 				/** get canvas from the GameObject */
        		canvas = GetComponent <Canvas> ();
				canvas.renderMode = RenderMode.ScreenSpaceCamera;
	
			//	size = new Vector2 (0, 0); //canvas.pixelRect.width, canvas.pixelRect.height);
			
				(graph = new GameObject ()).name = "TestGraph";
				
				graph.transform.parent = canvas.transform;
				/** set text component properties */
				(graph.AddComponent (Type.GetType ("Graph")) as Graph).initialize ();
			}

		private Vector2 size;

		void Update ()
			{
				Vector2 s = new Vector2 (canvas.pixelRect.width, canvas.pixelRect.height);

				if ( size != s )
					{
						size = s;
						graph.GetComponent <CanvasElement> ().geometry (canvas.pixelRect);
						//Debug.Log (canvas.pixelRect);
					}
			}

		public void show ()
    		{
				//Debug.Log ("in sgow");			
				//text.text = text1; //"object: " + obj + 
				//			"\n" +
				//			"distance: " + hit.distance +
				//			"\n" +
				//			"center: " + center;	
				//geometry.SetText ("The first number is {0} and the 2nd is {1:2} and the 3rd is {3:0}.",
				//4, 6.345f, 3.5f);
				//	int width = (int) ((4.0f * canvas.pixelRect.width / 10.0f) / 10.0f);
				//	Debug.Log ("width : " + width);
  			/*	geometryinfo.text (String.Format (
								   "<mspace=16>{1,-20}{2,7}\n\n{3,-20}{4,7:0.000}\n{5,-20}{6,7:0.000}\n{7,-20}{8,7:0.000}\n{9,-20}{10,7:0.000}</mspace>", 
								   0, 
								   "Material :", "Steel",
								   "Length (m) :", info.geometry.length, 
								   "Inner radius (m) :", info.geometry.radius_inner,
								   "Outer radius (m) :", info.geometry.radius_outer, 
								   "Wall thickness (m) :", info.geometry.wall_thick)); //= Stringinfo.tube; //label + (m_frame);
					
				parameterinfo.text (String.Format (
								   "{1,-20}{2,7}\n\n{3,-20}{4,10:0.000000}\n{5,-20}{6,7:0.000}\n{7,-20}{8,7:0.000}\n{9,-20}{10,7:0.000}", 
								   0, 
								   "Substance :", "Water",
								   "Flow rate (m3/s) :", info.parameters.flow_rate, 
								   "In temperature (m) :", info.parameters.in_temperature,
								   "Out temperature (m) :", info.parameters.out_temperature, 
								   "Ambient temperature (m) :", info.parameters.ambient));//temperaturetext.text = info.temperature;*/
			}
	}
	

