namespace AsmFlow
{
    public class MemoryOperand
    {
        public object BaseRegister { get; }
        public object IndexRegister { get; }
        public int Scale { get; }
        public int Displacement { get; }
        public int Size { get; set; } // تغییر به set برای قابلیت نوشتن

        public MemoryOperand(object baseRegister = null, object indexRegister = null,
                           int scale = 1, int displacement = 0, int size = 0)
        {
            BaseRegister = baseRegister;
            IndexRegister = indexRegister;
            Scale = scale;
            Displacement = displacement;
            Size = size;

            Validate();
        }

        private void Validate()
        {
            // اعتبارسنجی scale (باید 1, 2, 4, یا 8 باشد)
            if (Scale != 1 && Scale != 2 && Scale != 4 && Scale != 8)
                throw new ArgumentException("Scale must be 1, 2, 4, or 8");

            // اعتبارسنجی size
            if (Size != 0 && Size != 1 && Size != 2 && Size != 4 && Size != 8)
                throw new ArgumentException("Size must be 0, 1, 2, 4, or 8 bytes");

            // اعتبارسنجی رجیسترها
            ValidateRegister(BaseRegister, "Base");
            ValidateRegister(IndexRegister, "Index");
        }

        private void ValidateRegister(object register, string registerType)
        {
            if (register == null) return;

            if (!(register is Register16 || register is Register32 || register is Register64))
                throw new ArgumentException($"{registerType} register must be 16, 32, or 64-bit");
        }

        public override string ToString()
        {
            var parts = new List<string>();

            // اندازه حافظه (مانند byte, word, dword, qword)
            if (Size > 0)
            {
                parts.Add(GetSizePrefix());
            }

            parts.Add("[");

            var components = new List<string>();

            // Base register
            if (BaseRegister != null)
            {
                components.Add(GetRegisterString(BaseRegister));
            }

            // Index register با scale
            if (IndexRegister != null)
            {
                var indexStr = GetRegisterString(IndexRegister);
                if (Scale > 1)
                    indexStr += $"*{Scale}";
                components.Add(indexStr);
            }

            // Displacement
            if (Displacement != 0)
            {
                var dispStr = Displacement.ToString();
                if (Displacement > 0)
                    dispStr = $"+{dispStr}";
                components.Add(dispStr);
            }

            parts.Add(string.Join("+", components));
            parts.Add("]");

            return string.Join("", parts);
        }

        private string GetSizePrefix()
        {
            return Size switch
            {
                1 => "byte ",
                2 => "word ",
                4 => "dword ",
                8 => "qword ",
                _ => string.Empty
            };
        }

        private string GetRegisterString(object register)
        {
            return register switch
            {
                Register16 reg16 => reg16.ToString().ToLower(),
                Register32 reg32 => reg32.ToString().ToLower(),
                Register64 reg64 => reg64.ToString().ToLower(),
                _ => string.Empty
            };
        }

        // Factory methods برای ایجاد آدرس‌های رایج
        public static MemoryOperand DirectAddress(string label, int size = 0)
        {
            return new MemoryOperand(null, null, 1, 0, size);
        }

        public static MemoryOperand RegisterIndirect(object baseRegister, int size = 0)
        {
            return new MemoryOperand(baseRegister, null, 1, 0, size);
        }

        public static MemoryOperand BasedIndexed(object baseRegister, object indexRegister,
                                               int scale = 1, int size = 0)
        {
            return new MemoryOperand(baseRegister, indexRegister, scale, 0, size);
        }

        public static MemoryOperand DisplacementOnly(int displacement, int size = 0)
        {
            return new MemoryOperand(null, null, 1, displacement, size);
        }
    }
}
