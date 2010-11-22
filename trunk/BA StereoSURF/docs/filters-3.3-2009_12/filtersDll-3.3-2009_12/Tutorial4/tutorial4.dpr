program tutorial4;

uses
  Forms,
  main in 'main.pas' {FormMain},
  wrapper_filtersdll in '..\wrapperPascal\wrapper_filtersdll.pas',
  image in '..\wrapperPascal\image.pas',
  imageIOVideo in '..\wrapperPascal\imageIOVideo.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TFormMain, FormMain);
  Application.Run;
end.
