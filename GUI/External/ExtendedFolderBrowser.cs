//using System;
//using System.Windows.Forms;
//using System.ComponentModel;
//using System.Security;
//using System.Runtime.InteropServices;
//using System.Text;

//namespace FolderBrowser
//{
//    public delegate bool ShowNewButtonHandler(string selectedPath);

//    /// Created By:				Goldberg Royee
//    /// Date:					6/8/2006
//    /// Reason:					This class is an extended class
//    /// for the folderBrowseDialog of .NET.
//    /// This class add a functionality to disable the 'Make New Folder' Button
//    /// whenever a CD path selected.
//    public class ExtendedFolderBrowser
//    {
//        #region Win32API Class
//        /// <summary>
//        /// 
//        /// </summary>
//        private class Win32API
//        {
//            [DllImport("shell32.dll", CharSet=CharSet.Auto)]
//            public static extern IntPtr SHBrowseForFolder([In] BROWSEINFO lpbi);
//            [DllImport("shell32.dll")]
//            public static extern int SHGetMalloc([Out, MarshalAs(UnmanagedType.LPArray)] IMalloc[] ppMalloc);
//            [DllImport("shell32.dll", CharSet=CharSet.Auto)]
//            public static extern bool SHGetPathFromIDList(IntPtr pidl, IntPtr pszPath);
//            [DllImport("shell32.dll")]
//            public static extern int SHGetSpecialFolderLocation(IntPtr hwnd, int csidl, ref IntPtr ppidl);
//            [DllImport("user32.dll", CharSet=CharSet.Auto)]
//            public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, string lParam);
//            [DllImport("user32.dll", EntryPoint="SendMessage", CharSet=CharSet.Auto)]
//            public static extern IntPtr SendMessage2(HandleRef hWnd, int msg, int wParam, int lParam);
//            [DllImport("user32.dll")] 
//            public static extern bool EnableWindow(IntPtr hWnd,bool bEnable);
//            [DllImport("user32.dll") ] 
//            public static extern int GetWindowText(int hWnd, StringBuilder text, int count); 
//            [DllImport("user32.dll") ] 
//            public static extern IntPtr FindWindow(string lpClassName,string lpWindowName);
//            [DllImport("user32.dll")] 
//            public static extern IntPtr GetWindowThreadProcessId(IntPtr hwnd, IntPtr lpdwProcessId); 

//            [DllImport("user32.dll") ] 
//            public static extern IntPtr FindWindowEx(IntPtr hwndParent,
//                IntPtr hwndChildAfter,
//                string lpszClass,
//                string lpszWindow
//                );
			

//            [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto), ComVisible(false)]
//            public class BROWSEINFO
//            {
//                public IntPtr hwndOwner;
//                public IntPtr pidlRoot;
//                public IntPtr pszDisplayName;
//                public string lpszTitle;
//                public int ulFlags;
//                public BrowseCallbackProc lpfn;
//                public IntPtr lParam;
//                public int iImage;
//            }
			
//            [ComImport, Guid("00000002-0000-0000-c000-000000000046"), SuppressUnmanagedCodeSecurity, InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
//            public interface IMalloc
//            {
//                IntPtr Alloc(int cb);
//                void Free(IntPtr pv);
//                IntPtr Realloc(IntPtr pv, int cb);
//                int GetSize(IntPtr pv);
//                int DidAlloc(IntPtr pv);
//                void HeapMinimize();
//            }
 
//            public const int WM_LBUTTONDOWN = 0x0201;
//            public const int BFFM_INITIALIZED = 1;
//            public const int BFFM_SELCHANGED = 2;

//            public delegate int BrowseCallbackProc(IntPtr hwnd, int msg, IntPtr lParam, IntPtr lpData);
//        }
//        #endregion

//        #region InternalFolderBrowser Class
//        /// <summary>
//        /// 
//        /// </summary>
//        private class InternalFolderBrowser : CommonDialog
//        {
//            private string m_selectedPath = null;
//            private Environment.SpecialFolder m_rootFolder;
//            public event EventHandler SelectedFolderChanged;
//            private string m_descriptionText = String.Empty;
//            /// <summary>
//            /// 
//            /// </summary>
//            /// <returns></returns>
//            private Win32API.IMalloc GetSHMalloc()
//            {
//                Win32API.IMalloc[] mallocArray1 = new Win32API.IMalloc[1];
//                Win32API.SHGetMalloc(mallocArray1);
//                return mallocArray1[0];
//            }

