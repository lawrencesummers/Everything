namespace RDIFramework.Utilities
{
    using Microsoft.CSharp;
    using System;
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.Net;
    using System.Text;
    using System.Web.Services.Description;

    public class InvokeServiceHelper
    {
        public static object Request(string url, string @namespace, string classname, string methodname, object[] args)
        {
            object obj3;
            try
            {
                WebClient client = new WebClient();
                ServiceDescription serviceDescription = ServiceDescription.Read(client.OpenRead(url + "?WSDL"));
                ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
                importer.AddServiceDescription(serviceDescription, "", "");
                CodeNamespace namespace2 = new CodeNamespace(@namespace);
                CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
                codeCompileUnit.Namespaces.Add(namespace2);
                importer.Import(namespace2, codeCompileUnit);
                ICodeCompiler compiler = new CSharpCodeProvider().CreateCompiler();
                CompilerParameters options = new CompilerParameters {
                    GenerateExecutable = false,
                    GenerateInMemory = true
                };
                options.ReferencedAssemblies.Add("System.dll");
                options.ReferencedAssemblies.Add("System.XML.dll");
                options.ReferencedAssemblies.Add("System.Web.Services.dll");
                options.ReferencedAssemblies.Add("System.Data.dll");
                CompilerResults results = compiler.CompileAssemblyFromDom(options, codeCompileUnit);
                if (results.Errors.HasErrors)
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (CompilerError error in results.Errors)
                    {
                        builder.Append(error.ToString());
                        builder.Append(Environment.NewLine);
                    }
                    throw new Exception(builder.ToString());
                }
                Type type = results.CompiledAssembly.GetType(@namespace + "." + classname, true, true);
                object obj2 = Activator.CreateInstance(type);
                obj3 = type.GetMethod(methodname).Invoke(obj2, args);
            }
            catch (Exception)
            {
                throw new Exception(methodname);
            }
            return obj3;
        }
    }
}

