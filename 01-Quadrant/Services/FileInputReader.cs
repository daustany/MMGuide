using Quadrant.Interfaces;
using Quadrant.Models;

namespace Quadrant.Services
{
    /// <summary>
    /// Service for reading dial sets from file input.
    /// Follows Single Responsibility Principle - only handles file I/O.
    /// </summary>
    public class FileInputReader : IInputReader
    {
        private readonly IInputValidator _validator;
        private readonly IDialParser _parser;

        public FileInputReader(IInputValidator validator, IDialParser parser)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _parser = parser ?? throw new ArgumentNullException(nameof(parser));
        }

        public List<DialSet> ReadDialSets(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be null or empty", nameof(filePath));

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Input file not found: {filePath}");

            var lines = ReadNonBlankLines(filePath);

            if (!_validator.ValidateInputStructure(lines))
                throw new InvalidDataException("Input file structure is invalid");

            return _parser.ParseDialSets(lines);
        }

        private List<string> ReadNonBlankLines(string filePath)
        {
            var lines = new List<string>();

            foreach (var line in File.ReadLines(filePath))
            {
                var trimmed = line.Trim();
                if (!string.IsNullOrEmpty(trimmed))
                    lines.Add(trimmed);
            }

            return lines;
        }
    }
}
