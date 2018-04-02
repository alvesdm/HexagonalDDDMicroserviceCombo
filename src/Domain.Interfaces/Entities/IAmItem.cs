using Domain.Shared;

namespace Domain.Interfaces.Entities
{
    public interface IAmItem: IAmUnique, IAmChangeTrackable, IAmSoftDeletable
    {
        string Description { get; set; }
    }
}
