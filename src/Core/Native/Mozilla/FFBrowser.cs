#region WatiN Copyright (C) 2006-2011 Jeroen van Menen

//Copyright 2006-2011 Jeroen van Menen
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

#endregion Copyright

using System;

namespace WatiN.Core.Native.Mozilla
{
    /// <summary>
    /// Wrapper around the XUL:browser class, see: http://developer.mozilla.org/en/docs/XUL:browser
    /// </summary>
    public class FFBrowser : JSBrowserBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FFBrowser"/> class.
        /// </summary>
        /// <param name="clientPort">The client port.</param>
        public FFBrowser(ClientPortBase clientPort) : base(clientPort)
        {
        }

        /// <summary>
        /// Load a URL into the document. see: http://developer.mozilla.org/en/docs/XUL:browser#m-loadURI
        /// </summary>
        /// <param name="url">The URL to laod.</param>
        /// <param name="waitForComplete">If false, makes to execution of LoadUri asynchronous.</param>
        protected override void LoadUri(Uri url, bool waitForComplete)
        {
            var command = string.Format("{0}.loadURI(\"{1}\");", ClientPort.PromptName, url.AbsoluteUri);

            //var command = string.Format("w0.content.location.href = \"{0}\"; }}",url.AbsoluteUri);
            if (!waitForComplete)
            {
                command = JSUtils.WrapCommandInTimer(command);
            }

            ClientPort.Write(command);
        }

        public override INativeDocument NativeDocument
        {
            get { return new FFDocument(ClientPort);  }
        }
    }
}