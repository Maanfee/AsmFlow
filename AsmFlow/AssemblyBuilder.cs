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
                default:
                    throw new InvalidOperationException($"Unsupported architecture: {_architecture}");
            }

            sb.AppendLine();
            sb.AppendLine("section .text");

            // شروع برنامه
            if (_architecture == Architecture.x86_16)
            {
                sb.AppendLine("start:");
            }
            else
            {
                sb.AppendLine("global _start");
                sb.AppendLine("_start:");
            }

            // دستورات
            foreach (var instruction in _instructions)
            {
                if (instruction.EndsWith(":"))
                    sb.AppendLine(instruction);
                else
                    sb.AppendLine($"    {instruction}");
            }

            // اضافه کردن بخش خروج از برنامه برای مدل‌های مختلف
            sb.AppendLine();
            sb.AppendLine("; Exit program");

            if (_architecture == Architecture.x86_16)
            {
                sb.AppendLine("    mov ax, 0x4C00");
                sb.AppendLine("    int 0x21");
            }
            else if (_architecture == Architecture.x86_32)
            {
                sb.AppendLine("    mov eax, 1    ; sys_exit");
                sb.AppendLine("    xor ebx, ebx  ; exit code 0");
                sb.AppendLine("    int 0x80");
            }
            else if (_architecture == Architecture.x86_64)
            {
                sb.AppendLine("    mov rax, 60   ; sys_exit");
                sb.AppendLine("    xor rdi, rdi  ; exit code 0");
                sb.AppendLine("    syscall");
            }

            return sb.ToString();
        }

        public void SaveToFile(string filename)
        {
            try
            {
                var directory = Path.GetDirectoryName(filename);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var content = Generate(Assembler.NASM);
                File.WriteAllText(filename, content);

                Console.WriteLine($"Assembly code saved to: {Path.GetFullPath(filename)}");
            }
            catch (Exception ex)
            {
                throw new AssemblyException($"Failed to save file: {filename}", ex);
            }
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
                // انواع عددی موجود
                byte b => FormatValue(b, operandSize),
                sbyte sb => FormatValue(sb, operandSize),
                ushort w => FormatValue(w, operandSize),
                short sw => FormatValue(sw, operandSize),
                uint dw => FormatValue(dw, operandSize),
                int sdw => FormatValue(sdw, operandSize),
                ulong qw => FormatValue(qw, operandSize),
                long sqw => FormatValue(sqw, operandSize),

                // رجیسترها
                Register8 reg => reg.ToString().ToLower(),
                Register16 reg => reg.ToString().ToLower(),
                Register32 reg => reg.ToString().ToLower(),
                Register64 reg => reg.ToString().ToLower(),

                // Memory operands جدید
                MemoryOperand mem => mem.ToString(),

                // لیبل‌ها و ثوابت
                string str => str,

                // برای null مقادیر
                null => throw new ArgumentNullException(nameof(source), "Source cannot be null"),

                // خطا برای نوع‌های ناشناخته
                _ => throw new ArgumentException(
                    $"Unsupported source type: {source.GetType().Name}. " +
                    $"Supported types: numeric types, registers, MemoryOperand, and strings.")
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
                64 => $"0x{dynamicValue:X8}",  // اصلاح: X16 به X8 (حداکثر 16 رقم هگز نیاز نیست)
                _ => dynamicValue.ToString()
            };
        }

        // ========== Utility Methods ==========
        public string CreateLabel(string baseName)
        {
            if (string.IsNullOrWhiteSpace(baseName))
                throw new ArgumentException("Base name cannot be null or empty", nameof(baseName));

            // حذف کاراکترهای غیرمجاز
            var cleanName = new string(baseName.Where(c =>
                char.IsLetterOrDigit(c) || c == '_').ToArray());

            if (string.IsNullOrEmpty(cleanName))
                cleanName = "label";

            if (!_labelCounters.ContainsKey(cleanName))
                _labelCounters[cleanName] = 0;

            _labelCounters[cleanName]++;
            return $"{cleanName}_{_labelCounters[cleanName]}";
        }

        public IAssemblyBuilder LocalLabel(string name)
        {
            // لیبل‌های محلی با نقطه شروع می‌شوند
            _instructions.Add($".{name}:");
            return this;
        }

        // ========== Comment and Formatting ==========
        public IAssemblyBuilder Comment(string comment)
        {
            if (!string.IsNullOrWhiteSpace(comment))
            {
                _instructions.Add($"; {comment}");
            }
            return this;
        }

        public IAssemblyBuilder NewLine()
        {
            _instructions.Add(string.Empty);
            return this;
        }

        public IAssemblyBuilder Section(string sectionName)
        {
            _instructions.Add($"section .{sectionName}");
            return this;
        }

        // ========== Instruction Count ==========
        public int InstructionCount => _instructions.Count(i => !string.IsNullOrWhiteSpace(i) && !i.StartsWith(";"));

        // ========== Get Architecture Info ==========
        public Architecture GetArchitecture() => _architecture;

        // ========== Clone Builder ==========
        public IAssemblyBuilder Clone()
        {
            var clone = new AssemblyBuilder(_architecture);
            clone._instructions.AddRange(_instructions);
            return clone;
        }
    }
}
