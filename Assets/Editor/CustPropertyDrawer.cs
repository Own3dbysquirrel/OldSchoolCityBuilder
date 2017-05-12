using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomPropertyDrawer(typeof(CustomGUIArrayLayout))]
public class CustPropertyDrawer : PropertyDrawer {

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.PrefixLabel (position, label);
		Rect newposition = position;
		newposition.y += 20f;
		SerializedProperty data = property.FindPropertyRelative ("rows");
		//data.rows[0][]
		for(int j = 0; j < 7;j++)
		{
			SerializedProperty row = data.GetArrayElementAtIndex(j).FindPropertyRelative("row");
			newposition.height = 18f;
			if (row.arraySize != 7)
				row.arraySize = 7;
			newposition.width = position.width / 14;
			for (int i = 0; i < 7; i++) {
				EditorGUI.PropertyField(newposition,row.GetArrayElementAtIndex(i), GUIContent.none);
				newposition.x += newposition.width;
			}

			newposition.x = position.x;
			newposition.y += 18f;
		}
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label){
		return 20f * 8;
	}
}
