using System.Text;

namespace AsmFlow
{
    public class AssemblyBuilder : IAssemblyBuilder
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

        // ========== MOV Instructions ==========
        public IAssemblyBuilder MOV(Register8 dest, object source)
        {
            string destStr = dest.ToString().ToLower();
            string sourceStr = GetSourceString(source, 8);
            _instructions.Add($"mov {destStr}, {sourceStr}");
            return this;
        }

        public IAssemblyBuilder MOV(Register16 dest, object source)
        {
            string destStr = dest.ToString().ToLower();
            string sourceStr = GetSourceString(source, 16);
            _instructions.Add($"mov {destStr}, {sourceStr}");
            return this;
        }

        public IAssemblyBuilder MOV(Register32 dest, object source)
        {
            string destStr = dest.ToString().ToLower();
            string sourceStr = GetSourceString(source, 32);

            // تشخیص خودکار برای MOVZX/MOVSX
            if (source is Register8 srcReg8)
            {
                _instructions.Add($"movzx {destStr}, {srcReg8.ToString().ToLower()}");
            }
            else
            {
                _instructions.Add($"mov {destStr}, {sourceStr}");
            }

            return this;
        }

        public IAssemblyBuilder MOV(Register64 dest, object source)
        {
            string destStr = dest.ToString().ToLower();
            string sourceStr = GetSourceString(source, 64);

            // تشخیص خودکار برای extension
            switch (source)
            {
                case Register8 srcReg8:
                    _instructions.Add($"movzx {destStr}, {srcReg8.ToString().ToLower()}");
                    break;
                case Register16 srcReg16:
                    _instructions.Add($"movzx {destStr}, {srcReg16.ToString().ToLower()}");
                    break;
                case Register32 srcReg32:
                    if (_architecture == Architecture.x86_64)
                        _instructions.Add($"movsxd {destStr}, {srcReg32.ToString().ToLower()}");
                    else
                        _instructions.Add($"mov {destStr}, {srcReg32.ToString().ToLower()}");
                    break;
                default:
                    _instructions.Add($"mov {destStr}, {sourceStr}");
                    break;
            }

            return this;
        }

        // ========== System Instructions ==========
        public IAssemblyBuilder HLT()
        {
            _instructions.Add("hlt");
            return this;
        }

        public IAssemblyBuilder NOP()
        {
            _instructions.Add("nop");
            return this;
        }

        public IAssemblyBuilder RET()
        {
            _instructions.Add("ret");
            return this;
        }

        public IAssemblyBuilder INT(byte interruptNumber)
        {
            _instructions.Add($"int {interruptNumber}");
            return this;
        }

        // ========== Arithmetic Instructions ==========
        public IAssemblyBuilder ADD(Register8 dest, object source)
        {
            _instructions.Add($"add {dest.ToString().ToLower()}, {GetSourceString(source, 8)}");
            return this;
        }

        public IAssemblyBuilder ADD(Register16 dest, object source)
        {
            _instructions.Add($"add {dest.ToString().ToLower()}, {GetSourceString(source, 16)}");
            return this;
        }

        public IAssemblyBuilder ADD(Register32 dest, object source)
        {
            _instructions.Add($"add {dest.ToString().ToLower()}, {GetSourceString(source, 32)}");
            return this;
        }

        public IAssemblyBuilder ADD(Register64 dest, object source)
        {
            _instructions.Add($"add {dest.ToString().ToLower()}, {GetSourceString(source, 64)}");
            return this;
        }

        public IAssemblyBuilder SUB(Register8 dest, object source)
        {
            _instructions.Add($"sub {dest.ToString().ToLower()}, {GetSourceString(source, 8)}");
            return this;
        }

        public IAssemblyBuilder SUB(Register16 dest, object source)
        {
            _instructions.Add($"sub {dest.ToString().ToLower()}, {GetSourceString(source, 16)}");
            return this;
        }

        public IAssemblyBuilder SUB(Register32 dest, object source)
        {
            _instructions.Add($"sub {dest.ToString().ToLower()}, {GetSourceString(source, 32)}");
            return this;
        }

        public IAssemblyBuilder SUB(Register64 dest, object source)
        {
            _instructions.Add($"sub {dest.ToString().ToLower()}, {GetSourceString(source, 64)}");
            return this;
        }

