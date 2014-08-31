#include "NTHeader.h"
#include <ida.hpp>
#include <idp.hpp>
#include <loader.hpp>
#include <kernwin.hpp>

#define MakePtr( cast, ptr, addValue ) (cast)( (DWORD_PTR)(ptr) + (DWORD_PTR)(addValue))

NTHeader::NTHeader()
{
	char inputFilePath[QMAXPATH];
	get_input_file_path(inputFilePath, sizeof(inputFilePath));
	parsePEFile(inputFilePath);
}

void NTHeader::parsePEFile(const char* fileName)
{
	hFile_ = CreateFile(fileName, GENERIC_READ, FILE_SHARE_READ, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, 0);
	if (hFile_ == INVALID_HANDLE_VALUE)
		throw std::exception("Unable to open file!");

	hFileMapping_ = CreateFileMapping(hFile_, NULL, PAGE_READONLY, 0, 0, NULL);
	if (hFileMapping_ == 0 )
	{
		CloseHandle(hFile_);
		throw std::exception("Unable to create file mapping!");
	}

	mappedView_ = (PBYTE)MapViewOfFile(hFileMapping_, FILE_MAP_READ, 0, 0, 0);
	if (mappedView_ == 0)
	{
		CloseHandle(hFileMapping_);
		CloseHandle(hFile_);
		throw std::exception("Unable to map view of file!");
	}

	PIMAGE_DOS_HEADER pDosHeader = (PIMAGE_DOS_HEADER)mappedView_;	
	pNTHeader_ = MakePtr(PIMAGE_NT_HEADERS, pDosHeader, pDosHeader->e_lfanew);
}

NTHeader::NTHeader(std::string fileName)
{
	parsePEFile(fileName.c_str());
}

NTHeader::~NTHeader()
{
	UnmapViewOfFile(mappedView_);
	CloseHandle(hFileMapping_);
	CloseHandle(hFile_);
}