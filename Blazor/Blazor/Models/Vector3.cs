using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Models
{
    [Owned]
    public class Vector3(float x, float y, float z)
    {
        public float X { get; set; } = x;
        public float Y { get; set; } = y;
        public float Z { get; set; } = z;

        public override string ToString() => $"[{X}; {Y}; {Z}]";
    }

    public class VectorSizeAttribute : ValidationAttribute
    {
        override protected ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not Vector3 vector)
                return new ValidationResult("Vector size can only validate a vector.");

            if (vector.X <= 0 || vector.Y <= 0 || vector.Z <= 0)
                return new ValidationResult("X, Y and Z must be higher than 0.", [validationContext?.MemberName ?? string.Empty]);

            return ValidationResult.Success; //This is actually null!
        }
    }
}