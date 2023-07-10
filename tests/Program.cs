
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SimpleGameEngine.Test;

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
    public static readonly bool EXIT_ON_ERROR = false;

    private static int _countFailures = 0;
    private static int _countSuccess = 0;

    public static void Main()
    {
        Console.WriteLine("Starting Tests...");

        var classes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => type.IsDefined(typeof(TestClass))).ToList();

        Console.WriteLine("Running Tests...");
        Run(classes);
        Console.WriteLine("End of Tests!");
        Console.WriteLine(String.Format("\t{0} Classes, {1} Tests: ", classes.Count, _countFailures + _countSuccess));
        var defaultColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Error.WriteLine("\t\t{0} Success", _countSuccess);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Error.WriteLine("\t\t{0} Failures", _countFailures);
        Console.ForegroundColor = defaultColor;
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
                _countSuccess++;
            }
            catch (Exception e)
            {
                var defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine(" Fail");
                Console.Error.WriteLine((e.InnerException ?? e).ToString());
                Console.ForegroundColor = defaultColor;
                _countFailures++;
            }
        }
    }

    // Debug.Assert is too slow, using this instead
    [StackTraceHidden]
    public static void Assert(bool condition, string message = "")
    {
        if (!condition) throw new AssertException(message);
    }

    [StackTraceHidden]
    public static void AssertTrue(bool actual, string message = "")
    {
        if (!actual)
            Assert(false, message + " Was expected a True value!");
    }

    [StackTraceHidden]
    public static void AssertFalse(bool actual, string message = "")
    {
        if (actual)
            Assert(false, message + " Was expected a False value!");
    }

    [StackTraceHidden]
    public static void AssertEquals<T>(T? actual, T? expected, string message = "")
    {
        bool failByNull = expected is null && actual is not null;
        bool failByDiff = expected is not null && !expected.Equals(actual);
        if (failByNull || failByDiff)
            Assert(false, message + " Expected: <" + expected + ">, Actual: <" + actual + ">.");
    }

    [StackTraceHidden]
    public static void AssertPrecisionEquals(float result, float expected, string message = "", float relativeEpsilon = 0.01f)
    {
        float absA = Math.Abs(result);
        float absB = Math.Abs(expected);
        float diff = Math.Abs(result - expected);

        bool isEquals;
        if (result == expected)
        {
            // shortcut, handles infinities
            isEquals = true;
        }
        else if (result == 0 || expected == 0 || absA + absB < float.MinValue)
        {
            // relative error is less meaningful here
            isEquals = diff < (relativeEpsilon * float.MinValue);
        }
        else
        {
            // use relative error
            isEquals = diff / (absA + absB) < relativeEpsilon;
        }
        Assert(isEquals, message + " Expected: <" + expected + ">, Actual: <" + result + ">.");
    }

    [StackTraceHidden]
    public static void AssertPrecisionEquals(double result, double expected, string message = "", double relativeEpsilon = 0.01f)
    {
        double absA = Math.Abs(result);
        double absB = Math.Abs(expected);
        double diff = Math.Abs(result - expected);

        bool isEquals;
        if (result == expected)
        {
            // shortcut, handles infinities
            isEquals = true;
        }
        else if (result == 0 || expected == 0 || absA + absB < double.MinValue)
        {
            // relative error is less meaningful here
            isEquals = diff < (relativeEpsilon * double.MinValue);
        }
        else
        {
            // use relative error
            isEquals = diff / (absA + absB) < relativeEpsilon;
        }
        Assert(isEquals, message + " Expected: <" + expected + ">, Actual: <" + result + ">.");
    }
}
