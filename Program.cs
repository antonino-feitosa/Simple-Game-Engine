namespace SGE;

using global::System.Runtime.InteropServices;

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

        var game = new Game();
        var device = new DeviceWindows(game);
        var loading = new LoadingSystem(device, game);
        
        //PlatformTest.Test(platform);
        //PositionSystemTest.Test(game);
        //MotionSystemTest.Test(game);
        //CameraSystemTest.Test(game);
        //AnimationSystemTest.Test(game);
        //ResizeTest.Test(game);
        
        game.AttachSystem(loading);
        Application.Run(device);
        game.Start();
    }
}
