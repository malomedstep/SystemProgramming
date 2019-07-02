#include <stdio.h>
#include <Windows.h>
#include <tchar.h>

void _tmain() {
    STARTUPINFO si;
    PROCESS_INFORMATION pi;
    ZeroMemory(&si, sizeof(si));
    si.cb = sizeof(si);
    ZeroMemory(&pi, sizeof(pi));
    LPCWSTR app = _T("C:\\Windows\\System32\\notepad.exe");
    CreateProcess(
        app,
        NULL,
        NULL,
        NULL,
        FALSE,
        0,
        NULL,
        NULL,
        &si,
        &pi
    );
    Sleep(1000);
    // TerminateProcess(pi.hProcess, 0);
    WaitForSingleObject(pi.hProcess, INFINITE);
    printf("\nFINITO LA COMEDIA\n");
}