using System;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.API.Entities
{
    public class ProductModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public decimal Price { get; set; }
        public int StockAmount { get; set; }
    }
}