        public IAssemblyBuilder INC(Register8 dest)
        {
            _instructions.Add($"inc {dest.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder INC(Register16 dest)
        {
            _instructions.Add($"inc {dest.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder INC(Register32 dest)
        {
            _instructions.Add($"inc {dest.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder INC(Register64 dest)
        {
            _instructions.Add($"inc {dest.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder DEC(Register8 dest)
        {
            _instructions.Add($"dec {dest.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder DEC(Register16 dest)
        {
            _instructions.Add($"dec {dest.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder DEC(Register32 dest)
        {
            _instructions.Add($"dec {dest.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder DEC(Register64 dest)
        {
            _instructions.Add($"dec {dest.ToString().ToLower()}");
            return this;
        }

        // ========== Logical Instructions ==========
        public IAssemblyBuilder AND(Register8 dest, object source)
        {
            _instructions.Add($"and {dest.ToString().ToLower()}, {GetSourceString(source, 8)}");
            return this;
        }

        public IAssemblyBuilder AND(Register16 dest, object source)
        {
            _instructions.Add($"and {dest.ToString().ToLower()}, {GetSourceString(source, 16)}");
            return this;
        }

        public IAssemblyBuilder AND(Register32 dest, object source)
        {
            _instructions.Add($"and {dest.ToString().ToLower()}, {GetSourceString(source, 32)}");
            return this;
        }

        public IAssemblyBuilder AND(Register64 dest, object source)
        {
            _instructions.Add($"and {dest.ToString().ToLower()}, {GetSourceString(source, 64)}");
            return this;
        }

        public IAssemblyBuilder OR(Register8 dest, object source)
        {
            _instructions.Add($"or {dest.ToString().ToLower()}, {GetSourceString(source, 8)}");
            return this;
        }

        public IAssemblyBuilder OR(Register16 dest, object source)
        {
            _instructions.Add($"or {dest.ToString().ToLower()}, {GetSourceString(source, 16)}");
            return this;
        }

        public IAssemblyBuilder OR(Register32 dest, object source)
        {
            _instructions.Add($"or {dest.ToString().ToLower()}, {GetSourceString(source, 32)}");
            return this;
        }

        public IAssemblyBuilder OR(Register64 dest, object source)
        {
            _instructions.Add($"or {dest.ToString().ToLower()}, {GetSourceString(source, 64)}");
            return this;
        }

        public IAssemblyBuilder XOR(Register8 dest, object source)
        {
            _instructions.Add($"xor {dest.ToString().ToLower()}, {GetSourceString(source, 8)}");
            return this;
        }

        public IAssemblyBuilder XOR(Register16 dest, object source)
        {
            _instructions.Add($"xor {dest.ToString().ToLower()}, {GetSourceString(source, 16)}");
            return this;
        }

        public IAssemblyBuilder XOR(Register32 dest, object source)
        {
            _instructions.Add($"xor {dest.ToString().ToLower()}, {GetSourceString(source, 32)}");
            return this;
        }

        public IAssemblyBuilder XOR(Register64 dest, object source)
        {
            _instructions.Add($"xor {dest.ToString().ToLower()}, {GetSourceString(source, 64)}");
            return this;
        }

        public IAssemblyBuilder NOT(Register8 dest)
        {
            _instructions.Add($"not {dest.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder NOT(Register16 dest)
        {
            _instructions.Add($"not {dest.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder NOT(Register32 dest)
        {
            _instructions.Add($"not {dest.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder NOT(Register64 dest)
        {
            _instructions.Add($"not {dest.ToString().ToLower()}");
            return this;
        }

        // ========== Comparison Instructions ==========
        public IAssemblyBuilder CMP(Register8 dest, object source)
        {
            _instructions.Add($"cmp {dest.ToString().ToLower()}, {GetSourceString(source, 8)}");
            return this;
        }

        public IAssemblyBuilder CMP(Register16 dest, object source)
        {
            _instructions.Add($"cmp {dest.ToString().ToLower()}, {GetSourceString(source, 16)}");
            return this;
        }

        public IAssemblyBuilder CMP(Register32 dest, object source)
        {
            _instructions.Add($"cmp {dest.ToString().ToLower()}, {GetSourceString(source, 32)}");
            return this;
        }

        public IAssemblyBuilder CMP(Register64 dest, object source)
        {
            _instructions.Add($"cmp {dest.ToString().ToLower()}, {GetSourceString(source, 64)}");
            return this;
        }

        // ========== Shift Instructions ==========
        public IAssemblyBuilder SHL(Register8 dest, object count)
        {
            _instructions.Add($"shl {dest.ToString().ToLower()}, {GetSourceString(count, 8)}");
            return this;
        }

        public IAssemblyBuilder SHL(Register16 dest, object count)
        {
            _instructions.Add($"shl {dest.ToString().ToLower()}, {GetSourceString(count, 16)}");
            return this;
        }

        public IAssemblyBuilder SHL(Register32 dest, object count)
        {
            _instructions.Add($"shl {dest.ToString().ToLower()}, {GetSourceString(count, 32)}");
            return this;
        }

        public IAssemblyBuilder SHL(Register64 dest, object count)
        {
            _instructions.Add($"shl {dest.ToString().ToLower()}, {GetSourceString(count, 64)}");
            return this;
        }

        public IAssemblyBuilder SHR(Register8 dest, object count)
        {
            _instructions.Add($"shr {dest.ToString().ToLower()}, {GetSourceString(count, 8)}");
            return this;
        }

        public IAssemblyBuilder SHR(Register16 dest, object count)
        {
            _instructions.Add($"shr {dest.ToString().ToLower()}, {GetSourceString(count, 16)}");
            return this;
        }

        public IAssemblyBuilder SHR(Register32 dest, object count)
        {
            _instructions.Add($"shr {dest.ToString().ToLower()}, {GetSourceString(count, 32)}");
            return this;
        }

        public IAssemblyBuilder SHR(Register64 dest, object count)
        {
            _instructions.Add($"shr {dest.ToString().ToLower()}, {GetSourceString(count, 64)}");
            return this;
        }

        // ========== Jump Instructions ==========
        public IAssemblyBuilder JMP(object target)
        {
            _instructions.Add($"jmp {GetSourceString(target, 0)}");
            return this;
        }

        public IAssemblyBuilder JZ(object target)
        {
            _instructions.Add($"jz {GetSourceString(target, 0)}");
            return this;
        }

        public IAssemblyBuilder JNZ(object target)
        {
            _instructions.Add($"jnz {GetSourceString(target, 0)}");
            return this;
        }

        public IAssemblyBuilder JE(object target)
        {
            _instructions.Add($"je {GetSourceString(target, 0)}");
            return this;
        }

        public IAssemblyBuilder JNE(object target)
        {
            _instructions.Add($"jne {GetSourceString(target, 0)}");
            return this;
        }

        public IAssemblyBuilder JC(object target)
        {
            _instructions.Add($"jc {GetSourceString(target, 0)}");
            return this;
        }

        public IAssemblyBuilder JNC(object target)
        {
            _instructions.Add($"jnc {GetSourceString(target, 0)}");
            return this;
        }

        public IAssemblyBuilder JA(object target)
        {
            _instructions.Add($"ja {GetSourceString(target, 0)}");
            return this;
        }

        public IAssemblyBuilder JB(object target)
        {
            _instructions.Add($"jb {GetSourceString(target, 0)}");
            return this;
        }

        // ========== Labels ==========
        public IAssemblyBuilder Label(string name)
        {
            _instructions.Add($"{name}:");
            return this;
        }

        // ========== Stack Instructions ==========
        public IAssemblyBuilder PUSH(object source)
        {
            _instructions.Add($"push {GetSourceString(source, 0)}");
            return this;
        }

        public IAssemblyBuilder POP(object dest)
        {
            _instructions.Add($"pop {GetSourceString(dest, 0)}");
            return this;
        }

        // ========== String Instructions ==========
        public IAssemblyBuilder MOVSB()
        {
            _instructions.Add("movsb");
            return this;
        }

        public IAssemblyBuilder MOVSW()
        {
            _instructions.Add("movsw");
            return this;
        }

        public IAssemblyBuilder MOVSD()
        {
            _instructions.Add("movsd");
            return this;
        }

        public IAssemblyBuilder MOVSQ()
        {
            _instructions.Add("movsq");
            return this;
        }

        // ========== Test Instruction ==========
        public IAssemblyBuilder TEST(Register8 dest, object source)
        {
            _instructions.Add($"test {dest.ToString().ToLower()}, {GetSourceString(source, 8)}");
            return this;
        }

        public IAssemblyBuilder TEST(Register16 dest, object source)
        {
            _instructions.Add($"test {dest.ToString().ToLower()}, {GetSourceString(source, 16)}");
            return this;
        }

        public IAssemblyBuilder TEST(Register32 dest, object source)
        {
            _instructions.Add($"test {dest.ToString().ToLower()}, {GetSourceString(source, 32)}");
            return this;
        }

        public IAssemblyBuilder TEST(Register64 dest, object source)
        {
            _instructions.Add($"test {dest.ToString().ToLower()}, {GetSourceString(source, 64)}");
            return this;
        }

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
