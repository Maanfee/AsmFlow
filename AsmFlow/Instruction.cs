using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace AsmFlow
{
    public partial class AssemblyBuilder
    {
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

        // ========== MOV with Memory Operands ==========
        public IAssemblyBuilder MOV(Register8 dest, MemoryOperand source)
        {
            source.Size = 1; // برای 8-bit
            _instructions.Add($"mov {dest.ToString().ToLower()}, {source}");
            return this;
        }

        public IAssemblyBuilder MOV(Register16 dest, MemoryOperand source)
        {
            source.Size = 2; // برای 16-bit
            _instructions.Add($"mov {dest.ToString().ToLower()}, {source}");
            return this;
        }

        public IAssemblyBuilder MOV(Register32 dest, MemoryOperand source)
        {
            source.Size = 4; // برای 32-bit
            _instructions.Add($"mov {dest.ToString().ToLower()}, {source}");
            return this;
        }

        public IAssemblyBuilder MOV(Register64 dest, MemoryOperand source)
        {
            source.Size = 8; // برای 64-bit
            _instructions.Add($"mov {dest.ToString().ToLower()}, {source}");
            return this;
        }

        public IAssemblyBuilder MOV(MemoryOperand dest, Register8 source)
        {
            dest.Size = 1;
            _instructions.Add($"mov {dest}, {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder MOV(MemoryOperand dest, Register16 source)
        {
            dest.Size = 2;
            _instructions.Add($"mov {dest}, {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder MOV(MemoryOperand dest, Register32 source)
        {
            dest.Size = 4;
            _instructions.Add($"mov {dest}, {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder MOV(MemoryOperand dest, Register64 source)
        {
            dest.Size = 8;
            _instructions.Add($"mov {dest}, {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder MOV(MemoryOperand dest, object immediate)
        {
            string immediateStr = GetSourceString(immediate, dest.Size * 8);
            _instructions.Add($"mov {dest}, {immediateStr}");
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

        // ========== ADD with Memory Operands ==========
        public IAssemblyBuilder ADD(Register8 dest, MemoryOperand source)
        {
            source.Size = 1;
            _instructions.Add($"add {dest.ToString().ToLower()}, {source}");
            return this;
        }

        public IAssemblyBuilder ADD(Register16 dest, MemoryOperand source)
        {
            source.Size = 2;
            _instructions.Add($"add {dest.ToString().ToLower()}, {source}");
            return this;
        }

        public IAssemblyBuilder ADD(Register32 dest, MemoryOperand source)
        {
            source.Size = 4;
            _instructions.Add($"add {dest.ToString().ToLower()}, {source}");
            return this;
        }

        public IAssemblyBuilder ADD(Register64 dest, MemoryOperand source)
        {
            source.Size = 8;
            _instructions.Add($"add {dest.ToString().ToLower()}, {source}");
            return this;
        }

        public IAssemblyBuilder ADD(MemoryOperand dest, Register8 source)
        {
            dest.Size = 1;
            _instructions.Add($"add {dest}, {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder ADD(MemoryOperand dest, Register16 source)
        {
            dest.Size = 2;
            _instructions.Add($"add {dest}, {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder ADD(MemoryOperand dest, Register32 source)
        {
            dest.Size = 4;
            _instructions.Add($"add {dest}, {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder ADD(MemoryOperand dest, Register64 source)
        {
            dest.Size = 8;
            _instructions.Add($"add {dest}, {source.ToString().ToLower()}");
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

        // ========== Multiplication and Division ==========
        public IAssemblyBuilder MUL(Register8 source)
        {
            ArchitectureValidator.ValidateInstruction(_architecture, "mul", source);
            _instructions.Add($"mul {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder MUL(Register16 source)
        {
            ArchitectureValidator.ValidateInstruction(_architecture, "mul", source);
            _instructions.Add($"mul {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder MUL(Register32 source)
        {
            ArchitectureValidator.ValidateInstruction(_architecture, "mul", source);
            _instructions.Add($"mul {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder MUL(Register64 source)
        {
            ArchitectureValidator.ValidateInstruction(_architecture, "mul", source);
            _instructions.Add($"mul {source.ToString().ToLower()}");
            return this;
        }

        // ========== Signed Multiplication ==========
        public IAssemblyBuilder IMUL(Register8 source)
        {
            ArchitectureValidator.ValidateInstruction(_architecture, "imul", source);
            _instructions.Add($"imul {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder IMUL(Register16 source)
        {
            ArchitectureValidator.ValidateInstruction(_architecture, "imul", source);
            _instructions.Add($"imul {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder IMUL(Register32 source)
        {
            ArchitectureValidator.ValidateInstruction(_architecture, "imul", source);
            _instructions.Add($"imul {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder IMUL(Register64 source)
        {
            ArchitectureValidator.ValidateInstruction(_architecture, "imul", source);
            _instructions.Add($"imul {source.ToString().ToLower()}");
            return this;
        }

        // IMUL با دو عملوند
        public IAssemblyBuilder IMUL(Register8 dest, object source)
        {
            string sourceStr = GetSourceString(source, 8);
            _instructions.Add($"imul {dest.ToString().ToLower()}, {sourceStr}");
            return this;
        }

        public IAssemblyBuilder IMUL(Register16 dest, object source)
        {
            string sourceStr = GetSourceString(source, 16);
            _instructions.Add($"imul {dest.ToString().ToLower()}, {sourceStr}");
            return this;
        }

        public IAssemblyBuilder IMUL(Register32 dest, object source)
        {
            string sourceStr = GetSourceString(source, 32);
            _instructions.Add($"imul {dest.ToString().ToLower()}, {sourceStr}");
            return this;
        }

        public IAssemblyBuilder IMUL(Register64 dest, object source)
        {
            string sourceStr = GetSourceString(source, 64);
            _instructions.Add($"imul {dest.ToString().ToLower()}, {sourceStr}");
            return this;
        }

        // IMUL با سه عملوند
        public IAssemblyBuilder IMUL(Register8 dest, Register8 source, int constant)
        {
            _instructions.Add($"imul {dest.ToString().ToLower()}, {source.ToString().ToLower()}, {constant}");
            return this;
        }

        public IAssemblyBuilder IMUL(Register16 dest, Register16 source, int constant)
        {
            _instructions.Add($"imul {dest.ToString().ToLower()}, {source.ToString().ToLower()}, {constant}");
            return this;
        }

        public IAssemblyBuilder IMUL(Register32 dest, Register32 source, int constant)
        {
            _instructions.Add($"imul {dest.ToString().ToLower()}, {source.ToString().ToLower()}, {constant}");
            return this;
        }

        public IAssemblyBuilder IMUL(Register64 dest, Register64 source, int constant)
        {
            _instructions.Add($"imul {dest.ToString().ToLower()}, {source.ToString().ToLower()}, {constant}");
            return this;
        }

        // ========== Division ==========
        public IAssemblyBuilder DIV(Register8 source)
        {
            ArchitectureValidator.ValidateInstruction(_architecture, "div", source);
            _instructions.Add($"div {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder DIV(Register16 source)
        {
            ArchitectureValidator.ValidateInstruction(_architecture, "div", source);
            _instructions.Add($"div {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder DIV(Register32 source)
        {
            ArchitectureValidator.ValidateInstruction(_architecture, "div", source);
            _instructions.Add($"div {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder DIV(Register64 source)
        {
            ArchitectureValidator.ValidateInstruction(_architecture, "div", source);
            _instructions.Add($"div {source.ToString().ToLower()}");
            return this;
        }

        // ========== Signed Division ==========
        public IAssemblyBuilder IDIV(Register8 source)
        {
            ArchitectureValidator.ValidateInstruction(_architecture, "idiv", source);
            _instructions.Add($"idiv {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder IDIV(Register16 source)
        {
            ArchitectureValidator.ValidateInstruction(_architecture, "idiv", source);
            _instructions.Add($"idiv {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder IDIV(Register32 source)
        {
            ArchitectureValidator.ValidateInstruction(_architecture, "idiv", source);
            _instructions.Add($"idiv {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder IDIV(Register64 source)
        {
            ArchitectureValidator.ValidateInstruction(_architecture, "idiv", source);
            _instructions.Add($"idiv {source.ToString().ToLower()}");
            return this;
        }

        // ========== Load Effective Address ==========
        public IAssemblyBuilder LEA(Register32 dest, string address)
        {
            if (_architecture == Architecture.x86_16)
                throw new InvalidOperationException("LEA with 32-bit register is not supported in 16-bit mode");

            _instructions.Add($"lea {dest.ToString().ToLower()}, [{address}]");
            return this;
        }

        public IAssemblyBuilder LEA(Register64 dest, string address)
        {
            if (_architecture != Architecture.x86_64)
                throw new InvalidOperationException("LEA with 64-bit register is only supported in 64-bit mode");

            _instructions.Add($"lea {dest.ToString().ToLower()}, [{address}]");
            return this;
        }

        // ========== Call Instruction ==========
        public IAssemblyBuilder CALL(object target)
        {
            _instructions.Add($"call {GetSourceString(target, 0)}");
            return this;
        }

        // ========== Exchange ==========
        public IAssemblyBuilder XCHG(Register8 dest, Register8 source)
        {
            _instructions.Add($"xchg {dest.ToString().ToLower()}, {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder XCHG(Register16 dest, Register16 source)
        {
            _instructions.Add($"xchg {dest.ToString().ToLower()}, {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder XCHG(Register32 dest, Register32 source)
        {
            _instructions.Add($"xchg {dest.ToString().ToLower()}, {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder XCHG(Register64 dest, Register64 source)
        {
            if (_architecture != Architecture.x86_64)
                throw new InvalidOperationException("XCHG with 64-bit registers is only supported in 64-bit mode");

            _instructions.Add($"xchg {dest.ToString().ToLower()}, {source.ToString().ToLower()}");
            return this;
        }

        // ========== Stack Frame ==========
        public IAssemblyBuilder ENTER(uint frameSize, byte nestingLevel)
        {
            _instructions.Add($"enter {frameSize}, {nestingLevel}");
            return this;
        }

        public IAssemblyBuilder LEAVE()
        {
            _instructions.Add("leave");
            return this;
        }

        // ========== System Call ==========
        public IAssemblyBuilder SYSCALL()
        {
            if (_architecture != Architecture.x86_64)
                throw new ArchitectureMismatchException(Architecture.x86_64, _architecture, "syscall");

            _instructions.Add("syscall");
            return this;
        }

        // ========== String Operations Extended ==========
        public IAssemblyBuilder STOSB()
        {
            _instructions.Add("stosb");
            return this;
        }

        public IAssemblyBuilder STOSW()
        {
            _instructions.Add("stosw");
            return this;
        }

        public IAssemblyBuilder STOSD()
        {
            _instructions.Add("stosd");
            return this;
        }

        public IAssemblyBuilder STOSQ()
        {
            if (_architecture != Architecture.x86_64)
                throw new ArchitectureMismatchException(Architecture.x86_64, _architecture, "stosq");
            _instructions.Add("stosq");
            return this;
        }

        public IAssemblyBuilder LODSB()
        {
            _instructions.Add("lodsb");
            return this;
        }

        public IAssemblyBuilder LODSW()
        {
            _instructions.Add("lodsw");
            return this;
        }

        public IAssemblyBuilder LODSD()
        {
            _instructions.Add("lodsd");
            return this;
        }

        public IAssemblyBuilder LODSQ()
        {
            if (_architecture != Architecture.x86_64)
                throw new ArchitectureMismatchException(Architecture.x86_64, _architecture, "lodsq");
            _instructions.Add("lodsq");
            return this;
        }

        public IAssemblyBuilder CMPSB()
        {
            _instructions.Add("cmpsb");
            return this;
        }

        public IAssemblyBuilder CMPSW()
        {
            _instructions.Add("cmpsw");
            return this;
        }

        public IAssemblyBuilder CMPSD()
        {
            _instructions.Add("cmpsd");
            return this;
        }

        public IAssemblyBuilder CMPSQ()
        {
            if (_architecture != Architecture.x86_64)
                throw new ArchitectureMismatchException(Architecture.x86_64, _architecture, "cmpsq");
            _instructions.Add("cmpsq");
            return this;
        }

        public IAssemblyBuilder SCASB()
        {
            _instructions.Add("scasb");
            return this;
        }

        public IAssemblyBuilder SCASW()
        {
            _instructions.Add("scasw");
            return this;
        }

        public IAssemblyBuilder SCASD()
        {
            _instructions.Add("scasd");
            return this;
        }

        public IAssemblyBuilder SCASQ()
        {
            if (_architecture != Architecture.x86_64)
                throw new ArchitectureMismatchException(Architecture.x86_64, _architecture, "scasq");
            _instructions.Add("scasq");
            return this;
        }

        public IAssemblyBuilder REP()
        {
            _instructions.Add("rep");
            return this;
        }

        public IAssemblyBuilder REPE()
        {
            _instructions.Add("repe");
            return this;
        }

        public IAssemblyBuilder REPNE()
        {
            _instructions.Add("repne");
            return this;
        }

        // ========== Bit Manipulation ==========
        public IAssemblyBuilder BSF(Register32 dest, Register32 source)
        {
            _instructions.Add($"bsf {dest.ToString().ToLower()}, {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder BSF(Register64 dest, Register64 source)
        {
            if (_architecture != Architecture.x86_64)
                throw new ArchitectureMismatchException(Architecture.x86_64, _architecture, "bsf");
            _instructions.Add($"bsf {dest.ToString().ToLower()}, {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder BSF(Register32 dest, MemoryOperand source)
        {
            source.Size = 4;
            _instructions.Add($"bsf {dest.ToString().ToLower()}, {source}");
            return this;
        }

        public IAssemblyBuilder BSF(Register64 dest, MemoryOperand source)
        {
            if (_architecture != Architecture.x86_64)
                throw new ArchitectureMismatchException(Architecture.x86_64, _architecture, "bsf");
            source.Size = 8;
            _instructions.Add($"bsf {dest.ToString().ToLower()}, {source}");
            return this;
        }

        // ========== Conditional Moves ==========
        public IAssemblyBuilder CMOVZ(Register32 dest, Register32 source)
        {
            _instructions.Add($"cmovz {dest.ToString().ToLower()}, {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder CMOVZ(Register64 dest, Register64 source)
        {
            if (_architecture != Architecture.x86_64)
                throw new ArchitectureMismatchException(Architecture.x86_64, _architecture, "cmovz");
            _instructions.Add($"cmovz {dest.ToString().ToLower()}, {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder CMOVZ(Register32 dest, MemoryOperand source)
        {
            source.Size = 4;
            _instructions.Add($"cmovz {dest.ToString().ToLower()}, {source}");
            return this;
        }

        public IAssemblyBuilder CMOVZ(Register64 dest, MemoryOperand source)
        {
            if (_architecture != Architecture.x86_64)
                throw new ArchitectureMismatchException(Architecture.x86_64, _architecture, "cmovz");
            source.Size = 8;
            _instructions.Add($"cmovz {dest.ToString().ToLower()}, {source}");
            return this;
        }

        // ========== Set Byte on Condition ==========
        public IAssemblyBuilder SETZ(Register8 dest)
        {
            _instructions.Add($"setz {dest.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder SETZ(MemoryOperand dest)
        {
            dest.Size = 1;
            _instructions.Add($"setz {dest}");
            return this;
        }

        // ========== Processor Control ==========
        public IAssemblyBuilder CPUID()
        {
            _instructions.Add("cpuid");
            return this;
        }

        public IAssemblyBuilder RDTSC()
        {
            _instructions.Add("rdtsc");
            return this;
        }

        public IAssemblyBuilder PAUSE()
        {
            _instructions.Add("pause");
            return this;
        }

        public IAssemblyBuilder LFENCE()
        {
            _instructions.Add("lfence");
            return this;
        }

        public IAssemblyBuilder MFENCE()
        {
            _instructions.Add("mfence");
            return this;
        }

        public IAssemblyBuilder SFENCE()
        {
            _instructions.Add("sfence");
            return this;
        }

        // ========== Floating Point (حداقل) ==========
        public IAssemblyBuilder FLD(MemoryOperand source)
        {
            source.Size = 4; // یا 8 برای double
            _instructions.Add($"fld {source}");
            return this;
        }

        public IAssemblyBuilder FSTP(MemoryOperand dest)
        {
            dest.Size = 4; // یا 8 برای double
            _instructions.Add($"fstp {dest}");
            return this;
        }

        public IAssemblyBuilder FADD()
        {
            _instructions.Add("fadd");
            return this;
        }

        public IAssemblyBuilder FSUB()
        {
            _instructions.Add("fsub");
            return this;
        }

        public IAssemblyBuilder FMUL()
        {
            _instructions.Add("fmul");
            return this;
        }

        public IAssemblyBuilder FDIV()
        {
            _instructions.Add("fdiv");
            return this;
        }

        // ========== Bit Scan Reverse ==========
        public IAssemblyBuilder BSR(Register32 dest, Register32 source)
        {
            ArchitectureValidator.ValidateInstruction(_architecture, "bsr", source);
            _instructions.Add($"bsr {dest.ToString().ToLower()}, {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder BSR(Register64 dest, Register64 source)
        {
            if (_architecture != Architecture.x86_64)
                throw new ArchitectureMismatchException(Architecture.x86_64, _architecture, "bsr");
            _instructions.Add($"bsr {dest.ToString().ToLower()}, {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder BSR(Register32 dest, MemoryOperand source)
        {
            source.Size = 4;
            _instructions.Add($"bsr {dest.ToString().ToLower()}, {source}");
            return this;
        }

        public IAssemblyBuilder BSR(Register64 dest, MemoryOperand source)
        {
            if (_architecture != Architecture.x86_64)
                throw new ArchitectureMismatchException(Architecture.x86_64, _architecture, "bsr");
            source.Size = 8;
            _instructions.Add($"bsr {dest.ToString().ToLower()}, {source}");
            return this;
        }

        // ========== Bit Test ==========
        public IAssemblyBuilder BT(Register32 dest, object bit)
        {
            _instructions.Add($"bt {dest.ToString().ToLower()}, {GetSourceString(bit, 32)}");
            return this;
        }

        public IAssemblyBuilder BT(Register64 dest, object bit)
        {
            if (_architecture != Architecture.x86_64)
                throw new ArchitectureMismatchException(Architecture.x86_64, _architecture, "bt");
            _instructions.Add($"bt {dest.ToString().ToLower()}, {GetSourceString(bit, 64)}");
            return this;
        }

        public IAssemblyBuilder BT(MemoryOperand dest, object bit)
        {
            _instructions.Add($"bt {dest}, {GetSourceString(bit, 0)}");
            return this;
        }

        // ========== Bit Test and Set ==========
        public IAssemblyBuilder BTS(Register32 dest, object bit)
        {
            _instructions.Add($"bts {dest.ToString().ToLower()}, {GetSourceString(bit, 32)}");
            return this;
        }

        public IAssemblyBuilder BTS(Register64 dest, object bit)
        {
            if (_architecture != Architecture.x86_64)
                throw new ArchitectureMismatchException(Architecture.x86_64, _architecture, "bts");
            _instructions.Add($"bts {dest.ToString().ToLower()}, {GetSourceString(bit, 64)}");
            return this;
        }

        public IAssemblyBuilder BTS(MemoryOperand dest, object bit)
        {
            _instructions.Add($"bts {dest}, {GetSourceString(bit, 0)}");
            return this;
        }

        // ========== Bit Test and Reset ==========
        public IAssemblyBuilder BTR(Register32 dest, object bit)
        {
            _instructions.Add($"btr {dest.ToString().ToLower()}, {GetSourceString(bit, 32)}");
            return this;
        }

        public IAssemblyBuilder BTR(Register64 dest, object bit)
        {
            if (_architecture != Architecture.x86_64)
                throw new ArchitectureMismatchException(Architecture.x86_64, _architecture, "btr");
            _instructions.Add($"btr {dest.ToString().ToLower()}, {GetSourceString(bit, 64)}");
            return this;
        }

        public IAssemblyBuilder BTR(MemoryOperand dest, object bit)
        {
            _instructions.Add($"btr {dest}, {GetSourceString(bit, 0)}");
            return this;
        }

        // ========== Bit Test and Complement ==========
        public IAssemblyBuilder BTC(Register32 dest, object bit)
        {
            _instructions.Add($"btc {dest.ToString().ToLower()}, {GetSourceString(bit, 32)}");
            return this;
        }

        public IAssemblyBuilder BTC(Register64 dest, object bit)
        {
            if (_architecture != Architecture.x86_64)
                throw new ArchitectureMismatchException(Architecture.x86_64, _architecture, "btc");
            _instructions.Add($"btc {dest.ToString().ToLower()}, {GetSourceString(bit, 64)}");
            return this;
        }

        public IAssemblyBuilder BTC(MemoryOperand dest, object bit)
        {
            _instructions.Add($"btc {dest}, {GetSourceString(bit, 0)}");
            return this;
        }

        // ========== Conditional Moves ==========
        public IAssemblyBuilder CMOVNZ(Register32 dest, Register32 source)
        {
            _instructions.Add($"cmovnz {dest.ToString().ToLower()}, {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder CMOVNZ(Register64 dest, Register64 source)
        {
            if (_architecture != Architecture.x86_64)
                throw new ArchitectureMismatchException(Architecture.x86_64, _architecture, "cmovnz");
            _instructions.Add($"cmovnz {dest.ToString().ToLower()}, {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder CMOVA(Register32 dest, Register32 source)
        {
            _instructions.Add($"cmova {dest.ToString().ToLower()}, {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder CMOVA(Register64 dest, Register64 source)
        {
            if (_architecture != Architecture.x86_64)
                throw new ArchitectureMismatchException(Architecture.x86_64, _architecture, "cmova");
            _instructions.Add($"cmova {dest.ToString().ToLower()}, {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder CMOVB(Register32 dest, Register32 source)
        {
            _instructions.Add($"cmovb {dest.ToString().ToLower()}, {source.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder CMOVB(Register64 dest, Register64 source)
        {
            if (_architecture != Architecture.x86_64)
                throw new ArchitectureMismatchException(Architecture.x86_64, _architecture, "cmovb");
            _instructions.Add($"cmovb {dest.ToString().ToLower()}, {source.ToString().ToLower()}");
            return this;
        }

        // ========== Set Byte on Condition ==========
        public IAssemblyBuilder SETNZ(Register8 dest)
        {
            _instructions.Add($"setnz {dest.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder SETA(Register8 dest)
        {
            _instructions.Add($"seta {dest.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder SETB(Register8 dest)
        {
            _instructions.Add($"setb {dest.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder SETC(Register8 dest)
        {
            _instructions.Add($"setc {dest.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder SETNC(Register8 dest)
        {
            _instructions.Add($"setnc {dest.ToString().ToLower()}");
            return this;
        }

        public IAssemblyBuilder SETNZ(MemoryOperand dest)
        {
            dest.Size = 1;
            _instructions.Add($"setnz {dest}");
            return this;
        }
    }
}
