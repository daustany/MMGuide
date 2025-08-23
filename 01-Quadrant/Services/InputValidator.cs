using Quadrant.Interfaces;
using Quadrant.Models;

namespace Quadrant.Services
{
    /// <summary>
    /// Service for validating input data.
    /// Follows Single Responsibility Principle - only handles validation logic.
    /// </summary>
    public class InputValidator : IInputValidator
    {
        private readonly DialConfiguration _configuration;

        public InputValidator(DialConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public bool ValidateDialLine(string line, int lineNumber)
        {
            if (string.IsNullOrWhiteSpace(line))
                return false;

            if (line.Length != _configuration.DialCount)
                return false;

            return IsAllValidDigits(line);
        }

        public bool ValidateInputStructure(IEnumerable<string> lines)
        {
            if (lines == null)
                return false;

            var lineList = lines.ToList();

            // Must have even number of lines (pairs of current/target)
            if (lineList.Count % 2 != 0)
                return false;

            // Validate each line
            for (int i = 0; i < lineList.Count; i++)
            {
                if (!ValidateDialLine(lineList[i], i + 1))
                    return false;
            }

            return true;
        }

        private bool IsAllValidDigits(string line)
        {
            foreach (var c in line)
            {
                if (!char.IsDigit(c))
                    return false;

                var digit = c - '0';
                if (!_configuration.IsValidDialValue(digit))
                    return false;
            }
            return true;
        }
    }
}
