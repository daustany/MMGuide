using Quadrant.Interfaces;
using Quadrant.Models;

namespace Quadrant.Services
{
    /// <summary>
    /// Service for parsing dial data from string format.
    /// Follows Single Responsibility Principle - only handles parsing logic.
    /// </summary>
    public class DialParser : IDialParser
    {
        private readonly DialConfiguration _configuration;
        private readonly IInputValidator _validator;

        public DialParser(DialConfiguration configuration, IInputValidator validator)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public int[] ParseDialLine(string line, int lineNumber)
        {
            if (!_validator.ValidateDialLine(line, lineNumber))
                throw new InvalidDataException($"Malformed line {lineNumber}: '{line}' (must be exactly {_configuration.DialCount} digits {_configuration.MinDialValue}-{_configuration.MaxDialValue})");

            var result = new int[_configuration.DialCount];
            for (int i = 0; i < _configuration.DialCount; i++)
            {
                result[i] = line[i] - '0';
            }

            return result;
        }

        public List<DialSet> ParseDialSets(IEnumerable<string> lines)
        {
            if (lines == null)
                throw new ArgumentNullException(nameof(lines));

            var lineList = lines.ToList();
            var dialSets = new List<DialSet>();

            for (int i = 0; i < lineList.Count; i += 2)
            {
                var current = ParseDialLine(lineList[i], i + 1);
                var target = ParseDialLine(lineList[i + 1], i + 2);
                dialSets.Add(DialSet.Create(current, target));
            }

            return dialSets;
        }
    }
}
