using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CharacterDisplay))]
public class CharacterDisplayEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CharacterDisplay characterDisplay = (CharacterDisplay)target;
        if (GUILayout.Button("Update character"))
        {
            characterDisplay.ChangeCharacter();
        }
    }

}
