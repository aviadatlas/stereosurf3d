object Form1: TForm1
  Left = 726
  Top = 306
  Width = 512
  Height = 550
  Caption = 'Filters - TestPlugin'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  OnCreate = FormCreate
  OnDestroy = FormDestroy
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 11
    Top = 12
    Width = 100
    Height = 13
    Caption = 'Filters DLL Version = '
  end
  object Label2: TLabel
    Left = 10
    Top = 61
    Width = 25
    Height = 13
    Caption = 'scale'
  end
  object txtVersion: TEdit
    Left = 120
    Top = 8
    Width = 121
    Height = 21
    Color = clScrollBar
    Enabled = False
    TabOrder = 0
  end
  object cmdLoad: TButton
    Left = 250
    Top = 32
    Width = 38
    Height = 21
    Caption = 'load'
    TabOrder = 1
    OnClick = cmdLoadClick
  end
  object Panel1: TPanel
    Left = 0
    Top = 83
    Width = 504
    Height = 440
    Align = alBottom
    Anchors = [akLeft, akTop, akRight, akBottom]
    TabOrder = 2
    object ViewerImage: TImage
      Left = 1
      Top = 1
      Width = 502
      Height = 438
      Align = alClient
      Stretch = True
    end
  end
  object txtImageToLoad: TEdit
    Left = 8
    Top = 32
    Width = 233
    Height = 21
    TabOrder = 3
    Text = '.\lenna_color.bmp'
  end
  object cmdRun: TButton
    Left = 250
    Top = 59
    Width = 73
    Height = 21
    Caption = 'run plugin'
    TabOrder = 4
    OnClick = cmdRunClick
  end
  object seScale: TSpinEdit
    Left = 48
    Top = 56
    Width = 57
    Height = 22
    Increment = 10
    MaxValue = 999999999
    MinValue = 2
    TabOrder = 5
    Value = 100
    OnChange = seScaleChange
  end
end
