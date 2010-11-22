program tutorial3;

uses
  Forms,
  main in 'main.pas' {formMain},
  wrapper_filtersdll in '..\wrapperPascal\wrapper_filtersdll.pas',
  imageIOVideo in '..\wrapperPascal\imageIOVideo.pas',
  image in '..\wrapperPascal\image.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TformMain, formMain);
  Application.Run;
end.