//            /// <summary>
//            /// 
//            /// </summary>
//            public override void Reset()
//            {
//                m_rootFolder = Environment.SpecialFolder.Desktop;
//                m_selectedPath = string.Empty;
//            }

//            /// <summary>
//            /// 
//            /// </summary>
//            /// <param name="hwndOwner"></param>
//            /// <returns></returns>
//            protected override bool RunDialog(System.IntPtr hwndOwner)
//            {
//                IntPtr ptr1 = IntPtr.Zero;
//                bool flag1 = false;
//                Win32API.SHGetSpecialFolderLocation(hwndOwner, (int)m_rootFolder, ref ptr1);
//                if (ptr1 == IntPtr.Zero)
//                {
//                    Win32API.SHGetSpecialFolderLocation(hwndOwner, 0, ref ptr1);
//                    if (ptr1 == IntPtr.Zero)
//                    {
//                        throw new Exception("FolderBrowserDialogNoRootFolder");
//                    }
//                }

//                //Initialize the OLE to current thread.
//                Application.OleRequired();
//                IntPtr ptr2 = IntPtr.Zero;
//                try
//                {
//                    Win32API.BROWSEINFO browseinfo1 = new Win32API.BROWSEINFO();
//                    IntPtr ptr3 = Marshal.AllocHGlobal((int) (260 * Marshal.SystemDefaultCharSize));
//                    IntPtr ptr4 = Marshal.AllocHGlobal((int) (260 * Marshal.SystemDefaultCharSize));
//                    Win32API.BrowseCallbackProc proc1 = new Win32API.BrowseCallbackProc(this.FolderBrowserDialog_BrowseCallbackProc);
//                    browseinfo1.pidlRoot = ptr1;
//                    browseinfo1.hwndOwner = hwndOwner;
//                    browseinfo1.pszDisplayName = ptr3;
//                    browseinfo1.lpszTitle = m_descriptionText;
//                    browseinfo1.ulFlags = 0x40;
//                    browseinfo1.lpfn = proc1;
//                    browseinfo1.lParam = IntPtr.Zero;
//                    browseinfo1.iImage = 0;
//                    ptr2 = Win32API.SHBrowseForFolder(browseinfo1);

//                    string s = Marshal.PtrToStringAuto(ptr3);

//                    if (ptr2 != IntPtr.Zero)
//                    {
//                        Win32API.SHGetPathFromIDList(ptr2, ptr4);
//                        this.m_selectedPath = Marshal.PtrToStringAuto(ptr4);
//                        Marshal.FreeHGlobal(ptr4);
//                        Marshal.FreeHGlobal(ptr3);
//                        flag1 = true;
//                    }
//                }
//                finally
//                {
//                    Win32API.IMalloc malloc1 = GetSHMalloc();
//                    malloc1.Free(ptr1);
//                    if (ptr2 != IntPtr.Zero)
//                    {
//                        malloc1.Free(ptr2);
//                    }
//                }
//                return flag1;
//            }

//            /// <summary>
//            /// 
//            /// </summary>
//            /// <param name="hwnd"></param>
//            /// <param name="msg"></param>
//            /// <param name="lParam"></param>
//            /// <param name="lpData"></param>
//            /// <returns></returns>
//            private int FolderBrowserDialog_BrowseCallbackProc(IntPtr hwnd, int msg, IntPtr lParam, IntPtr lpData)
//            {
//                switch (msg)
//                {
//                    case Win32API.BFFM_INITIALIZED:
//                        if (m_selectedPath != string.Empty)
//                        {
//                            Win32API.SendMessage(new HandleRef(null, hwnd), 0x467, 1, m_selectedPath);
//                        }
//                        break;

//                    case Win32API.BFFM_SELCHANGED: //Selction Changed
//                    {
//                        IntPtr ptr1 = lParam;
//                        if (ptr1 != IntPtr.Zero)
//                        {
//                            IntPtr ptr2 = Marshal.AllocHGlobal((int) (260 * Marshal.SystemDefaultCharSize));
//                            bool flag1 = Win32API.SHGetPathFromIDList(ptr1, ptr2);
//                            m_selectedPath = Marshal.PtrToStringAuto(ptr2);

