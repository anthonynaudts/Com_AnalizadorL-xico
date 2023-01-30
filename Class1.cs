var source = @"using System;
using System.Text;
using System.Reflection;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;

namespace __ScriptExecution {
public class __Executor { 

    public async Task<string> GetJsonFromAlbumViewer(int id)
    {
        Console.WriteLine(""Starting..."");

        var wc = new WebClient();
        var uri = new Uri(""https://albumviewer.west-wind.com/api/album/"" + id);

        string json = ""123"";
        try{
            Console.WriteLine(""Retrieving..."");
            json =  await wc.DownloadStringTaskAsync(uri);

            Console.WriteLine(""JSON retrieved..."");
        }
        catch(Exception ex) {
            Console.WriteLine(""ERROR in method: "" + ex.Message);
        }

        Console.WriteLine(""All done in method"");

        dynamic name = ""Rick"";
        Console.WriteLine(name);

        var s = Westwind.Utilities.StringUtils.ExtractString(""132123123"",""13"",""23"");
        return json;
    }

} }";

#if NETFRAMEWORK
	AddNetFrameworkDefaultReferences();
#else
AddNetCoreDefaultReferences();

// Core specific - not in base framework (for demonstration only)
AddAssembly("System.Net.WebClient.dll");
#endif

AddAssembly(typeof(Westwind.Utilities.StringUtils));

// Set up compilation Configuration
var tree = SyntaxFactory.ParseSyntaxTree(source.Trim());
var compilation = CSharpCompilation.Create("Executor.cs")
    .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary,
        optimizationLevel: OptimizationLevel.Release))
    .WithReferences(References)
    .AddSyntaxTrees(tree);

string errorMessage = null;
Assembly assembly = null;

bool isFileAssembly = false;
Stream codeStream = null;
using (codeStream = new MemoryStream())
{
    // Actually compile the code
    EmitResult compilationResult = null;
    compilationResult = compilation.Emit(codeStream);

    // Compilation Error handling
    if (!compilationResult.Success)
    {
        var sb = new StringBuilder();
        foreach (var diag in compilationResult.Diagnostics)
        {
            sb.AppendLine(diag.ToString());
        }
        errorMessage = sb.ToString();

        Assert.IsTrue(false, errorMessage);

        return;
    }

    // Load
    assembly = Assembly.Load(((MemoryStream)codeStream).ToArray());
}

// Instantiate
dynamic instance = assembly.CreateInstance("__ScriptExecution.__Executor");

// Call
var json = await instance.GetJsonFromAlbumViewer(37);

Console.WriteLine(json)