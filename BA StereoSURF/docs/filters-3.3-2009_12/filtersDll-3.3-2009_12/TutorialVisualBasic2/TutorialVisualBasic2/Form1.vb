Imports Filters = FiltersDllDotNet.Filters
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices


Public Class Form1

    Private filterImageLoader As Int32
    Private filterImageSaver As Int32
    Private imageLoaded As IntPtr
    Private imageTmp As IntPtr
    Private imageToSave As IntPtr = IntPtr.Zero


    Public Sub New()
        InitializeComponent()
        Filters.initialize()
        Text = Text + " (Filters version = " + Filters.getVersion() + ")"
        filterImageLoader = Filters.createFilter("filterImageLoader")
        filterImageSaver = Filters.createFilter("filterImageSaver")
    End Sub


    Protected Overrides Sub Finalize()
        Filters.deleteFilter(filterImageLoader)
        Filters.deleteFilter(filterImageSaver)
        Filters.unInitialize()
    End Sub

    Private Sub cmdLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoad.Click
        Dim strImageToLoad As String = txtFileName.Text
        Filters.setParameterString(filterImageLoader, "filesName", strImageToLoad)
        Filters.run(filterImageLoader)
        Dim images As IntPtr = Filters.getOutputImages(filterImageLoader, "outImages")
        Dim imagesCount As Int32 = Filters.image_getImagesCount(images)
        txtImageCount.Text = imagesCount.ToString()
        If imagesCount = 1 Then
            imageLoaded = Filters.image_getImagesImageAtIndex(images, 0)
            imageTmp = Filters.image_createImageFromImage(imageLoaded)
            setImageToSave(imageTmp)
        End If
    End Sub

    Private Sub setImageToSave(ByVal image As IntPtr)
        If imageToSave <> 0 Then
            Filters.image_freeImage(imageToSave)
        End If
        imageToSave = Filters.image_createImageFromImage(image)
        showImage(imageToSave)
        Dim bitmapFilters As Filters.TFBitmap32 = Filters.helper_ptrToTFBitmap32(imageToSave)
        txtImageSize.Text = "width:" + Str$(bitmapFilters.width) + ", height:" + Str$(bitmapFilters.height)
    End Sub

    Private Sub showImage(ByVal image As IntPtr)
        Dim bitmapFilters As Filters.TFBitmap32 = Filters.helper_ptrToTFBitmap32(image)
        Dim returnMap As Bitmap = New Bitmap(bitmapFilters.width, bitmapFilters.height, PixelFormat.Format32bppArgb)
        Dim bitmapData2 As BitmapData = returnMap.LockBits(New Rectangle(0, 0, returnMap.Width, returnMap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb)
        Dim imageFilterPointer As IntPtr = bitmapFilters.bits
        Dim imagePointer2 As IntPtr = bitmapData2.Scan0
        Dim tempBits((bitmapFilters.width - 1) * 4) As Byte
        Dim i As Integer
        For i = 0 To bitmapFilters.height - 1
            Marshal.Copy(imageFilterPointer, tempBits, 0, tempBits.Length)
            Marshal.Copy(tempBits, 0, imagePointer2, tempBits.Length)
            imagePointer2 = imagePointer2.ToInt32 + bitmapFilters.width * 4
            imageFilterPointer = imageFilterPointer.ToInt32 + bitmapFilters.width * 4
        Next
        returnMap.UnlockBits(bitmapData2)
        pictureBox1.Image = returnMap
    End Sub


    Private Sub cmdBlobExplorer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBlobExplorer.Click
        txtInfo.Items.Clear()
        Dim filterBlobExplorer As Int32 = Filters.createFilter("filterBlobExplorer")
        Dim imageBlob As IntPtr = Filters.image_createImageLike(imageLoaded)
        Filters.setParameterImage(filterBlobExplorer, "inImage", imageLoaded)
        Filters.setParameterImage(filterBlobExplorer, "outImage", imageBlob)
        Filters.setParameterInteger(filterBlobExplorer, "intensityBackground", 100)
        Filters.setParameterInteger(filterBlobExplorer, "intensityPrecision", 10)
        Filters.setParameterString(filterBlobExplorer, "enableBlobArea", "TRUE")
        Filters.setParameterInteger(filterBlobExplorer, "blobAreaMin", 40)
        Filters.setParameterBoolean(filterBlobExplorer, "criticalPoints", True)
        Filters.setParameterInteger(filterBlobExplorer, "contourCriticalPointsAppoximationAccuracy", 10)
        Filters.setParameterBoolean(filterBlobExplorer, "blobSurfaceInfo", True)
        Filters.run(filterBlobExplorer)
        setImageToSave(imageBlob)
        Dim paPFBlobs As IntPtr = Filters.getOutputArrayPointers(filterBlobExplorer, "blobs")
        Dim length_blobs As Int32 = Filters.getOutputArrayPointersLength(filterBlobExplorer, "blobs")
        txtInfo.Items.Add("filterBlobExplorer length_blobs = [" + Str$(length_blobs) + "]")
        Dim b As Integer
        For b = 0 To length_blobs - 1

            txtInfo.Items.Add("  blob[" + Str$(b) + "] :")
            Dim ptrOnBlob As IntPtr = Filters.helper_getItemOfArrayPointers(paPFBlobs, b)
            Dim blob As Filters.TFBlob = Filters.helper_ptrToTFBlob(ptrOnBlob)
            txtInfo.Items.Add("    blob->index = [" + Str$(blob.index) + "]")
            txtInfo.Items.Add("    blob->length_segmentList = [" + Str$(blob.length_segmentList) + "]")
            txtInfo.Items.Add("    blob->length_approximatedSegmentList = [" + Str$(blob.length_approximatedSegmentList) + "]")
            Dim segments(blob.length_approximatedSegmentList) As Filters.TFSegment
            segments = Filters.helper_ptrToTFSegmentArray(blob.approximatedSegmentList, blob.length_approximatedSegmentList)
            Dim s As Integer
            For s = 0 To blob.length_approximatedSegmentList - 1
                Dim segment As Filters.TFSegment = segments(s)
                txtInfo.Items.Add("      (" + Str$(segment.p1.x) + "," + Str$(segment.p1.y) + ") -> (" + Str$(segment.p2.x) + "," + Str$(segment.p2.y) + ")")
            Next
            txtInfo.Items.Add("    blob->length_vectorChain = [" + Str$(blob.length_vectorChain) + "]")
            Dim vectors(blob.length_vectorChain) As Filters.TFVector
            vectors = Filters.helper_ptrToTFVectorArray(blob.vectorChain, blob.length_vectorChain)
            For s = 0 To blob.length_vectorChain - 1
                Dim vector As Filters.TFVector = vectors(s)
                txtInfo.Items.Add("      point=(" + Str$(vector.point.x) + "," + Str$(vector.point.y) + "), angle=(" + Str$(vector.angle) + "), length=(" + Str$(vector.length) + ")")
            Next
            txtInfo.Items.Add("    blob->perimeter = [" + Str$(blob.perimeter) + "]")
            txtInfo.Items.Add("    blob->gravityCenter = (" + Str$(blob.gravityCenter.x) + "," + Str$(blob.gravityCenter.y) + ")")
        Next
        Filters.deleteFilter(filterBlobExplorer)
    End Sub

    Private Sub tabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabControl1.SelectedIndexChanged
        Select tabControl1.SelectedIndex
            Case 0
                txtFileName.Text = "lenna_color.bmp"
            Case 1
                txtFileName.Text = "blob2.tif"
            Case 2
                txtFileName.Text = "lenna_color.bmp"
            Case 3
                txtFileName.Text = "blobRotation.tif"
            Case 4
                txtFileName.Text = "blob2.tif"
            Case 5
                txtFileName.Text = "blob2.tif"
            Case 6
                txtFileName.Text = "320_240.bmp"
            Case 7
                txtFileName.Text = "lenna_color.bmp"
            Case 8
                txtFileName.Text = "lenna_color.bmp"
        End Select
        cmdLoad.PerformClick()
    End Sub
End Class
