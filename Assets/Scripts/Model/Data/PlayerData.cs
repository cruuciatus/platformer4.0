using System; 
using UnityEngine;



[Serializable]

public class PlayerData
{
    [SerializeField] private InventoryData _inventory;


    public IntProperty HP = new IntProperty();
    public IntProperty MaxHP = new IntProperty();
    public PerksData Perks = new PerksData();
    public LevelData Levels = new LevelData();
    
    public InventoryData Inventory => _inventory;

   public PlayerData Clone()
  {
        var json = JsonUtility.ToJson(this);
        return JsonUtility.FromJson<PlayerData>(json);
      
    }

}
