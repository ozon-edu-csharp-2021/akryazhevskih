using System;

namespace MerchandiseService.Infrastructure.Repositories.Models
{
    internal class MerchDb
    {
        public long Id { get; set; }

        public long EmployeeId { get; set; }

        public int Status { get; set; }

        public int Type { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? IssuedAt { get; set; }
    }
}
