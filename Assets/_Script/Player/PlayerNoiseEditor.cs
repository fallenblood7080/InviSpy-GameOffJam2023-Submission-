using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (NoiseHandler))]
public class PlayerNoiseEditor : Editor 
{

	void OnSceneGUI() 
    {
		NoiseHandler fow = (NoiseHandler)target;

		Handles.color = Color.white;

		Handles.DrawWireArc (fow.transform.position, Vector3.up, Vector3.forward, 360, fow.noiseHearingRange);
	}

}