//                            //Fire Event
//                            if(SelectedFolderChanged != null)
//                            {
//                                SelectedFolderChanged(this,null);
//                            }
//                            Marshal.FreeHGlobal(ptr2);
//                            Win32API.SendMessage2(new HandleRef(null, hwnd), 0x465, 0, flag1 ? 1 : 0);
//                        }
//                        break;
//                    }
//                }
//                return 0;
//            }

//            /// <summary>
//            /// 
//            /// </summary>
//            public string SelectedPath
//            {
//                get
//                {
//                    return m_selectedPath;
//                }
//            }

//            /// <summary>
//            /// 
//            /// </summary>
//            public string Description
//            {
//                get
//                {
//                    return m_descriptionText;
//                }
//                set
//                {
//                    m_descriptionText = (value == null) ? string.Empty : value;

//                }
//            }

//        }
//        #endregion
		
//        #region Private Members

//        private InternalFolderBrowser m_InternalFolderBrowser = null;
//        public event EventHandler SelectedFolderChanged;
//        private const string MAKE_NEW_FOLDER_BUTTON = "&Make New Folder";
		
//        private ShowNewButtonHandler m_ShowNewButtonHandler = null;
//        private const string BROWSE_FOR_FOLDER_CLASS_NAME =  "#32770";
//        #endregion

//        #region CTOR
//        /// <summary>
//        /// 
//        /// </summary>
//        public ExtendedFolderBrowser()
//        {
//            m_InternalFolderBrowser = new InternalFolderBrowser();
//            m_InternalFolderBrowser.SelectedFolderChanged+=new EventHandler(m_InternalFolderBrowser_SelectedFolderChanged);
//        }
//        #endregion

//        #region  Helper Methods
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="handle"></param>
//        /// <param name="state"></param>
//        private void UpdateButtonState(IntPtr handle, bool state)
//        {
//            if(handle != IntPtr.Zero)
//            {
//                Win32API.EnableWindow(handle,state);
//            }
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="windowHandle"></param>
//        /// <returns></returns>
//        private bool isFromTheSameThread(IntPtr windowHandle)
//        {
//            //Get the thread that running given handler
//            IntPtr activeThreadID = Win32API.GetWindowThreadProcessId(windowHandle, 
//                IntPtr.Zero); 

//            //Get current thread
//            int currentThread = AppDomain.GetCurrentThreadId();
			
//            return (currentThread == activeThreadID.ToInt32());

//        }

//        private IntPtr GetButtonHandle(IntPtr handle)
//        {
//            //First time
//            if(handle == IntPtr.Zero)
//            {
//                //Get Handle for class with name "#32770"
//                IntPtr parent = Win32API.FindWindow(BROWSE_FOR_FOLDER_CLASS_NAME,null);

//                //If the window we found is in the same thread we are running
//                //then it is The 'Browse For Folder' Dialog, otherwise keep searching
//                if(!isFromTheSameThread(parent))
//                {
//                    //Keep searching from this point
//                    return GetButtonHandle(parent);
//                }
//                else
//                {
//                    return   Win32API.FindWindowEx(parent,IntPtr.Zero,"Button", null);
//                }
//            }
//            else
//            {
//                //Find next window
//                IntPtr parent = Win32API.FindWindowEx(IntPtr.Zero,handle,BROWSE_FOR_FOLDER_CLASS_NAME, null);
//                if(!isFromTheSameThread(parent))
//                {
//                    return GetButtonHandle(parent);
//                }
//                else
//                {
//                    //We found the 'Browse For Folder' Dialog handler.
//                    //Lets return the child handler of 'Maker new Folder' button
//                    return   Win32API.FindWindowEx(parent,IntPtr.Zero,"Button", null);
//                }
//            }
//        }


//        /// <summary>
//        /// A different version for finding the 'Make New Foler' button handler.
//        /// We currently don't use this version, because it requires a Localization.
//        /// </summary>
//        /// <returns></returns>
//        private IntPtr GetButtonHandle()
//        {
//            //This should be using localization!!!!!!!!!!!!!!!!!#32770 (Dialog)
//            IntPtr root = Win32API.FindWindow(null,"Browse For Folder");
//            IntPtr child = Win32API.FindWindowEx(root,IntPtr.Zero,"Button", null);
//            return child;
//        }

