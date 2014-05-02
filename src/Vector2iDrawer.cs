using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Cell{
	[CustomPropertyDrawer(typeof(Vector2i))]
	public class Vector2iDrawer : PropertyDrawer{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label){
		EditorGUI.BeginProperty(position,label,property);
		
		position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);
		
		int indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;
		
		//Drawer positionRects
		Rect xRect = new Rect (position.x-40, position.y, 100, position.height);
		Rect yRect = new Rect (position.x+90, position.y, 100, position.height);
		
		//EditorFields
		EditorGUI.PropertyField (xRect, property.FindPropertyRelative ("x"), GUIContent.none);
		EditorGUI.PropertyField (yRect, property.FindPropertyRelative ("y"), GUIContent.none);
		EditorGUI.indentLevel = indent;
		
		EditorGUI.LabelField(new Rect(position.x-60,position.y, 100, position.height),"x");
		EditorGUI.LabelField(new Rect(position.x+70,position.y, 100, position.height),"y");
		EditorGUI.EndProperty();
		
		}
	}
}
