' Sequential Access and Files Programming Assignment.
' This program keeps track of a church's missionaries that are serving around the world.
' Benjamin Andrews.


' “I will not use code that was never covered in class or in our textbook. If you do you must be able to explain your code when asked by the professor.
' Using code outside of the resources provided, it can be flagged and reported as an academic integrity violation if there is any suspicion of copying/cheating.” 


Public Class frmMain

    ' Declare variable to hold filename from input
    Dim strfilename As String

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Get user input, error check, and open file on load

        strfilename = InputBox("Enter file name.") & ".txt"

        If strfilename = ".txt" Then
            MessageBox.Show("Please enter a file name.")
            strfilename = InputBox("Enter file name.") & ".txt"
        End If

        updateinfo()

    End Sub

    ' Sub to add info to list box.
    Private Sub updateinfo()
        ' Sub to update file in list box and keep accurate count

        lstMembers.DataSource = IO.File.ReadAllLines(strfilename)
        lstMembers.SelectedIndex = -1

        Dim strcount As String
        strcount = lstMembers.Items.Count()
        txtCount.Text = strcount

    End Sub

    Private Sub mnuExit_Click(sender As Object, e As EventArgs) Handles mnuExit.Click
        ' Closes the application
        Me.Close()

    End Sub

    Private Sub mnuCreate_Click(sender As Object, e As EventArgs) Handles mnuCreate.Click
        ' Opens input box, error checks for no input and creates file.
        Dim strnewname As String = InputBox("Enter a file name.")
        strfilename = strnewname & ".txt"
        If strfilename = ".txt" Then
            MessageBox.Show("Enter a file name.")
            strfilename = strnewname & ".txt"
        End If
        Dim sw As IO.StreamWriter = IO.File.CreateText(strfilename)
        sw.Close()

        updateinfo()

    End Sub

    Private Sub mnuAscend_Click(sender As Object, e As EventArgs) Handles mnuAscend.Click
        ' Sort the file contents by acscending order
        Dim ascendquery = From item In IO.File.ReadAllLines(strfilename)
                          Order By item Ascending
                          Select item
        lstMembers.DataSource = ascendquery.ToList

    End Sub

    Private Sub mnuDescend_Click(sender As Object, e As EventArgs) Handles mnuDescend.Click
        ' Sort the file contents by descending order
        Dim descendquery = From item In IO.File.ReadAllLines(strfilename)
                           Order By item Descending
                           Select item
        lstMembers.DataSource = descendquery.ToList
    End Sub

    Private Sub mnuDelete_Click(sender As Object, e As EventArgs) Handles mnuDelete.Click
        ' Deletes selected item from file.

        Dim strdelete As String
        strdelete = lstMembers.SelectedItem

        Dim deletequery = From item In IO.File.ReadAllLines(strfilename)
                          Where item <> strdelete
                          Select item

        IO.File.WriteAllLines(strfilename, deletequery)

        updateinfo()


    End Sub

    Private Sub mnuAdd_Click(sender As Object, e As EventArgs) Handles mnuAdd.Click

        Dim strnewname As String
        strnewname = InputBox("Enter a name.")

        ' Error check

        If strnewname = "" Then
            MessageBox.Show("Enter a name.")
            strnewname = InputBox("Enter a name.")
        End If

        ' Add input to file.
        Dim sw As IO.StreamWriter
        sw = IO.File.AppendText(strfilename)
        sw.WriteLine(strnewname)
        sw.Close()


        updateinfo()
    End Sub
End Class
