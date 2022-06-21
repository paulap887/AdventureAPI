using System;
using System.ComponentModel.DataAnnotations;

namespace Adventure.Api.Validator
{
    /// <summary>
    /// Required Not Empty Attribute
    /// </summary>
    public class RequiredNotEmptyAttribute : RequiredAttribute
    {
        /// <summary>
        /// Is Valid Check 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value is string) return !String.IsNullOrEmpty((string)value);

            return base.IsValid(value);
        }
    } 
}
