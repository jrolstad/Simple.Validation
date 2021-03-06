using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Simple.Validation.Tests
{
    public static class ExtensionMethods
    {
        public static void AssertValidFor(this IEnumerable<ValidationResult> self, string propertyName, object type)
        {
            var result = GetValidationResult(self, propertyName, type);
            Assert.That(result, Is.Null);
        }

        public static void AssertInvalidFor(this IEnumerable<ValidationResult> self, string propertyName, object type, ValidationResultSeverity severity = ValidationResultSeverity.Error)
        {
            var result = GetValidationResult(self, propertyName, type);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Severity, Is.EqualTo(severity));
        }

        private static ValidationResult GetValidationResult(IEnumerable<ValidationResult> self, string propertyName, object type)
        {
            ValidationResult result;
            if (type == null)
            {
                result = self.FirstOrDefault(v => v.PropertyName == propertyName && v.Type == null);
            }
            else
            {
                result = self.FirstOrDefault(v => v.PropertyName == propertyName && v.Type != null && v.Type.Equals(type));
            }
            return result;
        }
    }
}