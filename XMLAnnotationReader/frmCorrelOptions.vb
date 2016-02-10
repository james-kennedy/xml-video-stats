Option Strict On
Option Explicit On

Imports System.IO
Imports System.Xml

Public Class frmCorrelOptions

    Public Property msDirectory As String

    Private mdsCorrelations As New DataSet
    Private mdsInstances As New DataSet
    Private mArrTracks(-1) As String            'stores track names across all files

#Region "Form Handling"
    ''' <summary>
    ''' Prepare the form for use
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub frmCorrelOptions_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Try
            PopulateCombos()

            Me.Top = 0
            Me.Left = 0

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Populates the combo boxes with the track names on load - reads in directly from the XML
    ''' </summary>
    Private Sub PopulateCombos()
        Dim pclsFuncs As New clsGeneralFuncs
        Dim dirAnnotations As DirectoryInfo     'directory info for annotations
        Dim fiFileListing As FileInfo()         'file listing of directory
        Dim iTrackCount As Integer = 0

        Try
            cboTracks.Items.Add("*ALL*")
            cboTracks.Text = "*ALL*"

            dirAnnotations = New DirectoryInfo(msDirectory)                      'get handle on the folder
            fiFileListing = dirAnnotations.GetFiles()                           'get a list of the files
            mArrTracks = pclsFuncs.GetTrackNames(fiFileListing)

            For iTrackCount = 0 To mArrTracks.Length - 1
                cboTracks.Items.Add(mArrTracks(iTrackCount))
                cboEndTrack.Items.Add(mArrTracks(iTrackCount))
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Activates the drop down for track depending on if we are normalising - it's not required if we aren't
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub radNormEvents_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles radNormEvents.CheckedChanged

        Try
            'TODO: this is wrong - if we are restricting to overlap only with set tracks, this shouldn't be active
            If radNormEvents.Checked = True Then
                cboEndTrack.Enabled = True
            Else
                cboEndTrack.Enabled = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Starts the necessary stages of processing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmdStart_Click(sender As System.Object, e As System.EventArgs) Handles cmdStart.Click
        Dim pclsFuncs As New clsGeneralFuncs

        Try
            CreateCorrelationsDataset()
            FillCorrelations()
            If chkSummary.Checked Then AddSummarySheet()
            pclsFuncs.ExportToExcel(mdsCorrelations)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region

#Region "Data Handling"
    ''' <summary>
    ''' Create the structure for the correlations dataset by reading in the raw XML files.
    ''' </summary>
    Private Sub CreateCorrelationsDataset()
        Dim iFileCount As Integer = 0
        Dim iTrackCount As Integer = 0
        Dim arrTrackArray As String()
        Dim dirAnnotations As DirectoryInfo     'directory info for annotations
        Dim fiFileListing As FileInfo()         'file listing of directory
        Dim fiIndividualFile As FileInfo
        Dim pclsFuncs As New clsGeneralFuncs

        Try
            mdsCorrelations = New DataSet
            mdsInstances = New DataSet

            dirAnnotations = New DirectoryInfo(msDirectory)                     'get handle on the folder
            fiFileListing = dirAnnotations.GetFiles()                           'get a list of the files

            For Each fiIndividualFile In fiFileListing
                If fiIndividualFile.Extension = ".anvil" Then
                    mdsCorrelations.Tables.Add(Path.GetFileNameWithoutExtension(fiIndividualFile.FullName))     'add a new table for this file
                    mdsCorrelations.Tables(iFileCount).Columns.Add()          'add new column
                    mdsCorrelations.Tables(iFileCount).Rows.Add()             'add new row
                    mdsCorrelations.Tables(iFileCount).Rows(0).Item(0) = ""

                    mdsInstances.Tables.Add()
                    mdsInstances.Tables(iFileCount).Columns.Add("trackname")
                    mdsInstances.Tables(iFileCount).Columns.Add("instances")
                    mdsInstances.Tables(iFileCount).Columns.Add("time")

                    arrTrackArray = pclsFuncs.GetTrackNamesFromOneFile(fiIndividualFile)

                    For iTrackCount = 0 To arrTrackArray.Length - 1
                        With mdsCorrelations.Tables(iFileCount)
                            .Columns.Add()          'add new column
                            .Rows.Add()             'add new row
                            .Rows(iTrackCount + 1).Item(0) = arrTrackArray(iTrackCount).ToString  'put track name in new field
                            .Rows(0).Item(iTrackCount + 1) = arrTrackArray(iTrackCount).ToString  'put track name in new field
                        End With

                        mdsInstances.Tables(iFileCount).Rows.Add()
                        mdsInstances.Tables(iFileCount).Rows(iTrackCount).Item(0) = arrTrackArray(iTrackCount).ToString
                    Next

                    iFileCount += 1
                End If
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Populate the correlations dataset - this is where most of the work is done.
    ''' Convert XML to dataset, then loop through and find correlations, taking into account the track restriction and window settings in place.
    ''' </summary>
    Private Sub FillCorrelations()
        Dim iTableCount As Integer = 0
        Dim iRowCount As Integer = 0
        Dim iCompareCount As Integer = 0
        Dim iTrackOne As Integer = 0
        Dim iTrackTwo As Integer = 0
        Dim dblStartOne As Double
        Dim dblStartTwo As Double
        Dim dblEndOne As Double
        Dim dblEndTwo As Double
        Dim dsTrackTimes As DataSet
        Dim pclsFuncs As New clsGeneralFuncs
        Dim bOverlap As Boolean = False
        Dim bTrackRestrict As Boolean = False
        Dim sRestrictName As String = ""
        Dim sEndTrack As String = ""
        Dim dblStartRestrict As Double
        Dim dblEndRestrict As Double
        Dim dblThird As Double
        Dim bContinue As Boolean = True

        Try
            dsTrackTimes = pclsFuncs.ConvertXMLToDataSet(msDirectory)           'fetch the track times for the XML files

            If cboTracks.Text <> "*ALL*" Then
                bTrackRestrict = True
                sRestrictName = cboTracks.Text
            End If

            sEndTrack = cboEndTrack.Text

            'go through each coder
            For iTableCount = 0 To dsTrackTimes.Tables.Count - 1
                'pick up the start and end of the overlap track
                If bTrackRestrict Then
                    For iRowCount = 0 To dsTrackTimes.Tables(iTableCount).Rows.Count - 1
                        If CStr(dsTrackTimes.Tables(iTableCount).Rows(iRowCount).Item("trackname")) = sRestrictName Then
                            dblStartRestrict = CDbl(dsTrackTimes.Tables(iTableCount).Rows(iRowCount).Item("start"))
                            dblEndRestrict = CDbl(dsTrackTimes.Tables(iTableCount).Rows(iRowCount).Item("end"))

                            dblThird = (dblEndRestrict - dblStartRestrict) / 3

                            'modify the overlap values based on the portion we want to look at
                            If radFirst.Checked = True Then
                                dblEndRestrict = dblStartRestrict + dblThird                  'no change to start, finish at 1/3
                            ElseIf radSecond.Checked = True Then
                                dblStartRestrict = dblStartRestrict + dblThird                'start at 1/3, finish at 2/3
                                dblEndRestrict = dblStartRestrict + dblThird + dblThird
                            ElseIf radThird.Checked = True Then
                                dblStartRestrict = dblStartRestrict + dblThird + dblThird     'start at 2/3, no change to end
                            End If
                        End If
                    Next
                End If

                iRowCount = 0   'reset just incase

                'go through each row and column to compare overlaps
                For iRowCount = 0 To dsTrackTimes.Tables(iTableCount).Rows.Count - 1
                    For iCompareCount = 0 To dsTrackTimes.Tables(iTableCount).Rows.Count - 1
                        If iCompareCount <> iRowCount Then                      'dont compare rows to themselves
                            dblStartOne = CDbl(dsTrackTimes.Tables(iTableCount).Rows(iRowCount).Item("start"))       'gather the starts and ends here
                            dblEndOne = CDbl(dsTrackTimes.Tables(iTableCount).Rows(iRowCount).Item("end"))

                            dblStartTwo = CDbl(dsTrackTimes.Tables(iTableCount).Rows(iCompareCount).Item("start"))
                            dblEndTwo = CDbl(dsTrackTimes.Tables(iTableCount).Rows(iCompareCount).Item("end"))

                            bOverlap = False    'reset overlap flag
                            bContinue = True    'reset flag to continue - used by track restriction

                            If bTrackRestrict Then
                                bContinue = False   'default to false, overwrite with true if within bounds
                                'check for an overlap between at least one of our segments and the overlap restrictor
                                If (dblStartRestrict < dblEndOne) And (dblStartOne < dblEndRestrict) Then bContinue = True
                                If (dblStartRestrict < dblEndTwo) And (dblStartTwo < dblEndRestrict) Then bContinue = True
                            End If

                            If bContinue Then
                                'code for overlap only (no window): If (dblStartOne < dblEndTwo) And (dblStartTwo < dblEndOne) Then
                                If chkWindowOnly.Checked = False Then
                                    If (dblStartOne - nudWinStartPre.Value < dblEndTwo) And (dblStartTwo < dblEndOne + nudWinEndPost.Value) Then
                                        bOverlap = True
                                    End If
                                Else
                                    'only work out for the stuff in the window
                                    If ((dblStartTwo > (dblStartOne - nudWinStartPre.Value)) And (dblStartTwo < (dblStartOne + nudWinStartPost.Value))) Or _
                                        ((dblStartTwo > (dblEndOne - nudWinEndPre.Value)) And (dblStartTwo < (dblEndOne + nudWinEndPost.Value))) Then
                                        bOverlap = True
                                    End If
                                End If

                                If bOverlap Then
                                    iTrackOne = TrackNameToNumber(mdsCorrelations.Tables(iTableCount), CStr(dsTrackTimes.Tables(iTableCount).Rows(iRowCount).Item("trackname")))      'get the track number for row 1
                                    iTrackTwo = TrackNameToNumber(mdsCorrelations.Tables(iTableCount), CStr(dsTrackTimes.Tables(iTableCount).Rows(iCompareCount).Item("trackname")))  'get the track number for row 2

                                    If iTrackOne <> -1 And iTrackTwo <> -1 Then
                                        If IsDBNull(mdsCorrelations.Tables(iTableCount).Rows(iTrackOne).Item(iTrackTwo)) Then
                                            mdsCorrelations.Tables(iTableCount).Rows(iTrackOne).Item(iTrackTwo) = 0
                                        End If

                                        mdsCorrelations.Tables(iTableCount).Rows(iTrackOne).Item(iTrackTwo) = CInt(mdsCorrelations.Tables(iTableCount).Rows(iTrackOne).Item(iTrackTwo)) + 1    'add one to the correlation table for this pair
                                    Else
                                        MessageBox.Show("A track name wasn't found")
                                        Exit Sub
                                    End If
                                End If
                            End If
                        End If
                    Next
                Next
            Next

            'if we are normalising we need to loop over the set again, table by table to get info on track instances and divide our current values by this
            If radNormEvents.Checked = True Then
                'NormaliseDataToEvents(dsTrackTimes)
                NormaliseByTime(dsTrackTimes)
                'NormaliseOverlapMinOverSumInstances(dsTrackTimes)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Create a dataset where each table is each video, the rows have each track, with a total number of times they occur and the elapsed time they occur for
    ''' </summary>
    ''' <param name="dsTrackTimes">Dataset from source XML files</param>
    ''' <returns>True if it worked, false if not</returns>
    Private Function PopulateMDSInstances(ByVal dsTrackTimes As DataSet) As Boolean
        Dim iTableCount As Integer = 0
        Dim iRowCount As Integer = 0
        Dim iInstancesCount As Integer = 0
        Dim sTrackName As String
        Dim dblLength As Double = 0

        PopulateMDSInstances = False

        Try
            'loop through dataset of track times and match each record of a trackname to tracknames from mdsInstances and increment
            For iTableCount = 0 To dsTrackTimes.Tables.Count - 1
                For iRowCount = 0 To dsTrackTimes.Tables(iTableCount).Rows.Count - 1
                    sTrackName = CStr(dsTrackTimes.Tables(iTableCount).Rows(iRowCount).Item("trackname"))

                    dblLength = CDbl(dsTrackTimes.Tables(iTableCount).Rows(iRowCount).Item("end")) - CDbl(dsTrackTimes.Tables(iTableCount).Rows(iRowCount).Item("start"))

                    For iInstancesCount = 0 To mdsInstances.Tables(iTableCount).Rows.Count - 1
                        If CStr(mdsInstances.Tables(iTableCount).Rows(iInstancesCount).Item("trackname")) = sTrackName Then
                            If IsDBNull(mdsInstances.Tables(iTableCount).Rows(iInstancesCount).Item("instances")) Then mdsInstances.Tables(iTableCount).Rows(iInstancesCount).Item("instances") = 0
                            If IsDBNull(mdsInstances.Tables(iTableCount).Rows(iInstancesCount).Item("time")) Then mdsInstances.Tables(iTableCount).Rows(iInstancesCount).Item("time") = 0

                            mdsInstances.Tables(iTableCount).Rows(iInstancesCount).Item("instances") = CInt(mdsInstances.Tables(iTableCount).Rows(iInstancesCount).Item("instances")) + 1
                            mdsInstances.Tables(iTableCount).Rows(iInstancesCount).Item("time") = CDbl(mdsInstances.Tables(iTableCount).Rows(iInstancesCount).Item("time")) + dblLength
                        End If
                    Next
                Next
            Next

            PopulateMDSInstances = True

        Catch ex As Exception
            PopulateMDSInstances = False
            MessageBox.Show(ex.Message)
        End Try
    End Function
#End Region

#Region "Normalisation functions"
    ''' <summary>
    ''' This is a deprecated normalisation left here because the code could be useful - normalises so that the number given is the times the row occurs for each column, as a proportion of the total row occurences
    ''' </summary>
    ''' <param name="dsTrackTimes"></param>
    ''' <remarks></remarks>
    Private Sub NormaliseDataToEvents(ByVal dsTrackTimes As DataSet)
        Dim iTableCount As Integer = 0
        Dim iRowCount As Integer = 1
        Dim iInstancesCount As Integer = 0
        Dim iColCount As Integer
        Dim sTrackName As String
        Dim iDivisor As Integer = 1 'initialise to 1 so we don't try to divide by 0 :-)

        Try
            PopulateMDSInstances(dsTrackTimes)

            'now do the loop over the mdsCorrelations and use the mdsInstances values to normalise the rows
            For iTableCount = 0 To mdsCorrelations.Tables.Count - 1
                For iRowCount = 1 To mdsCorrelations.Tables(iTableCount).Rows.Count - 1
                    'match up the track name with mdsInstances and then find what the number of instances is to set this as a divisor
                    sTrackName = CStr(mdsCorrelations.Tables(iTableCount).Rows(iRowCount).Item(0))

                    For iInstancesCount = 0 To mdsInstances.Tables(iTableCount).Rows.Count - 1
                        If CStr(mdsInstances.Tables(iTableCount).Rows(iInstancesCount).Item("trackname")) = sTrackName Then
                            iDivisor = CInt(mdsInstances.Tables(iTableCount).Rows(iInstancesCount).Item("instances"))
                            Exit For
                        End If
                    Next

                    'update each value in the current row
                    For iColCount = 1 To mdsCorrelations.Tables(iTableCount).Columns.Count - 1
                        If Not IsDBNull(mdsCorrelations.Tables(iTableCount).Rows(iRowCount).Item(iColCount)) Then
                            mdsCorrelations.Tables(iTableCount).Rows(iRowCount).Item(iColCount) = CDbl(CInt(mdsCorrelations.Tables(iTableCount).Rows(iRowCount).Item(iColCount)) / iDivisor)
                        End If
                    Next

                    iColCount = 1   'reset counter
                Next
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' This normalises all of the correlations to overlaps per minute.
    ''' The overall length is based on whether there is a track restriction in place, in which case the time is the length of the restriction track, otherwise it's the full video length
    ''' </summary>
    ''' <param name="dsTrackTimes">Dataset of the raw data directly taken from the XML used to find the track info lengths</param>
    Private Sub NormaliseByTime(ByVal dsTrackTimes As DataSet)
        Dim iTableCount As Integer = 0
        Dim iRowCount As Integer = 1
        Dim iColCount As Integer = 1
        Dim dblVidLength As Double = 0

        Try
            'for each video
            For iTableCount = 0 To dsTrackTimes.Tables.Count - 1
                dblVidLength = GetVideoLength(dsTrackTimes.Tables(iTableCount))

                'now go through and do the normalisation
                For iRowCount = 1 To mdsCorrelations.Tables(iTableCount).Rows.Count - 1
                    For iColCount = 1 To mdsCorrelations.Tables(iTableCount).Columns.Count - 1
                        'number in cell = number in cell / (vid length / 60) ...also handle if there have been no correlations
                        If Not IsDBNull(mdsCorrelations.Tables(iTableCount).Rows(iRowCount).Item(iColCount)) Then
                            mdsCorrelations.Tables(iTableCount).Rows(iRowCount).Item(iColCount) = CDbl(CInt(mdsCorrelations.Tables(iTableCount).Rows(iRowCount).Item(iColCount)) / (dblVidLength / 60))
                        End If
                    Next
                Next
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' I think this should be used for normalising with a window
    ''' </summary>
    ''' <param name="dsTrackTimes"></param>
    ''' <remarks></remarks>
    Private Sub NormaliseOverlapMinOverSumInstances(ByVal dsTrackTimes As DataSet)
        Dim iTableCount As Integer = 0
        Dim iRowCount As Integer = 1
        Dim iColCount As Integer = 1
        Dim iRowInst As Integer = 0
        Dim iColInst As Integer = 0

        'provides: (overlaps/min)/(instances1/min x instances2/min)
        'which simplifies to: overlap/(instances1 + instances2)

        Try
            PopulateMDSInstances(dsTrackTimes)

            'for each video
            For iTableCount = 0 To dsTrackTimes.Tables.Count - 1
                For iRowCount = 1 To mdsCorrelations.Tables(iTableCount).Rows.Count - 1
                    iRowInst = GetInstancesByName(iTableCount, CStr(mdsCorrelations.Tables(iTableCount).Rows(iRowCount).Item(0)))       'get row instances

                    For iColCount = 1 To mdsCorrelations.Tables(iTableCount).Columns.Count - 1
                        iColInst = GetInstancesByName(iTableCount, CStr(mdsCorrelations.Tables(iTableCount).Rows(0).Item(iColCount)))   'get col instances

                        If Not IsDBNull(mdsCorrelations.Tables(iTableCount).Rows(iRowCount).Item(iColCount)) Then
                            'cell = cell/(rowinst + colinst)
                            mdsCorrelations.Tables(iTableCount).Rows(iRowCount).Item(iColCount) = CDbl(CInt(mdsCorrelations.Tables(iTableCount).Rows(iRowCount).Item(iColCount)) / (iRowInst + iColInst))
                        End If
                    Next
                Next
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' I think this should be used when normalising without a window
    ''' </summary>
    ''' <param name="dsTrackTimes"></param>
    ''' <remarks></remarks>
    Private Sub NormaliseOverlapMinTimeMin(ByVal dsTrackTimes As DataSet)
        Dim dblVidLength As Double = 0
        Dim iTableCount As Integer = 0
        Dim iRowCount As Integer = 1
        Dim iColCount As Integer = 1
        Dim dblRowTime As Double = 0
        Dim dblColTime As Double = 0

        'gets: (overlap/min) / (time1/min * time2/min)
        'which simplifies to: (min * overlap) / (time1 * time2)

        Try
            PopulateMDSInstances(dsTrackTimes)

            'for each coder
            For iTableCount = 0 To dsTrackTimes.Tables.Count - 1
                dblVidLength = GetVideoLength(dsTrackTimes.Tables(iTableCount)) / 60    'get the video length and convert to minutes

                For iRowCount = 1 To mdsCorrelations.Tables(iTableCount).Rows.Count - 1
                    dblRowTime = GetTimeByName(iTableCount, CStr(mdsCorrelations.Tables(iTableCount).Rows(iRowCount).Item(0)))       'get row time

                    For iColCount = 1 To mdsCorrelations.Tables(iTableCount).Columns.Count - 1
                        dblColTime = GetTimeByName(iTableCount, CStr(mdsCorrelations.Tables(iTableCount).Rows(0).Item(iColCount)))   'get col time

                        If Not IsDBNull(mdsCorrelations.Tables(iTableCount).Rows(iRowCount).Item(iColCount)) Then
                            'cell = cell * min/(rowtime + coltime)
                            mdsCorrelations.Tables(iTableCount).Rows(iRowCount).Item(iColCount) = CDbl((dblVidLength * CInt(mdsCorrelations.Tables(iTableCount).Rows(iRowCount).Item(iColCount))) / (dblRowTime * dblColTime))
                        End If
                    Next
                Next
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region

#Region "Utility Functions"
    ''' <summary>
    ''' Convert a track name to it's row location in the datatable
    ''' </summary>
    ''' <param name="dtDataTable">Datatable to search</param>
    ''' <param name="sTrackName">Track to find</param>
    ''' <returns>Row number for the track</returns>
    Private Function TrackNameToNumber(ByVal dtDataTable As DataTable, ByVal sTrackName As String) As Integer
        Dim iRowCount As Integer = 0

        TrackNameToNumber = -1

        Try
            For iRowCount = 0 To dtDataTable.Rows.Count - 1
                If CStr(dtDataTable.Rows(iRowCount).Item(0)) = sTrackName Then
                    TrackNameToNumber = iRowCount
                    Exit For
                End If
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Returns the number of instances based on the data in mdsInstances given a trackname and table (video) reference
    ''' </summary>
    ''' <param name="iTableNo">Table to search</param>
    ''' <param name="sTrackName">Track to search for</param>
    ''' <returns>Integer with the number of element instances of this track in this video</returns>
    Private Function GetInstancesByName(ByVal iTableNo As Integer, ByVal sTrackName As String) As Integer
        Dim iRowCount As Integer = 0

        GetInstancesByName = 0

        Try
            For iRowCount = 0 To mdsInstances.Tables(iTableNo).Rows.Count - 1
                If CStr(mdsInstances.Tables(iTableNo).Rows(iRowCount).Item("trackname")) = sTrackName Then
                    GetInstancesByName = CInt(IIf(IsDBNull(mdsInstances.Tables(iTableNo).Rows(iRowCount).Item("instances")), 0, CInt(mdsInstances.Tables(iTableNo).Rows(iRowCount).Item("instances"))))
                    Exit For
                End If
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Returns the length of time based on the data in mdsInstances given a trackname and table (video) reference
    ''' </summary>
    ''' <param name="iTableNo">Table to search</param>
    ''' <param name="sTrackName">Track to search for</param>
    ''' <returns>Double with the elapsed time of this track in this video</returns>
    Private Function GetTimeByName(ByVal iTableNo As Integer, ByVal sTrackName As String) As Double
        Dim iRowCount As Integer = 0

        GetTimeByName = 0

        Try
            For iRowCount = 0 To mdsInstances.Tables(iTableNo).Rows.Count - 1
                If CStr(mdsInstances.Tables(iTableNo).Rows(iRowCount).Item("trackname")) = sTrackName Then
                    GetTimeByName = CDbl(IIf(IsDBNull(mdsInstances.Tables(iTableNo).Rows(iRowCount).Item("time")), 0, CDbl(mdsInstances.Tables(iTableNo).Rows(iRowCount).Item("time"))))
                    Exit For
                End If
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Given a 'video', i.e. datatable from the XML dataset, return the total length - use the value from the combo if restricting by particular tracks
    ''' </summary>
    ''' <param name="dtDataTable">Table for this video</param>
    ''' <returns>Double in seconds of the length of the video/restricted track</returns>
    Private Function GetVideoLength(ByVal dtDataTable As DataTable) As Double
        Dim iTrackCount As Integer = 0
        Dim dblVidStart As Double = 0
        Dim dblVidEnd As Double = 0
        Dim dblVidLength As Double = 0
        Dim sEndTrack As String = ""
        Dim bRestrict As Boolean = False

        GetVideoLength = 0

        Try
            'pick up the correct track to denote the normalisation period
            If cboTracks.Text <> "*ALL*" Then
                sEndTrack = cboTracks.Text
                bRestrict = True
            Else
                sEndTrack = cboEndTrack.Text
            End If

            For iTrackCount = 0 To dtDataTable.Rows.Count - 1
                If CStr(dtDataTable.Rows(iTrackCount).Item("trackname")) = sEndTrack Then
                    'if we have a track restriction then the start is the start of that track, otherwise its the start of the video
                    dblVidStart = 0
                    If bRestrict Then dblVidStart = CDbl(dtDataTable.Rows(iTrackCount).Item("start"))
                    dblVidEnd = CDbl(dtDataTable.Rows(iTrackCount).Item("end"))
                    dblVidLength = dblVidEnd - dblVidStart

                    'we've got what we came for, so exit - this works on the basis that the restrict track only has 1 element
                    Exit For
                End If
            Next

            GetVideoLength = dblVidLength

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function
#End Region

    ''' <summary>
    ''' Adds a summary sheet where all tracks and coders are collated - this can get big!
    ''' </summary>
    Private Sub AddSummarySheet()
        Dim dtSummary As DataTable
        Dim iTableCount As Integer = 0
        Dim iRowCount As Integer = 1
        Dim iColCount As Integer = 1
        Dim sCurrentCombo As String = ""
        Dim iComboCount As Integer = 0

        Try
            dtSummary = New DataTable
            dtSummary.TableName = "Summary"
            dtSummary.Columns.Add()

            'create a column with all of the combined correlation names in
            For iRowCount = 1 To mdsCorrelations.Tables(0).Rows.Count - 1
                For iColCount = 1 To mdsCorrelations.Tables(0).Columns.Count - 1
                    dtSummary.Rows.Add(CStr(mdsCorrelations.Tables(0).Rows(iRowCount).Item(0)) & " | " & CStr(mdsCorrelations.Tables(0).Rows(0).Item(iColCount)))
                Next
            Next

            iRowCount = 1   'reset counters
            iColCount = 1

            'convert subject tables into columns with the values translated into rows
            For iTableCount = 0 To mdsCorrelations.Tables.Count - 1
                dtSummary.Columns.Add()     'one column for each subject

                For iRowCount = 1 To mdsCorrelations.Tables(iTableCount).Rows.Count - 1
                    For iColCount = 1 To mdsCorrelations.Tables(iTableCount).Columns.Count - 1
                        'need to match combo strings as not all correl tables have all tracks
                        sCurrentCombo = CStr(mdsCorrelations.Tables(iTableCount).Rows(iRowCount).Item(0)) & " | " & CStr(mdsCorrelations.Tables(iTableCount).Rows(0).Item(iColCount))

                        For iComboCount = 0 To dtSummary.Rows.Count - 1
                            If CStr(dtSummary.Rows(iComboCount).Item(0)) = sCurrentCombo Then
                                dtSummary.Rows(iComboCount).Item(iTableCount + 1) = CDbl(IIf(IsDBNull(mdsCorrelations.Tables(iTableCount).Rows(iRowCount).Item(iColCount)), 0, mdsCorrelations.Tables(iTableCount).Rows(iRowCount).Item(iColCount)))
                                Exit For
                            End If
                        Next
                        iComboCount = 0
                    Next
                    iColCount = 0
                Next
                iRowCount = 0
            Next

            mdsCorrelations.Tables.Add(dtSummary)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

End Class