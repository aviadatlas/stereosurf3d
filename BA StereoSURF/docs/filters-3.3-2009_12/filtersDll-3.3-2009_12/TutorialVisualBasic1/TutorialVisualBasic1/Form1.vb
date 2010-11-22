Imports Filters = FiltersDllDotNet.Filters

Public Class Form1

    Public Sub New()
        InitializeComponent()
        Filters.initialize()
        txtVersion.Text = Filters.getVersion()
        Dim fMax As Int32 = Filters.getFiltersCount()
        For f As Integer = 0 To fMax - 1
            Dim filterName As String = Filters.getFiltersNameAtIndex(f)
            cbFiltersName.Items.Add(filterName)
        Next f

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        Filters.unInitialize()
    End Sub

    Private Sub cbFiltersName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFiltersName.SelectedIndexChanged
        Dim filterName As String = cbFiltersName.SelectedItem
        ' create a instance of the selected Filter
        Dim filter As Int32 = Filters.createFilter(filterName)
        ' for each parameters of this Filters
        lstParameters.Items.Clear()
        Dim pMax As Int32 = Filters.getParametersCount(filter)
        For p As Integer = 0 To pMax - 1
            ' we get the name and help of parameter
            Dim parameterName As New System.Text.StringBuilder(1024)
            Dim parameterHelp As New System.Text.StringBuilder(1024)
            Filters.getParameterName(filter, p, parameterName)
            Filters.getParameterHelp(filter, p, parameterHelp)
            Dim tmpStr As String = parameterName.ToString + " : " + parameterHelp.ToString
            lstParameters.Items.Add(tmpStr)
        Next p
        ' for each outputs of this Filters
        lstOutputs.Items.Clear()
        Dim oMax As Int32 = Filters.getOutputsCount(filter)
        For o As Integer = 0 To oMax - 1
            ' we get the name of output
            Dim outputName As New System.Text.StringBuilder(1024)
            Filters.getOutputName(filter, o, outputName)
            Dim tmpStr As String = outputName.ToString()
            lstOutputs.Items.Add(tmpStr)
        Next o
        ' we delete this instance of the selected Filter
        Filters.deleteFilter(filter)
    End Sub

End Class
