using AsmFlow;
using static AsmFlow.Register8;
using static AsmFlow.Register16;
using static AsmFlow.Register32;
using static AsmFlow.Register64;

namespace Examples
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnExamples_Click(object sender, EventArgs e)
        {
            txtResult.Clear();

            txtResult.Text += ("=== Example 1: Basic MOV Operations ===\n");
            Example1();

            txtResult.Text += ("\n=== Example 2: Arithmetic Operations ===\n");
            Example2();

            txtResult.Text += ("\n=== Example 3: Loop with Labels ===\n");
            Example3();

            txtResult.Text += ("\n=== Example 4: Different Architectures ===\n");
            Example4();

            txtResult.Text += ("\n=== Example 5: Instructions ===\n");
            Example5();

            txtResult.Text += ("\n=== Example 6: New Instructions ===\n");
            Example6();
        }

        private void Example1()
        {
            try
            {
                var asm = AssemblyFactory.CreateBuilder(Architecture.x86_64);

                asm.MOV(AL, (byte)0)
                   .MOV(AX, 0x10)
                   .MOV(EBX, 0x12345678)
                   .MOV(RCX, 0x1122334455667788)
                   .MOV(AL, CL)
                   .MOV(EAX, EBX)
                   .MOV(RAX, 60)
                   .XOR(RDI, RDI)
                   .SYSCALL();

                txtResult.Text += asm.Generate(Assembler.NASM);
                asm.SaveToFile("example1.asm");
                txtResult.Text += "\n✓ Saved to example1.asm\n";
            }
            catch (Exception ex)
            {
                txtResult.Text += $"Error in Example 1: {ex.Message}\n";
            }
        }

        private void Example2()
        {
            try
            {
                var asm = AssemblyFactory.CreateBuilder(Architecture.x86_32);

                asm.MOV(EAX, 10)
                   .MOV(EBX, 20)
                   .ADD(EAX, EBX)
                   .SUB(EAX, 5)
                   .AND(EAX, 0xFF)
                   .XOR(EBX, EBX)
                   .MOV(EAX, 100)
                   .MOV(EBX, 25)
                   .MUL(EBX)
                   .MOV(ECX, 4)
                   .DIV(ECX)
                   .MOV(EAX, 1)
                   .XOR(EBX, EBX)
                   .INT(0x80);

                txtResult.Text += asm.Generate(Assembler.NASM);
                asm.SaveToFile("example2.asm");
                txtResult.Text += "\n✓ Saved to example2.asm\n";
            }
            catch (Exception ex)
            {
                txtResult.Text += $"Error in Example 2: {ex.Message}\n";
            }
        }

        private void Example3()
        {
            try
            {
                var asm = AssemblyFactory.CreateBuilder(Architecture.x86_16);

                asm.MOV(CX, 5)
                   .MOV(AX, 0)
                   .Label("loop_start")
                   .ADD(AX, 1)
                   .CMP(CX, 0)
                   .JZ("loop_end")
                   .DEC(CX)
                   .JMP("loop_start")
                   .Label("loop_end")
                   .MOV(AX, 0x4C00)
                   .INT(0x21);

                txtResult.Text += asm.Generate(Assembler.NASM);
                asm.SaveToFile("example3.asm");
                txtResult.Text += "\n✓ Saved to example3.asm\n";
            }
            catch (Exception ex)
            {
                txtResult.Text += $"Error in Example 3: {ex.Message}\n";
            }
        }

        private void Example4()
        {
            try
            {
                // 64-bit
                txtResult.Text += "64-bit Example:\n";
                var asm64 = AssemblyFactory.CreateBuilder(Architecture.x86_64);
                asm64.MOV(RAX, 0x123456789ABCDEF0)
                     .ADD(RBX, 0x1000)
                     .LEA(RCX, "rbx+8")
                     .XCHG(RAX, RBX)
                     .MOV(RAX, 10)
                     .IMUL(RAX, 5)
                     .IMUL(RDX, RAX, 3)
                     .MOV(RAX, 60)
                     .XOR(RDI, RDI)
                     .SYSCALL();

                txtResult.Text += asm64.Generate(Assembler.NASM);
                asm64.SaveToFile("example4_64.asm");

                // 32-bit
                txtResult.Text += "\n32-bit Example:\n";
                var asm32 = AssemblyFactory.CreateBuilder(Architecture.x86_32);
                asm32.MOV(EAX, 0x12345678)
                     .PUSH(EAX)
                     .POP(EBX)
                     .ENTER(16, 0)
                     .LEA(ECX, "ebp-4")
                     .LEAVE()
                     .MOV(EAX, 1)
                     .XOR(EBX, EBX)
                     .INT(0x80);

                txtResult.Text += asm32.Generate(Assembler.NASM);
                asm32.SaveToFile("example4_32.asm");

                txtResult.Text += "\n✓ Saved to example4_64.asm and example4_32.asm\n";
            }
            catch (Exception ex)
            {
                txtResult.Text += $"Error in Example 4: {ex.Message}\n";
            }
        }

        private void Example5()
        {
            try
            {
                txtResult.Text += "New Instructions (Phase 1):\n";
                var asm = AssemblyFactory.CreateBuilder(Architecture.x86_64);

                asm.MOV(RAX, 10)
                   .MOV(RBX, 20)
                   .IMUL(RCX, RAX, 5)
                   .MOV(RAX, 100)
                   .MOV(RBX, 7)
                   .XOR(RDX, RDX)
                   .DIV(RBX)
                   .Label("main")
                   .MOV(RDI, 42)
                   .CALL("my_function")
                   .Label("my_function")
                   .ENTER(32, 0)
                   .MOV(RAX, RDI)
                   .ADD(RAX, 10)
                   .LEAVE()
                   .RET()
                   .MOV(RAX, 0xAAAAAAAAAAAAAAAA)
                   .MOV(RBX, 0xBBBBBBBBBBBBBBBB)
                   .XCHG(RAX, RBX)
                   .MOV(RAX, 60)
                   .XOR(RDI, RDI)
                   .SYSCALL();

                txtResult.Text += asm.Generate(Assembler.NASM);
                asm.SaveToFile("example5_new_instructions.asm");
                txtResult.Text += "\n✓ Saved to example5_new_instructions.asm\n";
            }
            catch (Exception ex)
            {
                txtResult.Text += $"Error in Example 5: {ex.Message}\n";
            }
        }

        private void Example6()
        {
            try
            {
                txtResult.Text += "=== Example 6: Memory Operands (Phase 2) ===\n";
                var asm = AssemblyFactory.CreateBuilder(Architecture.x86_64);

                asm.Comment("Example 6: Memory Operations")
                   .Comment("1. Direct memory access")
                   .MOV(RAX, MemoryOperand.DirectAddress("my_var", 8))
                   .NewLine()
                   .Comment("2. Register indirect")
                   .MOV(RBX, 0x1000)
                   .MOV(RAX, MemoryOperand.RegisterIndirect(RBX, 8))
                   .NewLine()
                   .Comment("3. Based-indexed with scale")
                   .MOV(RCX, 0x2000)
                   .MOV(RDX, 4) // index
                   .MOV(RAX, MemoryOperand.BasedIndexed(RCX, RDX, 8, 8)) // rcx + rdx*8
                   .NewLine()
                   .Comment("4. Store to memory")
                   .MOV(RAX, 0x123456789ABCDEF0)
                   .MOV(MemoryOperand.RegisterIndirect(RBX, 8), RAX)
                   .NewLine()
                   .Comment("5. String operations")
                   .MOV(RDI, RBX) // destination
                   .MOV(RAX, 0)   // value to store
                   .MOV(RCX, 10)  // count
                   .REP()
                   .STOSQ()
                   .NewLine()
                   .Comment("6. Bit manipulation")
                   .MOV(RAX, 0b10101010)
                   .BT(RAX, 3)    // test bit 3
                   .BTS(RAX, 5)   // set bit 5
                   .BTR(RAX, 1)   // reset bit 1
                   .NewLine()
                   .Comment("7. Conditional move")
                   .MOV(RAX, 10)
                   .MOV(RBX, 20)
                   .CMP(RAX, RBX)
                   .CMOVA(RCX, RAX) // move if above
                   .NewLine()
                   .Comment("8. Set condition")
                   .SETZ(AL)        // set AL to 1 if zero flag
                   .NewLine()
                   .Comment("Exit program")
                   .MOV(RAX, 60)
                   .XOR(RDI, RDI)
                   .SYSCALL();

                txtResult.Text += asm.Generate(Assembler.NASM);
                asm.SaveToFile("example6_memory.asm");
                txtResult.Text += "\n✓ Saved to example6_memory.asm\n";
            }
            catch (Exception ex)
            {
                txtResult.Text += $"Error in Example 6: {ex.Message}\n";
            }
        }

        // اضافه کردن دکمه برای تست خطاها
        private void btnErrorTest_Click(object sender, EventArgs e)
        {
            txtResult.Clear();
            txtResult.Text += "=== Error Testing ===\n";

            try
            {
                // تست خطای معماری
                var asm16 = AssemblyFactory.CreateBuilder(Architecture.x86_16);
                asm16.SYSCALL();  // این باید خطا بدهد
            }
            catch (Exception ex)
            {
                txtResult.Text += $"✓ Caught error: {ex.Message}\n";
            }

            try
            {
                // تست خطای رجیستر
                var asm64 = AssemblyFactory.CreateBuilder(Architecture.x86_64);
                asm64.MOV(RAX, 0x1122334455667788)
                     .ADD(EAX, RAX);  // نوع رجیسترها همخوانی ندارد
            }
            catch (Exception ex)
            {
                txtResult.Text += $"✓ Caught error: {ex.Message}\n";
            }
        }
    }
}