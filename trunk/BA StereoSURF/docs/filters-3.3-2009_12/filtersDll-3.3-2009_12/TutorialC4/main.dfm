object Form1: TForm1
  Left = 0
  Top = 0
  Caption = 'Filters TutorialC4'
  ClientHeight = 506
  ClientWidth = 489
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  OnCreate = FormCreate
  OnDestroy = FormDestroy
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 8
    Top = 8
    Width = 101
    Height = 13
    Caption = 'Filters DLL Version = '
  end
  object txtFiltersVersion: TEdit
    Left = 115
    Top = 5
    Width = 121
    Height = 21
    Enabled = False
    TabOrder = 0
  end
  object txtImageToLoad: TEdit
    Left = 8
    Top = 32
    Width = 228
    Height = 21
    TabOrder = 1
    Text = '.\lenna_color.bmp'
  end
  object cmdLoad: TButton
    Left = 242
    Top = 30
    Width = 75
    Height = 25
    Caption = 'load'
    TabOrder = 2
    OnClick = cmdLoadClick
  end
  object cmdSave: TButton
    Left = 242
    Top = 59
    Width = 75
    Height = 25
    Caption = 'save'
    TabOrder = 3
    OnClick = cmdSaveClick
  end
  object txtImageToSave: TEdit
    Left = 8
    Top = 59
    Width = 228
    Height = 21
    TabOrder = 4
    Text = '.\lenna_color_output.jpeg'
  end
  object cmdSobel: TButton
    Left = 336
    Top = 44
    Width = 75
    Height = 25
    Caption = 'sobel'
    TabOrder = 5
    OnClick = cmdSobelClick
  end
  object Panel1: TPanel
    Left = 0
    Top = 90
    Width = 489
    Height = 416
    Align = alBottom
    Anchors = [akLeft, akTop, akRight, akBottom]
    TabOrder = 6
    object ViewerImage: TImage
      Left = 1
      Top = 1
      Width = 487
      Height = 414
      Align = alClient
      Stretch = True
      ExplicitLeft = 120
      ExplicitTop = 48
      ExplicitWidth = 105
      ExplicitHeight = 105
    end
  end
end
