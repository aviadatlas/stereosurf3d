object formMain: TformMain
  Left = 416
  Top = 221
  Width = 612
  Height = 500
  Caption = 'Filters - Tutorial 3'
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
  object Panel1: TPanel
    Left = 0
    Top = 0
    Width = 604
    Height = 473
    Align = alClient
    TabOrder = 0
    object Label1: TLabel
      Left = 8
      Top = 8
      Width = 24
      Height = 13
      Caption = 'filters'
    end
    object Label2: TLabel
      Left = 8
      Top = 51
      Width = 52
      Height = 13
      Caption = 'parameters'
    end
    object outputs: TLabel
      Left = 8
      Top = 288
      Width = 35
      Height = 13
      Caption = 'outputs'
    end
    object lblFiltersTotal: TLabel
      Left = 40
      Top = 8
      Width = 20
      Height = 13
      Caption = 'total'
    end
    object cbFilters: TComboBox
      Left = 8
      Top = 24
      Width = 265
      Height = 21
      Style = csDropDownList
      ItemHeight = 13
      TabOrder = 0
      OnChange = cbFiltersChange
    end
    object txtParameters: TMemo
      Left = 8
      Top = 67
      Width = 585
      Height = 214
      ScrollBars = ssVertical
      TabOrder = 1
    end
    object txtOutputs: TMemo
      Left = 8
      Top = 304
      Width = 585
      Height = 161
      TabOrder = 2
    end
  end
end
