using UnityEngine;

[CreateAssetMenu(fileName = "GameManager", menuName = "AdventureKingdom/GameManager", order = 0)]
public class GameManager : ScriptableObject 
{ 
    public GameObject Core = null;
    public GameObject Canvas = null;
}