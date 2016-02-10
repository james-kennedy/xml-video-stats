Option Strict On
Option Explicit On

Imports System.IO
Imports System.Xml

Public Class frmMain

    Dim mArrTracks(-1) As String            'stores track names across all files
    Dim mdtTrackTimes As DataTable          'used to calculate correlation table
    Dim marrCorrels(,) As Integer           'correlation matrix

    Private Sub frmMain_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Try
            Me.Top = 0
            Me.Left = 0

            cmdCorrelations.Enabled = False     'dont enable this until we calculate them

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' File browser handler
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmdBrowse_Click(sender As System.Object, e As System.EventArgs) Handles cmdBrowse.Click
        Dim MyFolderBrowser As New System.Windows.Forms.FolderBrowserDialog
        Dim dlgResult As DialogResult

        Try
            cmdCorrelations.Enabled = False                                     'disable - new set loading
            MyFolderBrowser.Description = "Select Annotations Folder"           'name the browser
            MyFolderBrowser.RootFolder = Environment.SpecialFolder.MyComputer   'always start in my computer

            dlgResult = MyFolderBrowser.ShowDialog()                            'show dialog and store returned value

            If dlgResult = Windows.Forms.DialogResult.OK Then                   'if result is ok then update textbox
                txtAnnotationsPath.Text = MyFolderBrowser.SelectedPath
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

