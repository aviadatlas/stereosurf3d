program testplugin;

uses
  Forms,
  main in 'main.pas' {Form1},
  wrapper_filtersdll in '..\wrapperPascal\wrapper_filtersdll.pas',
  image in '..\wrapperPascal\image.pas',
  imageIOVideo in '..\wrapperPascal\imageIOVideo.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TForm1, Form1);
  Application.Run;
end.
