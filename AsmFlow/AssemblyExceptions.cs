namespace AsmFlow
{
    public class AssemblyException : Exception
    {
        public int LineNumber { get; }
        public string Instruction { get; }

        public AssemblyException(string message, int lineNumber = -1, string instruction = null)
            : base(message)
        {
            LineNumber = lineNumber;
            Instruction = instruction;
        }

        public AssemblyException(string message, Exception innerException,
            int lineNumber = -1, string instruction = null)
            : base(message, innerException)
        {
            LineNumber = lineNumber;
            Instruction = instruction;
        }
    }

    public class ArchitectureMismatchException : AssemblyException
    {
        public Architecture RequiredArchitecture { get; }
        public Architecture CurrentArchitecture { get; }

        public ArchitectureMismatchException(
            Architecture required,
            Architecture current,
            string instruction)
            : base($"Instruction '{instruction}' requires {required} architecture, " +
                  $"but current architecture is {current}", -1, instruction)
        {
            RequiredArchitecture = required;
            CurrentArchitecture = current;
        }
    }

    public class RegisterSizeException : AssemblyException
    {
        public Type RegisterType { get; }

        public RegisterSizeException(Type registerType, string instruction)
            : base($"Register type {registerType.Name} is not compatible with " +
                  $"instruction '{instruction}'", -1, instruction)
        {
            RegisterType = registerType;
        }
    }
}
