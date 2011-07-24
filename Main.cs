// SharpDevelop samples
// Copyright (c) 2010, AlphaSierraPapa
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification, are
// permitted provided that the following conditions are met:
//
// - Redistributions of source code must retain the above copyright notice, this list
//   of conditions and the following disclaimer.
//
// - Redistributions in binary form must reproduce the above copyright notice, this list
//   of conditions and the following disclaimer in the documentation and/or other materials
//   provided with the distribution.
//
// - Neither the name of the SharpDevelop team nor the names of its contributors may be used to
//   endorse or promote products derived from this software without specific prior written
//   permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS &AS IS& AND ANY EXPRESS
// OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY
// AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER
// IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT
// OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection; 

using ICSharpCode.AvalonEdit.Highlighting;
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

namespace ICSharpCode.HtmlSyntaxColorizer
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			IHighlightingDefinition highlightDefinition = null;
			var _assembly = Assembly.GetExecutingAssembly();
			using (Stream s = File.OpenRead("GherkinDefinition.xshd"))
			{
				using (XmlTextReader reader = new XmlTextReader(s))
				{
					highlightDefinition = HighlightingLoader.Load(reader, HighlightingManager.Instance);
				}				
			}

			HtmlWriter w = new HtmlWriter();
			w.ShowLineNumbers = false;
			w.CreateStylesheet = false;
			w.AlternateLineBackground = false;
			string code = File.ReadAllText("../../SampleFeature.txt");
			string html = w.GenerateHtml(code, highlightDefinition);
			File.WriteAllText("output.html", "<html><body>" + html + "</body></html>");
			Process.Start("output.html"); // view in browser
		}
	}
}
