using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Learn.Linq.Aggregate
{
    public class BinItem
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        
        private long storageLocationId;
        public long StorageLocationId
        {
            get { return storageLocationId; }
            set { storageLocationId = value; }
        }
        
        private double? quantity;
        public double? Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        private long inventoryItemId;
        public long InventoryItemId
        {
            get { return inventoryItemId; }
            set { inventoryItemId = value; }
        }        
    }
}