//        /// <summary>
//        /// Check if we should disable the 'Make New Folder' button
//        /// </summary>
//        private void CheckState()
//        {
//            if(m_ShowNewButtonHandler != null)
//            {
//                if(m_ShowNewButtonHandler(SelectedPath))
//                {
//                    //Disabel the button
//                    UpdateButtonState(GetButtonHandle(IntPtr.Zero), false);
//                }
//                else
//                {
//                    //Enable the button
//                    UpdateButtonState(GetButtonHandle(IntPtr.Zero),true);
//                }
//            }
//        }

//        private void m_InternalFolderBrowser_SelectedFolderChanged(object sender, EventArgs e)
//        {
//            CheckState();

//            //Fire event selection has changed in case anyone wants 
//            //to be notified.
//            if(SelectedFolderChanged != null)
//            {
//                SelectedFolderChanged(sender,e);
//            }
//        }
//        #endregion

//        #region FolderBrowserDialog Mathods

//        public DialogResult ShowDialog()
//        {
//            return ShowDialog(null);
//        }

//        public DialogResult ShowDialog(IWin32Window owner)
//        {
//            return m_InternalFolderBrowser.ShowDialog(owner);
//        }

//        public string SelectedPath
//        {
//            get
//            {
//                return m_InternalFolderBrowser.SelectedPath;
//            }
//        }

//        public string Description
//        {
//            get
//            {
//                return m_InternalFolderBrowser.Description;
//            }
//            set
//            {
//                m_InternalFolderBrowser.Description = value;
//            }
//        }

//        /// <summary>
//        /// Pass the delegate to your function, which will decide
//        /// if to enable the 'Make New Folder' button or not.
//        /// </summary>
//        public ShowNewButtonHandler SetNewFolderButtonCondition
//        {
//            get
//            {
//                return m_ShowNewButtonHandler;;
//            }
//            set
//            {
//                m_ShowNewButtonHandler = value;
//            }
//        }

//        #endregion
//    }
//}

//
// A replacement for the builtin System.Windows.Forms.FolderBrowserDialog class.
// This one includes an edit box, and also displays the full path in the edit box. 
//
// based on code from http://support.microsoft.com/default.aspx?scid=kb;[LN];306285 
// 
// 20 Feb 2009
//
// ========================================================================================
// Example usage:
// 
// string _folderName = "c:\\dinoch";
// private void button1_Click(object sender, EventArgs e)
// {
//     _folderName = (System.IO.Directory.Exists(_folderName)) ? _folderName : "";
//     var dlg1 = new Ionic.Utils.FolderBrowserDialogEx
//     {
//         Description = "Select a folder for the extracted files:",
//         ShowNewFolderButton = true,
//         ShowEditBox = true,
//         //NewStyle = false,
//         SelectedPath = _folderName,
//         ShowFullPathInEditBox= false,
//     };
//     dlg1.RootFolder = System.Environment.SpecialFolder.MyComputer;
// 
//     var result = dlg1.ShowDialog();
// 
//     if (result == DialogResult.OK)
//     {
//         _folderName = dlg1.SelectedPath;
//         this.label1.Text = "The folder selected was: ";
//         this.label2.Text = _folderName;
//     }
// }
//


namespace Ionic.Utils
{
using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Security.Permissions;
using System.Security;
using System.Threading;

    //[Designer("System.Windows.Forms.Design.FolderBrowserDialogDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), DefaultEvent("HelpRequest"), SRDescription("DescriptionFolderBrowserDialog"), DefaultProperty("SelectedPath")]
    public class FolderBrowserDialogEx : System.Windows.Forms.CommonDialog
    {
        private static readonly int MAX_PATH = 260;

        // Fields
        private PInvoke.BrowseFolderCallbackProc _callback;
        private string _descriptionText;
        private Environment.SpecialFolder _rootFolder;
        private string _selectedPath;
        private bool _selectedPathNeedsCheck;
        private bool _showNewFolderButton;
        private bool _showEditBox;
        private bool _showBothFilesAndFolders;
        private bool _newStyle = true;
        private bool _showFullPathInEditBox = true;
        private bool _dontIncludeNetworkFoldersBelowDomainLevel;
        private int _uiFlags;
        private IntPtr _hwndEdit;
        private IntPtr _rootFolderLocation;

