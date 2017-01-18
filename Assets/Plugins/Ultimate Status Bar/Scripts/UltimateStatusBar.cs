/* Written by Kaz Crowe */
/* UltimateStatusBar.cs ver 1.0.8 */
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


[ExecuteInEditMode]
public class UltimateStatusBar : MonoBehaviour
{
	// 
	public event Action controllerEvent;

	/* ----- > ASSIGNED VARIABLES < ----- */
	public Image statusBar;
	public Color statusBarColor = Color.white;
	public Text statusBarText;
	public Color statusBarTextColor = Color.white;
	
	float storedTargetFill = 0.0f;
	float storedMaxValue = 0.0f;
	float updatedFrac = 0.0f;

	/* ----- > STYLE AND OPTIONS < ----- */
	public bool showText = false;
	public bool usePercentage = false;
	public string additionalText = string.Empty;

	public enum AlternateStateOption{ None, Manual, Percentage, ColorBlended }
	public AlternateStateOption alternateStateOption = AlternateStateOption.None;
	public float triggerValue = 0.25f;
	public Color alternateStateColor = Color.white;
	public Color alternateStateColorAlt = Color.white;
	public bool flashing = false;
	public float flashingSpeed = 1.0f;
	bool currentState = false;

	public bool smoothFill = true;
	public float smoothFillDuration = 1.0f;
	bool isSmoothing = false;
	bool smoothFillReset = false;

	public bool fillConstraint = false;
	public float fillConstraintMin = 0.0f;
	public float fillConstraintMax = 1.0f;
	public bool keepControllerAwake = false;
	public float keepAwakeTrigger = 0.75f;
	public bool keepAwake = false;

	/* ----- > SCRIPT REFERENCE < ----- */
	static Dictionary<string, UltimateStatusBar> StatusBarLogics = new Dictionary<string, UltimateStatusBar>();
	public string statusBarName = string.Empty;

	
	void Awake ()
	{
		// If the statusBarName is assigned, then register this status bar for reference.
		if( Application.isPlaying == true && statusBarName != string.Empty )
			RegisterStatusBar( statusBarName, gameObject.GetComponent<UltimateStatusBar>() );
	}

	void Start ()
	{
		if( Application.isPlaying == false )
			return;

		// If the status bar or the text are assigned, then apply the color.
		if( statusBar != null )
			UpdateStatusBarColor( statusBarColor );
		if( statusBarText != null && showText == true )
			UpdateStatusBarTextColor( statusBarTextColor );
	}

	// This function is called by all UltimateStatusBar scripts to set up their status bars.
	void RegisterStatusBar ( string statusName, UltimateStatusBar statusBar )
	{
		if( StatusBarLogics.ContainsKey( statusName ) )
			StatusBarLogics.Remove( statusName );

		StatusBarLogics.Add( statusName, statusBar );
	}

	void ShowTextHandler ()
	{
		if( showText == false || statusBarText == null )
			return;

		// If the user does not want to show percentage, then show the current value next to the max value.
		if( usePercentage == false )
			statusBarText.text = additionalText + ( updatedFrac * storedMaxValue ).ToString() + " / " + storedMaxValue.ToString();
		// Else transfer the values into a percentage and display it.
		else
			statusBarText.text = additionalText + ( updatedFrac * 100 ).ToString( "F0" ) + "%";
	}
	
	void AlternateStateOptionHandler ()
	{
		switch( alternateStateOption )
		{
			case AlternateStateOption.Percentage:
			{
				if( updatedFrac <= triggerValue && currentState == false )
				{
					currentState = true;
					StartCoroutine( "AlternateStateFlashing" );
				}
				else if( updatedFrac > triggerValue && currentState == true )
					currentState = false;
			}break;
			case AlternateStateOption.ColorBlended:
			{
				AlternateStateColorBlend();
			}break;
			default:
			{
				// Do nothing, because the option is either none or manual.
			}break;
		}
	}
	
	void AlternateStateColorBlend ()
	{
		float currentValue = updatedFrac;
		Color updatedColor = Color.white;
		if( updatedFrac > 0.5 )
		{
			currentValue = Mathf.Lerp( -1.0f, 1.0f, updatedFrac );
			updatedColor = Color.Lerp( alternateStateColor, statusBarColor, currentValue );
		}
		else
		{
			currentValue *= 2;
			updatedColor = Color.Lerp( alternateStateColorAlt, alternateStateColor, currentValue );
		}
		statusBar.color = updatedColor;
	}
	
	IEnumerator AlternateStateFlashing ()
	{
		float step = -90.0f;
		while( currentState == true )
		{
			step += Time.deltaTime * flashingSpeed;
			if( step > 270 )
				step -= 360;
			statusBar.color = Color.Lerp( alternateStateColor, alternateStateColorAlt, ( Mathf.Sin( step ) + 1 ) / 2 );
			yield return null;
		}
		statusBar.color = statusBarColor;
	}

