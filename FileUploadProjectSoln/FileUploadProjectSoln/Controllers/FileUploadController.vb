Imports System.IO
Imports System.Web.Mvc
Imports Microsoft.Office.Interop.Word





Namespace Controllers
    Public Class FileUploadController
        Inherits Controller

        ' GET: FileUpload


        Public Sub New()

        End Sub


        <HttpGet>
        Function Index() As ActionResult
            Return View()
        End Function

        <HttpPost>
        Function Push(postedFile As HttpPostedFile) As JsonResult
            Dim fileData As String = ""
            If postedFile IsNot Nothing Then

                Dim contentLength As Long = postedFile.ContentLength
                Dim contentType As String = postedFile.ContentType
                Dim fileName As String = postedFile.FileName
                Dim contents As Stream = postedFile.InputStream
                Using sr As StreamReader = New StreamReader(contents)

                    fileData = sr.ReadToEnd()

                End Using



            End If
            Return Json({"status", fileData})
        End Function

        <HttpPost>
        Function Upload() As JsonResult

            'HttpContext.Current.Request.Files.AllKeys.Any()

            Dim keys As String() = Request.Files.AllKeys
            Dim fileData As String = ""

            If keys IsNot Nothing AndAlso keys.Count > 0 Then

                Dim f As HttpPostedFileBase = CType(Request.Files(0), HttpPostedFileBase)

                fileData = processPostedFile(f)

            End If



            Return Json({"status", fileData})
        End Function

        Private Shared Function processPostedFile(postedFile As HttpPostedFileBase) As String
            Dim fileData As String = ""

            If postedFile IsNot Nothing Then

                Dim contentLength As Long = postedFile.ContentLength
                Dim contentType As String = postedFile.ContentType
                Dim fileName As String = postedFile.FileName
                Dim contents As Stream = postedFile.InputStream

                Dim fileExtension As String = Path.GetExtension(postedFile.FileName)

                If fileExtension = ".docx" Or fileExtension = ".doc" Then
                    fileData = processPostedFileWord(postedFile)


                ElseIf fileExtension = ".xlsx" Or fileExtension = ".xls" Then

                    fileData = ExcelFileReader.getExcelFile(postedFile.FileName)


                Else
                    Using sr As StreamReader = New StreamReader(contents)
                        Dim wordContent As String = processPostedFileWord(postedFile)
                        fileData = sr.ReadToEnd()
                    End Using
                End If




            End If
            Return fileData
        End Function


        Private Shared Function processPostedFileWord(postedFile As HttpPostedFileBase) As String
            Dim wordApplication As Application = New Application()
            Dim wordDocument As Document = New Document()
            Dim FilePath As String = Path.GetFullPath(postedFile.FileName)
            Dim NA As System.Reflection.Missing = System.Type.Missing

            Try
                wordDocument = wordApplication.Documents.Open(FilePath, NA, NA, NA, NA, NA, NA, NA, NA, NA, NA, NA, NA, NA, NA, NA)
            Catch ex As Exception
                Throw ex
            End Try

            Dim sb As StringBuilder = New StringBuilder()

            For lineNumber As Integer = 0 To wordDocument.Paragraphs.Count - 1
                Dim fileLineText As String = wordDocument.Paragraphs(lineNumber + 1).Range.Text.Trim()

                If fileLineText <> String.Empty Then
                    sb.AppendLine(fileLineText)
                End If
            Next
            wordDocument.Close()
            wordApplication.Quit()
            Return Convert.ToString(sb)
        End Function

    End Class
End Namespace