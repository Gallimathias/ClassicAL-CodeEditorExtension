﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;

namespace AnZw.NavCodeEditor.Extensions.LanguageService
{
    public interface IMethod
    {

        int Id { get; }
        string Name { get; }
        string Signature { get; }

        IEnumerable<string> GetLines();
        Tuple<SnapshotPoint, SnapshotPoint> GetCodeInterval();

    }
}