        // Events
        //[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler HelpRequest
        {
            add
            {
                base.HelpRequest += value;
            }
            remove
            {
                base.HelpRequest -= value;
            }
        }

        // ctor
        public FolderBrowserDialogEx()
        {
            this.Reset();
        }

        // Factory Methods
        public static FolderBrowserDialogEx PrinterBrowser()
        {
            FolderBrowserDialogEx x = new FolderBrowserDialogEx();
	    // avoid MBRO comppiler warning when passing _rootFolderLocation as a ref:
	    x.BecomePrinterBrowser();
            return x;
        }

        public static FolderBrowserDialogEx ComputerBrowser()
        {
            FolderBrowserDialogEx x = new FolderBrowserDialogEx();
	    // avoid MBRO comppiler warning when passing _rootFolderLocation as a ref:
	    x.BecomeComputerBrowser();
            return x;
        }


	// Helpers
	private void BecomePrinterBrowser()
	{
            _uiFlags += BrowseFlags.BIF_BROWSEFORPRINTER;
            Description = "Select a printer:";
            PInvoke.Shell32.SHGetSpecialFolderLocation(IntPtr.Zero, CSIDL.PRINTERS, ref this._rootFolderLocation);
            ShowNewFolderButton = false;
            ShowEditBox = false;
	}       

	private void BecomeComputerBrowser()
	{
            _uiFlags += BrowseFlags.BIF_BROWSEFORCOMPUTER;
            Description = "Select a computer:";
            PInvoke.Shell32.SHGetSpecialFolderLocation(IntPtr.Zero, CSIDL.NETWORK, ref this._rootFolderLocation);
            ShowNewFolderButton = false;
            ShowEditBox = false;
	}


        private class CSIDL
        {
            public const int PRINTERS = 4;
            public const int NETWORK = 0x12;
        }

        private class BrowseFlags
        {
            public const int BIF_DEFAULT = 0x0000;
            public const int BIF_BROWSEFORCOMPUTER = 0x1000;
            public const int BIF_BROWSEFORPRINTER = 0x2000;
            public const int BIF_BROWSEINCLUDEFILES = 0x4000;
            public const int BIF_BROWSEINCLUDEURLS = 0x0080;
            public const int BIF_DONTGOBELOWDOMAIN = 0x0002;
            public const int BIF_EDITBOX = 0x0010;
            public const int BIF_NEWDIALOGSTYLE = 0x0040;
            public const int BIF_NONEWFOLDERBUTTON = 0x0200;
            public const int BIF_RETURNFSANCESTORS = 0x0008;
            public const int BIF_RETURNONLYFSDIRS = 0x0001;
            public const int BIF_SHAREABLE = 0x8000;
            public const int BIF_STATUSTEXT = 0x0004;
            public const int BIF_UAHINT = 0x0100;
            public const int BIF_VALIDATE = 0x0020;
            public const int BIF_NOTRANSLATETARGETS = 0x0400;
        }

        private static class BrowseForFolderMessages
        {
            // messages FROM the folder browser
            public const int BFFM_INITIALIZED = 1;
            public const int BFFM_SELCHANGED = 2;
            public const int BFFM_VALIDATEFAILEDA = 3;
            public const int BFFM_VALIDATEFAILEDW = 4;
            public const int BFFM_IUNKNOWN = 5;

            // messages TO the folder browser
            public const int BFFM_SETSTATUSTEXT = 0x464;
            public const int BFFM_ENABLEOK = 0x465;
            public const int BFFM_SETSELECTIONA = 0x466;
            public const int BFFM_SETSELECTIONW = 0x467;
        }

