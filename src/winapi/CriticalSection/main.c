#include <stdio.h>
#include <Windows.h>
#include <tchar.h>

int number = 0;

DWORD WINAPI ThreadProcedure_CriticalSection(LPVOID *param) {
    LPCRITICAL_SECTION cs = param;

    for (int i = 0; i < 1000000; ++i) {
        EnterCriticalSection(cs);
        number++;
        LeaveCriticalSection(cs);
    }

    return 0;
}

DWORD WINAPI ThreadProcedure_Mutex(LPVOID *param) {
    HANDLE mutex = param;

    for (int i = 0; i < 1000000; i++) {
        WaitForSingleObject(mutex, INFINITE);
        number++;
        ReleaseMutex(mutex);
    }
}

DWORD WINAPI ThreadProcedure_Interlocked(LPVOID *param) {
    for (int i = 0; i < 1000000; i++) {
        InterlockedIncrement(&number);
    }
}

void Mutex_demo() {
    HANDLE mutex = CreateMutex(NULL, FALSE, _T("mutex1"));

    number = 0;

    HANDLE thread1 = CreateThread(NULL, 0, ThreadProcedure_Mutex, mutex, 0, NULL);
    HANDLE thread2 = CreateThread(NULL, 0, ThreadProcedure_Mutex, mutex, 0, NULL);

    WaitForSingleObject(thread1, INFINITE);
    WaitForSingleObject(thread2, INFINITE);

    printf("%d", number);
}
void CriticalSection_demo() {
    CRITICAL_SECTION critical_section;
    InitializeCriticalSection(&critical_section);

    number = 0;

    HANDLE thread1 = CreateThread(NULL, 0, ThreadProcedure_CriticalSection, &critical_section, 0, NULL);
    HANDLE thread2 = CreateThread(NULL, 0, ThreadProcedure_CriticalSection, &critical_section, 0, NULL);

    WaitForSingleObject(thread1, INFINITE);
    WaitForSingleObject(thread2, INFINITE);

    printf("%d", number);
}
void Interlocked_demo() {
    HANDLE thread1 = CreateThread(NULL, 0, ThreadProcedure_Interlocked, NULL, 0, NULL);
    HANDLE thread2 = CreateThread(NULL, 0, ThreadProcedure_Interlocked, NULL, 0, NULL);

    number = 0;

    WaitForSingleObject(thread1, INFINITE);
    WaitForSingleObject(thread2, INFINITE);

    printf("%d", number);
}

int _tmain() {
    printf("Mutex demo: \n");
    Mutex_demo();

    printf("\n=================================\n");
    printf("Critical section demo: \n");
    CriticalSection_demo();

    printf("\n=================================\n");
    printf("Interlocked demo: \n");
    Interlocked_demo();

    return 0;
}