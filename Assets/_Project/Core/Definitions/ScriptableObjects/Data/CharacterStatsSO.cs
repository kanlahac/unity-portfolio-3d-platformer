using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatsSO", menuName = "Scriptable Objects/Data/Character Stats")]
public class CharacterStatsSO : ScriptableObject
{
    [Header("Base Stats")]
    [field: SerializeField] public float BaseMoveForce { get; private set; }
    [field: SerializeField] public float BaseJumpForce { get; private set; }
    [field: SerializeField] public float BaseGravityForce { get; private set; }

    [Header("Runtime values")]
    public float MoveForce;
    public float JumpForce;
    public float GravityForce;


    private void OnEnable()
    {
        ResetStats();
    }


    public void ResetStats()
    {
        MoveForce = BaseMoveForce;
        JumpForce = BaseJumpForce;
        GravityForce = BaseGravityForce;
    }
}