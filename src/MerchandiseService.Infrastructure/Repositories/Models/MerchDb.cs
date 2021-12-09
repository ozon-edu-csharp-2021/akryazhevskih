using System;

namespace MerchandiseService.Infrastructure.Repositories.Models
{
    internal class MerchDb
    {
        public long Id { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeeEmail { get; set; }

        public string ManagerName { get; set; }

        public string ManagerEmail { get; set; }

        public int Status { get; set; }

        public int Type { get; set; }

        public int Size { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? IssuedAt { get; set; }
    }
}
