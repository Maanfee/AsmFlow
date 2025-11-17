using System.Text;

namespace AsmFlow
{
    public partial class AssemblyBuilder : IAssemblyBuilder, IInstruction
    {
        public AssemblyBuilder(Architecture arch)
        {
            _instructions = new List<string>();
            _architecture = arch;
            _labelCounters = new Dictionary<string, int>();
        }

        private readonly List<string> _instructions;
        private Architecture _architecture;
        private readonly Dictionary<string, int> _labelCounters;
       
        // ========== Output Methods ==========
        public string Generate(Assembler Assembler)
        {
            var sb = new StringBuilder();

            // هدر بر اساس معماری
            switch (_architecture)
            {
                case Architecture.x86_16:
                    sb.AppendLine("bits 16");
                    sb.AppendLine("org 0x100");
                    break;
                case Architecture.x86_32:
                    sb.AppendLine("bits 32");
                    break;
                case Architecture.x86_64:
                    sb.AppendLine("bits 64");
                    break;
            }

            sb.AppendLine("section .text");

            if (_architecture == Architecture.x86_16)
            {
                sb.AppendLine("start:");
            }
            else
            {
                sb.AppendLine("global _start");
                sb.AppendLine("_start:");
            }

            foreach (var instruction in _instructions)
            {
                if (instruction.EndsWith(":"))
                    sb.AppendLine(instruction);
                else
                    sb.AppendLine($"    {instruction}");
            }

            return sb.ToString();
        }

        public void SaveToFile(string filename)
        {
            File.WriteAllText(filename, Generate(Assembler.NASM));
        }

        public List<string> GetInstructions()
        {
            return new List<string>(_instructions);
        }

        public void Clear()
        {
            _instructions.Clear();
        }

        // ========== Helper Methods ==========
        private string GetSourceString(object source, int operandSize)
        {
            return source switch
            {
                byte b => FormatValue(b, operandSize),
                sbyte sb => FormatValue(sb, operandSize),
                ushort w => FormatValue(w, operandSize),
                short sw => FormatValue(sw, operandSize),
                uint dw => FormatValue(dw, operandSize),
                int sdw => FormatValue(sdw, operandSize),
                ulong qw => FormatValue(qw, operandSize),
                long sqw => FormatValue(sqw, operandSize),
                Register8 reg => reg.ToString().ToLower(),
                Register16 reg => reg.ToString().ToLower(),
                Register32 reg => reg.ToString().ToLower(),
                Register64 reg => reg.ToString().ToLower(),
                string str => str,
                _ => throw new ArgumentException($"Unsupported source type: {source.GetType()}")
            };
        }

        private string FormatValue<T>(T value, int operandSize) where T : struct
        {
            if (typeof(T) == typeof(string))
                return value.ToString();

            dynamic dynamicValue = value;

            // برای اعداد منفی
            if (dynamicValue < 0)
                return dynamicValue.ToString();

            // فرمت هگزادسیمال بر اساس سایز operand
            return operandSize switch
            {
                8 => $"0x{dynamicValue:X2}",
                16 => $"0x{dynamicValue:X4}",
                32 => $"0x{dynamicValue:X8}",
                64 => $"0x{dynamicValue:X16}",
                _ => dynamicValue.ToString() // برای موارد عمومی
            };
        }

        // ========== Utility Methods ==========
        public string CreateLabel(string baseName)
        {
            if (!_labelCounters.ContainsKey(baseName))
                _labelCounters[baseName] = 0;

            _labelCounters[baseName]++;
            return $"{baseName}_{_labelCounters[baseName]}";
        }
    }
}
