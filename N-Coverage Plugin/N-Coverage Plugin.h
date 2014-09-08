#include <iostream>
#include <vector>
#include <fstream>

#include <ida.hpp>
#include <idp.hpp>
#include <auto.hpp>
#include <loader.hpp>
#include <kernwin.hpp>

void cleanup();
void dumpDB();
void idaapi btnCallback(TView* fields[], int code);
void showGUI();
void gatherFunctions();
void run(int arg);
int init();
void term();
bool openFileDialog(HWND hWndOwner, const std::string& title, const char* filter, std::string& chosenPath);
bool saveFileDialog(HWND hWndOwner, const std::string& title, const char* filter, const std::string& defExtension, std::string& chosenFile);
void colorizeHits(bgcolor_t color);
ulong idaapi sizer(void* obj);
void idaapi desc(void* obj, ulong n, char* const* arrptr);
void idaapi enter_cb(void* obj, ulong n);
void idaapi destroy_cb(void* obj);

//--------------------------------------------------------------------------
char wanted_name[] = "N-Coverage";
char wanted_hotkey[] = "Alt-2";

//--------------------------------------------------------------------------
//
//      PLUGIN DESCRIPTION BLOCK
//
//--------------------------------------------------------------------------
plugin_t PLUGIN =
{
	IDP_INTERFACE_VERSION,
	0,                    // plugin flags
	init,                 // initialize

	term,                 // terminate. this pointer may be NULL.

	run,                  // invoke plugin

	wanted_name,          // long comment about the plugin
	// it could appear in the status line
	// or as a hint

	wanted_name,          // multiline help about the plugin

	wanted_name,          // the preferred short name of the plugin
	wanted_hotkey         // the preferred hotkey to run the plugin
};