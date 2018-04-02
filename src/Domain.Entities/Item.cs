using Domain.Interfaces;
using System;
using Domain.Interfaces.Entities;

namespace Domain.Entities
{
    public class Item : IAmItem
    {
        public Guid UniqueId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateDeleted { get; set; }
        public string Description { get; set; }
    }
}
