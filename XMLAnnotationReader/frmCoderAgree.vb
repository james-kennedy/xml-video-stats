Option Strict On
Option Explicit On

Imports System.IO
Imports Microsoft.Office.Interop

Public Class frmCoderAgree

    Public Property msDirectory As String

    Private Sub frmCoderAgree_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Top = 0
        Me.Left = 0
    End Sub

    Private Sub cmdStart_Click(sender As System.Object, e As System.EventArgs) Handles cmdStart.Click
        Dim dirInfo As DirectoryInfo
        Dim dtCoderOne As DataTable
        Dim dtCoderTwo As DataTable
        Dim dtResults As DataTable
        Dim dsKappas As New DataSet
        Dim sFileList As String()
        Dim pclsFuncs As New clsGeneralFuncs

        Try
            'check there are further directories in the root, go through the directories and check for two .txt files in each
            If Not CheckDirectoryStructure(msDirectory) Then
                MessageBox.Show("Make sure the directory is configured correctly." & vbCrLf & "The root should contain further folders, each containing the two files to compare.")
            Else
                dirInfo = New DirectoryInfo(msDirectory)
                For Each folder As DirectoryInfo In dirInfo.GetDirectories
                    sFileList = Directory.GetFiles(folder.FullName)

                    dtCoderOne = ConvertTabToDataTable(sFileList(0))
                    dtCoderTwo = ConvertTabToDataTable(sFileList(1))

                    dtResults = CompareCoders(dtCoderOne, dtCoderTwo)

                    dsKappas.Tables.Add(dtResults.Copy)
                    dsKappas.Tables(dsKappas.Tables.Count - 1).TableName = folder.Name
                Next
            End If

            pclsFuncs.ExportToExcel(dsKappas)
            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Makes sure that the directory houses further directories, each with ONLY 2 files in - the pair for comparison
    ''' </summary>
    ''' <param name="sRoot">Root directory as a string</param>
    ''' <returns>True if good, false if not</returns>
    Private Function CheckDirectoryStructure(ByVal sRoot As String) As Boolean
        Dim dirInfo As DirectoryInfo

        CheckDirectoryStructure = True

        Try
            dirInfo = New DirectoryInfo(sRoot)

            If dirInfo.GetDirectories.Count = 0 Then
                CheckDirectoryStructure = False
            End If

            For Each folder As String In Directory.GetDirectories(sRoot)
                If Directory.GetFiles(folder).Length <> 2 Then
                    CheckDirectoryStructure = False
                    Exit For
                End If
            Next

            If CheckDirectoryStructure <> False Then CheckDirectoryStructure = True

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Convert a tab delimited file to a datatable. The names of columns are the header names.
    ''' </summary>
    ''' <param name="sFile">String of the .txt filepath</param>
    ''' <returns>A datatable of the file contents</returns>
    Private Function ConvertTabToDataTable(ByVal sFile As String) As DataTable
        Dim sLines As String()
        Dim sHeaders As String()
        Dim dtReturnTable As DataTable
        Dim iHeaderCount As Integer = 0
        Dim iRowCount As Integer = 1

        ConvertTabToDataTable = Nothing

        Try
            dtReturnTable = New DataTable()

            sLines = File.ReadAllLines(sFile)                   'read in the file

            sHeaders = sLines(0).Split(CChar(ControlChars.Tab)) 'split the headers

            'copy the headers into the column names (and create the columns)
            For iHeaderCount = 0 To sHeaders.Length - 1
                dtReturnTable.Columns.Add(New DataColumn(sHeaders(iHeaderCount), GetType(String)))
            Next

            'create and insert the rows (split on tab again)
            For iRowCount = 1 To sLines.Length - 1
                dtReturnTable.Rows.Add(sLines(iRowCount).Split(CChar(ControlChars.Tab)))
            Next

            ConvertTabToDataTable = dtReturnTable

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Compares the coders - loops over the datatables passed in, counts and calculates percentage and kappa agreement. Puts them in a datatable which is returned.
    ''' </summary>
    ''' <param name="dtOne">First coder datatable</param>
    ''' <param name="dtTwo">Second coder datatable</param>
    ''' <returns>A datatable with tracks, kappa, percent agreement, slices agreed, slices total</returns>
    Private Function CompareCoders(ByVal dtOne As DataTable, ByVal dtTwo As DataTable) As DataTable
        Dim dtContTable As DataTable
        Dim dtReturnData As DataTable
        Dim iOneValue As Integer = 0
        Dim iTwoValue As Integer = 0
        Dim iTotalRows As Integer = 0
        Dim iAgreeCount As Integer = 0
        Dim dblKappaValue As Double = 0
        Dim dblChanceAgree As Double = 0
        Dim dblPercentAgree As Double = 0
        Dim iCurrentContCol As Integer = 0

        CompareCoders = Nothing

        Try
            If dtOne.Rows.Count <> dtTwo.Rows.Count Then
                MessageBox.Show("There are different numbers of rows - something is awry!")
            Else
                If dtOne.Columns.Count <> dtTwo.Columns.Count Then
                    MessageBox.Show("There are different numbers of columns - something is awry!")
                Else
                    dtReturnData = New DataTable
                    dtReturnData.Columns.Add("track")
                    dtReturnData.Columns.Add("kappa")
                    dtReturnData.Columns.Add("percent")
                    dtReturnData.Columns.Add("agreed")
                    dtReturnData.Columns.Add("outof")

                    iTotalRows = dtOne.Rows.Count       'this is used in the Kappa calculation

                    'the last column is empty from the export, therefore -2
                    For iColCount = 2 To dtOne.Columns.Count - 2
                        iAgreeCount = 0                 'reset agreement
                        dblChanceAgree = 0
                        dblKappaValue = 0
                        dblPercentAgree = 0
                        dtContTable = Nothing           'reset contingency table
                        dtContTable = New DataTable
                        dtContTable.Rows.Add()          'coder 1
                        dtContTable.Rows.Add()          'coder 2

                        dtReturnData.Rows.Add()
                        dtReturnData.Rows(dtReturnData.Rows.Count - 1).Item("track") = dtOne.Columns(iColCount).ColumnName

                        'this goes to -2 as there appears to be a blank row at the end of each file
                        For iRowCount = 0 To dtOne.Rows.Count - 1
                            'get value from coder, see if we have if it, if we don't then add a column for it, increment contingency table cell
                            iOneValue = CInt(dtOne.Rows(iRowCount).Item(iColCount + 1))
                            iCurrentContCol = GetContingencyColumnForOption(iOneValue.ToString, dtContTable)
                            If iCurrentContCol = -1 Then
                                dtContTable.Columns.Add(iOneValue.ToString)
                                iCurrentContCol = dtContTable.Columns.Count - 1
                            End If
                            dtContTable.Rows(0).Item(iCurrentContCol) = CInt(IIf(IsDBNull(dtContTable.Rows(0).Item(iCurrentContCol)), 0, dtContTable.Rows(0).Item(iCurrentContCol))) + 1

                            'same process for second coder
                            iTwoValue = CInt(dtTwo.Rows(iRowCount).Item(iColCount + 1))
                            iCurrentContCol = GetContingencyColumnForOption(iTwoValue.ToString, dtContTable)
                            If iCurrentContCol = -1 Then
                                dtContTable.Columns.Add(iTwoValue.ToString)
                                iCurrentContCol = dtContTable.Columns.Count - 1
                            End If
                            dtContTable.Rows(1).Item(iCurrentContCol) = CInt(IIf(IsDBNull(dtContTable.Rows(1).Item(iCurrentContCol)), 0, dtContTable.Rows(1).Item(iCurrentContCol))) + 1

                            If iOneValue = iTwoValue Then iAgreeCount += 1
                        Next

                        'at this stage we have the quasi-contingency table:
                        ' coder |___option __|__option__|__option__| ....
                        ' one   |   1           2           3
                        ' two   |   1           2           3

                        'multiply contingency table columns, divide by total rows of dtOne or dtTwo and sum the results
                        For iChanceCol = 0 To dtContTable.Columns.Count - 1
                            dblChanceAgree += ((CInt(IIf(IsDBNull(dtContTable.Rows(0).Item(iChanceCol)), 0, dtContTable.Rows(0).Item(iChanceCol))) * _
                                                CInt(IIf(IsDBNull(dtContTable.Rows(1).Item(iChanceCol)), 0, dtContTable.Rows(1).Item(iChanceCol)))) / iTotalRows)
                        Next

                        dblPercentAgree = iAgreeCount / iTotalRows
                        dblKappaValue = (iAgreeCount - dblChanceAgree) / (iTotalRows - dblChanceAgree)

                        dtReturnData.Rows(dtReturnData.Rows.Count - 1).Item("kappa") = dblKappaValue
                        dtReturnData.Rows(dtReturnData.Rows.Count - 1).Item("percent") = dblPercentAgree
                        dtReturnData.Rows(dtReturnData.Rows.Count - 1).Item("agreed") = iAgreeCount
                        dtReturnData.Rows(dtReturnData.Rows.Count - 1).Item("outof") = iTotalRows

                        iColCount += 1  'skip it on an extra column as they come in pairs
                    Next

                    CompareCoders = dtReturnData
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Does a look up on the datatable column names for the option string passed in.
    ''' </summary>
    ''' <param name="sOption">Option to search for.</param>
    ''' <param name="dtContingency">Datatable to search</param>
    ''' <returns>Integer representing the column that option is found in</returns>
    Private Function GetContingencyColumnForOption(ByVal sOption As String, ByVal dtContingency As DataTable) As Integer
        Dim iCurrentContCol As Integer = -1

        GetContingencyColumnForOption = -1

        Try
            For iContCount = 0 To dtContingency.Columns.Count - 1
                If dtContingency.Columns(iContCount).ColumnName = sOption Then
                    iCurrentContCol = iContCount
                    Exit For
                End If
            Next

            GetContingencyColumnForOption = iCurrentContCol

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

End Class