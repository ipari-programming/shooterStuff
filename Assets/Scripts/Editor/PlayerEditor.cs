using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Cinemachine;

[CustomEditor(typeof(Player))]
public class PlayerInspector : Editor {

    public override void OnInspectorGUI()
    {
        Player player = (Player)target;

        player.cam = (CinemachineVirtualCamera)EditorGUILayout.ObjectField("Camera (cinemachine)", player.cam, typeof(CinemachineVirtualCamera), true);

        player.healthBar = (GameObject)EditorGUILayout.ObjectField("Health bar", player.healthBar, typeof(GameObject), true);
        player.healthBarFill = (GameObject)EditorGUILayout.ObjectField("Health bar filler", player.healthBarFill, typeof(GameObject), true);

        player.bulletPrefab = (GameObject)EditorGUILayout.ObjectField("Bullet prefab", player.bulletPrefab, typeof(GameObject), false);

        player.joystickMove = (Joystick)EditorGUILayout.ObjectField("Joystick move", player.joystickMove, typeof(Joystick), true);
        player.joystickShoot = (Joystick)EditorGUILayout.ObjectField("Joystick shoot", player.joystickShoot, typeof(Joystick), true);
    }

}
