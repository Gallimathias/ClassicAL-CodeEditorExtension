using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;
using AnZw.NavCodeEditor.Extensions.Snippets;

namespace AnZw.NavCodeEditor.Extensions
{
    public class CALCompletionSource : ICompletionSource, IDisposable
    {

        private CALCompletionSourceProvider sourceProvider;
        private ITextBuffer textBuffer;
        private bool mIsDisposed;

        public CALCompletionSource(CALCompletionSourceProvider sourceProvider, ITextBuffer textBuffer)
        {
            this.sourceProvider = sourceProvider;
            this.textBuffer = textBuffer;
        }

        public void Dispose()
        {
            if (mIsDisposed)
                return;

            textBuffer = null;
            sourceProvider = null;

            GC.SuppressFinalize(this);
            mIsDisposed = true;

        }

        private ITrackingSpan FindTokenSpanAtPosition(ITrackingPoint point, ICompletionSession session)
        {
            var currentPoint = session.TextView.Caret.Position.BufferPosition - 1;
            var navigator = sourceProvider.NavigatorService.GetTextStructureNavigator(textBuffer);
            var extent = navigator.GetExtentOfWord(currentPoint);

            return currentPoint.Snapshot.CreateTrackingSpan(extent.Span, SpanTrackingMode.EdgeInclusive);
        }
        void ICompletionSource.AugmentCompletionSession(ICompletionSession session, IList<CompletionSet> completionSets)
        {
            //get tracking span
            var trackingSpan = FindTokenSpanAtPosition(session.GetTriggerPoint(textBuffer), session);

            //find current indent
            var snapshot = session.TextView.TextSnapshot;
            var currentPoint = trackingSpan.GetStartPoint(snapshot);
            var snapshotLine = snapshot.GetLineFromPosition(currentPoint);

            var wordText = trackingSpan.GetText(snapshot);

            var lineText = snapshotLine.GetText();
            var lineStartPos = snapshotLine.Start.Position;
            var lineEndPos = snapshotLine.End.Position;
            var currentPos = currentPoint.Position;
            var indent = currentPos - lineStartPos;

            if (wordText == ".")
                indent++;

            //build completion list
            var completionList = new List<Completion>();

            //insert snippets
            foreach (var snippet in Session.Current.Settings.Snippets)
                completionList.Add(new SnippetCompletion(snippet, indent));

            //add completion set if it contains any entries
            if (completionList.Count > 0)
                completionSets.Add(new CompletionSet(
                    "Snippets",    //the non-localized title of the tab
                    "Snippets",    //the display title of the tab
                    trackingSpan,
                    completionList,
                    null));
        }
        
    }
}
