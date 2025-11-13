namespace AsmFlow
{
    public static class AssemblyFactory
    {
        public static IAssemblyBuilder CreateBuilder(Architecture arch)
        {
            return new AssemblyBuilder(arch);
        }
    }
}
