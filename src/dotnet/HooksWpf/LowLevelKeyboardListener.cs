using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Forms;

namespace HooksWpf {
    public class KeyPressedArgs : EventArgs {
        public KeyPressedArgs(Key keyPressed) {
            KeyPressed = keyPressed;
        }

        public Key KeyPressed { get; }
    }

    public class LowLevelKeyboardListener {
        private const int WH_KEYBOARD_LL = 13;
        private const int WH_MOUSE_LL = 14;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_MOUSEMOVE = 0x0200;
        private const int WM_SYSKEYDOWN = 0x0104;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpProc, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);


        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        public event EventHandler<KeyPressedArgs> OnKeyPressed;

        private LowLevelKeyboardProc _proc;
        private IntPtr _hookId = IntPtr.Zero;

        public LowLevelKeyboardListener() {
            _proc = HookCallback;
        }

        public void HookKeyboard() {
            _hookId = SetHook(_proc);
        }

        public void UnhookKeyboard() {
            UnhookWindowsHookEx(_hookId);
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc) {
            using (var currentProcess = Process.GetCurrentProcess()) {
                using (var currentModule = currentProcess.MainModule) {
                    SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(currentModule.ModuleName), 0);
                    return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(currentModule.ModuleName), 0);
                }
            }
        }

        private IntPtr HookCallback(
            int nCode, 
            IntPtr wParam, 
            IntPtr lParam
        ) {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN 
                || wParam == (IntPtr)WM_SYSKEYDOWN) {
                int vkCode = Marshal.ReadInt32(lParam);

                OnKeyPressed?.Invoke(this, new KeyPressedArgs(KeyInterop.KeyFromVirtualKey(vkCode)));
                //return IntPtr.Zero;
            }
            return (IntPtr)1;
            //return CallNextHookEx(_hookId, nCode, wParam, lParam);
        }


    }


    //static class WinAPIHelper {
    //    public static Point GetPoint(IntPtr lParam) {
    //        return new Point(GetInt(lParam));
    //    }
    //    public static MouseButtons GetButtons(IntPtr wParam) {
    //        MouseButtons buttons = MouseButtons.None;
    //        int btns = GetInt(wParam);
    //        if ((btns & MK_LBUTTON) != 0) buttons |= MouseButtons.Left;
    //        if ((btns & MK_RBUTTON) != 0) buttons |= MouseButtons.Right;
    //        return buttons;
    //    }
    //    static int GetInt(IntPtr ptr) {
    //        return IntPtr.Size == 8 ? unchecked((int)ptr.ToInt64()) : ptr.ToInt32();
    //    }
    //    const int MK_LBUTTON = 1;
    //    const int MK_RBUTTON = 2;
    //}
}
