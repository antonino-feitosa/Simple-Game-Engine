
namespace SimpleGameEngine.Test;

using global::System.Reflection;

[AttributeUsage(AttributeTargets.Class)]
public class TestClass : Attribute {}

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
            catch (TargetInvocationException e)
            {
                string message = e.InnerException?.StackTrace ?? "unknown";
                int index = message.IndexOf(" at ");
                index = message.IndexOf(" at ", index + 1);
                message = message[..index];
                message = message.Replace(" in ", "\n\tin ");
                Console.WriteLine("Fail " + message);
                if (EXIT_ON_ERROR)
                    Environment.Exit(0);
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
