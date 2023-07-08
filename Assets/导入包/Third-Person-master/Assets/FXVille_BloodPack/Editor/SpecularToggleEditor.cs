using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class SpecularToggleEditor : CustomMaterialEditor 
{
//	public override void OnInspectorGUI ()
//	{
//		base.OnInspectorGUI ();
//
//		if (!isVisible)
//		{
//			Debug.Log("not visible");	
//			return;
//		}
//		
//		Material targetMat = target as Material;
//		string[] keyWords = targetMat.shaderKeywords;
//
//		bool specularReflectionEnabled = keyWords.Contains ("SPECULAR_REFLECTION_ON");
//		EditorGUI.BeginChangeCheck();
//
//		specularReflectionEnabled = EditorGUILayout.Toggle ("Specular Reflection Enabled", specularReflectionEnabled);
//
//		if (EditorGUI.EndChangeCheck())
//		{
//			//If enabled, add keyword SPECULAR_REFLECTION_ON, otherwise add SPECULAR_REFLECTION_OFF
//			List<string> keywords = new List<string> { specularReflectionEnabled? "SPECULAR_REFLECTION_ON" : "SPECULAR_REFLECTION_OFF"};
//			targetMat.shaderKeywords = keywords.ToArray();
//			EditorUtility.SetDirty (targetMat);
//		}
//	}
	protected override void CreateToggleList()
	{
		Toggles.Add(new FeatureToggle("Specular Reflection Enabled", "reflection", "SPECULAR_REFLECTION_ON", "SPECULAR_REFLECTION_OFF"));
	}
}

