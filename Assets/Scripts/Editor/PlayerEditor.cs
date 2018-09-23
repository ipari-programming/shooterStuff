using UnityEngine;
using UnityEditor;
using TouchControlsKit;

[CustomEditor(typeof(Player))]
public class PlayerInspector : Editor {

    public override void OnInspectorGUI()
    {
        Player player = (Player)target;

        player.joystickMove = (TCKJoystick)EditorGUILayout.ObjectField("Joystick Move", player.joystickMove, typeof(TCKJoystick), true);
        player.joystickShoot = (TCKJoystick)EditorGUILayout.ObjectField("Joystick Shoot", player.joystickShoot, typeof(TCKJoystick), true);

        player.bulletPrefab = (GameObject)EditorGUILayout.ObjectField("Bullet prefab", player.bulletPrefab, typeof(GameObject), false);
    }

}
