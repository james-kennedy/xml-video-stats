Public Class frmCorrelations

    Dim marrCorrels(,) As Integer       'correlation matrix
    Dim mArrTracks(-1) As String        'stores track names across all files

#Region "Properties"
    Public Property Tracks() As String()
        Get
            Return mArrTracks
        End Get
        Set(ByVal value As String())
            mArrTracks = value
        End Set
    End Property

    Public Property Correlations() As Integer(,)
        Get
            Return marrCorrels
        End Get
        Set(ByVal value As Integer(,))
            marrCorrels = value
        End Set
    End Property
#End Region

    Private Sub frmCorrelations_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Try
            Me.Top = 0
            Me.Left = 0

            lstvKey.Items.Clear()
            lstvKey.Clear()

            PopulateKeyList(mArrTracks)
            dgvCorrels.DataSource = New Mommo.Data.ArrayDataView(marrCorrels)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub PopulateKeyList(ByVal arrTrackNames As String())
        Dim iCount As Integer = 0

        Try
            For iCount = 0 To mArrTracks.Length - 1
                lstvKey.Items.Add(CStr(iCount) & " - " & mArrTracks(iCount))
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub cmdClose_Click(sender As System.Object, e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub cmdCopyKey_Click(sender As System.Object, e As System.EventArgs) Handles cmdCopyKey.Click
        Dim build As New System.Text.StringBuilder
        Dim row As New List(Of String)

        Try
            'go through, build a string of the key
            For Each item As ListViewItem In lstvKey.Items
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
End Class