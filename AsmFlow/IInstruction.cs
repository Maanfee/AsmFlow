namespace AsmFlow
{
    public interface IInstruction
    {
        // متدهای اصلی MOV با object
        IAssemblyBuilder MOV(Register8 dest, object source);
        IAssemblyBuilder MOV(Register16 dest, object source);
        IAssemblyBuilder MOV(Register32 dest, object source);
        IAssemblyBuilder MOV(Register64 dest, object source);

        // دستورات سیستم
        IAssemblyBuilder HLT();
        IAssemblyBuilder NOP();
        IAssemblyBuilder RET();
        IAssemblyBuilder INT(byte interruptNumber);

        // دستورات ریاضی
        IAssemblyBuilder ADD(Register8 dest, object source);
        IAssemblyBuilder ADD(Register16 dest, object source);
        IAssemblyBuilder ADD(Register32 dest, object source);
        IAssemblyBuilder ADD(Register64 dest, object source);

        IAssemblyBuilder SUB(Register8 dest, object source);
        IAssemblyBuilder SUB(Register16 dest, object source);
        IAssemblyBuilder SUB(Register32 dest, object source);
        IAssemblyBuilder SUB(Register64 dest, object source);

        IAssemblyBuilder INC(Register8 dest);
        IAssemblyBuilder INC(Register16 dest);
        IAssemblyBuilder INC(Register32 dest);
        IAssemblyBuilder INC(Register64 dest);

        IAssemblyBuilder DEC(Register8 dest);
        IAssemblyBuilder DEC(Register16 dest);
        IAssemblyBuilder DEC(Register32 dest);
        IAssemblyBuilder DEC(Register64 dest);

        // دستورات منطقی
        IAssemblyBuilder AND(Register8 dest, object source);
        IAssemblyBuilder AND(Register16 dest, object source);
        IAssemblyBuilder AND(Register32 dest, object source);
        IAssemblyBuilder AND(Register64 dest, object source);

        IAssemblyBuilder OR(Register8 dest, object source);
        IAssemblyBuilder OR(Register16 dest, object source);
        IAssemblyBuilder OR(Register32 dest, object source);
        IAssemblyBuilder OR(Register64 dest, object source);

        IAssemblyBuilder XOR(Register8 dest, object source);
        IAssemblyBuilder XOR(Register16 dest, object source);
        IAssemblyBuilder XOR(Register32 dest, object source);
        IAssemblyBuilder XOR(Register64 dest, object source);

        IAssemblyBuilder NOT(Register8 dest);
        IAssemblyBuilder NOT(Register16 dest);
        IAssemblyBuilder NOT(Register32 dest);
        IAssemblyBuilder NOT(Register64 dest);

        // دستورات مقایسه
        IAssemblyBuilder CMP(Register8 dest, object source);
        IAssemblyBuilder CMP(Register16 dest, object source);
        IAssemblyBuilder CMP(Register32 dest, object source);
        IAssemblyBuilder CMP(Register64 dest, object source);

        // دستورات شیفت
        IAssemblyBuilder SHL(Register8 dest, object count);
        IAssemblyBuilder SHL(Register16 dest, object count);
        IAssemblyBuilder SHL(Register32 dest, object count);
        IAssemblyBuilder SHL(Register64 dest, object count);

        IAssemblyBuilder SHR(Register8 dest, object count);
        IAssemblyBuilder SHR(Register16 dest, object count);
        IAssemblyBuilder SHR(Register32 dest, object count);
        IAssemblyBuilder SHR(Register64 dest, object count);

        // پرش‌ها
        IAssemblyBuilder JMP(object target);
        IAssemblyBuilder JZ(object target);
        IAssemblyBuilder JNZ(object target);
        IAssemblyBuilder JE(object target);
        IAssemblyBuilder JNE(object target);
        IAssemblyBuilder JC(object target);
        IAssemblyBuilder JNC(object target);
        IAssemblyBuilder JA(object target);
        IAssemblyBuilder JB(object target);

        // تعریف لیبل
        IAssemblyBuilder Label(string name);

        // دستورات پشته
        IAssemblyBuilder PUSH(object source);
        IAssemblyBuilder POP(object dest);

        // دستورات رشته
        IAssemblyBuilder MOVSB();
        IAssemblyBuilder MOVSW();
        IAssemblyBuilder MOVSD();
        IAssemblyBuilder MOVSQ();

        // دستورات شرطی
        IAssemblyBuilder TEST(Register8 dest, object source);
        IAssemblyBuilder TEST(Register16 dest, object source);
        IAssemblyBuilder TEST(Register32 dest, object source);
        IAssemblyBuilder TEST(Register64 dest, object source);
    }
}
