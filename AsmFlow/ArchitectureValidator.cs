namespace AsmFlow
{
    public static class ArchitectureValidator
    {
        private static readonly HashSet<string> _64BitOnlyInstructions = new()
        {
            "syscall", "movsxd", "movsq", "stosq", "lodsq", "cmpsq", "scasq",
            "cmovz", "cmovnz", "cmova", "cmovb", // در 64-bit بهتر پشتیبانی می‌شوند
            "bsf", "bsr", "bt", "bts", "btr", "btc" // با رجیسترهای 64-bit
        };

        private static readonly HashSet<string> _SSE2Required = new()
        {
            "lfence", "mfence", "sfence"
        };

        public static void ValidateInstruction(Architecture arch, string instruction, params object[] operands)
        {
            // بررسی دستورات مخصوص 64-bit
            if (_64BitOnlyInstructions.Contains(instruction.ToLower()) && arch != Architecture.x86_64)
            {
                throw new ArchitectureMismatchException(
                    Architecture.x86_64,
                    arch,
                    instruction);
            }

            // بررسی SSE2
            if (_SSE2Required.Contains(instruction.ToLower()) && arch == Architecture.x86_16)
            {
                throw new InvalidOperationException(
                    $"{instruction} requires SSE2 which is not available in 16-bit mode");
            }

            // بررسی رجیسترها
            foreach (var operand in operands)
            {
                if (operand is MemoryOperand memOp)
                {
                    ValidateMemoryOperand(memOp, arch, instruction);
                }
                else if (!IsRegisterCompatible(operand, arch))
                {
                    throw new RegisterSizeException(operand.GetType(), instruction);
                }
            }
        }

        private static void ValidateMemoryOperand(MemoryOperand memOp, Architecture arch, string instruction)
        {
            // بررسی رجیسترهای base و index
            if (memOp.BaseRegister != null && !IsRegisterCompatible(memOp.BaseRegister, arch))
            {
                throw new RegisterSizeException(memOp.BaseRegister.GetType(), instruction);
            }

            if (memOp.IndexRegister != null && !IsRegisterCompatible(memOp.IndexRegister, arch))
            {
                throw new RegisterSizeException(memOp.IndexRegister.GetType(), instruction);
            }

            // در 16-bit mode، آدرس‌دهی محدودیت‌هایی دارد
            if (arch == Architecture.x86_16)
            {
                if (memOp.Scale > 1)
                    throw new InvalidOperationException(
                        "Scaling is not supported in 16-bit mode");

                if (memOp.IndexRegister != null && memOp.BaseRegister == null)
                    throw new InvalidOperationException(
                        "Index-only addressing is not supported in 16-bit mode");
            }
        }

        public static bool IsRegisterCompatible(object register, Architecture arch)
        {
            return register switch
            {
                Register8 => true,
                Register16 => true,
                Register32 => arch != Architecture.x86_16,
                Register64 => arch == Architecture.x86_64,
                _ => true // برای null یا انواع دیگر
            };
        }
    }
}