using UnityEngine;

[CreateAssetMenu(fileName = "Keybinds", menuName = "SO/Keybinds")]
public class KeybindsSO : ScriptableObject
{
    [Header("Gameplay")]
    public KeyCode swapMaskBind;
}