	// This function will fill the status bar over a period of time.
	IEnumerator SmoothFillDurationLogic ()
	{
		isSmoothing = true;
		float speed = 1.0f / smoothFillDuration;
		float currentFill = statusBar.fillAmount;
		float targetValue = updatedFrac;
		float startValue = fillConstraint == true ? ( statusBar.fillAmount - fillConstraintMin ) / ( fillConstraintMax - fillConstraintMin ) : statusBar.fillAmount;
		
		for( float t = 0.0f; t < 1.0f; t += Time.deltaTime * speed )
		{
			if( smoothFillReset == true )
			{
				smoothFillReset = false;
				currentFill = statusBar.fillAmount;
				targetValue = updatedFrac;
				startValue = fillConstraint == true ? ( statusBar.fillAmount - fillConstraintMin ) / ( fillConstraintMax - fillConstraintMin ) : statusBar.fillAmount;
				t = 0.0f;
			}
			statusBar.fillAmount = Mathf.Lerp( currentFill, storedTargetFill, t );
			updatedFrac = Mathf.Lerp( startValue, targetValue, t );
			AlternateStateOptionHandler();
			ShowTextHandler();
			yield return null;
		}
		// Finalize the fill and options.
		statusBar.fillAmount = storedTargetFill;
		updatedFrac = targetValue;
		AlternateStateOptionHandler();
		ShowTextHandler();

		isSmoothing = false;

		if( controllerEvent != null )
		{
			if( keepControllerAwake == true && targetValue <= keepAwakeTrigger )
				keepAwake = true;
			else
				keepAwake = false;

			controllerEvent();
		}
	}

	#region Public Functions
	/* --------------------------------------------- *** PUBLIC FUNCTIONS FOR THE USER *** --------------------------------------------- */
	public void UpdateStatusBar ( float currentValue, float maxValue )
	{
		// If the status bar is left unassigned, then return.
		if( statusBar == null )
			return;

		// Fix the value to be a percentage.
		updatedFrac = currentValue / maxValue;

		// If the value is greater than 1 or less than 0, then fix the values to being min/max.
		if( updatedFrac < 0 || updatedFrac > 1 )
			updatedFrac = updatedFrac < 0 ? 0 : 1;

		// Store the target amount of fill according to the users options.
		storedTargetFill = fillConstraint == true ? Mathf.Lerp( fillConstraintMin, fillConstraintMax, updatedFrac ) : updatedFrac;

		// Store the values so that other functions used can reference the maxValue.
		storedMaxValue = maxValue;

		if( smoothFill == false || Application.isPlaying == false )
		{
			statusBar.fillAmount = storedTargetFill;
			AlternateStateOptionHandler();
			ShowTextHandler();
		}
		else
		{
			if( isSmoothing == true )
				smoothFillReset = true;
			else
				StartCoroutine( "SmoothFillDurationLogic" );
		}	

		// If this script has a controller parent, then request to show the status bar.
		if( controllerEvent != null )
		{
			if( smoothFill == true )
				keepAwake = true;
			else if( keepControllerAwake == true && updatedFrac <= keepAwakeTrigger )
				keepAwake = true;
			else
				keepAwake = false;

			controllerEvent();
		}
	}

	public void UpdateStatusBarColor ( Color targetColor )
	{
		statusBarColor = targetColor;
		statusBar.color = statusBarColor;
	}

	public void UpdateStatusBarTextColor ( Color targetColor )
	{
		statusBarTextColor = targetColor;
		statusBarText.color = statusBarTextColor;
	}

	public void AlternateState ( bool targetState )
	{
		if( alternateStateOption != AlternateStateOption.None || targetState == currentState )
			return;

		currentState = targetState;

		if( flashing == true && targetState == true )
			StartCoroutine( "AlternateStateFlashing" );
		else
		{
			if( targetState == true )
				statusBar.color = alternateStateColor;
			else if( targetState == false )
				statusBar.color = statusBarColor;
		}
	}
	/* ------------------------------------------- *** END PUBLIC FUNCTIONS FOR THE USER *** ------------------------------------------- */
	#endregion

	#region Static Functions
	/* --------------------------------------------- *** STATIC FUNCTIONS FOR THE USER *** --------------------------------------------- */
	static public void UpdateStatus ( string statusName, float currentValue, float maxValue )
	{
		if( !StatusBarConfirmed( statusName ) )
			return;
		
		StatusBarLogics[ statusName ].UpdateStatusBar( currentValue, maxValue );
	}

	static public void UpdateStatusBarColor( string statusName, Color statusBarColor )
	{
		if( !StatusBarConfirmed( statusName ) )
			return;

		StatusBarLogics[ statusName ].UpdateStatusBarColor( statusBarColor );
	}

	static public void UpdateStatusBarTextColor ( string statusName, Color targetColor )
	{
		if( !StatusBarConfirmed( statusName ) )
			return;

		StatusBarLogics[ statusName ].UpdateStatusBarTextColor( targetColor );
	}

	static public void AlternateState ( string statusName, bool targetState )
	{
		if( !StatusBarConfirmed( statusName ) )
			return;

		StatusBarLogics[ statusName ].AlternateState( targetState );
	}

	static bool StatusBarConfirmed ( string statusName )
	{
		if( !StatusBarLogics.ContainsKey( statusName ) )
		{
			Debug.LogWarning( "No Ultimate Status Bar has been registered with the name: " + statusName + "." );
			return false;
		}
		return true;
	}
	/* ------------------------------------------- *** END STATIC FUNCTIONS FOR THE USER *** ------------------------------------------- */
	#endregion
}