using UnityEngine;

[CreateAssetMenu(fileName = "Music", menuName = "Scriptable Objects/Music")]
public class Music : ScriptableObject
{
    public string animationName;
    public AudioClip audio;
}