        private int FolderBrowserCallback(IntPtr hwnd, int msg, IntPtr lParam, IntPtr lpData)
        {
            switch (msg)
            {
                case BrowseForFolderMessages.BFFM_INITIALIZED:
                    if (this._selectedPath.Length != 0)
                    {
                        PInvoke.User32.SendMessage(new HandleRef(null, hwnd), BrowseForFolderMessages.BFFM_SETSELECTIONW, 1, this._selectedPath);
                        if (this._showEditBox && this._showFullPathInEditBox)
                        {
                            // get handle to the Edit box inside the Folder Browser Dialog
                            _hwndEdit = PInvoke.User32.FindWindowEx(new HandleRef(null, hwnd), IntPtr.Zero, "Edit", null);
                            PInvoke.User32.SetWindowText(_hwndEdit, this._selectedPath);
                        }
                    }
                    break;

                case BrowseForFolderMessages.BFFM_SELCHANGED:
                    IntPtr pidl = lParam;
                    if (pidl != IntPtr.Zero)
                    {
                        if (((_uiFlags & BrowseFlags.BIF_BROWSEFORPRINTER) == BrowseFlags.BIF_BROWSEFORPRINTER) ||
                            ((_uiFlags & BrowseFlags.BIF_BROWSEFORCOMPUTER) == BrowseFlags.BIF_BROWSEFORCOMPUTER))
                        {
                            // we're browsing for a printer or computer, enable the OK button unconditionally.
                            PInvoke.User32.SendMessage(new HandleRef(null, hwnd), BrowseForFolderMessages.BFFM_ENABLEOK, 0, 1);
                        }
                        else
                        {
                            IntPtr pszPath = Marshal.AllocHGlobal(MAX_PATH * Marshal.SystemDefaultCharSize);
                            bool haveValidPath = PInvoke.Shell32.SHGetPathFromIDList(pidl, pszPath);
                            String displayedPath = Marshal.PtrToStringAuto(pszPath);
                            Marshal.FreeHGlobal(pszPath);
                            // whether to enable the OK button or not. (if file is valid)
                            PInvoke.User32.SendMessage(new HandleRef(null, hwnd), BrowseForFolderMessages.BFFM_ENABLEOK, 0, haveValidPath ? 1 : 0);

                            // Maybe set the Edit Box text to the Full Folder path
                            if (haveValidPath && !String.IsNullOrEmpty(displayedPath))
                            {
                                if (_showEditBox && _showFullPathInEditBox)
                                {
                                    if (_hwndEdit != IntPtr.Zero)
                                        PInvoke.User32.SetWindowText(_hwndEdit, displayedPath);
                                }

                                if ((_uiFlags & BrowseFlags.BIF_STATUSTEXT) == BrowseFlags.BIF_STATUSTEXT)
                                    PInvoke.User32.SendMessage(new HandleRef(null, hwnd), BrowseForFolderMessages.BFFM_SETSTATUSTEXT, 0, displayedPath);
                            }
                        }
                    }
                    break;
            }
            return 0;
        }

        private static PInvoke.IMalloc GetSHMalloc()
        {
            PInvoke.IMalloc[] ppMalloc = new PInvoke.IMalloc[1];
            PInvoke.Shell32.SHGetMalloc(ppMalloc);
            return ppMalloc[0];
        }

        public override void Reset()
        {
            this._rootFolder = (Environment.SpecialFolder)0;
            this._descriptionText = string.Empty;
            this._selectedPath = string.Empty;
            this._selectedPathNeedsCheck = false;
            this._showNewFolderButton = true;
            this._showEditBox = true;
            this._newStyle = true;
            this._dontIncludeNetworkFoldersBelowDomainLevel = false;
            this._hwndEdit = IntPtr.Zero;
            this._rootFolderLocation = IntPtr.Zero;
        }

