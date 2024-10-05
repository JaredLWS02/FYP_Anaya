using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTable : MonoBehaviour
{
    [System.Serializable]
    public class ItemDrop
    {
        public string name;
        public Item item;
        public float percent=100;
        public Vector2Int quantity = new(1, 1);
        public bool stackQuantity=false;
    }

    // ============================================================================

    public List<ItemDrop> drops = new();

    public void Drop()
    {
        foreach(ItemDrop drop in drops)
        {
            if(Random.Range(0, 100f) <= drop.percent)
            {
                int quantity = Random.Range(drop.quantity.x, drop.quantity.y+1);

                if(drop.stackQuantity)
                {
                    ItemManager.Current.Spawn(transform.position, drop.item, quantity);
                }
                else
                {
                    for(int i=0; i<quantity; i++)
                    {
                        ItemManager.Current.Spawn(transform.position, drop.item);
                    }
                }
            }
        }
    }
}
