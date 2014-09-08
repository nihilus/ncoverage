#include <shlwapi.h>
#include "N-Coverage Plugin.h"
#include "NTHeader.h"

using namespace std;

struct Hit
{
	ea_t address;
	int count;
};

// options read from hits file
struct ImportOptions 
{
	unsigned char useColor;
	unsigned char createMarkers;
	unsigned char blendColors;
	unsigned char appendHitCount;
	unsigned char r;
	unsigned char g;
	unsigned char b;
};
ImportOptions options_;

vector<ea_t> functions_;
ea_t baseAddr_;
vector<Hit> hits_;
bool modal_;

void cleanup()
{
}

// dump database to file
void dumpDB()
{
	// try to get image base address from input file
	try
	{
		NTHeader ntHeader;
		baseAddr_ = ntHeader.getNTHeader()->OptionalHeader.ImageBase;
	}
	catch (std::exception* e)
	{
		verror(e->what(), NULL);
		return;
	}

	msg("Found IBA: %a\n", baseAddr_);
	msg("Enumerating functions...");
	gatherFunctions();
	msg("done. (%u functions found)\n", functions_.size());

	char inputFilePath[QMAXPATH];
	get_input_file_path(inputFilePath, sizeof(inputFilePath));

	char inputFile[QMAXPATH];
	get_root_filename(inputFile, QMAXPATH);
	string file = inputFile;
	if (saveFileDialog(NULL, "Select file to dump database to", "N-Coverage dumps (*.ndump)\0*.ndump\0\0", "ndump", file))
	{
		msg("Exporting to file %s...", file.c_str());
		ofstream fs;
		fs.open(file.c_str());
		fs.flags(ios_base::hex | ios_base::uppercase);
		fs << inputFilePath << endl;
		fs << baseAddr_ << endl;
		for (size_t i=0; i<functions_.size(); ++i) fs << functions_[i] << endl;
		fs.close();
		msg("done.\n", functions_.size());
	}
	else msg("Export canceled!\n");
}

// show open file dialog
bool openFileDialog(HWND hWndOwner, const string& title, const char* filter, string& chosenFile)
{
	OPENFILENAME ofn;
	char fileName[QMAXPATH];

	ZeroMemory(&ofn, sizeof(ofn));
	ofn.lStructSize = sizeof(ofn);
	ofn.hwndOwner = hWndOwner;
	ofn.lpstrFile = fileName;

	ofn.lpstrFile[0] = '\0';
	ofn.nMaxFile = sizeof(fileName);
	ofn.lpstrFilter = filter;
	ofn.nFilterIndex = 1;
	ofn.lpstrFileTitle = NULL;
	ofn.lpstrTitle = title.c_str();
	ofn.nMaxFileTitle = 0;
	ofn.Flags = OFN_PATHMUSTEXIST | OFN_FILEMUSTEXIST;

	if (GetOpenFileName(&ofn) == TRUE) chosenFile = string(ofn.lpstrFile);
	else return false;

	return true;
}

// show save dialog
bool saveFileDialog(HWND hWndOwner, const string& title, const char* filter, const string& defExtension, string& chosenFile)
{
	OPENFILENAME ofn;
	char fileName[QMAXPATH];

	if (chosenFile.size()) 
	{
		if (chosenFile.size() > 4){
			chosenFile = chosenFile.substr(0, chosenFile.size()-4);
			chosenFile.append(".ndump");
		}
		strncpy_s(fileName, QMAXPATH, chosenFile.c_str(), _TRUNCATE);
	}

	ZeroMemory(&ofn, sizeof(ofn));
	ofn.lStructSize = sizeof(ofn);
	ofn.hwndOwner = hWndOwner;
	ofn.lpstrFile = fileName;
	ofn.lpstrDefExt = defExtension.c_str();

	if (chosenFile.size() == 0) ofn.lpstrFile[0] = '\0';
	ofn.nMaxFile = sizeof(fileName);
	ofn.lpstrFilter = filter;
	ofn.nFilterIndex = 1;
	ofn.lpstrFileTitle = NULL;
	ofn.lpstrTitle = title.c_str();
	ofn.nMaxFileTitle = 0;
	ofn.Flags = OFN_PATHMUSTEXIST | OFN_OVERWRITEPROMPT;

	if (GetSaveFileName(&ofn) == TRUE) chosenFile = string(ofn.lpstrFile);
	else return false;

	return true;
}

