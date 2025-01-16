using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData", order = 1)]
public class GameData : ScriptableObject
{
  public List<Card> gameCards=new List<Card>();
  public bool isGameStarted;
  public List<Card> selectedCards = new List<Card>();

}
