using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace custos;

public class Custos(Logger logger)
{
    private readonly FilesController _filesController = new FilesController();
    
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();
    
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
    
    // Every 1 second get current window title
    public void Run()
    {
        logger.Log("Application started.", LogLevel.Info);
        while (true)
        {
            var handle = GetForegroundWindow();

            if (handle != IntPtr.Zero)
            {
                var windowTitle = GetActiveWindowTitle(handle);
                var processName = GetActiveWindowProcessName(handle);
                
                logger.Log($"Current window title: {windowTitle}", LogLevel.Info);
                logger.Log($"Current process name: {processName}", LogLevel.Info);
            }
            else
            {
                logger.Log("Could not get foreground window.", LogLevel.Error);
            }
            
            Thread.Sleep(100);
        }
    }

    private static string GetActiveWindowProcessName(IntPtr handle)
    {
        var result = GetWindowThreadProcessId(handle, out var processId);
        if (result == 0)
        {
            return "Desktop";
        }
        
        var process = Process.GetProcessById((int) processId);
        
        return process.ProcessName;
    }

    private static string GetActiveWindowTitle(IntPtr handle)
    {
        const int nChars = 256;
        var windowName = new StringBuilder(nChars);
    
        var length = GetWindowText(handle, windowName, nChars);
    
        return length > 0 ?
            windowName.ToString() :
            "Desktop";
    }
}