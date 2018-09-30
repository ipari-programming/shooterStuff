using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Cinemachine;
using TouchControlsKit;

[CustomEditor(typeof(Player))]
public class PlayerInspector : Editor {

    public override void OnInspectorGUI()
    {
        Player player = (Player)target;

        player.cam = (CinemachineVirtualCamera)EditorGUILayout.ObjectField("Camera (cinemachine)", player.cam, typeof(CinemachineVirtualCamera), true);

        player.healthBar = (GameObject)EditorGUILayout.ObjectField("Health bar", player.healthBar, typeof(GameObject), true);
        player.healthBarFill = (GameObject)EditorGUILayout.ObjectField("Health bar filler", player.healthBarFill, typeof(GameObject), true);

        player.joystickMove = (TCKJoystick)EditorGUILayout.ObjectField("Joystick Move", player.joystickMove, typeof(TCKJoystick), true);
        player.joystickShoot = (TCKJoystick)EditorGUILayout.ObjectField("Joystick Shoot", player.joystickShoot, typeof(TCKJoystick), true);

        player.bulletPrefab = (GameObject)EditorGUILayout.ObjectField("Bullet prefab", player.bulletPrefab, typeof(GameObject), false);
    }

}
