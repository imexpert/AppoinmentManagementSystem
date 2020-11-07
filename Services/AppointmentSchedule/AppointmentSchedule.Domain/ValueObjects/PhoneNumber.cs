using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NVI.DomainBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSchedule.Domain.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public int CountryCode { get; private set; }
        public int AreaCode { get; private set; }
        public int Number { get; private set; }

        public PhoneNumber(int countryCode, int areaCode, int number)
        {
            CountryCode = countryCode;
            AreaCode = areaCode;
            Number = number;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CountryCode;
            yield return AreaCode;
            yield return Number;
        }
        public string ToString()
        {
            return CountryCode.ToString() + AreaCode.ToString() + this.Number.ToString();
        }
    }
}
