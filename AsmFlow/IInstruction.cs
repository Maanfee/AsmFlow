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
        IAssemblyBuilder SYSCALL();

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

        // دستورات ضرب و تقسیم
        IAssemblyBuilder MUL(Register8 source);
        IAssemblyBuilder MUL(Register16 source);
        IAssemblyBuilder MUL(Register32 source);
        IAssemblyBuilder MUL(Register64 source);

        // IMUL با یک عملوند
        IAssemblyBuilder IMUL(Register8 source);
        IAssemblyBuilder IMUL(Register16 source);
        IAssemblyBuilder IMUL(Register32 source);
        IAssemblyBuilder IMUL(Register64 source);

        // IMUL با دو عملوند (اضافه کردن)
        IAssemblyBuilder IMUL(Register8 dest, object source);
        IAssemblyBuilder IMUL(Register16 dest, object source);
        IAssemblyBuilder IMUL(Register32 dest, object source);
        IAssemblyBuilder IMUL(Register64 dest, object source);

        // IMUL با سه عملوند (اضافه کردن)
        IAssemblyBuilder IMUL(Register8 dest, Register8 source, int constant);
        IAssemblyBuilder IMUL(Register16 dest, Register16 source, int constant);
        IAssemblyBuilder IMUL(Register32 dest, Register32 source, int constant);
        IAssemblyBuilder IMUL(Register64 dest, Register64 source, int constant);

        // دستورات تقسیم
        IAssemblyBuilder DIV(Register8 source);
        IAssemblyBuilder DIV(Register16 source);
        IAssemblyBuilder DIV(Register32 source);
        IAssemblyBuilder DIV(Register64 source);

        IAssemblyBuilder IDIV(Register8 source);
        IAssemblyBuilder IDIV(Register16 source);
        IAssemblyBuilder IDIV(Register32 source);
        IAssemblyBuilder IDIV(Register64 source);

        // دستورات پیشرفته
        IAssemblyBuilder LEA(Register32 dest, string address);
        IAssemblyBuilder LEA(Register64 dest, string address);

        IAssemblyBuilder CALL(object target);

        IAssemblyBuilder XCHG(Register8 dest, Register8 source);
        IAssemblyBuilder XCHG(Register16 dest, Register16 source);
        IAssemblyBuilder XCHG(Register32 dest, Register32 source);
        IAssemblyBuilder XCHG(Register64 dest, Register64 source);

        // دستورات کنترل برنامه
        IAssemblyBuilder ENTER(uint frameSize, byte nestingLevel);
        IAssemblyBuilder LEAVE();

        // Utility methods
        IAssemblyBuilder Comment(string comment);
        IAssemblyBuilder NewLine();
        IAssemblyBuilder Section(string sectionName);

        // ========== Memory Operations ==========
        IAssemblyBuilder MOV(Register8 dest, MemoryOperand source);
        IAssemblyBuilder MOV(Register16 dest, MemoryOperand source);
        IAssemblyBuilder MOV(Register32 dest, MemoryOperand source);
        IAssemblyBuilder MOV(Register64 dest, MemoryOperand source);

        IAssemblyBuilder MOV(MemoryOperand dest, Register8 source);
        IAssemblyBuilder MOV(MemoryOperand dest, Register16 source);
        IAssemblyBuilder MOV(MemoryOperand dest, Register32 source);
        IAssemblyBuilder MOV(MemoryOperand dest, Register64 source);

        IAssemblyBuilder MOV(MemoryOperand dest, object immediate);

        // ========== Advanced Arithmetic with Memory ==========
        IAssemblyBuilder ADD(Register8 dest, MemoryOperand source);
        IAssemblyBuilder ADD(Register16 dest, MemoryOperand source);
        IAssemblyBuilder ADD(Register32 dest, MemoryOperand source);
        IAssemblyBuilder ADD(Register64 dest, MemoryOperand source);

        IAssemblyBuilder ADD(MemoryOperand dest, Register8 source);
        IAssemblyBuilder ADD(MemoryOperand dest, Register16 source);
        IAssemblyBuilder ADD(MemoryOperand dest, Register32 source);
        IAssemblyBuilder ADD(MemoryOperand dest, Register64 source);

        // ========== String Operations (Extended) ==========
        IAssemblyBuilder STOSB();
        IAssemblyBuilder STOSW();
        IAssemblyBuilder STOSD();
        IAssemblyBuilder STOSQ();

        IAssemblyBuilder LODSB();
        IAssemblyBuilder LODSW();
        IAssemblyBuilder LODSD();
        IAssemblyBuilder LODSQ();

        IAssemblyBuilder CMPSB();
        IAssemblyBuilder CMPSW();
        IAssemblyBuilder CMPSD();
        IAssemblyBuilder CMPSQ();

        IAssemblyBuilder SCASB();
        IAssemblyBuilder SCASW();
        IAssemblyBuilder SCASD();
        IAssemblyBuilder SCASQ();

        IAssemblyBuilder REP();
        IAssemblyBuilder REPE();
        IAssemblyBuilder REPNE();

        // ========== Bit Manipulation ==========
        IAssemblyBuilder BSF(Register32 dest, Register32 source);
        IAssemblyBuilder BSF(Register64 dest, Register64 source);
        IAssemblyBuilder BSF(Register32 dest, MemoryOperand source);
        IAssemblyBuilder BSF(Register64 dest, MemoryOperand source);

        IAssemblyBuilder BSR(Register32 dest, Register32 source);
        IAssemblyBuilder BSR(Register64 dest, Register64 source);
        IAssemblyBuilder BSR(Register32 dest, MemoryOperand source);
        IAssemblyBuilder BSR(Register64 dest, MemoryOperand source);

        IAssemblyBuilder BT(Register32 dest, object bit);
        IAssemblyBuilder BT(Register64 dest, object bit);
        IAssemblyBuilder BT(MemoryOperand dest, object bit);

        IAssemblyBuilder BTS(Register32 dest, object bit);
        IAssemblyBuilder BTS(Register64 dest, object bit);
        IAssemblyBuilder BTS(MemoryOperand dest, object bit);

        IAssemblyBuilder BTR(Register32 dest, object bit);
        IAssemblyBuilder BTR(Register64 dest, object bit);
        IAssemblyBuilder BTR(MemoryOperand dest, object bit);

        IAssemblyBuilder BTC(Register32 dest, object bit);
        IAssemblyBuilder BTC(Register64 dest, object bit);
        IAssemblyBuilder BTC(MemoryOperand dest, object bit);

        // ========== Conditional Moves (CMOV) ==========
        IAssemblyBuilder CMOVZ(Register32 dest, Register32 source);
        IAssemblyBuilder CMOVZ(Register64 dest, Register64 source);
        IAssemblyBuilder CMOVZ(Register32 dest, MemoryOperand source);
        IAssemblyBuilder CMOVZ(Register64 dest, MemoryOperand source);

        IAssemblyBuilder CMOVNZ(Register32 dest, Register32 source);
        IAssemblyBuilder CMOVNZ(Register64 dest, Register64 source);

        IAssemblyBuilder CMOVA(Register32 dest, Register32 source);
        IAssemblyBuilder CMOVA(Register64 dest, Register64 source);

        IAssemblyBuilder CMOVB(Register32 dest, Register32 source);
        IAssemblyBuilder CMOVB(Register64 dest, Register64 source);

        // ========== Set Byte on Condition (SETcc) ==========
        IAssemblyBuilder SETZ(Register8 dest);
        IAssemblyBuilder SETNZ(Register8 dest);
        IAssemblyBuilder SETA(Register8 dest);
        IAssemblyBuilder SETB(Register8 dest);
        IAssemblyBuilder SETC(Register8 dest);
        IAssemblyBuilder SETNC(Register8 dest);

        IAssemblyBuilder SETZ(MemoryOperand dest);
        IAssemblyBuilder SETNZ(MemoryOperand dest);

        // ========== Floating Point (حداقل دستورات) ==========
        IAssemblyBuilder FLD(MemoryOperand source);
        IAssemblyBuilder FSTP(MemoryOperand dest);
        IAssemblyBuilder FADD();
        IAssemblyBuilder FSUB();
        IAssemblyBuilder FMUL();
        IAssemblyBuilder FDIV();

        // ========== Processor Control ==========
        IAssemblyBuilder CPUID();
        IAssemblyBuilder RDTSC();
        IAssemblyBuilder PAUSE();
        IAssemblyBuilder LFENCE();
        IAssemblyBuilder MFENCE();
        IAssemblyBuilder SFENCE();
    }
}
