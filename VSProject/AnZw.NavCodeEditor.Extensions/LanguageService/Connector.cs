using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.Text.Editor;
using AnZw.NavCodeEditor.Extensions.Reflection;

namespace AnZw.NavCodeEditor.Extensions.LanguageService
{
    public class Connector : ReflectionWrapper
    {
       
        public Context Context
        {
            get
            {
                if (context == null)
                    context = new Context(GetProperty("Context"));

                return context;
            }
        }
        public TypeInfoManager TypeInfoManager
        {
            get
            {
                if (typeInfoManager == null)
                    typeInfoManager = new TypeInfoManager(GetProperty("TypeInfoManager"));

                return typeInfoManager;
            }
        }
        public IMethodManager MethodManager
        {
            get
            {
                if (methodManager == null)
                    methodManager = new MethodManager(GetProperty("MethodManager"));

                return methodManager;
            }
        }

        private IMethodManager methodManager;
        private TypeInfoManager typeInfoManager ;
        private Context context;
       
        public Connector(ITextView textView)
        {
                var connectorTypeName = "Microsoft.Dynamics.Nav.Prod.CodeEditor.Intellisense.Connector";
                var connectorType = Type.GetType(connectorTypeName);
                if (connectorType == null)
                {
                    //load dynamics nav editor assemblies
                    Assembly sourceNavAssembly = Assembly.LoadFrom(Path.Combine(DirectoryHelper.CurrentAssemblyPath(), "Microsoft.Dynamics.Nav.CodeEditor.Intellisense.dll"));
                    connectorType = sourceNavAssembly.GetType(connectorTypeName);
                }

                //find connector
                MethodInfo getConnector = connectorType.GetMethod("GetConnector", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                object[] getConnectorParameters = { textView };
                var connector = getConnector.Invoke(null, getConnectorParameters);

                Initialize(connector, connectorType);

        }
        
    }
}
