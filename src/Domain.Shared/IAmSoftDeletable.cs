using System;

namespace Domain.Shared
{
    public interface IAmSoftDeletable
    {
        DateTime DateDeleted { get; set; }
    }
}