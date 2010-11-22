object Form1: TForm1
  Left = 726
  Top = 306
  Width = 512
  Height = 550
  Caption = 'Filters - Tutorial 2'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 11
    Top = 12
    Width = 100
    Height = 13
    Caption = 'Filters DLL Version = '
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
  object Panel1: TPanel
    Left = 0
    Top = 83
    Width = 504
    Height = 440
    Align = alBottom
    Anchors = [akLeft, akTop, akRight, akBottom]
    TabOrder = 1
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
    TabOrder = 2
    Text = '.\lenna_color.bmp'
  end
  object cmdSobel: TButton
    Left = 288
    Top = 43
    Width = 38
    Height = 21
    Caption = 'test'
    TabOrder = 3
    OnClick = cmdTestClick
  end
  object txtImageToSave: TEdit
    Left = 8
    Top = 56
    Width = 233
    Height = 21
    TabOrder = 4
    Text = '.\lenna_color_output.jpeg'
  end
  object txtInfo: TEdit
    Left = 328
    Top = 42
    Width = 121
    Height = 21
    Color = clScrollBar
    Enabled = False
    TabOrder = 5
  end
end