// read hits from file and initialize options struct
void importHits()
{
	// show file open dialog
	string path;
	if (openFileDialog(NULL, "Import hits from file", "N-Coverage hits (*.nhits)\0*.nhits\0\0", path))
	{
		unsigned int iba;
		// get code section virtual offset
		try
		{
			NTHeader ntHeader;
			PIMAGE_NT_HEADERS pNT = ntHeader.getNTHeader();
			iba = pNT->OptionalHeader.ImageBase;
		}
		catch (std::exception* e)
		{
			verror(e->what(), NULL);
			return;
		}
		
		// load hits from file
		ifstream fs;
		fs.open(path.c_str(), ios_base::binary);
		Hit hit;
		hits_.clear();
		// read configuration first
		fs.read((char*)&options_, sizeof(ImportOptions));
		// then get all hits
		while (fs.read((char*)&hit, sizeof(Hit)))
		{
			// hits from file are RVAs so we need to add IBA
			hit.address += iba;
			hits_.push_back(hit);
		}
		fs.close();

		// now handle different options
		if (options_.useColor) colorizeHits(RGB(options_.r, options_.g, options_.b));
	}
}

// iterate over all hits and assign color
void colorizeHits(bgcolor_t color)
{
	int count = 0;
	for (size_t i=0; i<hits_.size(); ++i)
	{
		func_t* f = get_fchunk(hits_[i].address);
		if (f) 
		{
			for (ea_t ea=f->startEA; ea<f->endEA; ++ea) set_item_color(ea, color);
			++count;
		}
	}
	msg("Sucessfully assigned color to %d functions\n", count);
}

// colum widths
const int widths[] = { 5, 9, 9 };
// column headers
const char* header[] =
{
	"#",
	"Address",
	"Hit Count"
};

// callback which returns the number of lines in the hits listview
ulong idaapi sizer(void* obj)
{
	return hits_.size();
}

// function that generates the list line
void idaapi desc(void* obj, ulong n, char* const* arrptr)
{
	// generate column headers
	if (n == 0)
	{
		for (int i=0; i<qnumber(header); ++i) qstrncpy(arrptr[i], header[i], MAXSTR);
		return;
	}
	
	--n;
	qsnprintf(arrptr[0], MAXSTR, "%d", n);
	qsnprintf(arrptr[1], MAXSTR, "%08a", hits_[n].address);
	qsnprintf(arrptr[2], MAXSTR, "%d", hits_[n].count);
}

// callback when user presses enter
void idaapi enter_cb(void* obj, ulong n)
{
	jumpto(hits_[n-1].address);
}

// return the size of the basic block starting at start
ea_t nextBBSize(ea_t start, ea_t end)
{
	ea_t currentEA = start;
	while(currentEA < end)
	{
		// try to interprete current EA as code
		if (!decode_insn(currentEA)) break;
		currentEA = get_item_end(currentEA);
		// should a "call" end our BB?
		if (is_basic_block_end(false)) break;
	}
	if (currentEA != start) return currentEA - start;
	else return 0;
}

