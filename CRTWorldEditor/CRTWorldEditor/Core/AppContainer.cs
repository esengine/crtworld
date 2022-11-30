﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRTWorldEditor.Core
{
    public class AppContainer
    {
        private System.Windows.Forms.Panel _hostPanel;
        private readonly ManualResetEvent _eventDone = new ManualResetEvent(false);
        private Process? _process = null;
        internal IntPtr _embededWindowHandle;

        public AppContainer(System.Windows.Forms.Panel panel)
        {
            this._hostPanel = panel;
            this._hostPanel.Resize += HostPanel_Resize;
        }


        private void HostPanel_Resize(object? sender, EventArgs e)
        {
            SetBounds();
        }

        public void ActivateWindow()
        {
            if (_process == null)
                return;

            if (_process.MainWindowHandle == IntPtr.Zero)
                return;

            Win32Api.SendMessage(_process.MainWindowHandle, Win32Api.WM_ACTIVATE, Win32Api.WA_ACTIVE, IntPtr.Zero);
        }

        public void SetBounds()
        {
            SetBounds(_hostPanel.Width, _hostPanel.Height);
        }

        public void SetBounds(int width, int height)
        {
            if (_process == null)
                return;

            if (_process.MainWindowHandle == IntPtr.Zero)
                return;

            if (width <= 0 || height <= 0)
                return;

            Win32Api.MoveWindow(_process.MainWindowHandle, 0, 0, width, height, true);

            ActivateWindow();//激活
        }

        public bool StartAndEmbedProcess(string processPath)
        {
            if (null != _process)
                return true;

            var isStartAndEmbedSuccess = false;
            _eventDone.Reset();

            ProcessStartInfo info = new ProcessStartInfo(processPath);
            info.WindowStyle = ProcessWindowStyle.Maximized;
            info.Arguments = $"-popupwindow";

            _process = Process.Start(info);

            if (_process == null)
            {
                return false;
            }

            _process.WaitForInputIdle();

            var thread = new Thread(() =>
            {
                while (true)
                {
                    if (_process.MainWindowHandle != (IntPtr)0)
                    {
                        _eventDone.Set();
                        break;
                    }
                    Thread.Sleep(10);
                }
            });
            thread.Start();

            if (_eventDone.WaitOne(10000))
            {
                isStartAndEmbedSuccess = EmbedApp(_process);
                if (!isStartAndEmbedSuccess)
                {
                    CloseApp(_process);
                }
            }
            return isStartAndEmbedSuccess;
        }

        public bool EmbedExistProcess(Process process)
        {
            _process = process;
            return EmbedApp(process);
        }

        /// <summary>
        /// 将外进程嵌入到当前程序
        /// </summary>
        /// <param name="process"></param>
        private bool EmbedApp(Process process)
        {
            //是否嵌入成功标志，用作返回值
            var isEmbedSuccess = false;
            //外进程句柄
            var processHwnd = process.MainWindowHandle;
            //容器句柄
            var panelHwnd = _hostPanel.Handle;

            if (processHwnd != (IntPtr)0 && panelHwnd != (IntPtr)0)
            {
                //把本窗口句柄与目标窗口句柄关联起来
                var setTime = 0;
                while (!isEmbedSuccess && setTime < 50)
                {
                    // Put it into this form
                    isEmbedSuccess = Win32Api.SetParent(processHwnd, panelHwnd) != 0;
                    Thread.Sleep(10);
                    setTime++;
                }

                // Remove border and whatnot
                //Win32Api.SetWindowLong(processHwnd, Win32Api.GWL_STYLE, Win32Api.WS_CHILDWINDOW | Win32Api.WS_CLIPSIBLINGS | Win32Api.WS_CLIPCHILDREN | Win32Api.WS_VISIBLE);

                SetBounds();

                //Move the window to overlay it on this window
                //Win32Api.MoveWindow(_process.MainWindowHandle, 0, 0, (int)ActualWidth, (int)ActualHeight, true);
            }

            if (isEmbedSuccess)
            {
                _embededWindowHandle = _process.MainWindowHandle;
            }

            return isEmbedSuccess;
        }

        /// <summary>
        /// 关闭进程
        /// </summary>
        /// <param name="process"></param>
        private void CloseApp(Process process)
        {
            if (process != null && !process.HasExited)
            {
                process.Kill();
            }
        }

        public void CloseProcess()
        {
            if (_process == null)
                return;

            CloseApp(_process);
            _process = null;
        }
    }

}
