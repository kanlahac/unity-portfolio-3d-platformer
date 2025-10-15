using System;
using UnityEngine;


[CreateAssetMenu(fileName = "GameEvent", menuName = "Scriptable Objects/Events/Game Event")]
public class GameEventSO : ScriptableObject
{
    public Action OnEvent { get; private set; }

    public void Raise() => OnEvent?.Invoke();
    public void Register(Action listener) => OnEvent += listener;
    public void Unregister(Action listener) => OnEvent -= listener;
}