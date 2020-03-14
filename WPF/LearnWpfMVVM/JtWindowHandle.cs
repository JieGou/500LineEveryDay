using System;

using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace LearnWpfMVVM
{
    public class JtWindowHandle : IWin32Window
    {
        private IntPtr _hwnd;

        public JtWindowHandle(IntPtr h)
        {
            //Debug.Assert(IntPtr.Zero != h, "expected non-null window handle");

            _hwnd = h;
        }

        public IntPtr Handle
        {
            get
            {
                return _hwnd;
            }
        }
    }
}