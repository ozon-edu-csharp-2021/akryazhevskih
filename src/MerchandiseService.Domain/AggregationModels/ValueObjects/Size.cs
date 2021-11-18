using System.Linq;
using MerchandiseService.Domain.Exceptions.ValueObjects;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.ValueObjects
{
    /// <summary>
    /// Размеры
    /// </summary>
    public class Size : Enumeration
    {
        public Size(int id, string name)
            : base(id, name)
        {
        }
        
        public static Size XS = new(1, nameof(XS));
        public static Size S = new(2, nameof(S));
        public static Size M = new(3, nameof(M));
        public static Size L = new(4, nameof(L));
        public static Size XL = new(5, nameof(XL));
        public static Size XXL = new(6, nameof(XXL));

        public static bool TryParse(int value, out Size size)
        {
            if (value < 1)
            {
                throw new SizeException("ID cannot be less than 1");
            }
            
            size = GetAll<Size>().FirstOrDefault(x => x.Id.Equals(value));

            return size is not null;
        }

        public static Size Parse(int value)
        {
            if (value < 1)
            {
                throw new SizeException("ID cannot be less than 1");
            }

            return GetAll<Size>().FirstOrDefault(x => x.Id.Equals(value));
        }
    }
}