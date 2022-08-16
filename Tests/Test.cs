namespace ExNihilo.Tests;

[TestClass]
public abstract class Test
{
    protected string CurrentPath { get; private set; }

    public Test()
    {
        CurrentPath = Path.Combine(Directory.GetCurrentDirectory(), "Output", GetType().Name.Replace("Test", string.Empty));
        Directory.CreateDirectory(CurrentPath);
    }

    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext context)
    {
        var outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        if (Directory.Exists(outputDirectory))
            Directory.Delete(outputDirectory, true);
    }
}
