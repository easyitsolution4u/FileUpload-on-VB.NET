
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading.Tasks
Imports Excel = Microsoft.Office.Interop.Excel

Public Class ExcelFileReader

    Public Shared Function getExcelFile(ByVal filePath As String) As String
        Dim excelApplication As Excel.Application = New Excel.Application()

        Dim excelBook As Excel.Workbook = excelApplication.Workbooks.Open(filePath)

        Dim firstExcelWorksheet As Excel._Worksheet = excelBook.Sheets(1)
        Dim xlRange As Excel.Range = firstExcelWorksheet.UsedRange
        Dim totalRowsInFile As Integer = xlRange.Rows.Count
        Dim totalColumnsCounts As Integer = xlRange.Columns.Count
        Dim recordsBuilder As New StringBuilder()
        For rowIndex As Integer = 1 To totalRowsInFile

            For columnIndex As Integer = 1 To totalColumnsCounts
                If xlRange.Cells(rowIndex, columnIndex) IsNot Nothing AndAlso xlRange.Cells(rowIndex, columnIndex).Value2 IsNot Nothing Then
                    Dim value As String = xlRange.Cells(rowIndex, columnIndex).Value2.ToString()
                    recordsBuilder.Append(value)
                End If
            Next
            recordsBuilder.AppendLine()
        Next

        cleanUp(excelApplication, excelBook, firstExcelWorksheet, xlRange)

        Return recordsBuilder.ToString()

    End Function

    Private Shared Sub cleanUp(excelApplication As Excel.Application, excelBook As Excel.Workbook, firstExcelWorksheet As Excel._Worksheet, xlRange As Excel.Range)
        GC.Collect()
        GC.WaitForPendingFinalizers()
        Marshal.ReleaseComObject(xlRange)
        Marshal.ReleaseComObject(firstExcelWorksheet)
        excelBook.Close()
        Marshal.ReleaseComObject(excelBook)
        excelApplication.Quit()
        Marshal.ReleaseComObject(excelApplication)
    End Sub
End Class





