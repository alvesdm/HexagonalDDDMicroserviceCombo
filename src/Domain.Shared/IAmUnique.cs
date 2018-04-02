using System;

namespace Domain.Shared
{
    public interface IAmUnique
    {
        Guid UniqueId { get; set; }
    }
}