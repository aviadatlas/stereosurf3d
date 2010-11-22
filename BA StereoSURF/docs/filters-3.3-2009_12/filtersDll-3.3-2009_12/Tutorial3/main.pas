unit main;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ExtCtrls,
  wrapper_filtersdll, image, imageIOVideo;

type
  TformMain = class(TForm)
    Panel1: TPanel;
    cbFilters: TComboBox;
    Label1: TLabel;
    Label2: TLabel;
    txtParameters: TMemo;
    outputs: TLabel;
    txtOutputs: TMemo;
    lblFiltersTotal: TLabel;
    procedure cbFiltersChange(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure FormDestroy(Sender: TObject);
  private
    procedure infoFilter( filterName : String );
  public
  end;

var
  formMain: TformMain;

implementation

{$R *.dfm}

procedure TformMain.FormCreate(Sender: TObject);
var
  filtersList : ArrayOfStringC;
  i : Integer;
begin
  initialize();
  cbFilters.Clear;
  filtersList := getFiltersList();
  for i:=0 to Length(filtersList)-1 do begin
    cbFilters.Items.Add( StrPas( filtersList[i] ) );
  end;
  lblFiltersTotal.Caption := '(total=' + IntToStr(Length(filtersList)) + ')';
end;

procedure TformMain.FormDestroy(Sender: TObject);
begin
  unInitialize();
end;

procedure TformMain.cbFiltersChange(Sender: TObject);
begin
  infoFilter( cbFilters.Items[ cbFilters.ItemIndex ] );
end;

procedure TformMain.infoFilter( filterName : String );
var
  cstr, cstr2 :  array[0..255] of char;
  parametersCount, p : Integer;
  outputsCount, o : Integer;
  currentFilter : Integer;
begin
  txtParameters.Lines.Clear;
  txtOutputs.Lines.Clear;
  // create new
  StrPCopy( cstr, filterName );
  currentFilter := createFilter( cstr );
  // show
  if currentFilter<>0 then begin
    // show parameters
    parametersCount := getParametersCount( currentFilter );
    for p:=0 to parametersCount-1 do begin
      getParameterName( currentFilter, p, cstr );
      getParameterHelp( currentFilter, p, cstr2 );
      txtParameters.Lines.Add( '[' + StrPas( cstr ) + '] > ' +StrPas( cstr2 ) );
    end;
    // show outputs
    outputsCount := getOutputsCount( currentFilter );
    for o:=0 to outputsCount-1 do begin
      getOutputName( currentFilter, o, cstr );
      txtOutputs.Lines.Add( '[' + StrPas( cstr ) + ']' );
    end;
    // run (use default parameters values)
    //run( currentFilter );
    // delete
    deleteFilter( currentFilter );
  end;
end;

end.
