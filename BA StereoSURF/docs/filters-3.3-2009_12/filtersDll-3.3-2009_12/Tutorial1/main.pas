unit main;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls,wrapper_filtersdll;

type
  TForm1 = class(TForm)
    txtVersion: TEdit;
    Label1: TLabel;
    procedure FormCreate(Sender: TObject);
    procedure FormDestroy(Sender: TObject);
  private
  public
  end;

var
  Form1: TForm1;

implementation

{$R *.dfm}


procedure TForm1.FormCreate(Sender: TObject);
begin
  initialize();
  txtVersion.Text := StrPas( getVersion() );
end;

procedure TForm1.FormDestroy(Sender: TObject);
begin
  unInitialize();
end;

end.
