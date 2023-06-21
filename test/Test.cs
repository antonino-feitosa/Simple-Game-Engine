
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SimpleGameEngine;

[AttributeUsage(AttributeTargets.Class)]
public class TestClass : Attribute { }

public class AssertException : Exception
{
    public AssertException() { }
    public AssertException(string message) : base(message) { }
    public AssertException(string message, Exception inner) : base(message, inner) { }
}

public class TestRunner
{
    [DllImport("kernel32.dll")] // to attach console on forms application
    static extern bool AttachConsole(int dwProcessId);
    private const int ATTACH_PARENT_PROCESS = -1;


    public static readonly bool EXIT_ON_ERROR = false;
    public static readonly bool RUN_TESTS = true;

    public static void Main()
    {
        AttachConsole(ATTACH_PARENT_PROCESS);

        if (!RUN_TESTS) return;

        Console.WriteLine("Starting Tests...");

        var classes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => type.IsDefined(typeof(TestClass))).ToList();

        Console.WriteLine("Running Tests...");
        Run(classes);
        Console.WriteLine("End of Tests!");

        Environment.Exit(0);
    }

    public static void Run(List<Type> classes)
    {
        foreach (var type in classes)
        {
            Console.WriteLine("\n\tRunning tests on " + type.Name);
            RunClass(type);
        }
    }

    protected static void RunClass(Type type)
    {
        var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
        var methodsWhitoutArgumentsOrReturn = methods.Where(m => m.GetParameters().Length == 0 && m.ReturnType == typeof(void));
        foreach (var method in methodsWhitoutArgumentsOrReturn)
        {
            try
            {
                Console.Write("\t\tRunning: " + method.Name);
                var testInstance = Activator.CreateInstance(type); // constructor is called for each test
                method.Invoke(testInstance, null);
                Console.WriteLine(" OK!");
            }
            catch (Exception e)
            {
                var defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine(" Fail");
                Console.Error.WriteLine((e.InnerException ?? e).ToString());
                Console.ForegroundColor = defaultColor;
            }
        }
    }

    // Debug.Assert is too slow, using this instead
    [StackTraceHidden]
    public static void Assert(bool condition, string message = "")
    {
        if (!condition) throw new AssertException(message);
    }
}
