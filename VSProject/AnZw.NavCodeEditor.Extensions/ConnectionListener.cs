using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AnZw.NavCodeEditor.Extensions
{
    [TextViewRole("DOCUMENT"),
        ContentType("C/AL"),
        Order(Before = "Default"),
        Export(typeof(IWpfTextViewConnectionListener))]
    public class ConnectionListener : IWpfTextViewConnectionListener
    {
        [Import]
        public ITextDocumentFactoryService factoryService;

        public void SubjectBuffersConnected(IWpfTextView textView, ConnectionReason reason, Collection<ITextBuffer> subjectBuffers)
        {

        }

        public void SubjectBuffersDisconnected(IWpfTextView textView, ConnectionReason reason, Collection<ITextBuffer> subjectBuffers)
        {
            if (reason != ConnectionReason.TextViewLifetime)
                return;
            
        }
    }
}