void test()
{
	int segCount = get_segm_qty();
	bool found = false;
	segment_t* s = NULL;
	for (int i=0; i<segCount && !found; ++i)
	{
		s = getnseg(i);
		if (s == NULL) continue;
		if (!s->is_loader_segm() || s->type != SEG_CODE) continue;
		found = true;
	}
	if (!found) return;

	ea_t endEA = s->endEA;
	ea_t start = 0x801221;
	ea_t nextEA;// = nextthat(start, endEA, f_isCode, NULL);
	
	nextEA = start;
	msg("erste nextEA: %a\n", nextEA);
	while (nextEA < endEA)
	{
		// make sure that we always start with code
		if (!isCode(get_flags_novalue(nextEA)))
		{
			 nextEA = nextthat(nextEA, endEA, f_isCode, NULL);
			 if (nextEA >= endEA) break;
		}
		ea_t bbSize = nextBBSize(nextEA, endEA);
		if (bbSize > 0)
		{
			// remember address and size
			msg("neuer BB: %a, size: %d\n", nextEA, bbSize);
			//..
			// move on to next BB
			nextEA += bbSize;
		}
	}
}

// button callback
void idaapi btnCallback(TView* fields[], int code)
{
	// hack, since we dont have to window handle of the main dialog
	if (modal_) return;
	modal_ = true;

	switch(code)
	{
	// import hits
	case 1:
		importHits();
		break;

	// dump db
	case 2:
		dumpDB();
	    break;

	// show window with a list of all imported hits
	case 3:
		if (hits_.size() > 0)
		{
			close_form(fields, code);
			choose2(false,								// non-modal window
					-1, -1, -1, -1,						// position is determined by Windows
					NULL,								// pass the created array
					qnumber(header),					// number of columns
					widths,								// widths of columns
					sizer,								// function that returns number of lines
					desc,								// function that generates a line
					"N-Coverage",						// window title
					-1,									// use the default icon for the window
					0,									// position the cursor on the first line
					NULL,								// "kill" callback
					NULL,								// "new" callback
					NULL,								// "update" callback
					NULL,								// "edit" callback
					enter_cb,							// function to call when the user pressed Enter
					NULL,								// function to call when the window is closed
					NULL,								// use default popup menu items
					NULL);								// use the same icon for all lines
		}
		else vwarning("No hits imported yet!", NULL);
		break;

	// test
	case 4:
		modal_ = true;
		test();
		modal_ = false;
		break;
	}
	modal_ = false;
}

// display main window
void showGUI()
{
	char form[] =
		"BUTTON YES Close\n"
		"BUTTON NO NONE\n"
		"BUTTON CANCEL NONE\n"
		"N-Coverage IDA Plugin\n"
		"<#Export database so it can be imported by N-Coverage#Dump database...:B:2:30::>\n\n"
		"<#Import set of hits from a file generated by N-Coverage#Import set of hits...:B:1:30::>\n\n"
		//"<#TEST#Test...:B:4:30::>\n\n"
		"<#Show list of hits imported from N-Coverage#Show hits window...:B:3:30::>\n\n\n";

	formcb_t btnCB = btnCallback;
	int ok = AskUsingForm_c(form, btnCB, btnCB, btnCB);
}

// scan IDA database for all functions
void gatherFunctions()
{
	functions_.clear();
	size_t count = get_func_qty();
	for (size_t i=0; i< count; ++i)
	{
		func_t* func = getn_func(i);
		if (func) functions_.push_back(func->startEA - baseAddr_);
		//if (func && !(func->flags & FUNC_THUNK)) functions_.push_back(func->startEA - baseAddr_);
		//if (func && (func->flags & FUNC_THUNK)) msg("poff: %a", func->startEA - baseAddr_);
	}
}

void run(int arg)
{
	// warn if auto analysis has not finished yet
	if (!autoIsOk())
	{
		if (askyn_c(0, "HIDECANCEL\n"
			"The analysis has not finished yet.\n"
			"The plugin will give wrong results.\n"
			"Do you want to continue?") <= 0)
			return;
	}
	
	// show main dialog
	showGUI();
}

int init()
{
	// Our plugin works only for x86 PE executables
	if (ph.id != PLFM_386 || inf.filetype != f_PE) return PLUGIN_SKIP;
	msg("Loaded N-Coverage plugin\n");
	modal_ = false;
	return PLUGIN_KEEP;
}

void term()
{
}