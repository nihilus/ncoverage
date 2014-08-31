ncoverage
=========

http://newgre.net/ncoverage

N-Coverage IDA Plugin and stand-alone Application
N-Coverage consists of two parts: first of all there is the main stand-alone application which uses a custom debugging engine to place breakpoints into a given process in order to record execution of functions during runtime. Function addresses are obtained from the IDA plugin which uses the IDA disassembler to spot function boundaries in the loaded binary.
While N-Coverage is running, all function hits are recorded and can be merged or diffed with other recordings, finally yielding a set of hits which can then be exported to a file. This final set is then again imported into IDA and the plugin shows a list of functions and/or assigns a user specified color to all functions contained in the set.



Notes
The tool offers pretty basic functionality, it's not production level code and lacks some features, but it works quite okay. Actually it's only listed here for the sake of completeness because it was used during my DRM analysis. If you're looking for an advanced code coverage solution you might wan't to check out paimei written by Pedram Amini.
