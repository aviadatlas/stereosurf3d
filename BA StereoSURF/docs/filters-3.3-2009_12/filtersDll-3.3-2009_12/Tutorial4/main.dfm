object FormMain: TFormMain
  Left = 283
  Top = 206
  Width = 800
  Height = 648
  Caption = 'Filters - Tutorial 4'
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
  object Panel3: TPanel
    Left = 0
    Top = 0
    Width = 792
    Height = 90
    Align = alClient
    TabOrder = 6
    object Label1: TLabel
      Left = 11
      Top = 12
      Width = 100
      Height = 13
      Caption = 'Filters DLL Version = '
    end
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
    Left = 243
    Top = 32
    Width = 38
    Height = 21
    Caption = 'load'
    TabOrder = 1
    OnClick = cmdLoadClick
  end
  object Panel1: TPanel
    Left = 0
    Top = 90
    Width = 792
    Height = 422
    Align = alBottom
    TabOrder = 2
    object ViewerImage: TImage
      Left = 1
      Top = 1
      Width = 790
      Height = 420
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
  object PageControl: TPageControl
    Left = 288
    Top = 10
    Width = 497
    Height = 73
    ActivePage = TabSheet8
    TabOrder = 4
    OnChange = PageControlChange
    object TabSheet1: TTabSheet
      Caption = 'Sobel'
      object cmdSobel: TButton
        Left = 11
        Top = 8
        Width = 38
        Height = 21
        Caption = 'sobel'
        TabOrder = 0
        OnClick = cmdSobelClick
      end
    end
    object TabSheet2: TTabSheet
      Caption = 'Canny'
      ImageIndex = 1
      object Label2: TLabel
        Left = 71
        Top = 12
        Width = 56
        Height = 13
        Caption = 'Threshold 1'
      end
      object Label3: TLabel
        Left = 198
        Top = 12
        Width = 56
        Height = 13
        Caption = 'Threshold 2'
      end
      object cmdCanny: TButton
        Left = 11
        Top = 8
        Width = 38
        Height = 21
        Caption = 'canny'
        TabOrder = 0
        OnClick = cmdCannyClick
      end
      object Canny_threshold1: TSpinEdit
        Left = 128
        Top = 7
        Width = 57
        Height = 22
        MaxValue = 255
        MinValue = 0
        TabOrder = 1
        Value = 0
        OnChange = Canny_threshold1Change
      end
      object Canny_threshold2: TSpinEdit
        Left = 256
        Top = 7
        Width = 57
        Height = 22
        MaxValue = 255
        MinValue = 0
        TabOrder = 2
        Value = 255
        OnChange = Canny_threshold2Change
      end
    end
    object TabSheet3: TTabSheet
      Caption = 'BlobExplorer'
      ImageIndex = 2
      object Label4: TLabel
        Left = 111
        Top = 12
        Width = 99
        Height = 13
        Caption = 'Blobs Pixel Area Max'
      end
      object btnBobExplorer: TButton
        Left = 11
        Top = 8
        Width = 75
        Height = 21
        Caption = 'BlobExplorer'
        TabOrder = 0
        OnClick = btnBobExplorerClick
      end
      object txtPixelAreaMax: TEdit
        Left = 216
        Top = 8
        Width = 73
        Height = 21
        Color = clScrollBar
        Enabled = False
        TabOrder = 1
      end
    end
    object TabSheet5: TTabSheet
      Caption = 'BlobExplorer (2)'
      ImageIndex = 4
      object btnBlobExplorerVector: TButton
        Left = 8
        Top = 8
        Width = 89
        Height = 25
        Caption = 'extract vectors'
        TabOrder = 0
        OnClick = btnBlobExplorerVectorClick
      end
      object btnBlobExplorerSurfcePerimeter: TButton
        Left = 112
        Top = 8
        Width = 137
        Height = 25
        Caption = 'extract surface, perimeter'
        TabOrder = 1
        OnClick = btnBlobExplorerSurfcePerimeterClick
      end
    end
    object TabSheet4: TTabSheet
      Caption = 'microbubbles'
      ImageIndex = 3
      object cmdTestMicrobubbles: TButton
        Left = 8
        Top = 8
        Width = 75
        Height = 25
        Caption = 'test'
        TabOrder = 0
        OnClick = cmdTestMicrobubblesClick
      end
    end
    object TabSheet6: TTabSheet
      Caption = 'Substract'
      ImageIndex = 5
      object btnSubstract: TButton
        Left = 8
        Top = 8
        Width = 75
        Height = 25
        Caption = 'Substract'
        TabOrder = 0
        OnClick = btnSubstractClick
      end
    end
    object TabSheet7: TTabSheet
      Caption = 'Texture'
      ImageIndex = 6
      object btnTextureCompute: TButton
        Left = 11
        Top = 8
        Width = 134
        Height = 21
        Caption = 'compute texture properties'
        TabOrder = 0
        OnClick = btnTextureComputeClick
      end
    end
    object imageFunctions: TTabSheet
      Caption = 'imageFunctions'
      ImageIndex = 7
      object btnImageFunctionsTest: TButton
        Left = 11
        Top = 8
        Width = 75
        Height = 21
        Caption = 'test'
        TabOrder = 0
        OnClick = btnImageFunctionsTestClick
      end
    end
    object TabSheet8: TTabSheet
      Caption = 'from Delphi TImage'
      ImageIndex = 8
      object testDelphiTImage: TImage
        Left = 96
        Top = 0
        Width = 57
        Height = 45
        Stretch = True
      end
      object cmdTestFromTImage: TButton
        Left = 8
        Top = 8
        Width = 75
        Height = 25
        Caption = 'test'
        TabOrder = 0
        OnClick = cmdTestFromTImageClick
      end
    end
  end
  object Panel2: TPanel
    Left = 0
    Top = 512
    Width = 792
    Height = 109
    Align = alBottom
    TabOrder = 5
    object txtOutput: TMemo
      Left = 1
      Top = 1
      Width = 790
      Height = 107
      Align = alClient
      BorderStyle = bsNone
      Ctl3D = False
      ParentCtl3D = False
      TabOrder = 0
    end
  end
end
