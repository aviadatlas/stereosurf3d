This is a tutorial in language C, with the free compiler Microsoft Visual Studio 2005 Express edition
You must first build TutorialC1.exe, and then execute it from the directory ..\Bin (if not, the image files will not be found..)


>>> To install AND configure Visual Studio 2005 express, all needed information is provided on :
http://msdn.microsoft.com/vstudio/express/visualc/download/


but I write here the main procedure :
-------------------------------------
(this configration is independent of Filters, it's only the standard configuration to use Visual Studio 2005 Express...)


-> after installing the IDE, don't forget to install the SDK (to build Win32 applications) :
(at this time the last version is "Windows® Server 2003 R2 Platform SDK Web Install")
http://msdn.microsoft.com/vstudio/express/visualc/usingpsdk/


then, Update the Visual C++ directories in the Projects and Solutions section in the Options dialog box:
Menu Tools \ Options \ Projetc and Solutions \ VC++ Directories :
Add the paths to the appropriate subsection, but it depend of your installation of this sdk, here a sample :
	Executable files: C:\Program Files\MMicrosoft Platform SDK for Windows Server 2003 R2\Bin 
	Include files: C:\Program Files\Microsoft Platform SDK for Windows Server 2003 R2\include 
	Library files: C:\Program Files\Microsoft Platform SDK for Windows Server 2003 R2\lib


then, edit the corewin_express.vsprops file, 
found in C:\Program Files\Microsoft Visual Studio 8\VC\VCProjectDefaults
and change the string that reads:
	AdditionalDependencies="kernel32.lib" 
to
	AdditionalDependencies="kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib"


then, the Win32 Windows Application type is disabled in the Win32 Application Wizard. 
To enable that type, you need to edit the file AppSettings.htm file located 
in the folder “C:\Program Files\Microsoft Visual Studio 8\VC\VCWizards\AppWiz\Generic\Application\html\1033\".
In a text editor comment out lines 441 - 444 by putting a // in front of them as shown here:
	// WIN_APP.disabled = true;
	// WIN_APP_LABEL.disabled = true; 
	// DLL_APP.disabled = true; 
	// DLL_APP_LABEL.disabled = true; 



that's all
...




