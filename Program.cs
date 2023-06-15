namespace SGE;

using global::System.Runtime.InteropServices;

public class DoubleBufferedForm : Form {
    public DoubleBufferedForm() {
        DoubleBuffered = true;
    }
}

static class Program
{
    [DllImport("kernel32.dll")]
    static extern bool AttachConsole(int dwProcessId);
    private const int ATTACH_PARENT_PROCESS = -1;


    [STAThread]
    static void Main()
    {
        // redirect console output to parent process;
        // must be before any calls to Console.WriteLine()
        AttachConsole(ATTACH_PARENT_PROCESS);

        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        ApplicationConfiguration.Initialize();

        var form = new DoubleBufferedForm();
        var game = new Game();
        var device = new DeviceWindows(form, game);
        
        Application.Run(form);
        device.Start();
    }
}
