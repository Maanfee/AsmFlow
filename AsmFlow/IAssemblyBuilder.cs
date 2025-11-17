namespace AsmFlow
{
    public interface IAssemblyBuilder : IInstruction
    {      
        // تولید خروجی
        string Generate(Assembler Assembler);
        void SaveToFile(string filename);
        List<string> GetInstructions();
        void Clear();
    }
}