        protected override bool RunDialog(IntPtr hWndOwner)
        {
            bool result = false;
            if (_rootFolderLocation == IntPtr.Zero)
            {
                PInvoke.Shell32.SHGetSpecialFolderLocation(hWndOwner, (int)this._rootFolder, ref _rootFolderLocation);
                if (_rootFolderLocation == IntPtr.Zero)
                {
                    PInvoke.Shell32.SHGetSpecialFolderLocation(hWndOwner, 0, ref _rootFolderLocation);
                    if (_rootFolderLocation == IntPtr.Zero)
                    {
                        throw new InvalidOperationException("FolderBrowserDialogNoRootFolder");
                    }
                }
            }
            _hwndEdit = IntPtr.Zero;
            //_uiFlags = 0;
            if (_dontIncludeNetworkFoldersBelowDomainLevel)
                _uiFlags += BrowseFlags.BIF_DONTGOBELOWDOMAIN;
            if (this._newStyle)
                _uiFlags += BrowseFlags.BIF_NEWDIALOGSTYLE;
            if (!this._showNewFolderButton)
                _uiFlags += BrowseFlags.BIF_NONEWFOLDERBUTTON;
            if (this._showEditBox)
                _uiFlags += BrowseFlags.BIF_EDITBOX;
            if (this._showBothFilesAndFolders)
                _uiFlags += BrowseFlags.BIF_BROWSEINCLUDEFILES;


            if (Control.CheckForIllegalCrossThreadCalls && (Application.OleRequired() != ApartmentState.STA))
            {
                throw new ThreadStateException("DebuggingException: ThreadMustBeSTA");
            }
            IntPtr pidl = IntPtr.Zero;
            IntPtr hglobal = IntPtr.Zero;
            IntPtr pszPath = IntPtr.Zero;
            try
            {
                PInvoke.BROWSEINFO browseInfo = new PInvoke.BROWSEINFO();
                hglobal = Marshal.AllocHGlobal(MAX_PATH * Marshal.SystemDefaultCharSize);
                pszPath = Marshal.AllocHGlobal(MAX_PATH * Marshal.SystemDefaultCharSize);
                this._callback = new PInvoke.BrowseFolderCallbackProc(this.FolderBrowserCallback);
                browseInfo.pidlRoot = _rootFolderLocation;
                browseInfo.Owner = hWndOwner;
                browseInfo.pszDisplayName = hglobal;
                browseInfo.Title = this._descriptionText;
                browseInfo.Flags = _uiFlags;
                browseInfo.callback = this._callback;
                browseInfo.lParam = IntPtr.Zero;
                browseInfo.iImage = 0;
                pidl = PInvoke.Shell32.SHBrowseForFolder(browseInfo);
                if (((_uiFlags & BrowseFlags.BIF_BROWSEFORPRINTER) == BrowseFlags.BIF_BROWSEFORPRINTER) ||
                ((_uiFlags & BrowseFlags.BIF_BROWSEFORCOMPUTER) == BrowseFlags.BIF_BROWSEFORCOMPUTER))
                {
                    this._selectedPath = Marshal.PtrToStringAuto(browseInfo.pszDisplayName);
                    result = true;
                }
                else
                {
                    if (pidl != IntPtr.Zero)
                    {
                        PInvoke.Shell32.SHGetPathFromIDList(pidl, pszPath);
                        this._selectedPathNeedsCheck = true;
                        this._selectedPath = Marshal.PtrToStringAuto(pszPath);
                        result = true;
                    }
                }
            }
            finally
            {
                PInvoke.IMalloc sHMalloc = GetSHMalloc();
                sHMalloc.Free(_rootFolderLocation);
                _rootFolderLocation = IntPtr.Zero;
                if (pidl != IntPtr.Zero)
                {
                    sHMalloc.Free(pidl);
                }
                if (pszPath != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pszPath);
                }
                if (hglobal != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(hglobal);
                }
                this._callback = null;
            }
            return result;
        }

        // Properties
        //[SRDescription("FolderBrowserDialogDescription"), SRCategory("CatFolderBrowsing"), Browsable(true), DefaultValue(""), Localizable(true)]

        /// <summary>
        /// This description appears near the top of the dialog box, providing direction to the user.
        /// </summary>
        public string Description
        {
            get
            {
                return this._descriptionText;
            }
            set
            {
                this._descriptionText = (value == null) ? string.Empty : value;
            }
        }

