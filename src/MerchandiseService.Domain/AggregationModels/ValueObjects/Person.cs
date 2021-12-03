using System.Collections.Generic;
using MerchandiseService.Domain.Exceptions.ValueObjects;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.ValueObjects
{
    /// <summary>
    /// ФИО
    /// </summary>
    public class Person : ValueObject
    {
        public Person(string firstName, string lastName, string middleName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                throw new PersonException("FirstName cannot be empty");
            }

            if (string.IsNullOrEmpty(lastName))
            {
                throw new PersonException("LastName cannot be empty");
            }

            if (string.IsNullOrEmpty(middleName))
            {
                throw new PersonException("MiddleName cannot be empty");
            }

            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
        }

        public Person(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                throw new PersonException("Value cannot be empty");
            }

            var value = fullName.Trim().Split(" ");

            if (value.Length != 3)
            {
                throw new PersonException($"Cannot read person from value {fullName}");
            }

            FirstName = value[0];
            LastName = value[1];
            MiddleName = value[2];
        }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; }

        public string FullName => $"{FirstName} {LastName} {MiddleName}";

        public string Initials => $"{FirstName.Substring(0, 1)}{LastName.Substring(0, 1)}{MiddleName.Substring(0, 1)}";

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
            yield return MiddleName;
        }
    }
}
