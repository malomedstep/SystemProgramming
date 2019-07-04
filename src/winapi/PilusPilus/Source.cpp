#include <iostream>
#include <vector>
#include <algorithm>
using namespace std;
//bool lambda1(int num) {
//    return num % 2 == 0;
//}

class func1 {
private:
    int n;
public:
    func1(int n) : n(n) {}
    bool operator()(int num) {
        return num % n;
    }
};

template <typename Ti, typename TPred>
int my_count_if(Ti begin, Ti end, TPred pred) {
    int c = 0;
    while (begin != end) {
        if (pred(*begin)) {
            c++;
        }
        begin++;
    }
    return c;
}

auto foo() {
    vector<int> v{ 7,9,5,4,8,3,2,6,5 };
    int n;
    cin >> n;
    int c = 15;
    char b = 's';
    // auto f = func1(n);
    auto lambda = [&n](int num) -> bool {
        return num % n;
    };
    return lambda;
}

int main() {
    func1 a(5);
    func1(4);


    /*auto lambda = foo();
    lambda(42);
    int c = my_count_if(v.begin(), v.end(),
        func1
        );*/

    /*
    (num) => {
        return num % 2 == 0;
    }

    */


    return 0;
}