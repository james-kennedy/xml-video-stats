Option Strict On
Option Explicit On

Imports System.IO
Imports System.Xml
Imports Microsoft.Office.Interop

Public Class clsGeneralFuncs

    ''' <summary>
    ''' Go through all of the ANVIL files in the given folder and populate the modular array mArrTracks with every track name that has annotation data,
    ''' appended with the annotation description, so our output is e.g.: 'Child Behaviour/Towards Robot'
    ''' </summary>
    ''' <param name="fiFileListing">FileInfo listing of a directory where the annotation files are held</param>
    Public Function GetTrackNames(ByVal fiFileListing As FileInfo()) As String()
        Dim fiIndividualFile As FileInfo
        Dim xmlFile As XmlDocument
        Dim xmlListNodes As XmlNodeList
        Dim xmlSingleNode As XmlNode
        Dim iNodeCount As Integer = 0
        Dim sTrackName As String
        Dim iCount As Integer = 0
        Dim bPresent As Boolean
        Dim xmlElNode As XmlNode
        Dim sAttDesc As String
        Dim sConcatName As String
        Dim arrTracks(-1) As String

        GetTrackNames = Nothing

        Try
            For Each fiIndividualFile In fiFileListing                                      'loop through the files
                If fiIndividualFile.Extension = ".anvil" Then                               'only look at the ANVIL files
                    xmlFile = New XmlDocument                                               'create XML document for this file
                    xmlFile.Load(fiIndividualFile.FullName)                                 'load the current XML file

                    xmlListNodes = xmlFile.SelectNodes("annotation/body/track")             'get a list of the nodes

                    For Each xmlSingleNode In xmlListNodes                                  'loop through the nodes
                        sTrackName = xmlSingleNode.Attributes.GetNamedItem("name").Value    'get the name of the node

                        If xmlSingleNode.HasChildNodes = True Then
                            For Each xmlElNode In xmlSingleNode.ChildNodes                  'loop through the child attributes for this node
                                If xmlElNode.HasChildNodes = True Then
                                    sAttDesc = xmlElNode.ChildNodes.Item(0).InnerText       'get the description of each attribute

                                    sConcatName = sTrackName & "~" & sAttDesc               'append to track name with a delimiter so we can split later

                                    For iCount = 0 To arrTracks.Length - 1                  'check if node is already in array
                                        If arrTracks(iCount) = sConcatName Then
                                            bPresent = True                                 'set flag because it is there
                                            Exit For
                                        End If
                                    Next

                                    If bPresent = False Then                                'if track isn't already in our array
                                        ReDim Preserve arrTracks(iNodeCount)                'resize track array to match no. of nodes
                                        arrTracks(iNodeCount) = sConcatName                 'add track name to latest slot
                                        iNodeCount += 1                                     'increment the number of nodes we have
                                    End If

                                    bPresent = False                                        'turn off flag for next time
                                End If
                            Next
                        End If
                    Next
                End If
            Next

            GetTrackNames = arrTracks

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Function

    ''' <summary>
    ''' Given an XML file - extract all track names
    ''' </summary>
    ''' <param name="fiFile">XML to search</param>
    ''' <returns>String array of track names</returns>
    Public Function GetTrackNamesFromOneFile(ByVal fiFile As FileInfo) As String()
        Dim xmlFile As XmlDocument
        Dim xmlListNodes As XmlNodeList
        Dim xmlSingleNode As XmlNode
        Dim iNodeCount As Integer = 0
        Dim sTrackName As String
        Dim iCount As Integer = 0
        Dim bPresent As Boolean
        Dim xmlElNode As XmlNode
        Dim sAttDesc As String
        Dim sConcatName As String
        Dim arrTracks(-1) As String

        GetTrackNamesFromOneFile = Nothing

        Try
            If fiFile.Extension = ".anvil" Then                                         'only look at the ANVIL files
                xmlFile = New XmlDocument                                               'create XML document for this file
                xmlFile.Load(fiFile.FullName)                                           'load the current XML file

                xmlListNodes = xmlFile.SelectNodes("annotation/body/track")             'get a list of the nodes

                For Each xmlSingleNode In xmlListNodes                                  'loop through the nodes
                    sTrackName = xmlSingleNode.Attributes.GetNamedItem("name").Value    'get the name of the node

                    If xmlSingleNode.HasChildNodes = True Then
                        For Each xmlElNode In xmlSingleNode.ChildNodes                  'loop through the child attributes for this node
                            If xmlElNode.HasChildNodes = True Then
                                sAttDesc = xmlElNode.ChildNodes.Item(0).InnerText       'get the description of each attribute

                                sConcatName = sTrackName & "~" & sAttDesc               'append to track name with a delimiter so we can split later

                                For iCount = 0 To arrTracks.Length - 1                  'check if node is already in array
                                    If arrTracks(iCount) = sConcatName Then
                                        bPresent = True                                 'set flag because it is there
                                        Exit For
                                    End If
                                Next

                                If bPresent = False Then                                'if track isn't already in our array
                                    ReDim Preserve arrTracks(iNodeCount)                'resize track array to match no. of nodes
                                    arrTracks(iNodeCount) = sConcatName                 'add track name to latest slot
                                    iNodeCount += 1                                     'increment the number of nodes we have
                                End If

                                bPresent = False                                        'turn off flag for next time
                            End If
                        Next
                    End If
                Next
            End If

            GetTrackNamesFromOneFile = arrTracks

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Function

    ''' <summary>
    ''' Loops through the directory passed in and convert anvil xml files into a dataset, with a datatable for each file
    ''' Stores track name, start time and end time
    ''' </summary>
    ''' <param name="sDirectory">Directory of anvil files</param>
    ''' <returns>Dataset of anvil tracks, with a datatable for each file</returns>
    Public Function ConvertXMLToDataSet(ByVal sDirectory As String) As DataSet
        Dim dirAnnotations As DirectoryInfo     'directory info for annotations
        Dim fiFileListing As FileInfo()         'file listing of directory
        Dim fiIndividualFile As FileInfo
        Dim iFileCount As Integer = 0
        Dim xmlFile As XmlDocument
        Dim xmlListNodes As XmlNodeList
        Dim xmlSingleNode As XmlNode
        Dim xmlChildNode As XmlNode
        Dim dsTrackTimes As DataSet
        Dim sTrackName As String
        Dim sAttDesc As String

        ConvertXMLToDataSet = Nothing

        Try
            dsTrackTimes = New DataSet

            dirAnnotations = New DirectoryInfo(sDirectory)                                              'get handle on the folder
            fiFileListing = dirAnnotations.GetFiles()                                                   'get a list of the files

            For Each fiIndividualFile In fiFileListing
                dsTrackTimes.Tables.Add(Path.GetFileNameWithoutExtension(fiIndividualFile.FullName))
                dsTrackTimes.Tables(iFileCount).Columns.Add("trackname", GetType(String))
                dsTrackTimes.Tables(iFileCount).Columns.Add("start", GetType(Double))
                dsTrackTimes.Tables(iFileCount).Columns.Add("end", GetType(Double))

                xmlFile = New XmlDocument                                                               'create XML document for this file
                xmlFile.Load(fiIndividualFile.FullName)                                                 'load the current XML file

                xmlListNodes = xmlFile.SelectNodes("annotation/body/track")                             'get a list of the nodes

                For Each xmlSingleNode In xmlListNodes                                                  'loop through the nodes
                    sTrackName = xmlSingleNode.Attributes.GetNamedItem("name").Value                    'get the name of the node

                    If xmlSingleNode.HasChildNodes = True Then                                          'make sure we have children
                        For Each xmlChildNode In xmlSingleNode.ChildNodes                               'go through the children one at a time
                            If xmlChildNode.HasChildNodes = True Then                                   'we should have children (but there may be errors in the XML)
                                sAttDesc = xmlChildNode.ChildNodes.Item(0).InnerText                    'get the description of each attribute

                                dsTrackTimes.Tables(iFileCount).Rows.Add(sTrackName & "~" & sAttDesc, CDbl(xmlChildNode.Attributes.GetNamedItem("start").Value), CDbl(xmlChildNode.Attributes.GetNamedItem("end").Value))
                            End If
                        Next
                    End If
                Next

                iFileCount += 1
            Next

            ConvertXMLToDataSet = dsTrackTimes

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Give the function a dataset, get it out in excel font size 8, autofit columns. Each datatable in the set goes on a new worksheet.
    ''' TODO: maybe make it so that column headers are inserted into the first row?
    ''' </summary>
    ''' <param name="dsDataToExport">Dataset to open in excel</param>
    Public Sub ExportToExcel(ByVal dsDataToExport As DataSet)
        Dim iTableCount As Integer = 0
        Dim iColCount As Integer = 0
        Dim iRowCount As Integer = 0
        Dim rExcelRange As Excel.Range

        Dim appXL As Excel.Application
        Dim wbXL As Excel.Workbook
        Dim shXL As Excel.Worksheet

        appXL = New Excel.Application

        Try
            wbXL = appXL.Workbooks.Add()

            'loop through rows and columns to simply copy and paste values on a 1-1 mapping
            For iTableCount = 0 To dsDataToExport.Tables.Count - 1
                wbXL.Sheets.Add()

                shXL = CType(appXL.ActiveWorkbook.ActiveSheet, Excel.Worksheet)
                shXL.Name = dsDataToExport.Tables(iTableCount).TableName

                For iColCount = 0 To dsDataToExport.Tables(iTableCount).Columns.Count - 1
                    For iRowCount = 0 To dsDataToExport.Tables(iTableCount).Rows.Count - 1
                        shXL.Cells(iRowCount + 1, iColCount + 1) = dsDataToExport.Tables(iTableCount).Rows(iRowCount).Item(iColCount)
                    Next
                Next

                rExcelRange = shXL.UsedRange
                rExcelRange.Font.Size = 8           'shrink the font
                rExcelRange.Columns.AutoFit()       'autofit the columns
            Next

            'CType(wbXL.Sheets("Sheet1"), Excel.Worksheet).Delete()  'remove the handy 3 that excel automatically puts in
            'CType(wbXL.Sheets("Sheet2"), Excel.Worksheet).Delete() 'commented out for excel 2013 (only has sheet 1 and errors)
            'CType(wbXL.Sheets("Sheet3"), Excel.Worksheet).Delete()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            appXL.Visible = True
            appXL.UserControl = True

            shXL = Nothing
            wbXL = Nothing
            'appXL.Quit()
            appXL = Nothing
        End Try
    End Sub

End Class
