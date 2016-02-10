Option Strict On
Option Explicit On

Imports System.IO
Imports System.Xml
Imports Microsoft.Office.Interop

Public Class frmBatchStats

    Public Property msDirectory As String

    Private mdsTrackInfo As DataSet
    Private mdsResults As DataSet
    Private mArrTracks(-1) As String            'stores track names across all files

    Private Sub frmBatchStats_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Try
            PopulateCombos()

            Me.Top = 0
            Me.Left = 0

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

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

    Private Sub cmdStart_Click(sender As System.Object, e As System.EventArgs) Handles cmdStart.Click
        Dim pclsFuncs As New clsGeneralFuncs

        Try
            mdsTrackInfo = pclsFuncs.ConvertXMLToDataSet(msDirectory)   'convert XML to dataset
            CreateResultsDataSet()                                      'create structure for our results to go into
            CountData()                                                 'this is where the real counting is done

            If chkSummary.Checked = True Then                           'add summary with aves and s.d's collated for all if needed
                AddSummarySheet()
            End If

            pclsFuncs.ExportToExcel(mdsResults)                         'export to excel!

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub CreateResultsDataSet()
        Dim iTableCount As Integer = 0
        Dim iRowCount As Integer = 0

        Try
            mdsResults = New DataSet

            'create dataset for results - table for each child, rows = tracks from above, cols = track name, time, instances, ave time, time/min, count/min
            For iTableCount = 0 To mdsTrackInfo.Tables.Count - 1
                mdsResults.Tables.Add(mdsTrackInfo.Tables(iTableCount).TableName)

                With mdsResults.Tables(iTableCount).Columns
                    .Add("trackname")
                    .Add("elapsedtime")
                    .Add("instances")
                    .Add("avetime")
                    .Add("timemin")
                    .Add("countmin")
                End With

                'add a row for each trackname and copy it over
                For iRowCount = 0 To mArrTracks.Length - 1
                    mdsResults.Tables(iTableCount).Rows.Add()

                    With mdsResults.Tables(iTableCount).Rows(iRowCount)
                        .Item("trackname") = mArrTracks(iRowCount)
                        .Item("elapsedtime") = 0
                        .Item("instances") = 0
                        .Item("avetime") = 0
                        .Item("timemin") = 0
                        .Item("countmin") = 0
                    End With
                Next
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub CountData()
        Dim bOverlapOn As Boolean = False
        Dim sOverlapTrack As String = ""
        Dim iTableCount As Integer = 0
        Dim iRowCount As Integer = 0
        Dim sEndTrack As String
        Dim dblVidLength As Double
        Dim sCurrentTrack As String
        Dim iResultsRows As Integer = 0
        Dim dblCurrentStart As Double
        Dim dblCurrentEnd As Double
        Dim dblStartOverlap As Double
        Dim dblEndOverlap As Double
        Dim dblThird As Double

        Try
            If cboTracks.Text <> "*ALL*" Then
                bOverlapOn = True
                sOverlapTrack = cboTracks.Text
            End If

            sEndTrack = cboEndTrack.Text

            For iTableCount = 0 To mdsTrackInfo.Tables.Count - 1
                'pick up the start and end of the overlap track
                If bOverlapOn Then
                    For iRowCount = 0 To mdsTrackInfo.Tables(iTableCount).Rows.Count - 1
                        If CStr(mdsTrackInfo.Tables(iTableCount).Rows(iRowCount).Item("trackname")) = sOverlapTrack Then
                            dblStartOverlap = CDbl(mdsTrackInfo.Tables(iTableCount).Rows(iRowCount).Item("start"))
                            dblEndOverlap = CDbl(mdsTrackInfo.Tables(iTableCount).Rows(iRowCount).Item("end"))

                            dblThird = (dblEndOverlap - dblStartOverlap) / 3

                            'modify the overlap values based on the portion we want to look at
                            If radFirst.Checked = True Then
                                dblEndOverlap = dblStartOverlap + dblThird                  'no change to start, finish at 1/3
                            ElseIf radSecond.Checked = True Then
                                dblStartOverlap = dblStartOverlap + dblThird                'start at 1/3, finish at 2/3
                                dblEndOverlap = dblStartOverlap + dblThird + dblThird
                            ElseIf radThird.Checked = True Then
                                dblStartOverlap = dblStartOverlap + dblThird + dblThird     'start at 2/3, no change to end
                            End If
                        End If
                    Next
                End If

                For iRowCount = 0 To mdsTrackInfo.Tables(iTableCount).Rows.Count - 1
                    'go through each datatable from mdsTrackInfo loop through rows, match to rows from 2. and update totals
                    sCurrentTrack = CStr(mdsTrackInfo.Tables(iTableCount).Rows(iRowCount).Item("trackname"))
                    dblCurrentStart = CDbl(mdsTrackInfo.Tables(iTableCount).Rows(iRowCount).Item("start"))
                    dblCurrentEnd = CDbl(mdsTrackInfo.Tables(iTableCount).Rows(iRowCount).Item("end"))

                    If bOverlapOn Then
                        'check for an overlap between our current segment and the overlap restrictor
                        If (dblStartOverlap < dblCurrentEnd) And (dblCurrentStart < dblEndOverlap) Then
                            'if the start of end of the current segment is outside of the track bounds, then bring back within
                            If dblCurrentStart < dblStartOverlap Then dblCurrentStart = dblStartOverlap
                            If dblCurrentEnd > dblEndOverlap Then dblCurrentEnd = dblEndOverlap

                            For iResultsRows = 0 To mdsResults.Tables(iTableCount).Rows.Count - 1
                                If sCurrentTrack = CStr(mdsResults.Tables(iTableCount).Rows(iResultsRows).Item("trackname")) Then
                                    mdsResults.Tables(iTableCount).Rows(iResultsRows).Item("instances") = CInt(mdsResults.Tables(iTableCount).Rows(iResultsRows).Item("instances")) + 1
                                    mdsResults.Tables(iTableCount).Rows(iResultsRows).Item("elapsedtime") = CDbl(mdsResults.Tables(iTableCount).Rows(iResultsRows).Item("elapsedtime")) + (dblCurrentEnd - dblCurrentStart)
                                End If
                            Next
                        End If
                    Else
                        'count everything
                        For iResultsRows = 0 To mdsResults.Tables(iTableCount).Rows.Count - 1
                            If sCurrentTrack = CStr(mdsResults.Tables(iTableCount).Rows(iResultsRows).Item("trackname")) Then
                                mdsResults.Tables(iTableCount).Rows(iResultsRows).Item("instances") = CInt(mdsResults.Tables(iTableCount).Rows(iResultsRows).Item("instances")) + 1
                                mdsResults.Tables(iTableCount).Rows(iResultsRows).Item("elapsedtime") = CDbl(mdsResults.Tables(iTableCount).Rows(iResultsRows).Item("elapsedtime")) + (dblCurrentEnd - dblCurrentStart)
                                Exit For
                            End If
                        Next
                    End If

                    'when each one is done, pick up video length from debriefing track (sEndTrack) end point
                    If sCurrentTrack = sEndTrack Then
                        dblVidLength = CDbl(mdsTrackInfo.Tables(iTableCount).Rows(iRowCount).Item("end"))
                    End If
                Next

                iRowCount = 0

                'if overlap is on then video length is actually just the overlap segment, so overwrite value from above
                If bOverlapOn Then
                    dblVidLength = dblEndOverlap - dblStartOverlap
                End If

                'go through the rows of our results set and do the maths for averages, counts/min and instances/min
                For iRowCount = 0 To mdsResults.Tables(iTableCount).Rows.Count - 1
                    With mdsResults.Tables(iTableCount).Rows(iRowCount)
                        .Item("avetime") = CDbl(CDbl(.Item("elapsedtime")) / CInt(.Item("instances")))
                        .Item("timemin") = CDbl(CDbl(.Item("elapsedtime")) / (dblVidLength / 60))
                        .Item("countmin") = CDbl(CDbl(.Item("instances")) / (dblVidLength / 60))
                    End With
                Next

                iRowCount = 0
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub AddSummarySheet()
        Dim dsTemp As New DataSet
        Dim iTrackCount As Integer = 0
        Dim iTableCount As Integer = 0
        Dim iRowCount As Integer = 0
        Dim iInsertRow As Integer = 0
        Dim iTempRowCount As Integer = 0
        Dim iTempColCount As Integer = 1
        Dim iTempTableCount As Integer = 0
        Dim dblSum As Double
        Dim dblStDev As Double
        Dim dblTemp As Double
        Dim iAveCount As Integer = 0

        Try
            dsTemp.Tables.Add("elapsedtime")
            dsTemp.Tables.Add("avetime")
            dsTemp.Tables.Add("timemin")
            dsTemp.Tables.Add("countmin")

            dsTemp.Tables("elapsedtime").Columns.Add("trackname")
            dsTemp.Tables("avetime").Columns.Add("trackname")
            dsTemp.Tables("timemin").Columns.Add("trackname")
            dsTemp.Tables("countmin").Columns.Add("trackname")

            For iTrackCount = 0 To mArrTracks.Length - 1
                dsTemp.Tables("elapsedtime").Rows.Add(mArrTracks(iTrackCount))
                dsTemp.Tables("avetime").Rows.Add(mArrTracks(iTrackCount))
                dsTemp.Tables("timemin").Rows.Add(mArrTracks(iTrackCount))
                dsTemp.Tables("countmin").Rows.Add(mArrTracks(iTrackCount))
            Next

            'loop through all other datatables matching by trackname and add up times/instances
            For iTableCount = 0 To mdsResults.Tables.Count - 1
                dsTemp.Tables("elapsedtime").Columns.Add(mdsResults.Tables(iTableCount).TableName)
                dsTemp.Tables("avetime").Columns.Add(mdsResults.Tables(iTableCount).TableName)
                dsTemp.Tables("timemin").Columns.Add(mdsResults.Tables(iTableCount).TableName)
                dsTemp.Tables("countmin").Columns.Add(mdsResults.Tables(iTableCount).TableName)

                For iRowCount = 0 To mdsResults.Tables(iTableCount).Rows.Count - 1
                    'get the row number of dsTemp which corresponds to this track
                    iInsertRow = TrackNameToNumber(dsTemp.Tables("elapsedtime"), CStr(mdsResults.Tables(iTableCount).Rows(iRowCount).Item(0)))

                    dsTemp.Tables("elapsedtime").Rows(iInsertRow).Item(iTableCount + 1) = CDbl(mdsResults.Tables(iTableCount).Rows(iRowCount).Item("elapsedtime"))
                    dsTemp.Tables("avetime").Rows(iInsertRow).Item(iTableCount + 1) = CDbl(mdsResults.Tables(iTableCount).Rows(iRowCount).Item("avetime"))
                    dsTemp.Tables("timemin").Rows(iInsertRow).Item(iTableCount + 1) = CDbl(mdsResults.Tables(iTableCount).Rows(iRowCount).Item("timemin"))
                    dsTemp.Tables("countmin").Rows(iInsertRow).Item(iTableCount + 1) = CDbl(mdsResults.Tables(iTableCount).Rows(iRowCount).Item("countmin"))
                Next
            Next

            'when the above is done, go through each of the dsTemp tables and work out the ave and S.D. for each row - add 2 new columns with this info in., add then divide by count ave time, time/min and count/min
            'additionally, would be useful here to have S.D. for each one
            For iTempTableCount = 0 To dsTemp.Tables.Count - 1
                dsTemp.Tables(iTempTableCount).Columns.Add("ave")

                For iTempRowCount = 0 To dsTemp.Tables(iTempTableCount).Rows.Count - 1
                    For iTempColCount = 1 To dsTemp.Tables(iTempTableCount).Columns.Count - 2
                        dblTemp = CDbl(dsTemp.Tables(iTempTableCount).Rows(iTempRowCount).Item(iTempColCount))
                        'some will be "empty" (-1) because all tracks are present for all tables, even if unused - we can't have legitimate negative values so this check is safe
                        If dblTemp > 0 Then
                            dblSum += dblTemp
                            iAveCount += 1
                        End If
                    Next

                    'for the ave time table it is important that we divide only by the number where a value is present
                    If dsTemp.Tables(iTempTableCount).TableName = "avetime" Then
                        dsTemp.Tables(iTempTableCount).Rows(iTempRowCount).Item("ave") = dblSum / iAveCount
                    Else
                        dsTemp.Tables(iTempTableCount).Rows(iTempRowCount).Item("ave") = dblSum / (iTempColCount - 1)   '-1 on the col count because we changed to base 1 to ignore the trackname col.
                    End If

                    dblSum = 0  'reset for next row
                    iAveCount = 0
                Next
            Next

            'reset counters
            iTempTableCount = 0
            iTempRowCount = 0
            iTempColCount = 1

            'now go through and add in the s.d.
            For iTempTableCount = 0 To dsTemp.Tables.Count - 1
                dsTemp.Tables(iTempTableCount).Columns.Add("sd")

                For iTempRowCount = 0 To dsTemp.Tables(0).Rows.Count - 1
                    For iTempColCount = 1 To dsTemp.Tables(0).Columns.Count - 3
                        dblTemp = CDbl(dsTemp.Tables(iTempTableCount).Rows(iTempRowCount).Item(iTempColCount))

                        If dsTemp.Tables(iTempTableCount).TableName = "avetime" Then
                            If dblTemp > 0 Then
                                dblStDev += ((dblTemp - CDbl(dsTemp.Tables(iTempTableCount).Rows(iTempRowCount).Item("ave"))) ^ 2)
                                iAveCount += 1
                            End If
                        Else
                            dblStDev += ((dblTemp - CDbl(dsTemp.Tables(iTempTableCount).Rows(iTempRowCount).Item("ave"))) ^ 2)
                        End If
                    Next

                    'for the ave time table it is important that we divide only by the number where a value is present
                    If dsTemp.Tables(iTempTableCount).TableName = "avetime" Then
                        dsTemp.Tables(iTempTableCount).Rows(iTempRowCount).Item("sd") = Math.Sqrt(dblStDev / (iAveCount - 1))
                    Else
                        dsTemp.Tables(iTempTableCount).Rows(iTempRowCount).Item("sd") = Math.Sqrt(dblStDev / (iTempColCount - 2))   'using sample std dev (ie take 1/(n-1); population would be 1/n)
                    End If

                    dblStDev = 0    'reset for next row
                    iAveCount = 0
                Next
            Next

            'add the above temp tables to mdsresults for output
            mdsResults.Tables.Add(dsTemp.Tables("elapsedtime").Copy)
            mdsResults.Tables.Add(dsTemp.Tables("avetime").Copy)
            mdsResults.Tables.Add(dsTemp.Tables("timemin").Copy)
            mdsResults.Tables.Add(dsTemp.Tables("countmin").Copy)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

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

    Private Sub cboTracks_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboTracks.SelectedIndexChanged
        If cboTracks.Text <> "*ALL*" Then
            cboEndTrack.Enabled = False
        Else
            cboEndTrack.Enabled = True
        End If
    End Sub

End Class