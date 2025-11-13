using AsmFlow;

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

            txtResult.Text += ("=== Example 1: Basic MOV Operations ===");
            Example1();

            txtResult.Text += ("\n=== Example 2: Arithmetic Operations ===");
            Example2();

            txtResult.Text += ("\n=== Example 3: Loop with Labels ===");
            Example3();

            txtResult.Text += ("\n=== Example 4: Different Architectures ===");
            Example4();
        }

        private void Example1()
        {
            var asm = AssemblyFactory.CreateBuilder(Architecture.x86_16);

            asm.MOV(Register8.AL, 0)
               .HLT()
               .MOV(Register16.AX, 0x10)
               .MOV(Register32.EBX, 0x12345678)
               .MOV(Register64.RCX, 0x1122334455667788)
               .MOV(Register8.AL, Register8.CL)
               .MOV(Register32.EAX, Register32.EBX);

            txtResult.Text += (asm.Generate(Assembler.NASM));
            asm.SaveToFile("example1.asm");
        }

        private void Example2()
        {
            var asm = AssemblyFactory.CreateBuilder(Architecture.x86_32);

            asm.MOV(Register32.EAX, 10)
               .MOV(Register32.EBX, 20)
               .ADD(Register32.EAX, Register32.EBX)
               .SUB(Register32.EAX, 5)
               .AND(Register32.EAX, 0xFF)
               .XOR(Register32.EBX, Register32.EBX);

            txtResult.Text += (asm.Generate(Assembler.NASM));
            asm.SaveToFile("example2.asm");
        }

        private void Example3()
        {
            var asm = AssemblyFactory.CreateBuilder(Architecture.x86_16);

            asm.MOV(Register16.CX, 5)
               .Label("loop_start")
               .ADD(Register16.AX, 1)
               .CMP(Register16.CX, 0)
               .JZ("loop_end")
               .SUB(Register16.CX, 1)
               .JMP("loop_start")
               .Label("loop_end")
               .RET();

            txtResult.Text += (asm.Generate(Assembler.NASM));
            asm.SaveToFile("example3.asm");
        }

        private void Example4()
        {
            // 64-bit
            var asm64 = AssemblyFactory.CreateBuilder(Architecture.x86_64);
            asm64.MOV(Register64.RAX, 0x123456789ABCDEF0)
                 .ADD(Register64.RBX, 0x1000);

            txtResult.Text += ("64-bit:");
            txtResult.Text += (asm64.Generate(Assembler.NASM));

            // 32-bit  
            var asm32 = AssemblyFactory.CreateBuilder(Architecture.x86_32);
            asm32.MOV(Register32.EAX, 0x12345678)
                 .PUSH(Register32.EAX)
                 .POP(Register32.EBX);

            txtResult.Text += ("32-bit:");
            txtResult.Text += (asm32.Generate(Assembler.NASM));
        }

        // **************************************
    }
}