        //[Localizable(false), SRCategory("CatFolderBrowsing"), SRDescription("FolderBrowserDialogRootFolder"), TypeConverter(typeof(SpecialFolderEnumConverter)), Browsable(true), DefaultValue(0)]
        public Environment.SpecialFolder RootFolder
        {
            get
            {
                return this._rootFolder;
            }
            set
            {
                if (!Enum.IsDefined(typeof(Environment.SpecialFolder), value))
                {
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(Environment.SpecialFolder));
                }
                this._rootFolder = value;
            }
        }

        //[Browsable(true), SRDescription("FolderBrowserDialogSelectedPath"), SRCategory("CatFolderBrowsing"), DefaultValue(""), Editor("System.Windows.Forms.Design.SelectedPathEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Localizable(true)]

        /// <summary>
        /// Set or get the selected path.  
        /// </summary>
        public string SelectedPath
        {
            get
            {
                if (((this._selectedPath != null) && (this._selectedPath.Length != 0)) && this._selectedPathNeedsCheck)
                {
                    new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this._selectedPath).Demand();
                    this._selectedPathNeedsCheck = false;
                }
                return this._selectedPath;
            }
            set
            {
                this._selectedPath = (value == null) ? string.Empty : value;
                this._selectedPathNeedsCheck = true;
            }
        }

        //[SRDescription("FolderBrowserDialogShowNewFolderButton"), Localizable(false), Browsable(true), DefaultValue(true), SRCategory("CatFolderBrowsing")]

        /// <summary>
        /// Enable or disable the "New Folder" button in the browser dialog.
        /// </summary>
        public bool ShowNewFolderButton
        {
            get
            {
                return this._showNewFolderButton;
            }
            set
            {
                this._showNewFolderButton = value;
            }
        }

        /// <summary>
        /// Show an "edit box" in the folder browser.
        /// </summary>
        /// <remarks>
        /// The "edit box" normally shows the name of the selected folder.  
        /// The user may also type a pathname directly into the edit box.  
        /// </remarks>
        /// <seealso cref="ShowFullPathInEditBox"/>
        public bool ShowEditBox
        {
            get
            {
                return this._showEditBox;
            }
            set
            {
                this._showEditBox = value;
            }
        }

        /// <summary>
        /// Set whether to use the New Folder Browser dialog style.
        /// </summary>
        /// <remarks>
        /// The new style is resizable and includes a "New Folder" button.
        /// </remarks>
        public bool NewStyle
        {
            get
            {
                return this._newStyle;
            }
            set
            {
                this._newStyle = value;
            }
        }


        public bool DontIncludeNetworkFoldersBelowDomainLevel
        {
            get { return _dontIncludeNetworkFoldersBelowDomainLevel; }
            set { _dontIncludeNetworkFoldersBelowDomainLevel = value; }
        }

        /// <summary>
        /// Show the full path in the edit box as the user selects it. 
        /// </summary>
        /// <remarks>
        /// This works only if ShowEditBox is also set to true. 
        /// </remarks>
        public bool ShowFullPathInEditBox
        {
            get { return _showFullPathInEditBox; }
            set { _showFullPathInEditBox = value; }
        }

        public bool ShowBothFilesAndFolders
        {
            get { return _showBothFilesAndFolders; }
            set { _showBothFilesAndFolders = value; }
        }
    }



    internal static class PInvoke
    {
        static PInvoke() { }

        public delegate int BrowseFolderCallbackProc(IntPtr hwnd, int msg, IntPtr lParam, IntPtr lpData);

        internal static class User32
        {
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, string lParam);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);

            [DllImport("user32.dll", SetLastError = true)]
            //public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
            //public static extern IntPtr FindWindowEx(HandleRef hwndParent, HandleRef hwndChildAfter, string lpszClass, string lpszWindow);
            public static extern IntPtr FindWindowEx(HandleRef hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern Boolean SetWindowText(IntPtr hWnd, String text);
        }

        [ComImport, Guid("00000002-0000-0000-c000-000000000046"), SuppressUnmanagedCodeSecurity, InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IMalloc
        {
            [PreserveSig]
            IntPtr Alloc(int cb);
            [PreserveSig]
            IntPtr Realloc(IntPtr pv, int cb);
            [PreserveSig]
            void Free(IntPtr pv);
            [PreserveSig]
            int GetSize(IntPtr pv);
            [PreserveSig]
            int DidAlloc(IntPtr pv);
            [PreserveSig]
            void HeapMinimize();
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class BROWSEINFO
        {
            public IntPtr Owner;
            public IntPtr pidlRoot;
            public IntPtr pszDisplayName;
            public string Title;
            public int Flags;
            public BrowseFolderCallbackProc callback;
            public IntPtr lParam;
            public int iImage;
        }



        [SuppressUnmanagedCodeSecurity]
        internal static class Shell32
        {
            // Methods
            [DllImport("shell32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr SHBrowseForFolder([In] PInvoke.BROWSEINFO lpbi);
            [DllImport("shell32.dll")]
            public static extern int SHGetMalloc([Out, MarshalAs(UnmanagedType.LPArray)] PInvoke.IMalloc[] ppMalloc);
            [DllImport("shell32.dll", CharSet = CharSet.Auto)]
            public static extern bool SHGetPathFromIDList(IntPtr pidl, IntPtr pszPath);
            [DllImport("shell32.dll")]
            public static extern int SHGetSpecialFolderLocation(IntPtr hwnd, int csidl, ref IntPtr ppidl);
        }

    }
}