#Region "Simple Population Stats"
    Private Sub cmdStart_Click(sender As System.Object, e As System.EventArgs) Handles cmdStart.Click
        Dim sDirectory As String                'directory entered by user
        Dim bExists As Boolean                  'whether directory exists
        Dim dirAnnotations As DirectoryInfo     'directory info for annotations
        Dim fiFileListing As FileInfo()         'file listing of directory
        Dim iTrackCount = 0                     'no. of tracks - for loop
        Dim dblTotalTime As Double = 0          'total elapsed time for track
        Dim iTotalCount As Integer = 0          'total count of instances for track
        Dim dblAverage As Double = 0            'average instance length for track
        Dim dblTimes(-1) As Double              'track time array
        Dim bCalcMin As Boolean = False         'flag to calculate per minute values
        Dim dblTimeMin As Double = 0            'time per minute
        Dim dblCountMin As Double = 0           'count per minute
        Dim iVidLength As Integer = 0           'total video length
        Dim iNoCoders As Integer = 0            'number of coders
        Dim pclsFuncs As New clsGeneralFuncs

        Try
            sDirectory = txtAnnotationsPath.Text
            bExists = IO.Directory.Exists(sDirectory)                               'check the directory entered exists

            Cursor.Current = Cursors.WaitCursor
            lstvResults.Items.Clear()

            If bExists = False Then
                Cursor.Current = Cursors.Default
                MessageBox.Show("Directory doesn't exist; please try again.")
            Else
                iVidLength = CInt(nudVideoLength.Value)                             'read in video length from form
                iNoCoders = CInt(nudCoders.Value)                                   'read in no. of coders from form
                If iNoCoders > 0 And iVidLength > 0 Then                            'if we have both then change per min flag
                    bCalcMin = True
                End If

                dirAnnotations = New DirectoryInfo(sDirectory)                      'get handle on the folder
                fiFileListing = dirAnnotations.GetFiles()                           'get a list of the files

                mArrTracks = pclsFuncs.GetTrackNames(fiFileListing)                 'populates the modular array with the track names
                BuildDataTable()                                                    'initialise datatable

                For iTrackCount = 0 To mArrTracks.Length - 1                        'go through the files looking for a track at a time
                    dblTimes = GetTrackTimes(fiFileListing, mArrTracks(iTrackCount)) 'loop through the files, searching on a track at a time

                    If Not dblTimes Is Nothing Then
                        For iTotalCount = 0 To dblTimes.Length - 1                  'calc total elapsed by adding all array indexes
                            dblTotalTime += dblTimes(iTotalCount)
                        Next

                        iTotalCount = dblTimes.Length                               'provide total count by taking count of index
                        dblAverage = dblTotalTime / iTotalCount                     'provide average by dividing time by instances

                        If bCalcMin = True Then                                     'if we have # coders and length of vid then calc /min
                            dblTimeMin = (dblTotalTime / iNoCoders) / (iVidLength / 60) 'time on track per minute of video
                            dblCountMin = (iTotalCount / iNoCoders) / (iVidLength / 60) 'count on track per minute of video

                            PopulateListview(iTrackCount, dblTotalTime, iTotalCount, dblAverage, dblTimeMin, dblCountMin)    'populate this info in the listview
                        Else
                            PopulateListview(iTrackCount, dblTotalTime, iTotalCount, dblAverage)    'populate this info in the listview
                        End If

                    Else
                        Cursor.Current = Cursors.Default
                        MessageBox.Show("An error occurred and the times couldn't be calculated.")
                    End If

                    dblTotalTime = 0                                                'reset time counter for next loop
                Next

                If CalculateCorrelations(mdtTrackTimes) Then         'now calculate correlations
                    cmdCorrelations.Enabled = True
                Else
                    cmdCorrelations.Enabled = False
                    Cursor.Current = Cursors.Default
                    MessageBox.Show("Couldn't create correlations table.")
                End If

                Cursor.Current = Cursors.Default
                MessageBox.Show("Annotations read successfully.")
            End If

        Catch ex As Exception
            Cursor.Current = Cursors.Default
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Constructs datatable to store start and end times of tracks. Used to calculate correlations.
    ''' </summary>
    Public Sub BuildDataTable()

        Try
            mdtTrackTimes = Nothing                                 'destroy old one
            mdtTrackTimes = New DataTable                           'make new one

            mdtTrackTimes.Columns.Add("fileno", GetType(Integer))   'file
            mdtTrackTimes.Columns.Add("track", GetType(String))     'track name
            mdtTrackTimes.Columns.Add("start", GetType(Double))     'start time
            mdtTrackTimes.Columns.Add("end", GetType(Double))       'end time

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Go through each file, on a track by track basis and calculate times for annotations
    ''' </summary>
    ''' <param name="fiFileListing">List of files in annotation directory</param>
    ''' <param name="sConcatName">Track to search on</param>
    Public Function GetTrackTimes(ByVal fiFileListing As FileInfo(), ByVal sConcatName As String) As Double()
        Dim fiIndividualFile As FileInfo
        Dim xmlFile As XmlDocument
        Dim xmlListNodes As XmlNodeList
        Dim xmlSingleNode As XmlNode
        Dim xmlChildNode As XmlNode
        Dim iChildCount As Integer = 0
        Dim dblStart As Double = 0
        Dim dblEnd As Double = 0
        Dim iTimeCount As Integer = 0
        Dim dblTrackTimes(-1) As Double
        Dim sTrack As String() = sConcatName.Split(New [Char]() {"~"c})     'sTrack(0) will be track name, (1) will be annotation
        Dim iFileCount As Integer = 0

        GetTrackTimes = Nothing

        Try
            For Each fiIndividualFile In fiFileListing                                                      'loop through the files
                If fiIndividualFile.Extension = ".anvil" Then                                               'only look at the ANVIL files
                    xmlFile = New XmlDocument                                                               'create XML document for this file
                    xmlFile.Load(fiIndividualFile.FullName)                                                 'load the current XML file

                    xmlListNodes = xmlFile.SelectNodes("annotation/body/track")                             'get a list of the nodes

                    For Each xmlSingleNode In xmlListNodes                                                  'loop through the nodes
                        If xmlSingleNode.Attributes.GetNamedItem("name").Value = sTrack(0) Then             'we only want to look at our current track

                            If xmlSingleNode.HasChildNodes = True Then                                          'make sure we have children
                                For Each xmlChildNode In xmlSingleNode.ChildNodes                               'go through the children one at a time
                                    If xmlChildNode.HasChildNodes = True Then                                   'we should have children (but there may be errors in the XML)
                                        If xmlChildNode.ChildNodes.Item(0).InnerText = sTrack(1) Then           'we only want our current annotation
                                            dblStart = CDbl(xmlChildNode.Attributes.GetNamedItem("start").Value) 'get the start value
                                            dblEnd = CDbl(xmlChildNode.Attributes.GetNamedItem("end").Value)    'get the end value

                                            ReDim Preserve dblTrackTimes(iTimeCount)                            'resize array
                                            dblTrackTimes(iTimeCount) = dblEnd - dblStart                       'put time difference into latest slot
                                            iTimeCount += 1                                                     'increment array counter

                                            mdtTrackTimes.Rows.Add(iFileCount, sConcatName, dblStart, dblEnd)   'add to our datatable for correlations
                                        End If
                                    End If
                                Next
                            End If

                        End If
                    Next
                End If

                iFileCount += 1     'increment file counter
            Next

            GetTrackTimes = dblTrackTimes

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Function

    ''' <summary>
    ''' Add a row to the listview with data from a given track
    ''' </summary>
    ''' <param name="iTrackCount">Current track number</param>
    ''' <param name="dblTotalTime">Total time for this track</param>
    ''' <param name="iTotalCount">No. of instances for this track</param>
    ''' <param name="dblAverage">Average time for this track</param>
    Private Sub PopulateListview(ByVal iTrackCount As Integer, ByVal dblTotalTime As Double, ByVal iTotalCount As Integer, ByVal dblAverage As Double, _
                                      Optional ByVal dblTimeMin As Double = 0, Optional ByVal dblCountMin As Double = 0)

        Try
            lstvResults.Items.Add(mArrTracks(iTrackCount))
            With lstvResults.Items(iTrackCount).SubItems
                .Add(CStr(dblTotalTime))
                .Add(CStr(iTotalCount))
                .Add(CStr(dblAverage))
                .Add(CStr(dblTimeMin))
                .Add(CStr(dblCountMin))
            End With

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' We're going to calculate the number of times things correlate based on start and end times
    ''' </summary>
    ''' <param name="dtTimes">DataTable with track names, start and end times</param>
    ''' <returns>True if successful, false if not</returns>
    Private Function CalculateCorrelations(ByVal dtTimes As DataTable) As Boolean
        Dim iRowCount As Integer = 0
        Dim iCompareCount As Integer = 0
        Dim iTrackOne As Integer = 0
        Dim iTrackTwo As Integer = 0
        Dim dblStartOne As Double
        Dim dblStartTwo As Double
        Dim dblEndOne As Double
        Dim dblEndTwo As Double

        CalculateCorrelations = False

        Try
            ReDim marrCorrels(mArrTracks.Length, mArrTracks.Length)     'set up the correlations array

            For iRowCount = 0 To dtTimes.Rows.Count - 1                 'loop through the rows in the datatable
                For iCompareCount = iRowCount + 1 To dtTimes.Rows.Count - 1 'we only want to compare with things after this row or we'll get duplication
                    If CInt(dtTimes.Rows(iRowCount).Item("fileno")) = CInt(dtTimes.Rows(iCompareCount).Item("fileno")) Then 'check the file numbers match

                        dblStartOne = CDbl(dtTimes.Rows(iRowCount).Item("start"))       'gather the starts and ends here
                        dblEndOne = CDbl(dtTimes.Rows(iRowCount).Item("end"))

                        dblStartTwo = CDbl(dtTimes.Rows(iCompareCount).Item("start"))
                        dblEndTwo = CDbl(dtTimes.Rows(iCompareCount).Item("end"))


                        If (dblStartOne < dblEndTwo) And (dblStartTwo < dblEndOne) Then                   'compare times for overlap
                            iTrackOne = TrackNameToNumber(CStr(dtTimes.Rows(iRowCount).Item("track")))      'get the track number for row 1
                            iTrackTwo = TrackNameToNumber(CStr(dtTimes.Rows(iCompareCount).Item("track")))  'get the track number for row 2

                            marrCorrels(iTrackTwo, iTrackOne) += 1      'add one to the correlation table for this pair
                        End If
                    End If
                Next
            Next

            CalculateCorrelations = True

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Function

    ''' <summary>
    ''' Takes the name of a track and finds it's number from the array of track listings
    ''' </summary>
    ''' <param name="sTrackName">Name of the track to search for</param>
    ''' <returns>Integer of track name</returns>
    Private Function TrackNameToNumber(ByVal sTrackName As String) As Integer
        Dim iCount As Integer = 0

        TrackNameToNumber = 0

        Try
            For iCount = 0 To mArrTracks.Length - 1
                If mArrTracks(iCount) = sTrackName Then
                    TrackNameToNumber = iCount
                    Exit Function
                End If
            Next

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Function

    Private Sub cmdCorrelations_Click(sender As System.Object, e As System.EventArgs) Handles cmdCorrelations.Click
        frmCorrelations.Tracks = mArrTracks
        frmCorrelations.Correlations = marrCorrels
        frmCorrelations.Show()
    End Sub

    ''' <summary>
    ''' Copy the big listview on frmMain into the clipboard
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmdCopyClip_Click(sender As System.Object, e As System.EventArgs) Handles cmdCopyClip.Click
        Dim build As New System.Text.StringBuilder
        Dim row As New List(Of String)

        Try
            'go through, build a string of the key
            For Each item As ListViewItem In lstvResults.Items
                For Each subitem As ListViewItem.ListViewSubItem In item.SubItems
                    row.Add(subitem.Text)
                Next

                build.AppendLine(String.Join(vbTab, row.ToArray))
                row.Clear()
            Next

            Clipboard.SetText(build.ToString)   'copy key to clipboard

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region

    ''' <summary>
    ''' Launches the batch stats form to process one coder's full population stats
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmdBatchStats_Click(sender As System.Object, e As System.EventArgs) Handles cmdBatchStats.Click
        Dim sDirectory As String = txtAnnotationsPath.Text

        Try
            If IO.Directory.Exists(sDirectory) Then
                frmBatchStats.msDirectory = sDirectory
                frmBatchStats.Show()
            Else
                MessageBox.Show("Directory doesn't exist")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Open the correlations form to set options for processing one coder's full population correlations
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmdBatchCorrels_Click(sender As System.Object, e As System.EventArgs) Handles cmdBatchCorrels.Click
        Dim sDirectory As String = txtAnnotationsPath.Text

        Try
            If IO.Directory.Exists(sDirectory) Then
                frmCorrelOptions.msDirectory = sDirectory
                frmCorrelOptions.Show()
            Else
                MessageBox.Show("Directory doesn't exist")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Open the coder agreement form - gives percentage and kappa for two coders.
    ''' Matching pairs of files must be stored in folders within the root directory provided in the textbox for 'annotations folder'
    ''' They should be the tab delimited format from ANVIL - File -> Export -> Annotation Frame-by-Frame (default options)
    ''' DO NOT INCLUDE THE LABELS FILES (or any other files - just the 2 tab delimited ones)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmdCoder_Click(sender As System.Object, e As System.EventArgs) Handles cmdCoder.Click
        Dim sDirectory As String = txtAnnotationsPath.Text

        Try
            If IO.Directory.Exists(sDirectory) Then
                frmCoderAgree.msDirectory = sDirectory
                frmCoderAgree.Show()
            Else
                MessageBox.Show("Directory doesn't exist")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub cmdClose_Click(sender As System.Object, e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

End Class
