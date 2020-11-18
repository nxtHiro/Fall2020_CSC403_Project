using Fall2020_CSC403_Project.code; 
using System;
using UnityEngine;

namespace Fall2020_CSC403_Project
{
 
	 public class pause : MonoBehaviour
	 {
	     bool paused = false;
 
	     void Update()
	     {
	         if(Input.GetButtonDown("escape")) // allows escape to be pressed from a pause menu to show
             paused = togglePause();
	     }
     
	     void OnGUI()
	     {
	         if(paused)
	         {
	             GUILayout.Label("Game is paused!");
	             if(GUILayout.Button("Click me to unpause"))
	                 paused = togglePause();
	         }
	     }
	     
	     bool togglePause() // this was the harderst paert for me , finding a time scale to allow the game to be indefinatly frozen
	     {
	         if(Time.timeScale == 0f)
	         {
	             Time.timeScale = 1f;
	             return(false);
	         }
	         else
	         {
	             Time.timeScale = 0f;
	             return(true);    
	         }
	     }
	 }
}