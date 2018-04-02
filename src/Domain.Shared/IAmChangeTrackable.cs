using System;

namespace Domain.Shared
{
    public interface IAmChangeTrackable
    {
        DateTime DateCreated { get; set; }
        DateTime DateUpdated { get; set; }
    }
}