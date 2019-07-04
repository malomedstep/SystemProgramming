
#include <windows.h>
#include <stdio.h>
#include <tchar.h>




//// __cdecl __stdcall
//unsigned long __stdcall ThreadFunction1(void* param) {
//    for (INT i = 0; i < 100; ++i) {
//        Sleep(500);
//        printf("\tSecond thread: %d\n", i);
//    }
//    return 0;
//}


DWORD WINAPI ThreadFunction(void* param) {
    int a = *((int*)param);
    for (INT i = 0; i < 100; ++i) {
        Sleep(500);
        printf("\tNew thread: %d\n", a);
    }
    return 0;
}


VOID foo(LPCWSTR path, INT offset) {
    WIN32_FIND_DATA fd;
    TCHAR mask[MAX_PATH];
    _tcscpy_s(mask, MAX_PATH, path);
    _tcscat_s(mask, MAX_PATH, _T("\\*.*"));
    HANDLE hFind = FindFirstFile(mask, &fd);
    do {
        Sleep(50);
        if (_tcscmp(_T(".."), fd.cFileName) == 0 || _tcscmp(_T("."), fd.cFileName) == 0) {
            continue;
        }
        if (fd.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY) {
            for (INT i = 0; i < offset * 2; ++i) {
                printf(" ");
            }
            _tprintf(_T("%*sDIR\n"), -70 + offset * 2, fd.cFileName);
            TCHAR nextPath[MAX_PATH];
            _tcscpy_s(nextPath, MAX_PATH, path);
            _tcscat_s(nextPath, MAX_PATH, fd.cFileName);
            _tcscat_s(nextPath, MAX_PATH, _T("\\"));
            foo(nextPath, offset + 1);
        }
        else {
            for (int i = 0; i < offset * 2; ++i) {
                printf(" ");
            }
            ULARGE_INTEGER ul;
            ul.HighPart = fd.nFileSizeHigh;
            ul.LowPart = fd.nFileSizeLow;
            ULONGLONG fileSize = ul.QuadPart;
            _tprintf(_T("%*s%ul\n"), -70 + offset * 2, fd.cFileName, fileSize);
        }
    } while (FindNextFile(hFind, &fd));
}

int main() {
    int threadParam1 = 42;
    HANDLE hThread1 = CreateThread(
        NULL,
        0,
        ThreadFunction,
        &threadParam1,
        0,
        NULL
    );
    int threadParam2 = 9999;
    HANDLE hThread2 = CreateThread(
        NULL,
        0,
        ThreadFunction,
        &threadParam2,
        0,
        NULL
    );

    system("pause");

    return 0;
}