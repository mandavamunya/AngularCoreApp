using System;

namespace Application.Core.Entities
{
    public class TrackDateEntity: BaseEntity
    {
        public DateTime CreateDate { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
