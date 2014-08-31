#define WIN32_LEAN_AND_MEAN
#include <windows.h>
#include <iostream>

class NTHeader
{
public:
	NTHeader();
	NTHeader(std::string fileName);
	~NTHeader();

	PIMAGE_NT_HEADERS getNTHeader() { return pNTHeader_; }

private:
	void parsePEFile(const char* fileName);

	HANDLE hFile_;
	HANDLE hFileMapping_;
	PBYTE mappedView_;
	PIMAGE_NT_HEADERS pNTHeader_;
};