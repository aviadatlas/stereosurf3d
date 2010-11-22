# make FiltersPlugin_Noise, a Filters tutorial C plugin, based on Eclipse C++ CDT and MINGW compiler

SRCDIR=C:/DEV/FiltersTutorial/FiltersPlugin_Noise/SrcC2/src

#change this destination directory if needed
BINDIR=C:/DEV/FiltersTutorial/Bin

FiltersPlugin_Noise.dll : main.o wrapper_filtersDll.o dllInterface.o
	g++ \
		-shared -Wl,--enable-stdcall-fixup \
		-DDECLAREDLLFCT_C \
		-o $(BINDIR)/FiltersPlugin_Noise.dll \
		main.o \
		wrapper_filtersDll.o \
		dllInterface.o \
		$(SRCDIR)/dllInterface.DEF

dllInterface.o : $(SRCDIR)/dllInterface.cpp
	g++ -c $(SRCDIR)/dllInterface.cpp

wrapper_filtersDll.o : $(SRCDIR)/wrapper_filtersDll.c
	gcc -c $(SRCDIR)/wrapper_filtersDll.c

main.o : $(SRCDIR)/main.cpp
	g++ -c $(SRCDIR)/main.cpp
    	
all : FiltersPlugin_Noise.dll
clean :
	-rm FiltersPlugin_Noise.dll main.o
