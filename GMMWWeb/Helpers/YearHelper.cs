using System.ComponentModel.DataAnnotations;

namespace GMMWWeb.Helpers;

/**
 * Provide helper logic on validation on specific year entry by comparing with current year and given minimum year
 * Display error if condition not met
 */
public class YearHelper : RangeAttribute
{
    public YearHelper(int minYear) : base(minYear, DateTime.UtcNow.Year) => ErrorMessage = $"Year must be between {minYear} and {DateTime.UtcNow.Year}";
}