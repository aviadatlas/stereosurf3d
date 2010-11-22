# make TutorialC2, a Filters tutorial C executable, based on Eclipse C++ CDT

#change this destination directory if needed
BINDIR=C:/DEV/FiltersTutorial/Bin

tutorialC2.exe : main.o wrapper_filtersDll.o
	g++ -o $(BINDIR)/tutorialC2.exe main.o wrapper_filtersDll.o

wrapper_filtersDll.o : wrapper_filtersDll.c
	gcc -c wrapper_filtersDll.c

main.o : main.cpp
	g++ -c main.cpp
    	
all : tutorialC2.exe
clean :
	-rm tutorialC2.exe main.o
