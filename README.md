# AsmFlow

A powerful and intuitive C# library for generating NASM assembly code programmatically. Write assembly instructions using fluent C# syntax and export to NASM-compatible files.

## 🚀 Features

- **Fluent API** - Chain assembly instructions in a natural, readable way
- **Multi-Architecture Support** - x86-16, x86-32, and x86-64 architectures
- **Type-Safe** - Compile-time checking of register types and sizes
- **Auto-Extension** - Automatic MOVZX/MOVSX for register size conversions
- **NASM Compatible** - Generate ready-to-assemble NASM code
- **Cross-Platform** - Works on Windows, Linux, and macOS

## 🏗️ Architecture Support
- **x86-16 (Real Mode)**
```csharp
var asm16 = AssemblyFactory.CreateBuilder(Architecture.x86_16);
```
- **x86-32 (Protected Mode)**
```csharp
var asm32 = AssemblyFactory.CreateBuilder(Architecture.x86_32);
```
- **x86-64 (Long Mode)**
```csharp
var asm64 = AssemblyFactory.CreateBuilder(Architecture.x86_64);
```
## 🎯 Quick Start
```csharp
using AsmFlow;

// Create a builder for x86-64 architecture
var asm = AssemblyFactory.CreateBuilder(Architecture.x86_64);

// Write assembly instructions using fluent syntax
asm.MOV(Register8.AL, 0)
   .MOV(Register16.AX, 0x10)
   .MOV(Register32.EBX, 0x12345678)
   .MOV(Register64.RCX, 0x1122334455667788)
   .HLT();

// Generate NASM code
string nasmCode = asm.Generate(Assembler.NASM);

// Save to file
asm.SaveToFile("output.asm");
```
## 📋 Generated Output
```csharp
bits 64
section .text
global _start
_start:
    mov al, 0
    mov ax, 0x0010
    mov ebx, 0x12345678
    mov rcx, 0x1122334455667788
    hlt
```
## 🔧 Advanced Usage
### Arithmetic Operations
```csharp
var asm = AssemblyFactory.CreateBuilder(Architecture.x86_32);
asm.MOV(Register32.EAX, 10)
   .MOV(Register32.EBX, 20)
   .ADD(Register32.EAX, Register32.EBX)
   .SUB(Register32.EAX, 5)
   .INC(Register32.ECX);
```
### Loops and Conditions
```csharp
var asm = AssemblyFactory.CreateBuilder(Architecture.x86_16);
asm.MOV(Register16.CX, 5)
   .Label("loop_start")
   .ADD(Register16.AX, 1)
   .CMP(Register16.CX, 0)
   .JZ("loop_end")
   .DEC(Register16.CX)
   .JMP("loop_start")
   .Label("loop_end")
   .RET();
```
### Stack Operations
```csharp
var asm = AssemblyFactory.CreateBuilder(Architecture.x86_64);
asm.MOV(Register64.RAX, 0x123456789ABCDEF0)
   .PUSH(Register64.RAX)
   .POP(Register64.RBX);
```
**AsmFlow** - Fluent Assembly Code Generation for .NET Developers 🚀

