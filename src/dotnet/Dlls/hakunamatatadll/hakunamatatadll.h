#pragma once
#include <iostream>
using namespace std;
using namespace System;
using namespace ContractDll;

namespace hakunamatatadll {
    public ref class HakunaMatataConverter 
        : public IPlugin
	{
        // Inherited via IPlugin
        
    public:
        // Inherited via IPlugin
         virtual property System::String ^ Name {
             System::String ^ get() {
                 return "C++/CLI";
             }
        }

         virtual System::String ^ Encode(System::String ^text, unsigned char param) {
             return "C++/CLI HAKUNA MATATA";
        }

    };
}
