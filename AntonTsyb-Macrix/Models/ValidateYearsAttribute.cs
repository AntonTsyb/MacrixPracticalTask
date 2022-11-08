using System;
using System.ComponentModel.DataAnnotations;

namespace MacrixPracticalTask.Models
{
    public class ValidateYearsAttribute : ValidationAttribute
    {
        private readonly DateTime _minValue = new DateTime(1900,1,1);
        private readonly DateTime _maxValue = DateTime.UtcNow;

        public override bool IsValid(object? value)
        {
            if(value == null) return false;
            DateTime val = (DateTime)value;
            return val >= _minValue && val <= _maxValue;
        }
    }
}
