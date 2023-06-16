
namespace test;

using global::System.Reflection;

public class AssertionException : Exception
{
    public AssertionException() { }
    public AssertionException(string message) : base(message) { }
    public AssertionException(string message, Exception inner) : base(message, inner) { }
}

public class Test
{
    public static readonly bool EXIT_ON_ERROR = false;
    public static readonly bool RUN_TESTS = true;
    public static void Execute()
    {
        if(!RUN_TESTS) return;
        
        Console.WriteLine("Starting Tests...");
        var classes = new List<Type>();

        classes.Add(typeof(DimensionTest));

        Console.WriteLine("Running Tests...");
        Run(classes);
        Console.WriteLine("End of Tests!");

        global::System.Environment.Exit(0);
    }

    public static void Run(List<Type> classes)
    {
        foreach (var type in classes)
        {
            Console.WriteLine("\tRunning tests on " + type.Name +"\n");
            RunClass(type);
        }
    }

    protected static void RunClass(Type type)
    {
        var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Static);
        foreach (var method in methods)
        {
            try
            {
                method.Invoke(null, null);
            }
            catch (global::System.Reflection.TargetInvocationException e)
            {
                string message = e.InnerException?.StackTrace ?? "unknown";
                int index = message.IndexOf(" at ");
                index = message.IndexOf(" at ", index + 1);
                message = message[..index];
                message = message.Replace(" in ", "\n\tin ");
                Console.WriteLine("Fail " + message);
                if (EXIT_ON_ERROR)
                    global::System.Environment.Exit(0);
            }
        }
    }

    [global::System.Diagnostics.StackTraceHidden]
    public static void Assert(bool assertion, string? message = null)
    {
        if (!assertion)
        {
            if (message != null)
                throw new AssertionException(message);
            else throw new AssertionException();
        }
    }
}
